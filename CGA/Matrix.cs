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
            values = new double[4, 4]{   { 0, 0, 0, 0},
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
            return new Vertex(values[0, 0] * vertex.X + values[0, 1] * vertex.Y + values[0, 2] * vertex.Z + values[0, 3] * vertex.W,
                              values[1, 0] * vertex.X + values[1, 1] * vertex.Y + values[1, 2] * vertex.Z + values[1, 3] * vertex.W,
                              values[2, 0] * vertex.X + values[2, 1] * vertex.Y + values[2, 2] * vertex.Z + values[2, 3] * vertex.W,
                              values[3, 0] * vertex.X + values[3, 1] * vertex.Y + values[3, 2] * vertex.Z + values[3, 3] * vertex.W);
        }
    }
}
