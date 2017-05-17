using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using SharpGL.SceneGraph.Core;

namespace lab_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Gemotery> shapesList = new List<Gemotery>();
        bool drawFrame = false;
        public MainWindow()
        {
            InitializeComponent();
            ClrPcker.SelectedColor = new Color() { R = 255, G = 0, B = 0, A = 255};
        }
        static public OpenGL gl;

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -10.0f);

            foreach (var item in shapesList)
            {
                item.Draw();
            }
            gl.PopMatrix();

        }
        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            gl = args.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Enable(OpenGL.GL_POINT_SMOOTH);
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            args.OpenGL.PointSize(6.0f);
            args.OpenGL.LineWidth(1.0f);
        }
        private void btnFrame_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in shapesList)
            {
                item.drawFrame = true;
            }
            drawFrame = true;
        }
        private void btnFrame_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in shapesList)
            {
                item.drawFrame = false;
            }
            drawFrame = false;
        }

        private void TranslateCheckX(object sender, RoutedEventArgs e)
        {
            btnTranslateY.IsChecked = false;
            btnTranslateZ.IsChecked = false;
            if (listBox.SelectedIndex != -1)
            {
                sliderTranslate.Value = shapesList[listBox.SelectedIndex].translateX;
            }
            
        }
        private void TranslateCheckY(object sender, RoutedEventArgs e)
        {
            btnTranslateX.IsChecked = false;
            btnTranslateZ.IsChecked = false;
            if (listBox.SelectedIndex != -1)
            {
                sliderTranslate.Value = shapesList[listBox.SelectedIndex].translateY;
            }
        }
        private void TranslateCheckZ(object sender, RoutedEventArgs e)
        {
            btnTranslateX.IsChecked = false;
            btnTranslateY.IsChecked = false;
            if (listBox.SelectedIndex != -1)
            {
                sliderTranslate.Value = shapesList[listBox.SelectedIndex].translateZ;
            }
        }
        private void RotateCheckX(object sender, RoutedEventArgs e)
        {
            btnRotateY.IsChecked = false;
            btnRotateZ.IsChecked = false;
            if (listBox.SelectedIndex != -1)
            {
                sliderRotate.Value = shapesList[listBox.SelectedIndex].rotateX;
            }
        }
        private void RotateCheckY(object sender, RoutedEventArgs e)
        {
            btnRotateX.IsChecked = false;
            btnRotateZ.IsChecked = false;
            if (listBox.SelectedIndex != -1)
            {
                sliderRotate.Value = shapesList[listBox.SelectedIndex].rotateY;
            }
        }
        private void RotateCheckZ(object sender, RoutedEventArgs e)
        {
            btnRotateX.IsChecked = false;
            btnRotateY.IsChecked = false;
            if (listBox.SelectedIndex != -1)
            {
                sliderRotate.Value = shapesList[listBox.SelectedIndex].rotateZ;
            }
        }
        private void ScaleCheckX(object sender, RoutedEventArgs e)
        {
            btnScaleY.IsChecked = false;
            btnScaleZ.IsChecked = false;
            if (listBox.SelectedIndex != -1)
            {
                sliderScale.Value = shapesList[listBox.SelectedIndex].scaleX;
            }
        }
        private void ScaleCheckY(object sender, RoutedEventArgs e)
        {
            btnScaleX.IsChecked = false;
            btnScaleZ.IsChecked = false;
            if (listBox.SelectedIndex != -1)
            {
                sliderScale.Value = shapesList[listBox.SelectedIndex].scaleY;
            }
        }
        private void ScaleCheckZ(object sender, RoutedEventArgs e)
        {
            btnScaleX.IsChecked = false;
            btnScaleY.IsChecked = false;
            if (listBox.SelectedIndex != -1)
            {
                sliderScale.Value = shapesList[listBox.SelectedIndex].scaleZ;
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (selectBox.SelectedIndex)
            {
                case 0:
                    Sphere sphere = new Sphere();
                    sphere.color = ClrPcker.SelectedColor.Value;
                    sphere.drawFrame = drawFrame;
                    shapesList.Add(sphere);
                    listBox.Items.Add("sphere");
                    break;
                case 1:
                    Cone cone = new Cone();
                    cone.color = ClrPcker.SelectedColor.Value;
                    cone.drawFrame = drawFrame;
                    shapesList.Add(cone);
                    listBox.Items.Add("cone");
                    break;
                case 2:
                    Cylinder cylinder = new Cylinder();
                    cylinder.color = ClrPcker.SelectedColor.Value;
                    cylinder.drawFrame = drawFrame;
                    shapesList.Add(cylinder);
                    listBox.Items.Add("cylinder");
                    break;
            }
            if (listBox.SelectedIndex != -1)
            {
                UpdateSliders();
            }
        }
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (listBox.SelectedIndex != -1)
            {
                shapesList[listBox.SelectedIndex].color = e.NewValue.Value;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                shapesList.RemoveAt(listBox.SelectedIndex);
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                ClrPcker.SelectedColor = shapesList[listBox.SelectedIndex].color;
                UpdateSliders();
            }
        }

        private void sliderTranslate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (listBox.SelectedIndex != -1)
            {
                if (btnTranslateX.IsChecked.Value)
                {
                    shapesList[listBox.SelectedIndex].translateX = (float)e.NewValue;
                }
                if (btnTranslateY.IsChecked.Value)
                {
                    shapesList[listBox.SelectedIndex].translateY = (float)e.NewValue;
                }
                if (btnTranslateZ.IsChecked.Value)
                {
                    shapesList[listBox.SelectedIndex].translateZ = (float)e.NewValue;
                }
            }
        }
        private void sliderRotate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (listBox.SelectedIndex != -1)
            {
                if (btnRotateX.IsChecked.Value)
                {
                    shapesList[listBox.SelectedIndex].rotateX = (float)e.NewValue;
                }
                if (btnRotateY.IsChecked.Value)
                {
                    shapesList[listBox.SelectedIndex].rotateY = (float)e.NewValue;
                }
                if (btnRotateZ.IsChecked.Value)
                {
                    shapesList[listBox.SelectedIndex].rotateZ = (float)e.NewValue;
                }
            }
        }
        private void sliderScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (listBox.SelectedIndex != -1)
            {
                if (btnScaleX.IsChecked.Value)
                {
                    shapesList[listBox.SelectedIndex].scaleX = (float)e.NewValue;
                }
                if (btnScaleY.IsChecked.Value)
                {
                    shapesList[listBox.SelectedIndex].scaleY = (float)e.NewValue;
                }
                if (btnScaleZ.IsChecked.Value)
                {
                    shapesList[listBox.SelectedIndex].scaleZ = (float)e.NewValue;
                }
            }
        }
        private void sliderTranstelation_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (listBox.SelectedIndex != -1)
            {
                shapesList[listBox.SelectedIndex].transtelation = (int)e.NewValue;
            }
        }
        private void UpdateSliders()
        {
            if (listBox.SelectedIndex != -1)
            {
                if (btnScaleX.IsChecked.Value)
                {
                    sliderScale.Value = shapesList[listBox.SelectedIndex].scaleX;
                }
                if (btnScaleY.IsChecked.Value)
                {
                    sliderScale.Value = shapesList[listBox.SelectedIndex].scaleY;
                }
                if (btnScaleZ.IsChecked.Value)
                {
                    sliderScale.Value = shapesList[listBox.SelectedIndex].scaleZ;
                }
                if (btnTranslateX.IsChecked.Value)
                {
                    sliderTranslate.Value =  shapesList[listBox.SelectedIndex].translateX;
                }
                if (btnTranslateY.IsChecked.Value)
                {
                    sliderTranslate.Value = shapesList[listBox.SelectedIndex].translateY;
                }
                if (btnTranslateZ.IsChecked.Value)
                {
                    sliderTranslate.Value = shapesList[listBox.SelectedIndex].translateZ;
                }
                if (btnRotateX.IsChecked.Value)
                {
                    sliderRotate.Value = shapesList[listBox.SelectedIndex].rotateX;
                }
                if (btnRotateY.IsChecked.Value)
                {
                    sliderRotate.Value = shapesList[listBox.SelectedIndex].rotateY;
                }
                if (btnRotateZ.IsChecked.Value)
                {
                    sliderRotate.Value = shapesList[listBox.SelectedIndex].rotateZ;
                }
                sliderTranstelation.Value = shapesList[listBox.SelectedIndex].transtelation;
            }
        }

    }
}
