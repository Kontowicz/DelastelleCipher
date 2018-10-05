using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace cipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool entered_password = false;
        private delastelle d = new delastelle();

        private void load_file(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                if (op.ShowDialog() == true)
                {
                    text.Text = File.ReadAllText(op.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, somthing gone wrong.\n Orginal message:" + ex.Message);
            }
        }

        private void save_file(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, text.Text);
            }
        }

        private void show_matrix(object sender, RoutedEventArgs e)
        {
            matrix_window win = new matrix_window();
            win.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string pass = new string  (password.Text.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
            string plain_text = new string(text.Text.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());

            if (pass == "" || pass == "Enterpassword")
            {
                MessageBoxResult result = MessageBox.Show("Enter password");
            }
            else if (plain_text == "")
            {
                MessageBoxResult result = MessageBox.Show("Enter text to encyption");
            }
            if(plain_text != "" || pass != "")
            {
                d.set_matrix(password.Text);
            }
        }

        private void password_clear(object sender, RoutedEventArgs e)
        {
            entered_password = true;
            password.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string pass = new string(password.Text.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
            string plain_text = new string(text.Text.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());

            if (pass == "" || pass == "Enterpassword")
            {
                MessageBoxResult result = MessageBox.Show("Enter password");
            }
            else if (plain_text == "")
            {
                MessageBoxResult result = MessageBox.Show("Enter text to encyption");
            }
            if (plain_text != "" || pass != "")
            {
                d.set_matrix(password.Text);
                text.Text = d.poziomo(plain_text);
            }
        }
    }
}
