using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DataTrajec
{
    public struct Record
    {
        public Int32 id;
        public DateTime time;
        public Single x, y, z;
        public string name;
    }


    public partial class Trajectories : Form
    {
        private enum State
        {
            Init,
            RotateX,
            RotateY,
            LeftDrag,
            RightDrag,
            MiddleDrag,
            Animating

        }
        private State state;
        private Dictionary<int, List<Record>> dicRec;
        private List<BAMCIS.GeoJSON.Polygon> regions;
        /// <summary>
        /// My OpenGL entry point
        /// </summary>
        private OpenTK.GLControl myView;

        private Record minRecord, maxRecord;

        private Vector3 eye, target, up;
        private Vector2 previousPos;

        private float animation;
        private float blend;

        private int alphaValue;
        private float startAltitude, endAltitude;

        private Color minColor;
        private Color maxColor;

        private Color defaultminColor = Color.Green;
        private Color defaultmaxColor = Color.Blue;

        public Trajectories()
        {
            InitializeComponent();
            InitView();
        }

        private void GlControl1_Load(object sender, EventArgs e)
        {

        }


        private void InitView()
        {
            //Init the toolkit
            OpenTK.Toolkit.Init();

            state = State.Init;
            myView = new OpenTK.GLControl();
            myView.Size = new Size(Width, Height);
            myView.Paint += MyView_Paint;
            myView.MouseMove += new MouseEventHandler(this.MouseMove);
            myView.MouseDown += new MouseEventHandler(this.MouseDown);
            myView.MouseUp += new MouseEventHandler(this.MouseUp);
            myView.MouseWheel += new MouseEventHandler(this.MouseWheel);
            this.Controls.Add(myView);
        }


        private void GetMinMax()
        {

            minRecord = new Record();
            maxRecord = new Record();/*
            minPoint = new Vector2d();
            maxPoint = new Vector2d();*/
            minRecord.x = float.MaxValue;
            minRecord.y = float.MaxValue;
            minRecord.z = float.MaxValue;
            maxRecord.x = float.MinValue;
            maxRecord.y = float.MinValue;
            maxRecord.z = float.MinValue;/*
            minPoint.X = float.MaxValue;
            minPoint.Y = float.MaxValue;
            maxPoint.X = float.MinValue;
            maxPoint.Y = float.MinValue;*/

            foreach (var recs in dicRec)
            {
                foreach (var rec in recs.Value)
                {
                    if (rec.x < minRecord.x)
                        minRecord.x = rec.x;
                    if (rec.y < minRecord.y)
                        minRecord.y = rec.y;
                    if (rec.z < minRecord.z)
                        minRecord.z = rec.z;

                    if (rec.x > maxRecord.x)
                        maxRecord.x = rec.x;
                    if (rec.y > maxRecord.y)
                        maxRecord.y = rec.y;
                    if (rec.z > maxRecord.z)
                        maxRecord.z = rec.z;
                }
            }
            /*
            foreach (var polygon in regions)
            {

                foreach (var rings in polygon.Coordinates.ToList())
                {
                    foreach (var point in rings.Coordinates.ToList())
                    {

                        if (point.Longitude < minPoint.X)
                            minPoint.X = point.Longitude;
                        if (point.Latitude < minPoint.Y)
                            minPoint.Y = point.Latitude;

                        if (point.Longitude > maxPoint.X)
                            maxPoint.X = point.Longitude;
                        if (point.Latitude > maxPoint.Y)
                            maxPoint.Y = point.Latitude;
                    }
                }
            }*/




        }

        private void ScaleData()
        {
            Record rec = new Record();
            foreach (var recs in dicRec)
            {
                for (int i = 0; i < recs.Value.Count(); i++)
                {

                    rec = recs.Value[i];
                    rec.x = Map(minRecord.x, maxRecord.x, -1, 1, rec.x);
                    rec.y = Map(minRecord.y, maxRecord.y, -1, 1, rec.y);
                    rec.z = Map(minRecord.z, maxRecord.z, -1, 1, rec.z);
                    recs.Value[i] = rec;
                }
            }


        }

        private void MyView_Paint(object sender, PaintEventArgs e)
        {
            GL.Viewport(0, 0, myView.ClientSize.Width, myView.ClientSize.Height);

            float aspect_ratio = myView.ClientSize.Width / (float)myView.ClientSize.Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView
            (MathHelper.PiOver4, aspect_ratio, 1f, 64f);
            Matrix4 ortho = Matrix4.CreateOrthographic
                ((float)myView.ClientSize.Width, (float)myView.ClientSize.Height, 1f, 64f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);


            Matrix4 lookat = Matrix4.LookAt(eye, target, up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DrawFranceMap();
            DrawTrajectories();

            myView.SwapBuffers();
        }

        private float Map(float srcMin, float srcMax, float destMin, float destMax, float nb)
        {
            return ((destMax - destMin) * (nb - srcMin) / (srcMax - srcMin)) + destMin;
        }

        private new void MouseWheel(object sender, MouseEventArgs e)
        {

            blend = MathHelper.Clamp((0.001f * e.Delta), -1f, 1f);


            Matrix3 scale = Matrix3.CreateScale(1f - blend);
            eye = Vector3.Transform((eye - target), scale) + target;

            myView.Invalidate();
        }


        private new void MouseMove(object sender, MouseEventArgs e)
        {

            switch (state)
            {
                case State.RightDrag :
                    float deltaTheta = Map(0, myView.Width, 0, MathHelper.TwoPi, previousPos.X - e.X);
                    float deltaPhi = Map(0, myView.Height, 0, MathHelper.Pi, previousPos.Y - e.Y);



                    Quaternion rotationX = Quaternion.FromAxisAngle(up, deltaTheta);
                    Quaternion rotationY = Quaternion.FromAxisAngle(Vector3.Cross(up, eye - target), deltaPhi);


                    eye = Vector3.Transform(eye, rotationX * rotationY);
                    up = Vector3.Transform(up, rotationY);

                    previousPos.X = e.X;
                    previousPos.Y = e.Y;

                    myView.Invalidate();
                    break;
                case State.LeftDrag:
                    state = State.LeftDrag;


                    float x = Map(0, myView.Width, 0f, 1f, previousPos.X - e.X);
                    float y = Map(0, myView.Height, 0f, 1f, previousPos.Y - e.Y);

                    //Pan the camera in the opposite way to pan the scene in the right way
                    Matrix4 panMatrix = Matrix4.CreateTranslation(-y * up + x * Vector3.Cross(up, eye - target));


                    eye = Vector3.TransformPosition(eye, panMatrix);
                    target = Vector3.TransformPosition(target, panMatrix);

                    previousPos.X = e.X;
                    previousPos.Y = e.Y;

                    myView.Invalidate();
                    break;
                case State.MiddleDrag:
                    state = State.MiddleDrag;
                    break;
                default:
                    break;

            }
        }

        private new void MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    HandleLeftDown(e);
                    break;
                case MouseButtons.Right:
                    HandleRightDown(e);
                    break;
                case MouseButtons.Middle:
                    HandleMiddleDown(e);
                    break;
                default:
                    break;
            }

        }

        private void HandleRightDown(MouseEventArgs e)
        {
            switch (state)
            {
                case State.Init:
                    state = State.RightDrag;
                    previousPos.X = e.X;
                    previousPos.Y = e.Y;
                    break;
                default:
                    break;
            }
        }

        private void HandleLeftDown(MouseEventArgs e)
        {
            switch (state)
            {
                case State.Init:
                    state = State.LeftDrag;
                    previousPos.X = e.X;
                    previousPos.Y = e.Y;
                    break;
                default:
                    break;
            }
        }
        private void HandleMiddleDown(MouseEventArgs e)
        {
            switch (state)
            {
                case State.Init:
                    state = State.MiddleDrag;
                    previousPos.X = e.X;
                    previousPos.Y = e.Y;
                    break;

                default:
                    break;
            }
        }


        private new void MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {

                case MouseButtons.Left:
                    HandleLeftUp();
                    break;
                case MouseButtons.Right:
                    HandleRightUp();
                    break;
                case MouseButtons.Middle:
                    HandleMiddleUp();
                    break;
                default:
                    break;
            }
        }

        private void HandleRightUp()
        {
            switch (state)
            {
                case State.RightDrag:
                    state = State.Init;
                    break;
                default:
                    break;
            }
        }

        private void HandleLeftUp()
        {
            switch (state)
            {
                case State.LeftDrag:
                    state = State.Init;
                    break;
                default:
                    break;
            }
        }
        private void HandleMiddleUp()
        {
            switch (state)
            {
                case State.MiddleDrag:
                    state = State.Init;
                    break;
                default:
                    break;
            }
        }

        Color GetAltidueColor(float altitude, Color minColor, Color maxColor, int alpha)
        {
            return Color.FromArgb(
                alpha,
                (int)Map(-1, 1, minColor.R, maxColor.R, altitude),
                (int)Map(-1, 1, minColor.G, maxColor.G, altitude),
                (int)Map(-1, 1, minColor.B, maxColor.B, altitude)
                );
        }

        private void AlphaTrack_ValueChanged(object sender, EventArgs e)
        {
            alphaValue = alphaTrack.Value;
            myView.Invalidate();
        }

        private void DrawTrajectories()
        {
            foreach (var traj in dicRec)
            {

                //Decide if we paint this trajectory
                bool isPaintable = true;

                foreach (var rec in traj.Value)
                {
                    isPaintable &= (rec.z >= startAltitude && rec.z <= endAltitude);
                }

                if (isPaintable)
                {

                    GL.Begin((PrimitiveType.LineStrip));
                    foreach (var rec in traj.Value)
                    {

                        GL.Color4(GetAltidueColor(rec.z, minColor, maxColor, alphaValue));
                        GL.Vertex3(new Vector3(
                            rec.x,
                            rec.y,
                            rec.z
                            ));

                    }
                    GL.End();
                }


            }
        }

        private void DrawFranceMap()
        {
            foreach (var polygon in regions)
            {

                foreach (var rings in polygon.Coordinates.ToList())
                {
                    GL.Begin(PrimitiveType.LineStrip);
                    foreach (var point in rings.Coordinates.ToList())
                    {

                        GL.Color4(Color.Yellow);
                        GL.Vertex3(new Vector3(
                        Map(minRecord.x, maxRecord.x, -1, 1, (float)point.Longitude),
                        Map(minRecord.y, maxRecord.y, -1, 1, (float)point.Latitude),
                        -1f));

                    }
                    GL.End();
                }
            }

        }

        private void MaxTrack_ValueChanged(object sender, EventArgs e)
        {
            endAltitude = maxTrack.Value / (float)100;
            myView.Invalidate();
        }

        private void Trajectories_Resize(object sender, EventArgs e)
        {
            if (myView != null)
            {
                myView.Width = Width;
                myView.Height = Height;
                myView.Invalidate();

            }
            flowLayoutPanel1.Height = Height;
            flowLayoutPanel1.Invalidate();
            rotateY.Location = new Point(0, Height / 2);
            rotateY.Invalidate();
            rotateX.Location = new Point((Width / 2), Height - (Height / 10));
            rotateX.Invalidate();


        }






        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (state)
                {
                    case State.Init:
                        state = State.RotateY;
                        previousPos.Y = e.Y;
                        break;

                    default:
                        break;
                }
            }

        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {


            switch (state)
            {
                case State.RotateY:
                    state = State.RotateY;
                    float deltaPhi = Map(0, myView.Height, 0, MathHelper.Pi, previousPos.Y - e.Y);
                    Quaternion rotationY = Quaternion.FromAxisAngle(Vector3.Cross(up, eye - target), deltaPhi);
                    eye = Vector3.Transform(eye, rotationY);
                    up = Vector3.Transform(up, rotationY);
                    previousPos.Y = e.Y;

                    myView.Invalidate();
                    break;
                case State.MiddleDrag:
                    state = State.MiddleDrag;
                    break;
                default:
                    break;

            }
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (state)
                {
                    case State.RotateY:
                        state = State.Init;

                        break;
                    default:
                        break;

                }
            }

        }

        private void rotateX_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (state)
                {
                    case State.Init:
                        state = State.RotateX;
                        previousPos.X = e.X;
                        break;
                    default:
                        break;

                }
            }

        }

        private void rotateX_MouseMove(object sender, MouseEventArgs e)
        {
            switch (state)
            {

                case State.RotateX:

                    state = State.RotateX;

                    float deltaTheta = Map(0, myView.Width, 0, MathHelper.TwoPi, previousPos.X - e.X);

                    Quaternion rotationX = Quaternion.FromAxisAngle(up, deltaTheta);

                    eye = Vector3.Transform(eye, rotationX);

                    previousPos.X = e.X;
                    myView.Invalidate();
                    break;
                case State.MiddleDrag:
                    state = State.MiddleDrag;
                    break;
                default:
                    break;

            }
        }

        private void rotateX_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (state)
                {
                    case State.RotateX:
                        state = State.Init;
                        break;
                    default:
                        break;

                }
            }

        }


        private void resetButton_Click(object sender, EventArgs e)
        {
            animationTimer.Enabled = true;
            state = State.Animating;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.Animating:
                    animation += 0.2f;
                    if (animation <= 1f)
                    {
                        state = State.Animating;

                        eye = Vector3.Lerp(eye, -3f * Vector3.UnitX, animation);
                        target = Vector3.Lerp(target, Vector3.Zero, animation);
                        up = Vector3.Lerp(up, Vector3.UnitZ, animation);
                        myView.Invalidate();
                    }
                    else
                    {
                        state = State.Init;
                        blend = 0f;
                        animation = 0f;
                        animationTimer.Enabled = false;

                    }
                    break;
                default:
                    break;
            }

        }




        private void ResetColor_Click(object sender, EventArgs e)
        {
            minColor = defaultminColor;
            maxColor = defaultmaxColor;
            colorminbutton.BackColor = defaultminColor;
            colormaxbutton.BackColor = defaultmaxColor;
            myView.Invalidate();
        }

        private void DefaultMinColor_Click(object sender, EventArgs e)
        {
            if (defaultMinClorDialog.ShowDialog() == DialogResult.OK)
            {
                defaultMinColor.BackColor = defaultMinClorDialog.Color;
                defaultminColor = defaultMinClorDialog.Color;
                myView.Invalidate();
            }
        }

        private void DefaultMaxColor_Click(object sender, EventArgs e)
        {
            if (defaultMaxColorDialog.ShowDialog() == DialogResult.OK)
            {
                defaultMaxColor.BackColor = defaultMaxColorDialog.Color;
                defaultmaxColor = defaultMaxColorDialog.Color;
                myView.Invalidate();
            }
        }

        private void Colorminbutton_Click(object sender, EventArgs e)
        {
            if (minColorDialog.ShowDialog() == DialogResult.OK)
            {
                colorminbutton.BackColor = minColorDialog.Color;
                minColor = minColorDialog.Color;
                myView.Invalidate();
            }
        }

        private void Colormaxbutton_Click(object sender, EventArgs e)
        {

            if (maxColorDialog.ShowDialog() == DialogResult.OK)
            {
                colormaxbutton.BackColor = maxColorDialog.Color;
                maxColor = maxColorDialog.Color;
                myView.Invalidate();
            }
        }

        private void MinTrack_ValueChanged(object sender, EventArgs e)
        {
            startAltitude = minTrack.Value / (float)100;
            myView.Invalidate();
        }

        private void Trajectories_Load(object sender, EventArgs e)
        {

            ReadMapFile("fr-administrative-area.csv");
            ReadTrajectoriesFile("trajectories.txt");
            GetMinMax();
            ScaleData();
            InitValues();
        }

        private void InitValues()
        {
            minColor = defaultminColor;
            maxColor = defaultmaxColor;
            blend = 0f;
            eye = -3f * Vector3.UnitX;
            target = Vector3.Zero;
            up = Vector3.UnitZ;
            previousPos = Vector2.Zero;
            alphaValue = 50;
            startAltitude = -1f;
            endAltitude = 1f;
            animation = 0f;
        }


        private void ReadTrajectoriesFile(string textFile)
        {
            string[] lines = File.ReadAllLines(textFile);
            char[] splitChar = new char[] { ';' };
            dicRec = new Dictionary<int, List<Record>>();
            foreach (string line in lines)
            {
                if (line[0] != '#')
                {
                    var items = line.Split(splitChar);
                    Record rec = new Record();
                    rec.id = Int32.Parse(items[0]);
                    rec.time = DateTime.Parse(items[1]);
                    rec.x = Single.Parse(items[2], CultureInfo.InvariantCulture);
                    rec.y = Single.Parse(items[3], CultureInfo.InvariantCulture);
                    rec.z = Single.Parse(items[4], CultureInfo.InvariantCulture);
                    rec.name = items[5];

                    if (!dicRec.ContainsKey(rec.id))
                        dicRec.Add(rec.id, new List<Record>());

                    dicRec[rec.id].Add(rec);

                }
            }
        }


        private void ReadMapFile(string jsonFile)
        {

            string[] lines = File.ReadAllLines(jsonFile);
            char[] splitChar = new char[] { ';' };

            regions = new List<BAMCIS.GeoJSON.Polygon>();
            foreach (string line in lines)
            {
                if (line[0] != '#')
                {
                    var items = line.Split(splitChar);

                    string geoShape = items[1];
                    geoShape = geoShape.TrimStart('"').TrimEnd('"');
                    geoShape = geoShape.Replace("\"\"", "\"");


                    BAMCIS.GeoJSON.Polygon polygon = JsonConvert.DeserializeObject<BAMCIS.GeoJSON.Polygon>(geoShape);
                    regions.Add(polygon);

                }
            }

        }
    }
}
