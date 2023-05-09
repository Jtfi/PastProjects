//Need File Header

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
// The following code is extracted from the MSDN site:
//https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener?view=net-5.0
//
namespace TCPIPServer
{
    class Server
    { 
        private static Dictionary<string, string> clientRange = new Dictionary<string, string>();
        private static Dictionary<string, string> clientCorrectNum = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();


                // Enter the listening loop.
                while (true)
                {

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    
                    ParameterizedThreadStart ts = new ParameterizedThreadStart(Worker);
                    Thread clientThread = new Thread(ts);
                    clientThread.Start(client);


                }
            }
            catch (SocketException e)
            {
                //Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            //Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        public static void Worker(Object o)
        {
            TcpClient client = (TcpClient)o;
            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            data = null;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                string[] separateStrings = { " " };
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                // Process the data sent by the client.

                //Split the string into pieces
                string[] clientMessageParts = data.Split(separateStrings, System.StringSplitOptions.RemoveEmptyEntries);
                //if there is just one piece

                int length = clientMessageParts.Length;

                if (clientMessageParts[0] == "Yes")
                {

                }



                if ((length == 1) && (clientMessageParts[0] != "Yes"))
                {
                    string randomRange;
                    int randomNumber;
                    Random num = new Random();

                    randomNumber = num.Next(1, 4);

                    
                    randomRange = ConfigurationManager.AppSettings.Get("randomRange" + randomNumber.ToString()); //Getting the range from app.config
                  
                    //Put name with which range you are using and the random number in the database 

                    //Now we need to split up the range into its 2 number values
                    string[] minAndMax = randomRange.Split(separateStrings, System.StringSplitOptions.RemoveEmptyEntries);

                    //Now min should be in minAndMax[0] and max should be in minAndMax[1]
                    //Now get a random correct number inside the ranges
                    randomNumber = num.Next(int.Parse(minAndMax[0]), int.Parse(minAndMax[1]));
                     //Insert (NAME, RANGE) and (NAME, CORRECT_NUM) into their dictionaries
                    clientRange.Add(clientMessageParts[0], randomRange);
                    clientCorrectNum.Add(clientMessageParts[0], randomNumber.ToString());

                    //make data (which is going to be sent back to the user) equal to a string
                    data = "Connected " + minAndMax[0] + " " + minAndMax[1]; //Send back if it connected and the min and max ranges
                }
                else if(length > 1)
                {
                    string range;
                    string correctNum;
                    //Get the value associated with the key which will return the range of that name/client
                    clientRange.TryGetValue(clientMessageParts[0],out range);
                    clientCorrectNum.TryGetValue(clientMessageParts[0], out correctNum);

                    //Split up the ranges again
                    string[] minAndMax = range.Split(separateStrings, System.StringSplitOptions.RemoveEmptyEntries);

                    //If the guess was the correct number
                    if (int.Parse(clientMessageParts[1]) == int.Parse(correctNum))
                    {
                        //Then the client WINSSSSSS
                        data = "You Win";
                    }
                    else
                    {
                        if(int.Parse(clientMessageParts[1]) > int.Parse(correctNum)) //If your guess is higher
                        {
                            int max = int.Parse(clientMessageParts[1]) - 1;
                            data = "Higher " + max.ToString();
                        }
                        else if (int.Parse(clientMessageParts[1]) < int.Parse(correctNum))//If your guess is lower
                        {
                            int min = int.Parse(clientMessageParts[1]) + 1;
                            data = "Lower " + min.ToString();
                        }
                    }

                }


                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);//encoding a message

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                
            }

            // Shutdown and end connection
            client.Close();
        }

    }
}

