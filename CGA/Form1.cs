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
        Matrix worldMatrix;
        Matrix scaleMatrix;
        Matrix movementMatrix;
        Matrix rotationMatrixX;
        Matrix rotationMatrixY;
        Matrix rotationMatrixZ;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            worldMatrix = new Matrix(new double[4, 4]{  { 1, 0, 1, 0},
                                                        { 0, 1, 0, 0},
                                                        { 0, 0, 1, 0},
                                                        { 0, 0, 0, 1} });

            scaleMatrix = new Matrix(new double[4, 4]{  { 0.5, 0, 0, 0},
                                                        { 0, 0.5, 0, 0},
                                                        { 0, 0, 0.5, 0},
                                                        { 0, 0, 0, 1} });

            movementMatrix = new Matrix(new double[4, 4]{  { 1, 0, 0, 0.5},
                                                           { 0, 1, 0, 0.5},
                                                           { 0, 0, 1, 0.5},
                                                           { 0, 0, 0, 1} });

            rotationMatrixX = new Matrix(new double[4, 4]{ { 1, 0, 0, 0},
                                                           { 0, Math.Cos(10), -Math.Sin(10), 0},
                                                           { 0, Math.Sin(10), Math.Cos(10), 0},
                                                           { 0, 0, 0, 1} });

            rotationMatrixY = new Matrix(new double[4, 4]{ { 0, 0, 1, 0},
                                                           { 0, 1, 0, 0},
                                                           { -1, 0, 0, 0},
                                                           { 0, 0, 0, 1} });

            rotationMatrixZ = new Matrix(new double[4, 4]{ { 0, 0, 0, 0},
                                                           { 0, 0, 0, 0},
                                                           { 0, 0, 1, 0},
                                                           { 0, 0, 0, 1} });

            List<Vertex> points = ReadFile();            
            //this.Paint += Form1_Paint;
        }

    private List<Vertex> ReadFile()
        {
            StreamReader sr = new StreamReader("Model.obj");
            string line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line[0] != '#')
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
                else break;
            }
            sr.Close();
            return points;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            //g.DrawLine(new Pen(Color.Red), 10, 10, 100, 100);

            for (int i = 0; i< points.Count; i++)
            {
                Point po1 = new Point();
                Point po2 = new Point();
                po1.X = Convert.ToInt32((points[i].X + 1) * 200);
                po1.Y = Convert.ToInt32(((-1)*points[i].Y + 1) * 200);
                po2.X = Convert.ToInt32((points[i].X + 1) * 200 + 2);
                po2.Y = Convert.ToInt32(((-1)*points[i].Y + 1) * 200 + 2);

                g.DrawLine(new Pen(Color.Black), po1, po2);
            }
        }

        

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Right)       //Keys.(клавиша которая вам необходима)
            {                   // button_Click кнопка по которой кликнуть надо
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = rotationMatrixX.Multiply(points[i]);
                }
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Clear();
            this.Paint += Form1_Paint;
        }

        private void button2_Click(object sender, EventArgs e)
        {                 // button_Click кнопка по которой кликнуть надо
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = rotationMatrixX.Multiply(points[i]);
                }
        }
    }
}
