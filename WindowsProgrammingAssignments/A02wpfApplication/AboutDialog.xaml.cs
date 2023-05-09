/* -- this is a FILEHEADER COMMENT --
	NAME	:	AboutDialog.cs
	PURPOSE :	The purpose of this file is to create another pop up window for the about 
                page under Help in the menu
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
using System.Windows.Shapes;

namespace A02wpfApplication
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();
        }


        //
        //
        //
        //
        private void Ok_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false;

        }


    }

}
