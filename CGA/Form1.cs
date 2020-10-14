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

namespace CGA
{
    public partial class Form1 : Form
    {
        List<Vertex> points = new List<Vertex>();
        List<Polygon> polygons = new List<Polygon>();

        Matrix worldMatrix;
        Matrix scaleMatrix;
        Matrix translationMatrix;
        Matrix rotationXMatrix;
        Matrix rotationYMatrix;
        Matrix rotationZMatrix;
        Matrix viewerMatrix;
        Matrix projectionMatrix;
        Matrix screenMatrix;

        Matrix superMatrix;

        double angleX = 0;
        double angleY = 0;
        double angleZ = 0;

        Vertex eye;
        Vertex target;
        Vertex up;

        double zNear = 20;
        double zFar = 1000;
        double width = 40;
        double height = 40;

        Graphics g;
        Bitmap bitmap;
        Point po1 = new Point();
        Point po2 = new Point();
        List<Vertex> drawPoints = new List<Vertex>();

        public Form1()
        {
            InitializeComponent();

            width = pbCanvas.Width;
            height = pbCanvas.Height;
            zNear = width / 2;

            bitmap = new Bitmap(pbCanvas.Width, pbCanvas.Height);
            g = Graphics.FromImage(bitmap);
            //this.KeyUp += Form1_KeyPress;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            worldMatrix = new Matrix(new double[4, 4]{  { 1, 0, 0, 0},
                                                        { 0, 1, 0, 0},
                                                        { 0, 0, 1, 0},
                                                        { 0, 0, 0, 1} });

            scaleMatrix = new Matrix(new double[4, 4]{  { 200, 0, 0, 0},
                                                        { 0, 200, 0, 0},
                                                        { 0, 0, 200, 0},
                                                        { 0, 0, 0, 1} });

            translationMatrix = new Matrix(new double[4, 4]{ { 1, 0, 0, 0},
                                                             { 0, 1, 0, 0},
                                                             { 0, 0, 1, 0},
                                                             { 0, 0, 0, 1} });

            rotationXMatrix = new Matrix(new double[4, 4]{ { 1, 0, 0, 0},
                                                           { 0, 1, 0, 0},
                                                           { 0, 0, 1, 0},
                                                           { 0, 0, 0, 1} });

            rotationYMatrix = new Matrix(new double[4, 4]{ { 1, 0, 0, 0},
                                                           { 0, 1, 0, 0},
                                                           { 0, 0, 1, 0},
                                                           { 0, 0, 0, 1} });

            rotationZMatrix = new Matrix(new double[4, 4]{ { 1, 0, 0, 0},
                                                           { 0, 1, 0, 0},
                                                           { 0, 0, 1, 0},
                                                           { 0, 0, 0, 1} });

            screenMatrix = new Matrix(new double[4, 4]{    { pbCanvas.Width/2, 0, 0, pbCanvas.Width/2},
                                                           { 0, -pbCanvas.Height/2, 0, pbCanvas.Height/2},
                                                           { 0, 0, 1, 0},
                                                           { 0, 0, 0, 1} });

            eye = new Vertex(0, 0, 1000);
            target = new Vertex(0, 0, 0);
            up = new Vertex(0, 1, 0);
            FillViewerMatrix();
            FillProjectionMatrix();

            List<Vertex> points = ReadFile();

            DrawModel();
            //this.Paint += Form1_Paint;
        }

        private List<Vertex> ReadFile()
        {
            StreamReader sr = new StreamReader("Model.obj");
            string line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line.Length < 2)
                {
                    continue;
                }
                if (line[0] == 'v' && line[1] == ' ')
                {
                    //string input;
                    //input.Split(';');
                    //Console.WriteLine(line);
                    string[] arr;
                    arr = line.Split(' ');
                    //Console.WriteLine(arr[0]);
                    Vertex vert = new Vertex();
                    vert.X = Convert.ToDouble(arr[1], NumberFormatInfo.InvariantInfo);
                    vert.Y = Convert.ToDouble(arr[2], NumberFormatInfo.InvariantInfo);
                    vert.Z = Convert.ToDouble(arr[3], NumberFormatInfo.InvariantInfo);
                    points.Add(vert);
                }
                if (line[0] == 'f' && line[1] == ' ')
                {
                    //string input;
                    //input.Split(';');
                    //Console.WriteLine(line);
                    string[] arr;
                    arr = line.Split(' ');
                    polygons.Add(new Polygon(Convert.ToInt32(arr[1].Split('/')[0]),
                                             Convert.ToInt32(arr[2].Split('/')[0]),
                                             Convert.ToInt32(arr[3].Split('/')[0])));
                    //Console.WriteLine(arr[0]);

                }
                //else break;
            }
            sr.Close();
            return points;
        }

        private void DrawModel()
        {
            drawPoints = new List<Vertex>();

            superMatrix = new Matrix(new double[4, 4]{    { 1, 0, 0, 0},
                                                             { 0, 1, 0, 0},
                                                             { 0, 0, 1, 0},
                                                             { 0, 0, 0, 1} });

            //superMatrix = superMatrix.Multiply(scaleMatrix);
            //superMatrix = superMatrix.Multiply(rotationXMatrix);
            //superMatrix = superMatrix.Multiply(rotationYMatrix);
            //superMatrix = superMatrix.Multiply(rotationZMatrix);
            //superMatrix = superMatrix.Multiply(translationMatrix);
            //superMatrix = superMatrix.Multiply(viewerMatrix);

            g.Clear(Color.White);
            for (int i = 0; i < points.Count; i++)
            {
                Vertex vertex = points[i].Clone();

                //vertex = superMatrix.Multiply(vertex);

                vertex = scaleMatrix.Multiply(vertex);
                vertex = rotationXMatrix.Multiply(vertex);
                vertex = rotationYMatrix.Multiply(vertex);
                vertex = rotationZMatrix.Multiply(vertex);
                vertex = translationMatrix.Multiply(vertex);

                vertex = viewerMatrix.Multiply(vertex);
                vertex = projectionMatrix.Multiply(vertex);
                vertex.X /= vertex.W;
                vertex.Y /= vertex.W;
                vertex.Z /= vertex.W;
                vertex.W /= vertex.W;
                vertex = screenMatrix.Multiply(vertex);


                //po1.X = Convert.ToInt32((vertex.X * +200));
                //po1.Y = Convert.ToInt32(((-1) * vertex.Y + 200));

                po1.X = Convert.ToInt32(vertex.X);
                po1.Y = Convert.ToInt32(vertex.Y);
                if (po1.X > 0 && po1.X < pbCanvas.Width && po1.Y > 0 && po1.Y < pbCanvas.Height)
                {
                    bitmap.SetPixel(po1.X, po1.Y, Color.Black);
                    vertex.wasDrawn = true;
                }
                else
                {
                    vertex.wasDrawn = false;
                }
                drawPoints.Add(vertex);
            }

            foreach (Polygon polygon in polygons)
            {
                if (drawPoints[polygon.vertex1Id-1].wasDrawn && drawPoints[polygon.vertex2Id-1].wasDrawn && drawPoints[polygon.vertex3Id - 1].wasDrawn)
                {
                    drawLine(drawPoints[polygon.vertex1Id - 1], drawPoints[polygon.vertex2Id - 1]);
                    drawLine(drawPoints[polygon.vertex2Id - 1], drawPoints[polygon.vertex3Id - 1]);
                    drawLine(drawPoints[polygon.vertex3Id - 1], drawPoints[polygon.vertex1Id - 1]);
                }
            }
            //g.DrawImage()
            pbCanvas.Image = bitmap;
        }

        private void drawLine(Vertex v1, Vertex v2)
        {
            double L = Math.Max(Math.Abs(v2.X - v1.X), Math.Abs(v2.Y - v1.Y));
            for (int i = 0; i < L; i++)
            {
                //res.Add(Convert.ToInt32(v1.X + (v2.X - v1.X)/L));
                //res.Add(Convert.ToInt32(v1.Y + (v2.Y - v1.Y) / L)); 
                
                po1.X = Convert.ToInt32(v1.X + (v2.X - v1.X) * i / L);
                po1.Y = Convert.ToInt32(v1.Y + (v2.Y - v1.Y) * i / L);
                if (po1.X > 0 && po1.X < pbCanvas.Width && po1.Y > 0 && po1.Y < pbCanvas.Height)
                {
                    bitmap.SetPixel(po1.X, po1.Y, Color.Black);
                }
            }
        }

        

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.A)       //Keys.(клавиша которая вам необходима)
            {                   // button_Click кнопка по которой кликнуть надо
                RotateY(0.1);
                e.Handled = true;
            }

            DrawModel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Clear();
            //this.Paint += Form1_Paint;
        }

        private void RotateX(double dAngleX)
        {
            angleX += dAngleX;
            rotationXMatrix.SetValue(1, 1, Math.Cos(angleX));
            rotationXMatrix.SetValue(1, 2, -Math.Sin(angleX));
            rotationXMatrix.SetValue(2, 1, Math.Sin(angleX));
            rotationXMatrix.SetValue(2, 2, Math.Cos(angleX));
        }
        private void RotateY(double dAngleY)
        {
            angleY += dAngleY;
            rotationYMatrix.SetValue(0, 0, Math.Cos(angleY));
            rotationYMatrix.SetValue(0, 2, Math.Sin(angleY));
            rotationYMatrix.SetValue(2, 0, -Math.Sin(angleY));
            rotationYMatrix.SetValue(2, 2, Math.Cos(angleY));
        }
        private void RotateZ(double dAngleZ)
        {
            angleZ += dAngleZ;
            rotationZMatrix.SetValue(0, 0, Math.Cos(angleZ));
            rotationZMatrix.SetValue(0, 1, -Math.Sin(angleZ));
            rotationZMatrix.SetValue(1, 0, Math.Sin(angleZ));
            rotationZMatrix.SetValue(1, 1, Math.Cos(angleZ));
        }

        private void Scale(double scale)
        {
            scaleMatrix.MultValue(0, 0, scale);
            scaleMatrix.MultValue(1, 1, scale);
            scaleMatrix.MultValue(2, 2, scale);
        }

        public void TranslateX(double dx)
        {
            translationMatrix.AddValue(0, 3, dx);
        }

        public void TranslateY(double dy)
        {
            translationMatrix.AddValue(1, 3, dy);
        }

        public void TranslateZ(double dz)
        {
            translationMatrix.AddValue(2, 3, dz);
        }

        public void TranslateCameraX(double dx)
        {
            eye.X += dx;
            FillViewerMatrix();
        }
        public void TranslateCameraY(double dy)
        {
            eye.Y += dy;
            FillViewerMatrix();
        }
        public void TranslateCameraZ(double dz)
        {
            eye.Z += dz;
            FillViewerMatrix();
        }

        public void FillViewerMatrix()
        {
            Vertex ZAxis = eye.Subtraction(target).Normalize();
            Vertex XAxis = up.VectorMultiplication(ZAxis).Normalize();
            Vertex YAxis = up.Clone();

            viewerMatrix = new Matrix(new double[4, 4]{ { XAxis.X, XAxis.Y, XAxis.Z, -(XAxis.ScalarMultiplication(eye))},
                                                        { YAxis.X, YAxis.Y, YAxis.Z, -(YAxis.ScalarMultiplication(eye))},
                                                        { ZAxis.X, ZAxis.Y, ZAxis.Z, -(ZAxis.ScalarMultiplication(eye))},
                                                        { 0, 0, 0, 1} });
        }

        public void FillProjectionMatrix()
        {
            projectionMatrix = new Matrix(new double[4, 4]{ { 2 * zNear / width, 0, 0, 0},
                                                            { 0, 2 * zNear / height, 0, 0},
                                                            { 0, 0, zFar / (zNear - zFar), zNear * zFar / (zNear - zFar)},
                                                            { 0, 0, -1, 0} });
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.A)
            {
                RotateY(-0.1);
            }
            if (e.KeyValue == (char)Keys.D)
            {
                RotateY(0.1);
            }
            if (e.KeyValue == (char)Keys.W)
            {
                RotateX(-0.1);
            }
            if (e.KeyValue == (char)Keys.S)
            {
                RotateX(0.1);
            }
            if (e.KeyValue == (char)Keys.Q)
            {
                RotateZ(0.1);
            }
            if (e.KeyValue == (char)Keys.E)
            {
                RotateZ(-0.1);
            }


            if (e.KeyValue == (char)Keys.T)
            {
                TranslateZ(-5);
            }
            if (e.KeyValue == (char)Keys.G)
            {
                TranslateZ(5);
            }
            if (e.KeyValue == (char)Keys.H)
            {
                TranslateX(5);
            }
            if (e.KeyValue == (char)Keys.F)
            {
                TranslateX(-5);
            }
            if (e.KeyValue == (char)Keys.R)
            {
                TranslateY(-5);
            }
            if (e.KeyValue == (char)Keys.Y)
            {
                TranslateY(5);
            }


            if (e.KeyValue == (char)Keys.I)
            {
                TranslateCameraZ(-5);
            }
            if (e.KeyValue == (char)Keys.K)
            {
                TranslateCameraZ(5);
            }
            if (e.KeyValue == (char)Keys.J)
            {
                TranslateCameraX(-5);
            }
            if (e.KeyValue == (char)Keys.L)
            {
                TranslateCameraX(5);
            }
            if (e.KeyValue == (char)Keys.U)
            {
                TranslateCameraY(-5);
            }
            if (e.KeyValue == (char)Keys.O)
            {
                TranslateCameraY(5);
            }


            if (e.KeyValue == (char)Keys.X)
            {
                Scale(1.05);
            }
            if (e.KeyValue == (char)Keys.Z)
            {
                Scale(0.95);
            }


            DrawModel();
        }
    }
}
