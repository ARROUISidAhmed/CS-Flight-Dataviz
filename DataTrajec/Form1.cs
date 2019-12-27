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
using System.Web;
using Json.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            LeftDrag,
            RightDrag,
            MiddleDrag

        }
        private State state;
        private Dictionary<int, List<Record>> dicRec;
        private Dictionary<int, List<PointF>> regions;
        /// <summary>
        /// My OpenGL entry point
        /// </summary>
        private OpenTK.GLControl myView;

        private Record minRecord, maxRecord;
        private PointF minPoint, maxPoint;
        private Vector3 eye, target, up;
        private Vector2 previousPos;
        private Quaternion quaternion;
        private int alphaValue;
        private float blend;
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
            myView.Size = new Size(1024, 920);
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
            maxRecord = new Record();
            minPoint = new PointF();
            maxPoint = new PointF();
            minRecord.x = float.MaxValue;
            minRecord.y = float.MaxValue;
            minRecord.z = float.MaxValue;
            maxRecord.x = float.MinValue;
            maxRecord.y = float.MinValue;
            maxRecord.z = float.MinValue;
            minPoint.X = float.MaxValue;
            minPoint.Y = float.MaxValue;
            maxPoint.X = float.MinValue;
            maxPoint.Y = float.MinValue;

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

            foreach (var region in regions)
            {
                foreach (var point in region.Value)
                {
                    if (point.X < minPoint.X)
                        minPoint.X = point.X;
                    if (point.Y < minPoint.Y)
                        minPoint.Y = point.Y;

                    if (point.X > maxPoint.X)
                        maxPoint.X = point.X;
                    if (point.Y > maxPoint.Y)
                        maxPoint.Y = point.Y;
                }
            }

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

            PointF p = new PointF();
            foreach (var region in regions)
            {
                for (int i = 0; i < region.Value.Count(); i++)
                {

                    p = region.Value[i];
                    p.X = Map(minPoint.X, maxPoint.X, -1, 1, p.X);
                    p.Y = Map(minPoint.Y, maxPoint.Y, -1, 1, p.Y);
                    region.Value[i] = p;;
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

        /*
        // functions:
        public static Vector4 convertScreenToWorldCoords(int x, int y)
        {
            int[] viewport = new int[4];
            Matrix4 modelViewMatrix, projectionMatrix;
            GL.GetFloat(GetPName.ModelviewMatrix, out modelViewMatrix);
            GL.GetFloat(GetPName.ProjectionMatrix, out projectionMatrix);
            GL.GetInteger(GetPName.Viewport, viewport);
            Vector2 mouse;
            mouse.X = x;
            mouse.Y = y;
            Vector4 vector = UnProject(ref projectionMatrix, modelViewMatrix, new Size(viewport[2], viewport[3]), mouse);
            return vector;
        }
        public static Vector4 UnProject(ref Matrix4 projection, Matrix4 view, Size viewport, Vector2 mouse)
        {
            Vector4 vec;

            vec.X = 2.0f * mouse.X / (float)viewport.Width - 1;
            vec.Y = 2.0f * mouse.Y / (float)viewport.Height - 1;
            vec.Z = 0;
            vec.W = 1.0f;

            Matrix4 viewInv = Matrix4.Invert(view);
            Matrix4 projInv = Matrix4.Invert(projection);

            Vector4.Transform(ref vec, ref projInv, out vec);
            Vector4.Transform(ref vec, ref viewInv, out vec);

            if (vec.W > float.Epsilon || vec.W < float.Epsilon)
            {
                vec.X /= vec.W;
                vec.Y /= vec.W;
                vec.Z /= vec.W;
            }

            return vec;
        }*/
        private new void MouseMove(object sender, MouseEventArgs e)
        {

            switch (state)
            {
                case State.LeftDrag:
                    state = State.LeftDrag;

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

                case State.RightDrag:
                    state = State.RightDrag;


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
                GL.Begin((PrimitiveType.LineStrip));

                foreach (var rec in traj.Value)
                {
                    GL.Color4(GetAltidueColor(rec.z, Color.Green, Color.Blue, alphaValue));
                    GL.Vertex3(new Vector3(
                        rec.x,
                        rec.y,
                        rec.z
                        ));
                }
                GL.End();

            }
        }

        private void DrawFranceMap()
        {
            foreach (var region in regions)
            {
                GL.Begin((PrimitiveType.LineStrip));

                foreach (var point in region.Value)
                {
                    GL.Color4(Color.Yellow);
                    GL.Vertex3(new Vector3(
                        point.X,
                        point.Y,
                        0f
                        ));
                }
                GL.End();
            }
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
            blend = 0f;
            eye = -3f * Vector3.UnitX;
            target = Vector3.Zero;
            up = Vector3.UnitZ;
            previousPos = Vector2.Zero;
            quaternion = Quaternion.Identity;
            alphaValue = 50;

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
            regions = new Dictionary<int, List<PointF>>();
            foreach (string line in lines)
            {
                if (line[0] != '#')
                {
                    var items = line.Split(splitChar);
                    int id = Int32.Parse(items[2]);

                    string geoShape = items[1];
                    geoShape = geoShape.TrimStart('"').TrimEnd('"');
                    geoShape = geoShape.Replace("\"\"", "\"");

                    JObject shape = JObject.Parse(geoShape);
                    if (!regions.ContainsKey(id))
                        regions.Add(id, new List<PointF>());

                    foreach (var points in shape["coordinates"])
                    {
                        foreach (var point in points)
                        {
                            regions[id].Add(new PointF(Single.Parse(point[0].ToString(), CultureInfo.InvariantCulture),
                                Single.Parse(point[1].ToString(), CultureInfo.InvariantCulture)));

                        }
                    }
                }
            }

        }
    }
}
