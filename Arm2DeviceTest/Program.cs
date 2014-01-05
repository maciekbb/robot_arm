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
                try
                {
                    while (true)
                    {
                        String sval = Console.ReadLine();
                        double val = Convert.ToDouble(sval);
                        device.MoveServo(1, val);

                    }
                }
                catch (Exception ex)
                {
                } 
                device.Close();
            }
            else
            {
                Console.WriteLine("Device not found");
            }
        }
    }
}
