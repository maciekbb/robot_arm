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

namespace arm2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double TransitionX = 0;
        private double TransitionY = 0;
        private double BetweenAngle = 0;
        private double FirstChange = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Base_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            moveBase(e.NewValue);
        }

        private void FirstArm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MoveFirstArm(e.NewValue);
        }

        private void SecondArm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MoveSecondArm(e.NewValue);
        }

        private void moveBase(double e)
        {
            Grid canvas = armTopViewPolyline.Parent as Grid;
            RotateTransform rotate = new RotateTransform((e - 0.5) * 90);
            rotate.CenterX = canvas.Width / 2;
            rotate.CenterY = canvas.Height / 2;
            armTopViewPolyline.RenderTransform = rotate;
        }
        
        void MoveFirstArm(double e) {
            Grid canvas = armSideViewPolygon1.Parent as Grid;
            RotateTransform rotate = new RotateTransform(-(e) * 135);
            FirstChange = e;
            rotate.CenterX = canvas.Width / 2;
            rotate.CenterY = canvas.Height / 2 + 20;
            armSideViewPolygon1.RenderTransform = rotate;

            TransformGroup allTransforms = new TransformGroup();
            TransitionX = 60*Math.Cos(-(e)*2.36)-60;
            TransitionY = 60*Math.Sin(-(e)*2.36);
           
            RotateTransform rotate2 = new RotateTransform(-(e-BetweenAngle) * 135);
            rotate2.CenterX = canvas.Width / 2 + 50 + TransitionX;
            rotate2.CenterY = canvas.Height / 2 + 20 + TransitionY;
            allTransforms.Children.Add(new TranslateTransform(TransitionX, TransitionY));
            allTransforms.Children.Add(rotate2);

            armSideViewPolygon2.RenderTransform = allTransforms;
        }

        void MoveSecondArm(double e)
        {
              TransformGroup allTransforms = new TransformGroup();
              Grid canvas = armSideViewPolygon2.Parent as Grid;
              RotateTransform rotate = new RotateTransform((e - FirstChange) * 135);
              BetweenAngle = e;
              rotate.CenterX = canvas.Width / 2 + 50 + TransitionX;
              rotate.CenterY = canvas.Height / 2 + 20 + TransitionY;
              allTransforms.Children.Add(new TranslateTransform(TransitionX, TransitionY));
              allTransforms.Children.Add(rotate);

              armSideViewPolygon2.RenderTransform = allTransforms;
          }
    }

}
