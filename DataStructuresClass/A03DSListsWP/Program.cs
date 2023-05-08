/*
 * Filename: A03DSListWP
 * By: Justin Fink
 * Date: Firday 08 October 2021
 * Description: This program instantiates 2 lists one with <string>
 *              and the other with <int> to see the differences in time (using the stopwatch from System.Diagnostics)
 *              when doing different functions
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace A03DataStructuresWP
{
    class Program
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        private static Stopwatch sw = new Stopwatch();
        private static List<string> list = new List<string>();  //String List data structure 
        private static List<int> intList = new List<int>();     //Integer List data structure

        static void Main(string[] args)
        {
            Console.WriteLine("STRING LIST\n\n");
            int addInputLoop = 0;           //Counter used to run through the input for statement
            int totalInputs = 25000000;     //Total amount of items you want in the specific data structures
            int length = 25;                 //Length of string/number to use to store in the data structures

            //Variables used for searching in the list
            string firstItem = "";
            string halfwayItem = "";
            string finalItem = "";

            sw.Start();
            for (addInputLoop = 0; addInputLoop <= totalInputs; ++addInputLoop) //Inserting into the list
            {
                string randomString = stringBuilder(length); //Make random String

                if(addInputLoop == 0) // Get First Item
                {
                    firstItem = randomString;
                }
                else if(addInputLoop == (totalInputs/2)) //Get Halfway Item
                {
                    halfwayItem = randomString;
                }
                else if(addInputLoop == totalInputs) // Get Final Item
                {
                    finalItem = randomString;
                }
                list.Add(randomString);
            }
            sw.Stop();

            TimeSpan ts = sw.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("The total amount of inputs into the list data structure is: " + totalInputs + "\nRunTime " + elapsedTime);
            
            // Time to find First Item
            sw.Restart();
            sw.Start();
            list.IndexOf(firstItem);
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the first item put into the list\n");
            
            //Time to find halfway item
            sw.Restart();
            sw.Start();
            list.IndexOf(halfwayItem);
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the halfway item put into the list\n");
           
            //Time to find final item
            sw.Restart();
            sw.Start();
            list.IndexOf(finalItem);
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the final item put into the list\n\n\n");


            Console.WriteLine("INTEGER LIST\n");
            //Integer List
            int min = 1;
            int max = 2147483647;

            int firstInt = 0;
            int halfwayInt = 0;
            int finalInt = 0;

            System.Random random = new System.Random();

            sw.Restart();
            sw.Start();
            for (addInputLoop = 0; addInputLoop <= totalInputs; ++addInputLoop) //Inserting into the list
            {
                int randomNum = random.Next(min,max);//Make random Int

                if (addInputLoop == 0) // Get First Item
                {
                     firstInt = randomNum;
                }
                else if (addInputLoop == (totalInputs / 2)) //Get Halfway Item
                {
                     halfwayInt = randomNum;
                }
                else if (addInputLoop == totalInputs) // Get Final Item
                {
                     finalInt = randomNum;
                }
                intList.Add(randomNum);
            }
            sw.Stop();

            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("The total amount of inputs into the list data structure is: " + totalInputs + "\nRunTime " + elapsedTime);

            // Time to find First Item
            sw.Restart();
            sw.Start();
            intList.IndexOf(firstInt);
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00000}", ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the first item put into the list\n");

            //Time to find halfway item
            sw.Restart();
            sw.Start();
            intList.IndexOf(halfwayInt);
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00000}", ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the halfway item put into the list\n");

            //Time to find final item
            sw.Restart();
            sw.Start();
            intList.IndexOf(finalInt);
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00000}", ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the final item put into the list\n");

            Console.ReadKey();
        }


        /*
         * The randomizer part of this function (lines inside the for loop) was found online 
         * at https://stackoverflow.com/questions/1122483/random-string-generator-returning-same-string
         * mainly I couldn't figure out why the function was returning the same string for like 10 inputs at a time
         * It was because the loop was running this function so fast that it gave the same value.
         * 
         * Function: stringBuilder()
         * Description: This function creates a random string of a certain length which the user can input
         * Parameters: This function takes only 1 parameter which is the length of the string you want to produce
         * Return Values: This function returns the random string
         */
        public static string stringBuilder(int lengthOfString)
        {
            int length = lengthOfString;
            StringBuilder str_build = new StringBuilder();

            char ch;

            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                str_build.Append(ch);
            }
            return str_build.ToString();
        }
    }
}











