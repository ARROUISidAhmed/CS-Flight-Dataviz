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
            LeftDrag,
            RightDrag,
            WheelDrag

        }
        private State state;
        private Dictionary<int, List<Record>> dicRec;
        /// <summary>
        /// My OpenGL entry point
        /// </summary>
        private OpenTK.GLControl myView;

        private Record min, max;
        private Vector3 eye, target, up;
        private Vector2 previousPos;
        private int alphaValue;
        private float length;
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
            this.Controls.Add(myView);
        }


        private void GetMinMax()
        {

            min = new Record();
            max = new Record();
            min.x = float.MaxValue;
            min.y = float.MaxValue;
            min.z = float.MaxValue;
            max.x = float.MinValue;
            max.y = float.MinValue;
            max.z = float.MinValue;

            foreach (var recs in dicRec)
            {
                foreach (var rec in recs.Value)
                {
                    if (rec.x < min.x)
                        min.x = rec.x;
                    if (rec.y < min.y)
                        min.y = rec.y;
                    if (rec.z < min.z)
                        min.z = rec.z;

                    if (rec.x > max.x)
                        max.x = rec.x;
                    if (rec.y > max.y)
                        max.y = rec.y;
                    if (rec.z > max.z)
                        max.z = rec.z;
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
                    rec.x = Map(min.x, max.x, -1, 1, rec.x);
                    rec.y = Map(min.y, max.y, -1, 1, rec.y);
                    rec.z = Map(min.z, max.z, -1, 1, rec.z);
                    recs.Value[i] = rec;
                }
            }
        }
        private void MyView_Paint(object sender, PaintEventArgs e)
        {
            GL.Viewport(0, 0, myView.ClientSize.Width, myView.ClientSize.Height);

            float aspect_ratio = myView.ClientSize.Width / (float)myView.ClientSize.Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView
                (MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);


            Matrix4 lookat = Matrix4.LookAt(eye, target, up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DrawTrajectories();

            myView.SwapBuffers();
        }

        private float Map(float srcMin, float srcMax, float destMin, float destMax, float nb)
        {
            return ((destMax - destMin) * (nb - srcMin) / (srcMax - srcMin)) + destMin;
        }

        private new void MouseMove(object sender, MouseEventArgs e)
        {

            switch (state)
            {
                case State.LeftDrag:
                    state = State.LeftDrag;
                    float deltaTheta = Map(0, myView.Width, 0, MathHelper.TwoPi, e.X - previousPos.X);
                    float deltaPhi = Map(0, myView.Height, 0, MathHelper.TwoPi, e.Y - previousPos.Y);

                    /*eye.X = (float)(length * Math.Sin(phi) * Math.Cos(theta));
                    eye.Z = (float)(length * Math.Sin(phi) * Math.Sin(theta));
                    eye.Y = (float)(length * Math.Cos(phi) );
                    */
                    Quaternion rotation = new Quaternion(0, deltaTheta, deltaPhi);
                    eye = Vector3.Transform(eye,rotation);
                    up = Vector3.Transform(up, rotation);

                    previousPos.X = e.X;
                    previousPos.Y = e.Y;

                    myView.Invalidate();
                    break;

                case State.RightDrag:
                    length = Map(0, myView.Width, 0f, 5f, e.X);
                    eye.Normalize();
                    eye *= length;

                    myView.Invalidate();
                    break;
                case State.WheelDrag:
                    state = State.WheelDrag;
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
                    state = State.WheelDrag;
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
                case State.WheelDrag:
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

        private void Trajectories_Load(object sender, EventArgs e)
        {

            ReadTextFile("trajectories.txt");
            GetMinMax();
            ScaleData();
            InitValues();
        }

        private void InitValues()
        {
            length = 3f;
            eye = -length * Vector3.UnitX;
            target = Vector3.Zero;
            up = Vector3.UnitY;
            previousPos = Vector2.Zero;
            alphaValue = 50;

        }


        private void ReadTextFile(string textFile)
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
    }
}
