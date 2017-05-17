using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace lab_2
{
    abstract class Gemotery
    {
        public Point3 center = new Point3();
        public Color color = new Color();
        public bool drawFrame { get; set; }
        public int transtelation { get; set; }
        public float scaleX { get; set; } = 1.0f;
        public float scaleY { get; set; } = 1.0f;
        public float scaleZ { get; set; } = 1.0f;
        public float rotateX { get; set; }
        public float rotateY { get; set; }
        public float rotateZ { get; set; }
        public float translateX { get; set; }
        public float translateY { get; set; }
        public float translateZ { get; set; }
        public abstract void Draw();

    }
}
