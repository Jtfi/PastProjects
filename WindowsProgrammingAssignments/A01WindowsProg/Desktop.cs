/* -- this is a FILEHEADER COMMENT --
	NAME	:	Desktop
	PURPOSE :	This is a child class of Device and Inherits properties
                methods and overrides and oveloads a method from Device.
                Desktop also creates a couple unique properties and methods to
                demonstrate an understanding of C# using OOP practices.
    PARENT  :   Device

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpandOOPAssignment01
{
    class Desktop : Device
    {
        //The speed represents how much percent faster the GPU is compared to the CPU to specific renders
        private double graphicsCardSpeed;


        public Desktop()
        {
            graphicsCardSpeed = 0.245; //24.5% faster at rendering
        }

        //Overloaded Method
        public bool unlockDevice(int password)
        {
            bool retCode = false;

            if (password.ToString() == PassAnswer)
            {
                retCode = true;
            }
            else
            {
                retCode = false;
            }

            return retCode;
        }

        public override string showClassProperties()
        {
            return base.showClassProperties() + "The graphics card speed is " + ((1 - graphicsCardSpeed) * 100).ToString() + "% faster\n";
        }


        public double videoRenderTimeDifference(double cpuRenderTimeInMin)
        {
            double minutes = 0;

            minutes = cpuRenderTimeInMin * graphicsCardSpeed;

            return minutes;
        }



        public double GraphicsCardSpeed
        {
            get { return graphicsCardSpeed; }
            set { graphicsCardSpeed = value; }
        }
    }
}
