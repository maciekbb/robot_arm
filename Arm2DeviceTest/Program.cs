using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arm2;

namespace Arm2DeviceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Arm2Device device = new Arm2Device();
            if(device.Open() == true)
            {
                Console.WriteLine("Device found");
                device.LedOn();
                System.Threading.Thread.Sleep(1000);
                device.LedOff();
                System.Threading.Thread.Sleep(1000);
                device.LedOn();
                System.Threading.Thread.Sleep(1000);
                device.LedOff();
                device.Close();
            }
            else
            {
                Console.WriteLine("Device not found");
            }
            Console.ReadLine();
        }
    }
}
