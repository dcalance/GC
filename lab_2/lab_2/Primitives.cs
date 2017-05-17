using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using System.Windows.Media;

namespace lab_2
{
    class Primitives
    {
        static public void DrawSphere(Point3 center, float radius, int transtelation, Color color, bool drawFrame = false)
        {

            MainWindow.gl.MatrixMode(OpenGL.GL_MODELVIEW);
            MainWindow.gl.PushMatrix();
            MainWindow.gl.Translate(0.0f, -radius, 0.0f);
            float gap = (radius * 2) / transtelation;

            Point3 drawPoint = new Point3 { x = 0, y = gap / 2, z = 0 };
            for (int i = 0; i < transtelation; i++)
            {
                float drawRadius1 = (float)Math.Sqrt(Math.Abs(Math.Pow(radius, 2) - Math.Pow(radius - drawPoint.y + gap / 2, 2)));
                float drawRadius2 = (float)Math.Sqrt(Math.Abs(Math.Pow(radius, 2) - Math.Pow(radius - drawPoint.y - gap / 2, 2)));
                DrawCone(drawPoint, gap, drawRadius1, drawRadius2, transtelation, color, false, drawFrame);
                drawPoint.y += gap;
            }
            MainWindow.gl.PopMatrix();
        }
        static public void DrawCone(Point3 center, float height, float radius1, float radius2, int transtelation, Color color, bool drawFaces = true, bool drawFrame = false)
        {
            MainWindow.gl.MatrixMode(OpenGL.GL_MODELVIEW);
            MainWindow.gl.PushMatrix();
            MainWindow.gl.Translate(center.x, center.y - height / 2, center.z + radius1);

            float division = 2 * (float)Math.PI / transtelation;

            POINTFLOAT p1 = new POINTFLOAT { x = radius1 * (float)Math.Cos(division), y = radius1 * (float)Math.Sin(division) };
            POINTFLOAT p2 = new POINTFLOAT { x = radius1 * (float)Math.Cos(division * 2), y = radius1 * (float)Math.Sin(division * 2) };
            float radius1Line = (float)Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
            float transRadius1 = (float)Math.Sqrt(Math.Pow(radius1, 2) - Math.Pow(radius1Line / 2, 2));

            p1 = new POINTFLOAT { x = radius2 * (float)Math.Cos(division), y = radius2 * (float)Math.Sin(division) };
            p2 = new POINTFLOAT { x = radius2 * (float)Math.Cos(division * 2), y = radius2 * (float)Math.Sin(division * 2) };
            float radius2Line = (float)Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
            float transRadius2 = (float)Math.Sqrt(Math.Pow(radius2, 2) - Math.Pow(radius2Line / 2, 2));

            p1 = new POINTFLOAT { x = 0, y = height };
            p2 = new POINTFLOAT { x = transRadius1 - transRadius2, y = height };

            float angleX = GetAngleDegrees(p1, p2);
            angleX = (radius1 < radius2) ? angleX : -angleX;

            POINTFLOAT normVect1, normVect2;
            POINTFLOAT p3;
            POINTFLOAT plane = new POINTFLOAT { x = 1, y = 0 };
            if (radius1 == 0)
            {
                p1 = new POINTFLOAT { x = radius2 * (float)Math.Cos(-Math.PI / 2), y = radius2 * (float)Math.Sin(-Math.PI / 2) };
                p2 = new POINTFLOAT { x = radius2 * (float)Math.Cos(-Math.PI / 2 + division), y = radius2 * (float)Math.Sin(-Math.PI / 2 + division) };
                p3 = new POINTFLOAT { x = radius2 * (float)Math.Cos(-Math.PI / 2 + division * 2), y = radius2 * (float)Math.Sin(-Math.PI / 2 + division * 2) };
            }
            else
            {
                p1 = new POINTFLOAT { x = radius1 * (float)Math.Cos(-Math.PI / 2), y = radius1 * (float)Math.Sin(-Math.PI / 2) };
                p2 = new POINTFLOAT { x = radius1 * (float)Math.Cos(-Math.PI / 2 + division), y = radius1 * (float)Math.Sin(-Math.PI / 2 + division) };
                p3 = new POINTFLOAT { x = radius1 * (float)Math.Cos(-Math.PI / 2 + division * 2), y = radius1 * (float)Math.Sin(-Math.PI / 2 + division * 2) };
            }

            normVect1 = new POINTFLOAT { x = p2.x - p1.x, y = p2.y - p1.y };
            normVect2 = new POINTFLOAT { x = p3.x - p2.x, y = p3.y - p2.y };
            float initialAngleY = GetAngleDegrees(normVect1, plane);
            float angleY = GetAngleDegrees(normVect1, normVect2);

            POINTFLOAT[] trapezeCoord = new POINTFLOAT[4];

            p1 = new POINTFLOAT { x = radius1 * (float)Math.Cos(division), y = radius1 * (float)Math.Sin(division) };
            p2 = new POINTFLOAT { x = radius1 * (float)Math.Cos(division * 2), y = radius1 * (float)Math.Sin(division * 2) };
            trapezeCoord[0] = new POINTFLOAT { x = 0, y = 0 };
            trapezeCoord[1] = new POINTFLOAT { x = (float)Math.Sqrt((float)Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2)), y = 0 };

            p3 = new POINTFLOAT { x = radius2 * (float)Math.Cos(division), y = radius2 * (float)Math.Sin(division) };
            POINTFLOAT p4 = new POINTFLOAT { x = radius2 * (float)Math.Cos(division * 2), y = radius2 * (float)Math.Sin(division * 2) };

            float topLength = (float)Math.Sqrt((float)Math.Pow(p3.x - p4.x, 2) + Math.Pow(p3.y - p4.y, 2));
            float trapezeHeight = (float)Math.Sqrt((float)Math.Pow(radius1 - radius2, 2) + Math.Pow(height, 2));
            trapezeHeight = (float)Math.Sqrt(Math.Pow(height, 2) + Math.Pow(transRadius1 - transRadius2, 2));
            trapezeCoord[2] = new POINTFLOAT { x = (trapezeCoord[1].x - (topLength)) / 2, y = trapezeHeight };
            trapezeCoord[3] = new POINTFLOAT { x = trapezeCoord[2].x + (topLength), y = trapezeHeight };

            MainWindow.gl.Rotate(initialAngleY, 0, 1, 0);
            for (int i = 0; i < transtelation; i++)
            {
                MainWindow.gl.Rotate(angleX, 1.0f, 0.0f, 0.0f);
                DrawTriangle(trapezeCoord[0], trapezeCoord[1], trapezeCoord[2], color, drawFrame);
                DrawTriangle(trapezeCoord[3], trapezeCoord[1], trapezeCoord[2], color, drawFrame);
                MainWindow.gl.Rotate(-angleX, 1.0f, 0.0f, 0.0f);
                MainWindow.gl.Translate(trapezeCoord[1].x, 0, 0);
                MainWindow.gl.Rotate(angleY, 0, 1, 0);

            }

            MainWindow.gl.PopMatrix();
            if (drawFaces)
            {
                MainWindow.gl.PushMatrix();
                MainWindow.gl.Translate(center.x, center.y - height / 2, center.z + radius1);
                MainWindow.gl.Translate(0, 0, -radius1);
                MainWindow.gl.Rotate(-90, 1.0f, 0.0f, 0.0f);
                POINTFLOAT center1 = new POINTFLOAT { x = 0, y = 0 };
                DrawCircle(center1, radius1, transtelation, color, drawFrame);
                MainWindow.gl.Translate(0.0f, 0.0f, height);
                DrawCircle(center1, radius2, transtelation, color, drawFrame);
                MainWindow.gl.PopMatrix();
            }
        }
        static public void DrawCylinder(Point3 center, float height, float radius, int transtelation, Color color, bool drawFrame = false)
        {
            MainWindow.gl.MatrixMode(OpenGL.GL_MODELVIEW);
            MainWindow.gl.PushMatrix();
            MainWindow.gl.Translate(center.x, center.y - height / 2, center.z + radius);

            float division = 2 * (float)Math.PI / transtelation;
            POINTFLOAT p1 = new POINTFLOAT { x = radius * (float)Math.Cos(-Math.PI / 2), y = radius * (float)Math.Sin(-Math.PI / 2) };
            POINTFLOAT p2 = new POINTFLOAT { x = radius * (float)Math.Cos(-Math.PI / 2 + division), y = radius * (float)Math.Sin(-Math.PI / 2 + division) };
            POINTFLOAT normVect1 = new POINTFLOAT { x = p2.x - p1.x, y = p2.y - p1.y };
            POINTFLOAT plane = new POINTFLOAT { x = 1, y = 0 };
            float intialAngle = GetAngleDegrees(normVect1, plane);

            POINTFLOAT p3 = new POINTFLOAT { x = radius * (float)Math.Cos(-Math.PI / 2 + division), y = radius * (float)Math.Sin(-Math.PI / 2 + division) };
            POINTFLOAT p4 = new POINTFLOAT { x = radius * (float)Math.Cos(-Math.PI / 2 + division * 2), y = radius * (float)Math.Sin(-Math.PI / 2 + division * 2) };
            POINTFLOAT normVect2 = new POINTFLOAT { x = p4.x - p3.x, y = p4.y - p3.y };
            float angle = GetAngleDegrees(normVect1, normVect2);

            p2 = new POINTFLOAT { x = (float)Math.Sqrt((float)Math.Pow((p1.x - p2.x), 2) + (float)Math.Pow((p1.y - p2.y), 2)), y = height };
            p1 = new POINTFLOAT { x = 0, y = 0 };
            MainWindow.gl.Rotate(intialAngle, 0, 1, 0);
            for (int i = 0; i < transtelation; i++)
            {
                DrawRect(p1, p2, color, drawFrame);
                MainWindow.gl.Translate(p2.x, 0, 0);
                MainWindow.gl.Rotate(angle, 0, 1, 0);

            }
            MainWindow.gl.PopMatrix();

            MainWindow.gl.PushMatrix();
            MainWindow.gl.Translate(center.x, center.y - height / 2, center.z + radius);
            POINTFLOAT center1 = new POINTFLOAT { x = 0, y = 0 };
            MainWindow.gl.Rotate(-90, 1.0f, 0.0f, 0.0f);
            MainWindow.gl.Translate(0.0f, radius, 0.0f);
            DrawCircle(center1, radius, transtelation, color, drawFrame);
            MainWindow.gl.Translate(0.0f, 0.0f, height);
            DrawCircle(center1, radius, transtelation, color, drawFrame);
            MainWindow.gl.PopMatrix();
        }
        static public void DrawPointTest(float x, float y, float z, Color color, bool drawFrame = false)
        {
            MainWindow.gl.MatrixMode(OpenGL.GL_MODELVIEW);
            MainWindow.gl.PushMatrix();
            MainWindow.gl.Translate(x, y, z);

            MainWindow.gl.Begin(OpenGL.GL_POINTS);
            MainWindow.gl.Color(color.R, color.G, color.B);
            MainWindow.gl.Vertex(0f, 0f);
            MainWindow.gl.End();

            MainWindow.gl.PopMatrix();

        }
        static public void DrawTriangle(POINTFLOAT p1, POINTFLOAT p2, POINTFLOAT p3, Color color, bool drawFrame = false)
        {
            if (!drawFrame)
            {
                MainWindow.gl.Begin(OpenGL.GL_TRIANGLES);
                MainWindow.gl.Color(color.R, color.G, color.B);
                MainWindow.gl.Vertex(p1.x, p1.y);
                MainWindow.gl.Vertex(p2.x, p2.y);
                MainWindow.gl.Vertex(p3.x, p3.y);
                MainWindow.gl.End();
            }
            else
            {
                MainWindow.gl.Begin(OpenGL.GL_LINES);
                MainWindow.gl.Color(color.R, color.G, color.B);
                MainWindow.gl.Vertex(p1.x, p1.y);
                MainWindow.gl.Vertex(p2.x, p2.y);
                MainWindow.gl.Vertex(p3.x, p3.y);
                MainWindow.gl.End();
            }
        }

        static public void DrawRect(POINTFLOAT p1, POINTFLOAT p4, Color color, bool drawFrame = false)
        {
            POINTFLOAT p2 = new POINTFLOAT { x = p1.x, y = p4.y };
            POINTFLOAT p3 = new POINTFLOAT { x = p4.x, y = p1.y };
            DrawTriangle(p3, p1, p2, color, drawFrame);
            DrawTriangle(p3, p4, p2, color, drawFrame);
        }
        static public void DrawCircle(POINTFLOAT center, float radius, int transtelation, Color color, bool drawFrame = false)
        {
            float angleInc = 2 * (float)Math.PI / transtelation;
            for (float theta = -(float)Math.PI / 2; theta <= 3 * (float)Math.PI / 2; theta += angleInc)
            {
                POINTFLOAT p2 = new POINTFLOAT { x = radius * (float)Math.Cos(theta), y = radius * (float)Math.Sin(theta) };
                POINTFLOAT p3 = new POINTFLOAT { x = radius * (float)Math.Cos(theta + angleInc), y = radius * (float)Math.Sin(theta + angleInc) };
                DrawTriangle(center, p2, p3, color, drawFrame);
            }
        }
        static public float GetAngleDegrees(POINTFLOAT p1, POINTFLOAT p2)
        {
            float dotProd = p1.x * p2.x + p1.y * p2.y;
            float magnProd = (float)Math.Sqrt(p1.x * p1.x + p1.y * p1.y) * (float)Math.Sqrt(p2.x * p2.x + p2.y * p2.y);
            float angle = (float)Math.Acos(dotProd / magnProd);
            return (float)(angle * 180 / Math.PI);
        }
        static public void DrawPrism(Point3 center, float sizeX, float sizeY, float sizeZ, Color color, bool drawFrame = false)
        {
            MainWindow.gl.MatrixMode(OpenGL.GL_MODELVIEW);
            MainWindow.gl.PushMatrix();
            MainWindow.gl.Translate(center.x - sizeX / 2, center.y - sizeY / 2, center.z - sizeZ / 2);

            POINTFLOAT p1 = new POINTFLOAT { x = 0, y = 0 };
            POINTFLOAT p2 = new POINTFLOAT { x = sizeX, y = sizeY };
            DrawRect(p1, p2, color, drawFrame);
            MainWindow.gl.Translate(0.0f, sizeY, 0.0f);
            MainWindow.gl.Rotate(90f, 1.0f, 0.0f, 0.0f);
            p1 = new POINTFLOAT { x = 0, y = 0 };
            p2 = new POINTFLOAT { x = sizeX, y = sizeZ };
            DrawRect(p1, p2, color, drawFrame);
            MainWindow.gl.Translate(0.0f, sizeZ, 0.0f);
            MainWindow.gl.Rotate(90f, 1.0f, 0.0f, 0.0f);
            p1 = new POINTFLOAT { x = 0, y = 0 };
            p2 = new POINTFLOAT { x = sizeX, y = sizeY };
            DrawRect(p1, p2, color, drawFrame);
            MainWindow.gl.Translate(0.0f, sizeY, 0.0f);
            MainWindow.gl.Rotate(90f, 1.0f, 0.0f, 0.0f);
            p1 = new POINTFLOAT { x = 0, y = 0 };
            p2 = new POINTFLOAT { x = sizeX, y = sizeZ };
            DrawRect(p1, p2, color, drawFrame);
            MainWindow.gl.Translate(0.0f, sizeZ, 0.0f);
            MainWindow.gl.Rotate(90f, 1.0f, 0.0f, 0.0f);

            MainWindow.gl.Rotate(-90f, 0.0f, 1.0f, 0.0f);
            p1 = new POINTFLOAT { x = 0, y = 0 };
            p2 = new POINTFLOAT { x = sizeZ, y = sizeY };
            DrawRect(p1, p2, color, drawFrame);
            MainWindow.gl.Translate(0.0f, 0.0f, -sizeX);
            DrawRect(p1, p2, color, drawFrame);
            MainWindow.gl.PopMatrix();

        }
    }
}
