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
using Arm2;

namespace arm2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double transitionX = 0;
        private double transitionY = 0;
        private double betweenArmsAngle = 0;
        private double baseAngle = 0;
        private Arm2Device device = new Arm2Device();
        private DeviceConnection deviceConnection;
        private double positionBase = 0.5;
        private double positionFirst = 0;
        private double positionSecond = 0;

        public MainWindow()
        {
            InitializeComponent();
            device.Open();
            deviceConnection = new DeviceConnection(device, button_move, label_status);
            deviceConnection.StartThread();
        }

        private void Base_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            moveBase(e.NewValue);
            positionBase = e.NewValue;
        }

        private void FirstArm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MoveFirstArm(e.NewValue);
            positionFirst = e.NewValue;
        }

        private void SecondArm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MoveSecondArm(e.NewValue);
            positionSecond = e.NewValue;
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
            baseAngle = e;
            rotate.CenterX = canvas.Width / 2;
            rotate.CenterY = canvas.Height / 2 + 20;
            armSideViewPolygon1.RenderTransform = rotate;

            TransformGroup allTransforms = new TransformGroup();
            transitionX = 60*Math.Cos(-(e)*2.36)-60;
            transitionY = 60*Math.Sin(-(e)*2.36);
           
            RotateTransform rotate2 = new RotateTransform(-(e-betweenArmsAngle) * 135);
            rotate2.CenterX = canvas.Width / 2 + 50 + transitionX;
            rotate2.CenterY = canvas.Height / 2 + 20 + transitionY;
            allTransforms.Children.Add(new TranslateTransform(transitionX, transitionY));
            allTransforms.Children.Add(rotate2);

            armSideViewPolygon2.RenderTransform = allTransforms;
        }

        void MoveSecondArm(double e)
        {
              TransformGroup allTransforms = new TransformGroup();
              Grid canvas = armSideViewPolygon2.Parent as Grid;
              RotateTransform rotate = new RotateTransform((e - baseAngle) * 135);
              betweenArmsAngle = e;
              rotate.CenterX = canvas.Width / 2 + 50 + transitionX;
              rotate.CenterY = canvas.Height / 2 + 20 + transitionY;
              allTransforms.Children.Add(new TranslateTransform(transitionX, transitionY));
              allTransforms.Children.Add(rotate);

              armSideViewPolygon2.RenderTransform = allTransforms;
          }

        private void moveDevice()
        {
            if (device.IsOpen())
            {
                device.MoveServo(1, positionBase);
                device.MoveServo(2, positionFirst);
                device.MoveServo(3, positionFirst);
                device.MoveServo(4, positionSecond);
            }
        }

        private void ButtonMove_Click(object sender, RoutedEventArgs e)
        {
            moveDevice();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            deviceConnection.StopThread();
        }
    }
}
