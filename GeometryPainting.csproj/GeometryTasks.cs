using System;

namespace GeometryTasks
{
     public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }
        
        public Vector Add(Vector vector)
        {
            return Geometry.Add(this, vector);
        }
        
        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;
        
        public double GetLength()
        {
            return Geometry.GetLength(this);
        }
        
        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }
    
    public class Geometry
    {
        public static double GetLength(Vector vec)
        {
            return Math.Sqrt(vec.X*vec.X+vec.Y*vec.Y);
        }
        
        public static double GetLength(Segment segment)
        {
            var X = segment.End.X - segment.Begin.X;
            var Y = segment.End.Y - segment.Begin.Y;

            return Math.Sqrt(X*X+ Y*Y);
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            if (segment.Begin.X==segment.End.X && segment.Begin.Y==segment.End.Y  && 
                segment.Begin.X==vector.X && segment.Begin.Y==vector.Y)
                return true;
            return (!segment.Begin.Equals(segment.End) && vector.X <= segment.End.X &&
                    vector.X >= segment.Begin.X &&
                    vector.Y <= segment.End.Y && vector.Y >= segment.Begin.Y &&
                    (vector.X - segment.Begin.X) * (segment.End.Y - segment.Begin.Y) ==
                    (vector.Y - segment.Begin.Y) * (segment.End.X - segment.Begin.X));
        }

        public static Vector Add(Vector vec1, Vector vec2)
        {
            return new Vector { X=vec1.X + vec2.X, Y=vec1.Y + vec2.Y};
        }
    }
}