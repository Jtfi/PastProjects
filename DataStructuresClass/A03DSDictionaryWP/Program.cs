/*
 * Filename: A03DSDictionaryWP
 * By: Justin Fink
 * Date: Firday 08 October 2021
 * Description: This program instantiates 2 dictionaries one with <string, string>
 *              and the other with <int, int> to see the differences in time (using the stopwatch from System.Diagnostics)
 *              when doing different functions
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace A03DSDictionaryWP
{
    class Program
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        private static Stopwatch sw = new Stopwatch();
        private static Dictionary<string, string> stringDict = new Dictionary<string, string>();
        private static Dictionary<int, int> intDict = new Dictionary<int, int>();

        static void Main(string[] args)
        {

            Console.WriteLine("STRING Dictionary\n\n");
          
            int addInputLoop = 0;           //Counter used to run through the input for statement
            int totalInputs = 1000000;     //Total amount of items you want in the specific data structures
            int length = 25;                //Length of string/number to use to store in the data structures

            //Variables used for searching in the list
            string firstItem = "";
            string halfwayItem = "";
            string finalItem = "";

            //String and String Dictionary
            sw.Start();
            for (addInputLoop = 0; addInputLoop <= totalInputs; addInputLoop++) //Inserting into the list
            {
                string randomStringKey = stringBuilder(length); //Make random String


                string randomString = stringBuilder(length);

                if (addInputLoop == 0) // Get First Item
                {
                    firstItem = randomStringKey;
                }
                else if (addInputLoop == (totalInputs / 2)) //Get Halfway Item
                {
                    halfwayItem = randomStringKey;
                }
                else if (addInputLoop == totalInputs) // Get Final Item
                {
                    finalItem = randomStringKey;
                }

                

                stringDict.Add(randomStringKey, randomString);
            }
            sw.Stop();

            TimeSpan ts = sw.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("The total amount of inputs into the dictionary data structure is: " + totalInputs + "\nRunTime " + elapsedTime);
            bool keyIsInDict = false;
            // Time to find First Item
            sw.Restart();
            sw.Start();
            keyIsInDict = stringDict.ContainsKey(firstItem); //It says most of the time it takes 3
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00000}", ts.Minutes, ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the first item put into the Dictionary\n");

            // Time to find Halfway Item
            sw.Restart();
            sw.Start();
            keyIsInDict = stringDict.ContainsKey(halfwayItem); //It says most of the time it takes 3 ticks to figure out that 
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00000}", ts.Minutes, ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the halfway item put into the Dictionary\n");
            // Time to find Final Item
            sw.Restart();
            sw.Start();
            keyIsInDict = stringDict.ContainsKey(finalItem);
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00000}", ts.Minutes, ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the final item put into the Dictionary\n");

            Console.WriteLine("INTEGER DICTIONARY\n");
            //Integer Dictionary
            int min = 1;
            int max = 2147483647;

            int firstInt = 0;
            int halfwayInt = 0;
            int finalInt = 0;

            System.Random random = new System.Random();

            int keyNum = 0;//Key value
            sw.Restart();
            sw.Start();
            for (addInputLoop = 0; addInputLoop <= totalInputs; ++addInputLoop) //Inserting into the list
            {
               
                int randomNum = random.Next(min, max);
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
                intDict.Add(keyNum, randomNum);
                keyNum++; 
            }
            sw.Stop();
            ts = sw.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:000}:{3:00000}", ts.Minutes, ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("The total amount of inputs into the dictionary data structure is: " + totalInputs + "\nRunTime " + elapsedTime);

            // Time to find Halfway Item
            sw.Restart();
            sw.Start();
            keyIsInDict = intDict.ContainsKey(firstInt); //It says most of the time it takes 3 ticks to figure out that 
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00000}", ts.Minutes, ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the first item put into the Dictionary\n");

            // Time to find Halfway Item
            sw.Restart();
            sw.Start();
            keyIsInDict = intDict.ContainsKey(halfwayInt); //It says most of the time it takes 3 ticks to figure out that 
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00000}", ts.Minutes, ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the halfway item put into the Dictionary\n");

            // Time to find Final Item
            sw.Restart();
            sw.Start();
            keyIsInDict = intDict.ContainsKey(finalInt);
            sw.Stop();
            ts = sw.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00000}", ts.Minutes, ts.Seconds, ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Took: " + elapsedTime + ". To find the final item put into the Dictionary\n");

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

            // creating a StringBuilder object()
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
