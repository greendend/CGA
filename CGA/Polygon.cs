using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGA
{
    class Polygon
    {
        public int vertex1Id { get; set; }
        public int vertex2Id { get; set; }
        public int vertex3Id { get; set; }

        public Polygon(int vertex1Id, int vertex2Id, int vertex3Id)
        {
            this.vertex1Id = vertex1Id;
            this.vertex2Id = vertex2Id;
            this.vertex3Id = vertex3Id;
        }
    }
}
