/* -- this is a FILEHEADER COMMENT --
	NAME	:	MainWindow.cs
	PURPOSE :	The purpose of this file is to be the main window for the notepad area with all the menus and 
                status bars and Textbox.
*/
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
using System.IO;
using Microsoft.Win32;


namespace A02wpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool savedText = true;


        public MainWindow()
        {
            InitializeComponent();
        }

        //
        // Function: FileNew_Click()
        // Description: Contains the main flow of what to do when you press New under File in the menu, it should 
        //              clear the page essentially making a new document. It warns the user if they have unsaved text and want to save it aswell.
        // Parameters:  object sender
        //              RoutedEventArgs e
        // Return Values: Nothing
        //
        private void FileNew_Click(object sender, RoutedEventArgs e)
        {
            string userInput = notepadTextbox.Text;

            if(savedText == false)//If there is text in the text box
            {
                //Tell them to save their work
                MessageBoxResult result = MessageBox.Show("You have unsaved text, do you want to save the changes?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    // Save and New code here  
                    SaveFile();
                    notepadTextbox.Text = String.Empty;
                }
                else if (result == MessageBoxResult.No)
                {
                    // New without Saving code here  
                    notepadTextbox.Text = String.Empty;
                }
            }
            else
            {
                //New doc
                notepadTextbox.Text = String.Empty;
            }
        }

        //
        // Function: FileOpen_Click()
        // Description: Contains the main flow of what to do when the user clicks 'open' under 'file' in the menu,
        //              this should bring up the OpenFileDialog and then the user can open a .txt file and it will print that files contents to the screen
        //              it also warns the user if they have unsaved text and want to save it.
        // Parameters:  object sender
        //              RoutedEventArgs e
        // Return Values: Nothing
        //
        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {
            if (savedText == false)//If there is text in the text box
            {
                //Tell them to save their work
                MessageBoxResult msgBox = MessageBox.Show("You have unsaved text, do you want to save the changes?", "Confirmation", MessageBoxButton.YesNo);
                if (msgBox == MessageBoxResult.Yes)
                {
                    // Open and Save code here  
                    SaveFile();
                    OpenNewFile();
                }
                else if (msgBox == MessageBoxResult.No)
                {
                    // Open without Saving code here  
                    OpenNewFile();
                }
            }
            else
            {
                //Text has been saved
                OpenNewFile();
            }

        }


        //
        // Function: FileSaveAs_Click()
        // Description: Contains the main flow of what to do when the user clicks 'Save As' under 'file' in the menu, 
        //              this should bring up the SaveFileDialog() and prompt the user to save their work.
        // Parameters:  object sender
        //              RoutedEventArgs e
        // Return Values: Nothing
        //
        private void FileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            //Just save the file
            SaveFile();
        }

        //
        // Function: FileClose_Click()
        // Description: Contains the main flow of what to do when the user clicks 'close' under 'file' in the menu,
        //              this should close the application and if there is unsaved text it will promt the user to save and close,
        //              close without saving or to cancel the close
        // Parameters:  object sender
        //              RoutedEventArgs e
        // Return Values: Nothing
        //
        private void FileClose_Click(object sender, RoutedEventArgs e)
        {

            CloseApplication();
        }

        //
        // Function: Window_Closing()
        // Description: Contains the main flow of what to do when the user clicks the corner 'x' button it works the same as close in the menu,
        //              this should close the application and if there is unsaved text it will promt the user to save and close,
        //              close without saving or to cancel the close.
        // Parameters:  object sender
        //              RoutedEventArgs e
        // Return Values: Nothing
        //
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseApplication();
        }

        //
        // Function: HelpAbout_Click()
        // Description: Contains the main flow of what to do when the user clicks 'about' under the 'help' button in the menu,
        //              this should open a modal dialog box where you cannot do anything in the notepad until you either 'x' out or click
        //              the 'Ok' button
        // Parameters:  object sender
        //              RoutedEventArgs e
        // Return Values: Nothing
        //
        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AboutDialog();

            dialog.ShowDialog();
            
        }

        //
        // Function: TextChanged()
        // Description: Contains the main flow of what to do when the user types anything into the notepad textbox, to keep track
        //              of if the document has been saved or not
        // Parameters:  object sender
        //              RoutedEventArgs e
        // Return Values: Nothing
        //
        private void TextChanged(object sender, RoutedEventArgs e)
        {
            //Whenever something is written it hasn't been saved
            savedText = false;

            statusTextbox.Text = "Character Count: " + notepadTextbox.Text.Length;
        }





        //
        // Function: SaveFile()
        // Description: All the code for saving a file using the SaveFileDialog()
        // Parameters:  Nothing
        // Return Values: Nothing
        //
        private void SaveFile()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                File.WriteAllText(dialog.FileName, notepadTextbox.Text);
                // Save document
                string filename = dialog.FileName;
                savedText = true;
            }
        }

        //
        // Function: SaveFile()
        // Description: All the code for closing out of the application
        // Parameters:  Nothing
        // Return Values: Nothing
        //
        private void CloseApplication()
        {
            if (savedText == false)
            {

                MessageBoxResult result = MessageBox.Show("You have unsaved text, do you want to save the changes?", "Confirmation", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    // Save and Close code here  
                    SaveFile();
                    System.Windows.Application.Current.Shutdown();
                }
                else if (result == MessageBoxResult.No)
                {
                    // Close without Saving code here  
                    System.Windows.Application.Current.Shutdown();
                }
                else
                {
                    // Cancel code here  
                }
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        //
        // Function: OpenNewFIle()
        // Description: All the code for opening a file using the OpenFileDialog()
        // Parameters:  Nothing
        // Return Values: Nothing
        //
        private void OpenNewFile()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            bool? result = ofd.ShowDialog();


            if (result == true)
            {
                //Get the path of specified file
                filePath = ofd.FileName;

                //Read the contents of the file into a stream
                var fileStream = ofd.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    notepadTextbox.Text = reader.ReadToEnd();
                }
            }

        }

    }
}
