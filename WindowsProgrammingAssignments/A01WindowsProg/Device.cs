/* -- this is a FILEHEADER COMMENT --
	NAME	:	Device
	PURPOSE :	This class is the parent of Phone, Laptop and Desktop,
                it provides the foundation of all those classes with 
                its properties and methods that will be used in them.
    PARENT :    None

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpandOOPAssignment01
{
    class Device
    {
        //Private Data members
        private bool powerStatus;
        private string passAnswer;
        private int volume;

        //Public Data members

        //Public Methods
        public Device()
        {
            powerStatus = false;
            volume = 0;
            passAnswer = "12345";
        }


        public void powerToggle()
        {
            if (PowerStatus == true)
            {
                PowerStatus = false;
            }
            else
            {
                PowerStatus = true;
            }
        }

        //This was going to be another overrided function if the other one wasn't allowed
        //can refactor to make it so all the classes dont show the same connectivity since 
        //random is weird
        public int internetConnectivity()
        {
            int connectivity = 0;

            Random random = new Random();

            connectivity = random.Next(101);

            return connectivity;
        }

        //Method that is going to be Overloaded in the child classes
        public bool unlockDevice()
        {
            if (PowerStatus == true)
            {
                //Device is always unlocked if the power is on
                return true;
            }

            return false;
        }

        //Method that is going to be Overrided in the child methods
        public virtual string showClassProperties()
        {
            string on;

            if (powerStatus == true)
            {
                on = "on";
            }
            else
            {
                on = "off";
            }

            return "The device is powered " + on + ", the volume is " + volume.ToString() + " and the password is " + passAnswer + ".\n";
        }




        //Properties
        public bool PowerStatus
        {
            get { return powerStatus; }
            set { powerStatus = value; }
        }

        public int Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        public string PassAnswer
        {
            get { return passAnswer; }
            set { passAnswer = value; }
        }
    }
}
