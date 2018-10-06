using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string pass = new string(password.Text.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
            if (password.Text != "" && password.Text != "Enter password")
            {
                d.set_matrix(pass);
                matrix_window win = new matrix_window(d.get_matrix());
                win.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Enter password");
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (remove_white.IsChecked == true)
            {
                string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
                string plain_text = text.Text;
                d.set_matrix(pass);
                if (pass == "" || pass == "Enterpassword")
                {
                    MessageBoxResult result = MessageBox.Show("Enter password");
                }
                else if (plain_text == "")
                {
                    MessageBoxResult result = MessageBox.Show("Enter text to encyption");
                }
                if (plain_text != "" && pass != "")
                {
                    if (horizontali.IsChecked == true)
                    {
                        text.Text = d.horizontally_decode(plain_text);
                    }
                    else if (gora_dol.IsChecked == true)
                    {
                        text.Text = d.gora_dol_decode(plain_text);
                    }
                    else if (dol_gora.IsChecked == true)
                    {
                        text.Text = d.dol_gora_decode(plain_text);
                    }
                }
            }
            else
            {
                string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
                string plain_text = text.Text;
                d.set_matrix(pass);
                if (pass == "" || pass == "Enterpassword")
                {
                    MessageBoxResult result = MessageBox.Show("Enter password");
                }
                else if (plain_text == "")
                {
                    MessageBoxResult result = MessageBox.Show("Enter text to encyption");
                }
                if (plain_text != "" && pass != "")
                {
                    if (horizontali.IsChecked == true)
                    {
                        text.Text = d.horizontaly_white_decode(plain_text);
                    }
                    else if (gora_dol.IsChecked == true)
                    {
                        text.Text = d.gora_gol_white_decode(plain_text);
                    }
                    else if (dol_gora.IsChecked == true)
                    {
                        text.Text = d.dol_gora_white_decode(plain_text);
                    }
                }
            }

        }

        private void password_clear(object sender, RoutedEventArgs e)
        {
            entered_password = true;
            password.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (remove_white.IsChecked == true)
            {
                string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
                string plain_text = Regex.Replace(text.Text.ToLower(), "[^a-z]", "");
                d.set_matrix(pass);
                if (pass == "" || pass == "Enterpassword")
                {
                    MessageBoxResult result = MessageBox.Show("Enter password");
                }
                else if (plain_text == "")
                {
                    MessageBoxResult result = MessageBox.Show("Enter text to encyption");
                }
                if (plain_text != "" && pass != "")
                {
                    if (horizontali.IsChecked == true)
                    {
                        text.Text = d.horizontally(plain_text);
                    }
                    else if(gora_dol.IsChecked == true)
                    {
                        text.Text = d.gora_dol(plain_text);
                    }
                    else if(dol_gora.IsChecked == true)
                    {
                        text.Text = d.dol_gora(plain_text);
                    }
                }
            }
            else
            {
                string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
                string plain_text = text.Text;
                d.set_matrix(pass);
                if (pass == "" || pass == "Enterpassword")
                {
                    MessageBoxResult result = MessageBox.Show("Enter password");
                }
                else if (plain_text == "")
                {
                    MessageBoxResult result = MessageBox.Show("Enter text to encyption");
                }
                if (plain_text != "" && pass != "")
                {
                    if (horizontali.IsChecked == true)
                    {
                        text.Text = d.horizontally_white(plain_text);
                    }
                    else if (gora_dol.IsChecked == true)
                    {
                        text.Text = d.gora_dol_white_encode(plain_text);
                    }
                    else if (dol_gora.IsChecked == true)
                    {
                        text.Text = d.dol_gora_white_encode(plain_text);
                    }
                }
            }
        }
    }
}
