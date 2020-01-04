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
            Blocked
        }

        private enum DrawingMode
        {
            Line,
            Particle
        }

        private enum ProjectionMode
        {
            Ortho,
            Perspective
        }


        private State state;
        private DrawingMode drawingMode;
        private ProjectionMode projectionMode;

        private Dictionary<int, List<Record>> trajectories;
        private List<BAMCIS.GeoJSON.Polygon> regions;
        private List<int> selectedTrajectories;
        private List<int> drawableTrajectories;
        /// <summary>
        /// My OpenGL entry point
        /// </summary>
        private OpenTK.GLControl myView;



        private Record minRecord, maxRecord;


        private Vector3 eye, target, up;
        private float left, right, top, bottom;
        private float fovy;
        private Vector2 previousPos;

        private DateTime currentDateTime;
        private float time;
        private float blend;
        private RectangleF selectionRectangle;

        private int alphaValue;
        private double startAltitude, endAltitude;

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

            rangeSlider1.LowerValueChanged += this.RangeSlider_LowerChanged;
            rangeSlider1.UpperValueChanged += this.RangeSlider_UpperChanged;
        }


        private void GetMinMax()
        {

            minRecord = new Record();
            maxRecord = new Record();
            minRecord.x = float.MaxValue;
            minRecord.y = float.MaxValue;
            minRecord.z = float.MaxValue;
            minRecord.time = DateTime.MaxValue;
            maxRecord.x = float.MinValue;
            maxRecord.y = float.MinValue;
            maxRecord.z = float.MinValue;
            maxRecord.time = DateTime.MinValue;

            foreach (var recs in trajectories)
            {
                foreach (var rec in recs.Value)
                {
                    if (rec.x < minRecord.x)
                        minRecord.x = rec.x;
                    if (rec.y < minRecord.y)
                        minRecord.y = rec.y;
                    if (rec.z < minRecord.z)
                        minRecord.z = rec.z;
                    if (rec.time < minRecord.time)
                        minRecord.time = rec.time;

                    if (rec.x > maxRecord.x)
                        maxRecord.x = rec.x;
                    if (rec.y > maxRecord.y)
                        maxRecord.y = rec.y;
                    if (rec.z > maxRecord.z)
                        maxRecord.z = rec.z;
                    if (rec.time > maxRecord.time)
                        maxRecord.time = rec.time;
                }
            }




        }

        private void ScaleData()
        {
            Record rec = new Record();
            foreach (var recs in trajectories)
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
            SetParameters();

            SetProjection();

            SetLookAtCamera();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DrawTrajectories();

            DrawFranceMap();

            myView.SwapBuffers();
        }

        private void SetProjection()
        {
            switch (projectionMode)
            {
                case ProjectionMode.Perspective:
                    SetPerspectiveProjection();
                    break;
                case ProjectionMode.Ortho:
                    SetOrthoProjection();
                    break;
                default:
                    break;
            }
        }


        private void SetParameters()
        {
            GL.Viewport(0, 0, myView.ClientSize.Width, myView.ClientSize.Height);
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        private void SetPerspectiveProjection()
        {
            float aspect_ratio = myView.ClientSize.Width / (float)myView.ClientSize.Height;
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView
            ((float)Math.PI * (fovy / 180f), aspect_ratio, 0.1f, 64f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        private void SetOrthoProjection()
        {

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity(); // reset matrix

            GL.Ortho(left, right, bottom, top, 0.1f, 64f);
        }

        private void SetLookAtCamera()
        {
            Matrix4 lookat = Matrix4.LookAt(eye, target, up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }

        private void DrawTrajectories()
        {
            switch (drawingMode)
            {
                case DrawingMode.Line:
                    DrawTrajectoriesLines();
                    break;
                case DrawingMode.Particle:
                    DrawTrajectoriesParticles();
                    break;
            }


        }

        private void DrawTrajectoriesParticles()
        {
            foreach (var i in drawableTrajectories.Except(selectedTrajectories).ToList())
            {

                GL.Begin((PrimitiveType.Points));
                for (int j = 0; j < trajectories[i].Count - 1; j++)
                {

                    GL.Color4(GetAltidueColor(trajectories[i][j].z, minColor, maxColor, alphaValue));

                    Record rCurrent = trajectories[i][j];
                    Record rNext = trajectories[i][(j + 1) % trajectories[i].Count];
                    float b = (float)((currentDateTime - minRecord.time).TotalSeconds /
                        (rNext.time - rCurrent.time).TotalSeconds) % 1f;

                    Vector3 point = Vector3.Lerp(
                        new Vector3(
                            rCurrent.x,
                            rCurrent.y,
                            rCurrent.z
                        ),
                        new Vector3(
                            rNext.x,
                            rNext.y,
                            rNext.z),
                        b
                        );

                    GL.Vertex3(point);

                }
                GL.End();
            }
            foreach (var i in selectedTrajectories)
            {

                GL.Begin((PrimitiveType.Points));
                for (int j = 0; j < trajectories[i].Count - 1; j++)
                {

                    GL.Color4(Color.White);

                    Record rCurrent = trajectories[i][j];
                    Record rNext = trajectories[i][(j + 1) % trajectories[i].Count];
                    float b = (float)((currentDateTime - minRecord.time).TotalSeconds /
                        (rNext.time - rCurrent.time).TotalSeconds) % 1f;

                    Vector3 point = Vector3.Lerp(
                        new Vector3(
                            rCurrent.x,
                            rCurrent.y,
                            rCurrent.z
                        ),
                        new Vector3(
                            rNext.x,
                            rNext.y,
                            rNext.z),
                        b
                        );

                    GL.Vertex3(point);

                }
                GL.End();
            }
        }

        private void DrawTrajectoriesLines()
        {
            foreach (var i in drawableTrajectories.Except(selectedTrajectories).ToList())
            {
                GL.Begin((PrimitiveType.LineStrip));
                foreach (var rec in trajectories[i])
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
            foreach (var i in selectedTrajectories)
            {
                GL.Begin((PrimitiveType.LineStrip));
                foreach (var rec in trajectories[i])
                {
                    GL.Color4(Color.White);
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
            foreach (var polygon in regions)
            {

                foreach (var rings in polygon.Coordinates.ToList())
                {
                    GL.Begin(PrimitiveType.LineStrip);
                    foreach (var point in rings.Coordinates.ToList())
                    {

                        GL.Color4(Color.Yellow);
                        GL.Vertex3(new Vector3(
                        (float)Mapd(minRecord.x, maxRecord.x, -1, 1, point.Longitude),
                        (float)Mapd(minRecord.y, maxRecord.y, -1, 1, point.Latitude),
                        -1f));

                    }
                    GL.End();
                }
            }

        }


        private static float Map(float srcMin, float srcMax, float destMin, float destMax, float nb)
        {
            return ((destMax - destMin) * (nb - srcMin) / (srcMax - srcMin)) + destMin;
        }

        private static double Mapd(double srcMin, double srcMax, double destMin, double destMax, double nb)
        {
            return ((destMax - destMin) * (nb - srcMin) / (srcMax - srcMin)) + destMin;
        }



        private new void MouseWheel(object sender, MouseEventArgs e)
        {
            switch (state)
            {
                case State.Init:
                    HandleMouseWheel(e);
                    break;
            }
        }

        private float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public static Vector3 ConvertScreenToWorlds(int x, int y)
        {
            int[] viewport = new int[4];
            Matrix4 modelViewMatrix, projectionMatrix;
            GL.GetFloat(GetPName.ModelviewMatrix, out modelViewMatrix);
            GL.GetFloat(GetPName.ProjectionMatrix, out projectionMatrix);
            GL.GetInteger(GetPName.Viewport, viewport);
            Vector2 mouse;
            mouse.X = x;
            mouse.Y = y;
            Vector4 vector = UnProject(ref projectionMatrix, ref modelViewMatrix, new Size(viewport[2], viewport[3]), mouse);
            Vector3 coords = new Vector3(vector.X, vector.Y, vector.Z);
            return coords;
        }
        public static Vector4 UnProject(ref Matrix4 projection, ref Matrix4 view, Size viewport, Vector2 screen)
        {
            Vector4 vec;

            vec.X = 2.0f * screen.X / (float)viewport.Width - 1;
            vec.Y = 2.0f * screen.Y / (float)viewport.Height - 1;
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
        }
        private static Vector2 ConvertWorldToScreen(float x, float y, float z)
        {
            int[] viewport = new int[4];
            Matrix4 modelViewMatrix, projectionMatrix;
            GL.GetFloat(GetPName.ModelviewMatrix, out modelViewMatrix);
            GL.GetFloat(GetPName.ProjectionMatrix, out projectionMatrix);
            GL.GetInteger(GetPName.Viewport, viewport);

            Vector4 world = new Vector4(x, y, z, 1f);


            world = Project(ref modelViewMatrix, ref projectionMatrix, world);
            Vector2 point = new Vector2(((world.X + 1f) / 2f) * viewport[2], (1f - (world.Y + 1f) / 2f) * viewport[3]);
            return point;
        }
        private static Vector4 Project(ref Matrix4 modelViewMatrix, ref Matrix4 projectionMatrix, Vector4 world)
        {

            Vector4.Transform(ref world, ref modelViewMatrix, out world);
            Vector4.Transform(ref world, ref projectionMatrix, out world);

            return world;

        }
        private void HandleMouseWheel(MouseEventArgs e)
        {

            blend = MathHelper.Clamp((0.001f * e.Delta), -1f, 1f);

            //Translate
            /*
            Vector3 dv = ConvertScreenToWorlds((int)((e.X - previousPos.X) * (1f - blend)),
                                            (int)((e.Y - previousPos.Y) * (1f - blend)));


            Matrix4 translation = Matrix4.CreateTranslation(-dv.Y * up - dv.X * Vector3.Cross(up, target - eye));

            target = Vector3.TransformPosition(target, translation);
            eye = Vector3.TransformPosition(eye, translation);
            */
            //Scale
            Matrix3 scale = Matrix3.CreateScale(1f - blend);

            eye = Vector3.Transform((eye - target), scale) + target;
            left *= 1f - blend;
            right *= 1f - blend;
            bottom *= 1f - blend;
            top *= 1f - blend;


            myView.Invalidate();

        }

        private new void MouseMove(object sender, MouseEventArgs e)
        {

            switch (state)
            {
                case State.RightDrag:
                    HandleRightDrag(e);
                    break;
                case State.LeftDrag:
                    HandleLeftDrag(e);
                    break;
                case State.MiddleDrag:
                    HandleMiddleDrag(e);
                    break;
                default:
                    break;

            }
        }

        private void ComputeSelectedTrajectories(MouseEventArgs e)
        {

            selectionRectangle = new RectangleF(Math.Min(previousPos.X, e.X),
                Math.Min(previousPos.Y, e.Y),
                Math.Abs(previousPos.X - e.X),
                Math.Abs(previousPos.Y - e.Y));

            PointF pointF;
            Vector2 screenCoords;

            selectedTrajectories.Clear();
            foreach (var i in drawableTrajectories)
            {
                foreach (var point in trajectories[i])
                {
                    /**
                     *  Constraints to be respected by the point are that the dot product between the right & up directions
                     *  is between the dot product of both startPoint and endPoint with the right & up directions
                     * 
                     *
                     */
                    screenCoords = ConvertWorldToScreen(point.x, point.y, point.z);
                    pointF = new PointF(screenCoords.X, screenCoords.Y);
                    if (selectionRectangle.Contains(pointF))
                    {
                        selectedTrajectories.Add(i);
                        break;
                    }

                }
            }

        }



        private void HandleLeftDrag(MouseEventArgs e)
        {
            state = State.LeftDrag;
            ComputeSelectedTrajectories(e);
            myView.Invalidate();
        }
        private void HandleMiddleDrag(MouseEventArgs e)
        {
            state = State.MiddleDrag;


            float x = Map(0, myView.Width, 0f, 1f, previousPos.X - e.X);
            float y = Map(0, myView.Height, 0f, 1f, previousPos.Y - e.Y);

            //Pan the camera in the opposite way to pan the scene in the right way
            Matrix4 panMatrix = Matrix4.CreateTranslation(-y * up - x * Vector3.Cross(up, target - eye));


            eye = Vector3.TransformPosition(eye, panMatrix);
            target = Vector3.TransformPosition(target, panMatrix);

            previousPos.X = e.X;
            previousPos.Y = e.Y;

            myView.Invalidate();
        }
        private void HandleRightDrag(MouseEventArgs e)
        {
            state = State.RightDrag;
            float deltaTheta = Map(0, myView.Width, 0, MathHelper.TwoPi, previousPos.X - e.X);
            float deltaPhi = Map(0, myView.Height, 0, MathHelper.Pi, previousPos.Y - e.Y);

            Quaternion rotationX = Quaternion.FromAxisAngle(up, deltaTheta);
            Quaternion rotationY = Quaternion.FromAxisAngle(Vector3.Cross(up, eye - target), deltaPhi);


            eye = Vector3.Transform(eye, rotationX * rotationY);
            up = Vector3.Transform(up, rotationY);

            previousPos.X = e.X;
            previousPos.Y = e.Y;

            myView.Invalidate();
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
                    selectedTrajectories.Clear();
                    myView.Invalidate();
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

        private Color GetAltidueColor(float altitude, Color minColor, Color maxColor, int alpha)
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
        private void FovyTrack_ValueChanged(object sender, EventArgs e)
        {
            fovy = fovyTrack.Value;
            myView.Invalidate();
        }

        private void RangeSlider_LowerChanged(double value)
        {
            startAltitude = value * 2 - 1;
            ComputeVisibleTrajectories();
            myView.Invalidate();
        }
        private void RangeSlider_UpperChanged(double value)
        {
            endAltitude = value * 2 - 1;
            ComputeVisibleTrajectories();
            myView.Invalidate();
        }

        private void ComputeVisibleTrajectories()
        {
            //Decide if we paint this trajectory
            drawableTrajectories.Clear();
            bool isPaintable;
            foreach (var traj in trajectories)
            {
                isPaintable = true;
                foreach (var rec in traj.Value)
                {
                    isPaintable &= (rec.z >= startAltitude && rec.z <= endAltitude);
                    if (!isPaintable)
                    {
                        break;
                    }

                }
                if (isPaintable)
                {
                    drawableTrajectories.Add(traj.Key);
                }
            }
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


        private void RotateY_MouseDown(object sender, MouseEventArgs e)
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

        private void RotateY_MouseMove(object sender, MouseEventArgs e)
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

        private void RotateY_MouseUp(object sender, MouseEventArgs e)
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

        private void RotateX_MouseDown(object sender, MouseEventArgs e)
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

        private void RotateX_MouseMove(object sender, MouseEventArgs e)
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

        private void RotateX_MouseUp(object sender, MouseEventArgs e)
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


        private void ResetButton_Click(object sender, EventArgs e)
        {
            resetAnimationTimer.Enabled = true;
            state = State.Blocked;
            myView.Invalidate();
        }

        private void ResetAnimationTimer_Tick(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.Blocked:
                    time += 0.2f;
                    if (time <= 1f)
                    {
                        state = State.Blocked;

                        eye = Vector3.Lerp(eye, -3f * Vector3.UnitX, time);
                        target = Vector3.Lerp(target, Vector3.Zero, time);
                        up = Vector3.Lerp(up, Vector3.UnitZ, time);
                        left = Lerp(left, -2f, time);
                        right = Lerp(right, 2f, time);
                        top = Lerp(top, 2f, time);
                        bottom = Lerp(bottom, -2f, time);
                        myView.Invalidate();
                    }
                    else
                    {
                        state = State.Init;
                        time = 0f;
                        resetAnimationTimer.Enabled = false;
                        myView.Invalidate();
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

        private void ParticleButton_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Particle;
            currentDateTime = minRecord.time;
            particleAnimationTimer.Enabled = true;
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Line;
            particleAnimationTimer.Enabled = false;
            myView.Invalidate();
        }

        private void ParticleAnimationTimer_Tick(object sender, EventArgs e)
        {
            switch (state)
            {
                default:
                    switch (drawingMode)
                    {
                        case DrawingMode.Particle:

                            if (currentDateTime < maxRecord.time)
                            {
                                currentDateTime = currentDateTime.AddSeconds((maxRecord.time - minRecord.time).TotalSeconds / (50f * 100f));
                                myView.Invalidate();
                            }
                            else
                            {
                                currentDateTime = minRecord.time;
                            }

                            break;
                    }
                    break;

            }

        }

        private void OrthographicButton_Click(object sender, EventArgs e)
        {
            projectionMode = ProjectionMode.Ortho;
            myView.Invalidate();
        }

        private void PerspectiveButton_Click(object sender, EventArgs e)
        {
            projectionMode = ProjectionMode.Perspective;
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
            fovy = 45f;
            eye = -3f * Vector3.UnitX;
            target = Vector3.Zero;
            up = Vector3.UnitZ;

            //For orthographic projection
            previousPos = Vector2.Zero;
            left = -2f;
            right = 2f;
            top = 2f;
            bottom = -2f;


            alphaValue = 50;
            startAltitude = -1.0;
            endAltitude = 1.0;
            time = 0f;
            state = State.Init;
            drawingMode = DrawingMode.Line;
            projectionMode = ProjectionMode.Perspective;
            currentDateTime = minRecord.time;
            selectedTrajectories = new List<int>();
            drawableTrajectories = trajectories.Keys.ToList();
        }

        private void ReadTrajectoriesFile(string textFile)
        {
            string[] lines = File.ReadAllLines(textFile);
            char[] splitChar = new char[] { ';' };
            trajectories = new Dictionary<int, List<Record>>();
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

                    if (!trajectories.ContainsKey(rec.id))
                        trajectories.Add(rec.id, new List<Record>());

                    trajectories[rec.id].Add(rec);

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
