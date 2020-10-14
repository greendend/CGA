using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CGA
{
    class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }
        public bool wasDrawn { get; set; }

        public Vertex()
        {
            W = 1;
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

        public Vertex Normalize()
        {
            double invLength = 1 / Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Vertex(X * invLength, Y * invLength, Z * invLength, W);
        }

        public Vertex Subtraction(Vertex subtrahend)
        {
            return new Vertex(X - subtrahend.X, Y - subtrahend.Y, Z - subtrahend.Z, W - subtrahend.W);
        }

        public Vertex Addition(Vertex term)
        {
            return new Vertex(X + term.X, Y + term.Y, Z + term.Z, W + term.W);
        }

        public Vertex VectorMultiplication(Vertex mult)
        {
            return new Vertex(Y * mult.Z - Z * mult.Y, -(X * mult.Z - Z * mult.X), X * mult.Y - Y * mult.X, W);
        }

        public double ScalarMultiplication(Vertex mult)
        {
            return X * mult.X + Y * mult.Y + Z * mult.Z;
        }

        public Vertex Clone()
        {
            return new Vertex(X, Y, Z, W);
        }
    }
}
