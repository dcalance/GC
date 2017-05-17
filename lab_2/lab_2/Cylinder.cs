using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    class Cylinder : Gemotery
    {
        public Cylinder()
        {
            center = new Point3();
            transtelation = 20;
        }
        public override void Draw()
        {
            MainWindow.gl.PushMatrix();
            MainWindow.gl.Rotate(rotateX, rotateY, rotateZ);
            MainWindow.gl.Translate(translateX, translateY, translateZ);
            MainWindow.gl.Scale(scaleX, scaleY, scaleZ);
            Primitives.DrawCylinder(center, 2, 1, transtelation, color, drawFrame);
            MainWindow.gl.PopMatrix();
        }
    }
}
