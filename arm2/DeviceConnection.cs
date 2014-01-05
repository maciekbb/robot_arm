using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Arm2;
using System.Windows.Media;
using System.Threading;

namespace arm2
{  
    public class DeviceConnection
    {
        private Label _label;
        private Button _button;
        private Arm2Device _device;
        private bool run = false;
        private Thread connectionThread;
        private Object syncObject = new Object();
        private Boolean lastState;

       public DeviceConnection(Arm2Device device, Button button, Label label)
        {
            _label = label;
            _button = button;
            _device = device;
            lastState = false;
        }

        public void StartThread()
        {
            run = true;
            connectionThread = new Thread(new ThreadStart(worker));
            connectionThread.Start();
        }

        public void StopThread()
        {
            lock(syncObject)
            {
                run = false;
            }
        }

        void worker()
        {
            while (true)
            {
                lock (syncObject)
                {
                    if (!run) break;
                }

                
                if (lastState)
                {
                    //test connection
                    if (!_device.Ping())
                    {
                        _device.Close();
                        lastState = false;
                        statusDisonnected();
                    }
                }
                else
                {
                    //try to connect
                    if (_device.Open())
                    {
                        lastState = true;
                        statusConnected();
                    }
                }


                Thread.Sleep(250);
            }
        }

        private void statusConnected()
        {
            _label.Dispatcher.Invoke((Action)delegate
            {
                _button.IsEnabled = true;
                _label.Content = "Device connected";
                _label.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            });
        }

        private void statusDisonnected()
        {
            _label.Dispatcher.Invoke((Action)delegate
            {
                _button.IsEnabled = false;
                _label.Content = "Device disconnected";
                _label.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            });
        }
    }
}
