/* -- this is a FILEHEADER COMMENT --
	NAME	:	Phone
	PURPOSE :	This is a child class of Device and Inherits properties
                methods and overrides and oveloads a method from Device.
                Phone also creates a couple unique properties and methods to
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
    class Phone : Device
    {
        private string faceID;
        private string mobileCarrier;

        public Phone() : base()
        {
            faceID = "White-Female";
            mobileCarrier = "Rogers";
        }

        //Overloaded Method
        public bool unlockDevice(string yourFace, string password)
        {
            bool retCode = false;

            if (yourFace == faceID)
            {
                //Face Match
                retCode = true;
                return retCode;
            }
            else
            {
                retCode = false;
            }

            if (PassAnswer == password)
            {
                //Password correct
                retCode = true;
            }
            else
            {
                //Password incorrect
                retCode = false;
            }

            return retCode;
        }

        public string mobileData()
        {
            int mobileDataGB = 0;

            mobileDataGB = mobileCarrier.Length;

            mobileDataGB = mobileDataGB * 8;

            return "Your mobile data plan gives you" + mobileDataGB + "GB of data a month!\n";
        }


        public override string showClassProperties()
        {
            return base.showClassProperties() + "You're face ID is tracking for a " + faceID + " and your mobile carrier is " + mobileCarrier + ". \n";
        }

        public string FaceID
        {
            get { return faceID; }
            set { faceID = value; }
        }

        public string MobileCarrier
        {
            get { return mobileCarrier; }
            set { mobileCarrier = value; }
        }
    }
}
