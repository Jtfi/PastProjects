/*
 * Filename: A04ThreadsWP
 * By: Justin Fink
 * Date: Firday 22 October 2021
 * Description: This program uses the command line to get 2 arguments from it, the full path to a text file
 *              and the size you want the text file to be, ten it opens up the text file and depending on whether or not
 *              the input is valid it will then create 50 threads to write random 50 character strings to it, then
 *              every second there will be 1 thread comparing the size of the file to the size the user put in the 
 *              command line and then if the file is over the user inputted size it will then stop.
 * 
 */

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;


namespace A04ThreadsWP
{
    class Program
    {
        static volatile bool done = true;
        
        private static readonly Random random = new Random((int)DateTime.Now.Ticks);
        private static Timer timer = null;

        private static Mutex mut = new Mutex();


        static void Main(string[] args)
        {
            Thread[] threads = new Thread[50];
            TimerCallback timercb = new TimerCallback(UpdateStatus);
            timer = new Timer(UpdateStatus, null, 0, 1000); //1 second

            string[] commandLine = Environment.GetCommandLineArgs(); //Here are the command line arguments

            bool validSize;
            string fileName;    //File name path
            int size;           //Size of the file



            //Get file
            fileName = commandLine[1];


            //Get size of file
            size = int.Parse(commandLine[2]);
            validSize = ValidateSize(size);


            if ((validSize == false) || (commandLine[1] == "/?"))
            {
                // Error/Usage 
                Console.WriteLine("USAGE: /n");
                Console.WriteLine("Command Line Argument 1: Must be the ABSOLUTE file path to the file /n");
                Console.WriteLine("Command Line Argument 2: Must be the file size you want in between 1,000 to 20,00,000/n");

            }
            else
            {

                //Using threads put 50 character strings to the file size using threads
                ThreadStart tStart = new ThreadStart(Task);
                //make 50 threads

                for (int i = 0; i < 50; i++)
                {
                    Thread t = new Thread(tStart);
                    t.Name = i.ToString();
                    threads[i] = t;
                }

                for (int i = 0; i < 50; i++)
                {
                    threads[i].Start();
                }

                for (int i = 0; i < 50; i++)
                {
                    threads[i].Join();
                }

            }


            Thread.Sleep(1500);      // Give the timer a chance to update the screen one last time.
            timer.Dispose();        // Get rid of the timer.

            Console.Write("\nPress Any Key to Continue...");
            Console.ReadKey();
        }




        /*
         * Function: Task()
         * Description: This is the worker threads job function
         * Parameters: None
         * Return Values: Nothing
         */
        static void Task()
        {

            string[] commandLine = Environment.GetCommandLineArgs(); //Here are the command line arguments

            string fileName;    //File name path
            //Get file
            fileName = commandLine[1];

            //If file does not exist create it



            //If volitile bool == false exit while loop
            while (done)
            {
                //set file to the file path given in the command line
                FileStream file = null;
                mut.WaitOne();
                try
                {
                    file = File.Open(fileName, FileMode.Append, FileAccess.Write);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Error reading from {0}. Message = {1}", fileName, e.Message);

                }
                finally
                {
                    string characters = StringBuilder(50);
                    byte[] bytes = Encoding.UTF8.GetBytes(characters);
                    file.Write(bytes, 0, bytes.Length);
                    //Close the file
                    file.Close();
                }
                mut.ReleaseMutex();
            }

        }

        /*
         * Function: UpdateStatus()
         * Description: This is the single thread that stops other threads when the size is validated to be over what the command
         *              line input is.
         * Parameters: The object state
         * Return Values: Nothing
         */
        private static void UpdateStatus(object state)
        {
            string[] commandLine = Environment.GetCommandLineArgs(); //Here are the command line arguments

            string fileName = commandLine[1];    //File name path

            int size = int.Parse(commandLine[2]);
            //Get file
            FileInfo fileSize = null;

            try
            {
                fileSize = new FileInfo(fileName);
            }
            catch (FileNotFoundException e)
            {
                //Print error message
                Console.WriteLine("Error reading from {0}. Message = {1}", fileName, e.Message);
            }
            finally
            {
                //Write file size after 1 second
                Console.WriteLine(fileSize.Length);

                //if file size is higher than the user inputted size in the command line
                if (fileSize.Length > size)
                {
                    //Set volitile bool to false which should stop the threads
                    done = false;
                }
            }

        }


        /*
         * Function: validateSize()
         * Description: All this function does is validate that the size is in range from 1,000 to 20,000,000
         * Parameters: The size of the file, listed on the command line
         * Return Values: This function returns a boolean, true if it is in the range false if it is not
         */
        public static bool ValidateSize(int size)
        {
            bool validInput = false;

            //Is size in range?
            if ((size >= 1000) && (size <= 20000000))
            {
                // Valid Input
                validInput = true;
            }

            return validInput;
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
        public static string StringBuilder(int lengthOfString)
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
