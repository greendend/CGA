using CGA;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGA
{
    class Matrix
    {
        double[ , ] values;

        public Matrix()
        {
            values = new double[4, 4]{  { 0, 0, 0, 0},
                                        { 0, 0, 0, 0},
                                        { 0, 0, 0, 0},
                                        { 0, 0, 0, 0} };
        }

        public Matrix(double[,] values)
        {
            this.values = values;
        }

        public Vertex Multiply(Vertex vertex) //пока без W
        {
            return new Vertex( values[0, 0] * vertex.X + values[0, 1] * vertex.Y + values[0, 2] * vertex.Z + values[0, 3] * vertex.W,
                              values[1, 0] * vertex.X + values[1, 1] * vertex.Y + values[1, 2] * vertex.Z + values[1, 3] * vertex.W,
                              values[2, 0] * vertex.X + values[2, 1] * vertex.Y + values[2, 2] * vertex.Z + values[2, 3] * vertex.W,
                              values[3, 0] * vertex.X + values[3, 1] * vertex.Y + values[3, 2] * vertex.Z + values[3, 3] * vertex.W);
        }
        public Matrix Multiply(Matrix matrix) //эта на входную
        {
            double[,] newValues = new double[4, 4] { {  values[0, 0]*matrix.values[0, 0] + values[0, 1]*matrix.values[1, 0] + values[0, 2]*matrix.values[2, 0] + values[0, 3]*matrix.values[3, 0],
                                                        values[0, 0]*matrix.values[0, 1] + values[0, 1]*matrix.values[1, 1] + values[0, 2]*matrix.values[2, 1] + values[0, 3]*matrix.values[3, 1],
                                                        values[0, 0]*matrix.values[0, 2] + values[0, 1]*matrix.values[1, 2] + values[0, 2]*matrix.values[2, 2] + values[0, 3]*matrix.values[3, 2],
                                                        values[0, 0]*matrix.values[0, 3] + values[0, 1]*matrix.values[1, 3] + values[0, 2]*matrix.values[2, 3] + values[0, 3]*matrix.values[3, 3]},

                                                     {  values[1, 0]*matrix.values[0, 0] + values[1, 1]*matrix.values[1, 0] + values[1, 2]*matrix.values[2, 0] + values[1, 3]*matrix.values[3, 0],
                                                        values[1, 0]*matrix.values[0, 1] + values[1, 1]*matrix.values[1, 1] + values[1, 2]*matrix.values[2, 1] + values[1, 3]*matrix.values[3, 1],
                                                        values[1, 0]*matrix.values[0, 2] + values[1, 1]*matrix.values[1, 2] + values[1, 2]*matrix.values[2, 2] + values[1, 3]*matrix.values[3, 2],
                                                        values[1, 0]*matrix.values[0, 3] + values[1, 1]*matrix.values[1, 3] + values[1, 2]*matrix.values[2, 3] + values[1, 3]*matrix.values[3, 3]},

                                                     {  values[2, 0]*matrix.values[0, 0] + values[2, 1]*matrix.values[1, 0] + values[2, 2]*matrix.values[2, 0] + values[2, 3]*matrix.values[3, 0],
                                                        values[2, 0]*matrix.values[0, 1] + values[2, 1]*matrix.values[1, 1] + values[2, 2]*matrix.values[2, 1] + values[2, 3]*matrix.values[3, 1],
                                                        values[2, 0]*matrix.values[0, 2] + values[2, 1]*matrix.values[1, 2] + values[2, 2]*matrix.values[2, 2] + values[2, 3]*matrix.values[3, 2],
                                                        values[2, 0]*matrix.values[0, 3] + values[2, 1]*matrix.values[1, 3] + values[2, 2]*matrix.values[2, 3] + values[2, 3]*matrix.values[3, 3]},

                                                     {  values[3, 0]*matrix.values[0, 0] + values[3, 1]*matrix.values[1, 0] + values[3, 2]*matrix.values[2, 0] + values[3, 3]*matrix.values[3, 0],
                                                        values[3, 0]*matrix.values[0, 1] + values[3, 1]*matrix.values[1, 1] + values[3, 2]*matrix.values[2, 1] + values[3, 3]*matrix.values[3, 1],
                                                        values[3, 0]*matrix.values[0, 2] + values[3, 1]*matrix.values[1, 2] + values[3, 2]*matrix.values[2, 2] + values[3, 3]*matrix.values[3, 2],
                                                        values[3, 0]*matrix.values[0, 3] + values[3, 1]*matrix.values[1, 3] + values[3, 2]*matrix.values[2, 3] + values[3, 3]*matrix.values[3, 3]},  };


            return new Matrix(newValues);
        }

        public void SetValue(int i, int j, double value)
        {
            values[i, j] = value;
        }
        public void AddValue(int i, int j, double value)
        {
            values[i, j] += value;
        }
        public void MultValue(int i, int j, double value)
        {
            values[i, j] *= value;
        }
    }
}
