/* -- this is a FILEHEADER COMMENT --
	NAME	:	Laptop
	PURPOSE :	This is a child class of Device and Inherits properties
                methods and overrides and oveloads a method from Device.
                Laptop also creates a couple unique properties and methods to
                demonstrate an understanding of C# using OOP practices.
    PARENT :    Device

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpandOOPAssignment01
{
    class Laptop : Device
    {
        private int defaultMic;
        private int newMic;


        public Laptop() : base()
        {
            newMic = 100;
            defaultMic = 100;
        }

        //Overloaded Method
        public bool unlockDevice(string password)
        {
            bool retCode = false;

            if (password == PassAnswer)
            {
                retCode = true;
            }
            else
            {
                retCode = false;
            }

            return retCode;
        }

        //Overriding Method
        public override string showClassProperties()
        {
            return base.showClassProperties() + "The default mics quality is set to " + defaultMic.ToString() + "% and your new mics quality is set to " + newMic.ToString() + "%.\n";
        }


        public int micDifference()
        {
            int micDifference = 0;

            micDifference = newMic - defaultMic;

            return micDifference;
        }

        //Properties
        public int NewMic
        {
            get { return newMic; }
            set { newMic = value; }
        }

        public int DefaultMic
        {
            get { return defaultMic; }
            set { defaultMic = value; }
        }
    }
}
