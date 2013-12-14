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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Base_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //currentStatus.Content = e.NewValue.ToString();
            RotateTransform rotate = new RotateTransform((e.NewValue) * 360);
            rotate.CenterX = 150;
            rotate.CenterY = 170;
            armTopViewPolyline.RenderTransform = rotate;
        }

        private void FirstArm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void SecondArm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
