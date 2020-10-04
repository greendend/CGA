using System;
using System.Collections.Generic;
using System.Text;

namespace CGA
{
    class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public Vertex()
        {

        }

        public Vertex(double X, double Y, double Z, double W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }

        public Vertex(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = 1;
        }
    }
}
