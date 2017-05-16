using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph;
using SharpGL.Enumerations;

namespace Example1
{
    public class Point3
    {
        public float x;
        public float y;
        public float z;
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private OpenGL gl;

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            //gl = args.OpenGL;	

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, -2.0f, -10.0f);
            POINTFLOAT p1 = new POINTFLOAT { x = 0.0f, y = 0.0f };
            POINTFLOAT p2 = new POINTFLOAT { x = 2.0f, y = -2.0f };
            POINTFLOAT p3 = new POINTFLOAT { x = 2.0f, y = -2.0f };

            Point3 d1 = new Point3 { x = 0.0f, y = 2.0f, z = 0.0f };
            Point3 d2 = new Point3 { x = 2.0f, y = 1.0f, z = 2.0f };

            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
            gl.Rotate(rotationY, 1.0f, 0.0f, 0.0f);
            gl.Rotate(10f, 1.0f, 0.0f, 0.0f);
            //DrawTriangle(p1, p2, p3);
            //DrawRect(p1, p2);
            //DrawCircle(p1, 2.0f, 10);
            //DrawPrism(d1, 4, 1, 5);
            DrawCylinder(d1, 4, 5, 20);
            DrawPointTest(0.0f, 0.0f, 0.0f);
            //DrawPointTest(4.0f, 0.0f, 0.0f);
            //DrawPointTest(5.0f, 0.0f, 0.0f);

            Teapot tp = new Teapot();
            //tp.Draw(gl, 14, 1, OpenGL.GL_FILL);
            gl.PopMatrix();

        }
        private void DrawPointTest(float x, float y, float z)
        {
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            gl.Translate(x, y, z);

            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(0.95f, 0.207, 0.031f);
            gl.Vertex(0f, 0f);
            gl.End();

            gl.PopMatrix();

        }
        private void DrawTriangle(POINTFLOAT p1, POINTFLOAT p2, POINTFLOAT p3)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(p1.x, p1.y);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(p2.x, p2.y);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(p3.x, p3.y);
            gl.End();
        }

        private void DrawRect(POINTFLOAT p1, POINTFLOAT p4)
        {
            POINTFLOAT p2 = new POINTFLOAT { x = p1.x, y = p4.y };
            POINTFLOAT p3 = new POINTFLOAT { x = p4.x, y = p1.y };
            DrawTriangle(p3, p1, p2);
            DrawTriangle(p3, p4, p2);
        }
        private void DrawCircle(POINTFLOAT center, float radius, int transtelation)
        {
            float division = 2 * (float)Math.PI / transtelation;
            for (int i = 0; i < transtelation; i++)
            {
                POINTFLOAT p2 = new POINTFLOAT { x = radius * (float)Math.Cos(division * i), y = radius * (float)Math.Sin(division * i) };
                POINTFLOAT p3 = new POINTFLOAT { x = radius * (float)Math.Cos(division * (i + 1)), y = radius * (float)Math.Sin(division * (i + 1)) };
                DrawTriangle(center, p2, p3);
            }
        }
        private void DrawCylinder(Point3 center, float height, float radius, int transtelation)
        {
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            //gl.Translate(center.x, center.y - height, center.z + radius);

            float division = 2 * (float)Math.PI / transtelation;
            POINTFLOAT p2 = new POINTFLOAT { x = radius * (float)Math.Cos(division * 1), y = radius * (float)Math.Sin(division * 1) };
            POINTFLOAT p3 = new POINTFLOAT { x = radius * (float)Math.Cos(division * 2), y = radius * (float)Math.Sin(division * 2) };
            float dotProd = p2.x * p3.x + p2.y * p3.y;
            float magnProd = (float)Math.Sqrt(p2.x * p2.x + p2.y * p2.y) * (float)Math.Sqrt(p3.x * p3.x + p3.y * p3.y);
            float angle = (float)Math.Acos(dotProd / magnProd);
            p3 = new POINTFLOAT { x = (float)Math.Sqrt((float)Math.Pow(p2.x - p3.x, 2) + (float)Math.Pow(p2.y - p3.y, 2)), y = height };
            p2 = new POINTFLOAT { x = 0, y = 0 };

            for (int i = 0; i < transtelation; i++)
            {
                DrawRect(p2, p3);
                gl.Translate(p3.x, 0, 0);
                gl.Rotate(angle * 180 / Math.PI, 0, 1, 0);
            }
            gl.PopMatrix();
            gl.PushMatrix();

            POINTFLOAT center1 = new POINTFLOAT { x = center.x, y = center.y };
            gl.Rotate(90, 1.0f, 0.0f, 0.0f);
            gl.Translate(0.0f, - radius, 0.0f);
            DrawPointTest(0, 0, 0);
            DrawCircle(center1, radius, transtelation);

            gl.PopMatrix();
        }
        private void DrawPrism(Point3 center, float sizeX, float sizeY, float sizeZ)
        {
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            gl.Translate(center.x - sizeX / 2, center.y - sizeY / 2, center.z - sizeZ / 2);

            POINTFLOAT p1 = new POINTFLOAT { x = 0, y = 0 };
            POINTFLOAT p2 = new POINTFLOAT { x = sizeX, y = sizeY };
            DrawRect(p1, p2);
            gl.Translate(0.0f, sizeY, 0.0f);
            gl.Rotate(90f, 1.0f, 0.0f, 0.0f);
            p1 = new POINTFLOAT { x = 0, y = 0 };
            p2 = new POINTFLOAT { x = sizeX, y = sizeZ };
            DrawRect(p1, p2);
            gl.Translate(0.0f, sizeZ, 0.0f);
            gl.Rotate(90f, 1.0f, 0.0f, 0.0f);
            p1 = new POINTFLOAT { x = 0, y = 0 };
            p2 = new POINTFLOAT { x = sizeX, y = sizeY };
            DrawRect(p1, p2);
            gl.Translate(0.0f, sizeY, 0.0f);
            gl.Rotate(90f, 1.0f, 0.0f, 0.0f);
            p1 = new POINTFLOAT { x = 0, y = 0 };
            p2 = new POINTFLOAT { x = sizeX, y = sizeZ };
            DrawRect(p1, p2);
            gl.Translate(0.0f, sizeZ, 0.0f);
            gl.Rotate(90f, 1.0f, 0.0f, 0.0f);

            gl.Rotate(-90f, 0.0f, 1.0f, 0.0f);
            p1 = new POINTFLOAT { x = 0, y = 0 };
            p2 = new POINTFLOAT { x = sizeZ, y = sizeY };
            DrawRect(p1, p2);
            gl.Translate(0.0f, 0.0f, -sizeX);
            DrawRect(p1, p2);
            gl.PopMatrix();

        }

        float rotation = 0;
        float rotationY = 0;

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            gl = args.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Enable(OpenGL.GL_POINT_SMOOTH);
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            //gl.Enable(OpenGL.GL_DEPTH_TEST);

            //float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            //float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };
            //float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            //float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            //float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            //float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            //gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            //gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            //gl.Enable(OpenGL.GL_LIGHTING);
            //gl.Enable(OpenGL.GL_LIGHT0);

            //gl.ShadeModel(OpenGL.GL_SMOOTH);
            args.OpenGL.PointSize(6.0f);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            rotation += 15.0f;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            rotationY += 15.0f;
        }
    }
}
