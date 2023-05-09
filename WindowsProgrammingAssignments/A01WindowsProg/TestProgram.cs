/*
 * Assignment: A01 C# and OOP 
 * Filename: TestProgram.cs
 * By: Justin Fink
 * Date: September 17, 2021
 * Description: This assignment 
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpandOOPAssignment01
{
    class TestProgram
    {
        static void Main()
        {
            //====================
            //---Phone Features---
            //====================
            Console.WriteLine("Phone Features:\n\n");
            Phone pPhone = new Phone();

            //Accessing the unique property
            pPhone.FaceID = "White-Male";

            //Overloaded method calls
            string pIntro = "The phone is unlocked: ";
            Console.WriteLine(pIntro + pPhone.unlockDevice("White-Male", "123").ToString());
            Console.WriteLine(pIntro + pPhone.unlockDevice("White-Female", "12345").ToString());
            Console.WriteLine(pIntro + pPhone.unlockDevice("White-Female", "12345678").ToString() + "\n");

            //Accessing the parent properties
            pPhone.Volume = 100;
            pPhone.PowerStatus = false;

            //Unique method call
            Console.WriteLine(pPhone.mobileData());

            //Overloaded and parent method call
            string internet = "This devices internet connectivity is: ";
            Console.WriteLine(internet + pPhone.internetConnectivity().ToString() + "%.\n");
            Console.WriteLine(pPhone.showClassProperties());



            //=====================
            //---Laptop Features---
            //=====================
            Console.WriteLine("\n\nLaptop Features:\n\n");
            Laptop pLaptop = new Laptop();

            pLaptop.NewMic = 120;

            //Overloaded method calls
            string lIntro = "The laptop is unlocked: ";
            Console.WriteLine(lIntro + pLaptop.unlockDevice("12345").ToString());
            Console.WriteLine(lIntro + pLaptop.unlockDevice("123456789").ToString() + "\n");

            //Accessing the parent properties
            pLaptop.PassAnswer = "Hello";
            pLaptop.Volume = 59;
            pLaptop.PowerStatus = false;

            //Unique method call
            Console.WriteLine("The mic difference is: " + pLaptop.micDifference() + "%.\n");

            //Overloaded and parent method call
            Console.WriteLine(internet + pLaptop.internetConnectivity().ToString() + "%.\n");
            Console.WriteLine(pLaptop.showClassProperties());



            //======================
            //---Desktop Features---
            //======================
            Console.WriteLine("\n\nDesktop Features:\n\n");
            Desktop pDesktop = new Desktop();

            pDesktop.GraphicsCardSpeed = 0.20;

            //Overloaded method calls
            string dIntro = "The desktop is unlocked: ";
            Console.WriteLine(dIntro + pDesktop.unlockDevice(12345).ToString());
            Console.WriteLine(dIntro + pDesktop.unlockDevice(123456723).ToString() + "\n");

            //Accessing the parent properties
            pDesktop.PassAnswer = "13890";
            pDesktop.Volume = 80;
            pDesktop.PowerStatus = false;

            //Unique method call
            double renderTimeCPU = 30;
            Console.WriteLine("The graphics render speed is now: " + pDesktop.videoRenderTimeDifference(renderTimeCPU).ToString() + " minutes instead of " + renderTimeCPU.ToString() + ".\n");

            //Overloaded and parent method call
            Console.WriteLine(internet + pDesktop.internetConnectivity().ToString() + "%.\n");
            Console.WriteLine(pDesktop.showClassProperties());
        }
    }
}
