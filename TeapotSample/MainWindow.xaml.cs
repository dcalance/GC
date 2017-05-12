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

            Point3 d1 = new Point3 { x = 0.0f, y = 0.0f, z = 0.0f };
            Point3 d2 = new Point3 { x = 2.0f, y = 1.0f, z = 2.0f };

            //gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
            //gl.Rotate(10f, 1.0f, 0.0f, 0.0f);
            //DrawTriangle(p1, p2, p3);
            //DrawRect(p1, p2);
            //DrawCircle(p1, 2.0f, 10);
            //DrawCube(d1, d2);
            DrawPointTest(0.0f, 0.0f, 0.0f);
            DrawPointTest(4.0f, 0.0f, 0.0f);
            DrawPointTest(5.0f, 0.0f, 0.0f);

            Teapot tp = new Teapot();
            //tp.Draw(gl, 14, 1, OpenGL.GL_FILL);


        }
        private void DrawPointTest(float x, float y, float z)
        {
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
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
            POINTFLOAT p2 = new POINTFLOAT { x = p1.x, y = p4.y};
            POINTFLOAT p3 = new POINTFLOAT { x = p4.x, y = p1.y};
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

        private void DrawCube(Point3 D1, Point3 D2)
        {
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.PushMatrix();
            gl.Translate(0, 0, 0);
            D2.x = Math.Abs(D2.x - D1.x);
            D2.y = Math.Abs(D2.y - D1.y);
            D2.z = Math.Abs(D2.z - D1.z);
            POINTFLOAT p1 = new POINTFLOAT { x = 0, y = 0 };
            POINTFLOAT p2 = new POINTFLOAT { x = D2.x, y = D2.y };
            float transalteValY = (float)Math.Sqrt(0 + D2.y * D2.y);
            //for (int i = 0; i < 4; i++)
            //{
            //    DrawRect(p1, p2);
            //    gl.Rotate(90f, 1.0f, 0.0f, 0.0f);
            //    //gl.Translate(0.0f, transalteValY, 0.0f);
            //}
            DrawRect(p1, p2);
            //gl.Rotate(90f, 1.0f, 0.0f, 0.0f);
            //p1 = new POINTFLOAT { x = 0, y = 0 };
            //p2 = new POINTFLOAT { x = D2.x, y = D2.z };
            //DrawRect(p1, p2);
            //gl.Translate(0.0f, (float)Math.Sqrt(0 + D2.z * D2.z), 0.0f);
            //gl.Rotate(-90f, 1.0f, 0.0f, 0.0f);
            //p1 = new POINTFLOAT { x = 0, y = 0 };
            //p2 = new POINTFLOAT { x = D2.x, y = D2.y };
            //DrawRect(p1, p2);
            //gl.Translate(0.0f, (float)Math.Sqrt(0 + D2.y * D2.y), 0.0f);
            //gl.Rotate(-90f, 1.0f, 0.0f, 0.0f);
            //p1 = new POINTFLOAT { x = 0, y = 0 };
            //p2 = new POINTFLOAT { x = D2.x, y = D2.z };
            //DrawRect(p1, p2);
            //gl.Translate(0.0f, -(float)Math.Sqrt(0 + D2.z * D2.z), 0.0f);
            //gl.Rotate(-90f, 1.0f, 0.0f, 0.0f);
            //p1 = new POINTFLOAT { x = 0, y = 0 };
            //DrawCircle(p1, 2, 40);
            gl.PopMatrix();

        }

        float rotation = 0;

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
            args.OpenGL.PointSize(3.0f);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            rotation += 15.0f;
        }
    }
}
