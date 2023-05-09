//Need File Header

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net;
using System.Net.Sockets;

namespace A05wpWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string ipAdd;
        private static Int32 port;
        private int exit;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] separateStrings = { " " };
            string com = "";
            ipError.Content = " ";
            portError.Content = " ";
            nameError.Content = " ";

            //------Error Messages------//
            if ((ipAddressTextbox.Text == null) || (ipAddressTextbox.Text == "IP Address") || (portNumTextbox.Text == null) || (portNumTextbox.Text == "Port Number") || (nameTextbox.Text == null) || (nameTextbox.Text == "Name"))
            {
                if ((ipAddressTextbox.Text == null) || (ipAddressTextbox.Text == "IP Address"))
                {
                    //IP Error
                    ipError.Content = "ERROR: Please enter your IP Adderss Number";
                }

                if ((portNumTextbox.Text == null) || (portNumTextbox.Text == "Port Number"))
                {
                    //IP Address Error
                    portError.Content = "ERROR: Please enter your Port Number";
                }

                if ((nameTextbox.Text == null) || (nameTextbox.Text == "Name"))
                {
                    //Name Error
                    nameError.Content = "ERROR: Please enter your Name";
                }
            }
            else
            {
                ipAdd = ipAddressTextbox.Text;
                port = int.Parse(portNumTextbox.Text);
                
                //If all info is somewhat right... didn't test if the ip/port were numbers since I do not know how
                //Submit to server and change views to the gameplay part...

                //------Grid visibility to change views------//
                //Hide the user input stuff
                ipAddressPortNameGrid.Visibility = Visibility.Hidden;
                //Show the gameplay
                gameplayWithServer.Visibility = Visibility.Visible;

                //Sever communication / seeing if it connects
                //Send string with the IP address to connect to, port number and name
                com = ConnectClient(ipAddressTextbox.Text, int.Parse(portNumTextbox.Text), nameTextbox.Text);

                string[] response = com.Split(separateStrings, System.StringSplitOptions.RemoveEmptyEntries);

                if (response[0] == "Connected")
                {
                    connectedLabel.Content = "Connected";
                    gameRangesLabel.Content = "The ranges you can guess between are: " + response[1].ToString() + "-" + response[2].ToString();
                    invisibleLabel.Content = response[1].ToString() + " " + response[2].ToString();
                }
                else
                {
                    connectedLabel.Content = "NOT CONNECTED";
                }
            }
            

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(exit == 1)
            {
                //If the user pressed the exit once then did something else i.e. this button then reset the exit number and take away the prompt
                exit = 0;
                exitLabel.Content = "";
            }

            guessError.Content = " ";
            string com;
            string[] separateStrings = { " " };

            //Actual gameplay
            string[] ranges = invisibleLabel.Content.ToString().Split(separateStrings, System.StringSplitOptions.RemoveEmptyEntries);



            if(gameGuessTextbox.Text == "Yes")//Yes to wanting to continue/ Play Again
            {
                //If the user wants to play again just give them another random app config file then it should loop fine
                //It 

                //Send the 'Yes'
                com = ConnectClient(ipAddressTextbox.Text, int.Parse(portNumTextbox.Text), nameTextbox.Text + " " + gameGuessTextbox.Text);

                //Get new min and max values back
                string[] response = com.Split(separateStrings, System.StringSplitOptions.RemoveEmptyEntries);
                gameRangesLabel.Content = "The ranges you can guess between are: " + response[0].ToString() + "-" + response[1].ToString();
                invisibleLabel.Content = response[0].ToString() + " " + response[1].ToString();
            }
            else if (gameGuessTextbox.Text == "" || gameGuessTextbox.Text == null || gameGuessTextbox.Text == " ")
            {
                guessError.Content = "Must guess something";
            }
            else
            {
                //If guess is in range...
                if ((int.Parse(ranges[0]) <= int.Parse(gameGuessTextbox.Text)) && (int.Parse(ranges[1]) >= int.Parse(gameGuessTextbox.Text)))
                {
                    //Send the number
                    com = ConnectClient(ipAddressTextbox.Text, int.Parse(portNumTextbox.Text), nameTextbox.Text + " " + gameGuessTextbox.Text);

                    string[] hiLoWin = com.Split(separateStrings, System.StringSplitOptions.RemoveEmptyEntries);

                    if (hiLoWin[0] == "Higher")
                    {
                        invisibleLabel.Content = ranges[0] + " " + hiLoWin[1];
                        gameRangesLabel.Content = "The ranges you can guess between are: " + ranges[0] + "-" + hiLoWin[1];
                    }
                    else if(hiLoWin[0] == "Lower")
                    {
                        invisibleLabel.Content = hiLoWin[1] + " " + ranges[1];
                        gameRangesLabel.Content = "The ranges you can guess between are: " + hiLoWin[1] + "-" + ranges[1];
                    }
                    else if(com == "You Win")
                    {
                        gameRangesLabel.Content = "YOU WIN!!!!! Type 'Yes' in the textbox and press the enter guess button to Play Again!!!";
                    }

                }
                else
                {
                    guessError.Content = "Must guess within the ranges";
                }
            }

            
        }

        private static string ConnectClient(String Ip, int port, String message)
        {
            string com = "";
            

            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.

                TcpClient client = new TcpClient(Ip, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                //Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);


                com = responseData;

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            return com;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {

            if (exit == 1)
            {
                //If they already pressed the button once and they pressed it again without pressing any other button exit
                this.Close();
            }

            if (exit == 0)
            {
                exit = 1;
                exitLabel.Content = "Are you sure you want to exit?";
            }
        }
    }
}
