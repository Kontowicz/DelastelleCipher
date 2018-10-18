using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

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

        private delastelle d = new delastelle();

        private void load_file(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                if (op.ShowDialog() == true)
                    text.Text = File.ReadAllText(op.FileName);
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
                File.WriteAllText(dialog.FileName, text.Text);
        } 

        private void show_matrix(object sender, RoutedEventArgs e)
        {
            string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");

            if (pass != "" && pass != "wpiszhaso")
            {
                password.Text = pass;
                d.set_matrix(pass);
                matrix_window win = new matrix_window(d.get_matrix());
                win.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Podaj hasło.");
                password.Text = "Wpisz hasło";
            }
        } 

        private void decrypt(object sender, RoutedEventArgs e)
        {
            if (step.IsChecked == true)
            {
                string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
                MessageBoxResult result = MessageBox.Show("Usuwanie z hasła znaków niedozwolonych.");
                if (result == MessageBoxResult.OK)
                {
                    password.Text = pass;
                }

                result = MessageBox.Show("Pobieranie tekstu do odszyfrowania.");
                string plain_text = text.Text.ToLower();
                result = MessageBox.Show("Sprawdzanie warunków które muszą spełaniać hasło i tekst do odszyfrowania.");
                if (pass == "" || plain_text == "" || pass == "wpiszhaso")
                {
                    if (pass == "" || pass == "wpiszhaso")
                    {
                        result = MessageBox.Show("Podaj hasło.");
                        password.Text = "Wpisz hasło";
                    }
                    if (plain_text == "")
                    {
                        result = MessageBox.Show("Podaj tekst.");
                    }
                }
                else
                {
                    result = MessageBox.Show("Ustawienie macierzy.");
                    if (result == MessageBoxResult.OK)
                    {
                        d.set_matrix(pass);
                        show_matrix_separate_thread();
                    }
                    if (remove_white.IsChecked == true)
                    {
                        if (horizontal.IsChecked == true)
                        {
                            result = MessageBox.Show("Odtwarzanie postaci przejściowej.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.pozimo_przejscowa_rozszufruj(plain_text);
                            }
                            result = MessageBox.Show("Odszyfrowanie.");
                            if (result == MessageBoxResult.OK)
                                text.Text = d.poziomo_rozszyfruj(plain_text);
                        }
                        if (up_down.IsChecked == true)
                        {
                            result = MessageBox.Show("Odtwarzanie postaci przejściowej.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.gora_dol_przejsciowa_rozszyforwanie(plain_text);
                            }
                            result = MessageBox.Show("Odszyfrowanie.");
                            if (result == MessageBoxResult.OK)
                                text.Text = d.gora_dol_rozszyforwanie(plain_text);
                        }
                        if (down_up.IsChecked == true)
                        {
                            result = MessageBox.Show("Odtwarzanie postaci przejściowej.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.dol_gora_przejsciowa_rozszyfrowanie(plain_text);
                            }
                            result = MessageBox.Show("Odszyfrowanie.");
                            if (result == MessageBoxResult.OK)
                                text.Text = d.dol_gora_rozszyforwanie(plain_text);

                        }
                    }
                    else
                    {

                        result = MessageBox.Show("Podział tekstu na tokeny.(Każdy token rozdzielony znakiem nowej lini)");
                        if (result == MessageBoxResult.OK)
                        {
                            var tmp = d.tokens_raw(plain_text);
                            string tokens = "";
                            for (int i = 0; i < tmp.Length; i++)
                            {
                                tokens += tmp[i];
                                tokens += "\n";
                            }
                            text.Text = tokens;
                        }
                        if (horizontal.IsChecked == true)
                        {
                            result = MessageBox.Show("Stworzenie postaci pośredniej tokentów bez znaków specjalnych i odtworzenie pierwotnej kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.poziomo_przejsciowa_specjalne(plain_text);
                            }
                            result = MessageBox.Show("Rozszyfrowanie tokenów które nie zawierają znaków specjalnych i odtworzenie pierwotnej kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.poziomo_rozszyfruj_specjlane(plain_text);
                            }

                        }
                        if (up_down.IsChecked == true)
                        {
                            result = MessageBox.Show("Stworzenie postaci pośredniej tokentów bez znaków specjalnych i odtworzenie kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.gora_dol_przejsciowa_specjalne(plain_text);
                            }
                            result = MessageBox.Show("Rozszyfrowanie tokenów które nie zawierają znaków specjalnych i odtworzenie pierwotnej kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.gora_dol_rozszyfruj_specjalne(plain_text);
                            }

                        }
                        if (down_up.IsChecked == true)
                        {
                            result = MessageBox.Show("Stworzenie postaci pośredniej tokentów bez znaków specjalnych i odtworzenie kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.dol_gora_przejsciowa_specjalne(plain_text);
                            }
                            result = MessageBox.Show("Rozszyfrowanie tokenów które nie zawierają znaków specjalnych i odtworzenie pierwotnej kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.dol_gora_rozszyfruj_specjalne(plain_text);
                            }

                        }
                    }
                }


            }
            else
            {
                string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
                password.Text = pass;
                string plain_text = text.Text.ToLower();
                if (pass == "" || plain_text == "" || pass == "wpiszhaso")
                {
                    if (pass == "" || pass == "wpiszhaso")
                    {
                        MessageBoxResult result = MessageBox.Show("Podaj hasło.");
                        password.Text = "Wpisz hasło";
                    }
                    if (plain_text == "")
                    {
                        MessageBoxResult result = MessageBox.Show("Podaj tekst.");
                    }
                }
                else
                {
                    d.set_matrix(pass);
                    if (remove_white.IsChecked == true)
                    {
                        if (horizontal.IsChecked == true)
                        {
                            text.Text = d.poziomo_rozszyfruj(plain_text);
                        }
                        if (up_down.IsChecked == true)
                        {
                            text.Text = d.gora_dol_rozszyforwanie(plain_text);
                        }
                        if (down_up.IsChecked == true)
                        {
                            text.Text = d.dol_gora_rozszyforwanie(plain_text);
                        }
                    }
                    else
                    {
                        if (horizontal.IsChecked == true)
                        {
                            text.Text = d.poziomo_rozszyfruj_specjlane(plain_text);
                        }
                        if (up_down.IsChecked == true)
                        {
                            text.Text = d.gora_dol_rozszyfruj_specjalne(plain_text);
                        }
                        if (down_up.IsChecked == true)
                        {
                            text.Text = d.dol_gora_rozszyfruj_specjalne(plain_text);
                        }
                    }
                }
            }
        } 

        private void password_clear(object sender, RoutedEventArgs e)
        {
            password.Text = "";
        }

        private void show_matrix_separate_thread()
        {
            System.Threading.Thread win = new System.Threading.Thread(delegate ()
            {
                var viewer = new matrix_window(d.get_matrix());
                viewer.Show();
                System.Windows.Threading.Dispatcher.Run();
            });

            win.SetApartmentState(System.Threading.ApartmentState.STA);
            win.Start();
        }

        private bool checkPasswordText(string pass, string plain_text)
        {
            if (pass == "" || pass == "wpiszhaso" || plain_text == "")
            {
                if (pass == "" || pass == "wpiszhaso")
                {
                    password.Text = pass;
                    MessageBoxResult result = MessageBox.Show("Podaj hasło.");
                    password.Text = "Wpisz hasło";
                    return false;
                }
                if (plain_text == "")
                {
                    MessageBoxResult result = MessageBox.Show("Podaj tekst.");
                    return false;
                }
            }
            return true;
        }

        private void enryptAllRemoveWhite()
        {
            string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
            string plain_text = Regex.Replace(text.Text.ToLower(), "[^a-z]", "");

            if (checkPasswordText(pass, plain_text) == true)
            {
                d.set_matrix(pass);
                if (horizontal.IsChecked == true)
                    text.Text = d.poziomo_szyforwanie(plain_text);
                else if (up_down.IsChecked == true)
                    text.Text = d.gora_dol_szyforwanie(plain_text);
                else if (down_up.IsChecked == true)
                    text.Text = d.dol_gora_szyfrowanie(plain_text);
            }
        }

        private bool checkPassword(string pass)
        {
            string plain_text = text.Text.ToLower();
            if (pass == "" || pass == "wpiszhaso" || plain_text == "")
            {
                if (pass == "" || pass == "wpiszhaso")
                {
                    MessageBoxResult result = MessageBox.Show("Podaj hasło");
                    password.Text = "Wpisz hasło";
                    return false;
                }
                if (plain_text == "")
                {
                    MessageBoxResult result = MessageBox.Show("Podaj tekst.");
                    return false;
                }
            }
            return true;
        }

        private void encryptAllRewrite()
        {
            string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
            if (checkPassword(pass) == true)
            {
                password.Text = pass;
                d.set_matrix(pass);
                if (horizontal.IsChecked == true)
                    text.Text = d.poziomo_szyfruj_specjalne(text.Text.ToLower());
                if (up_down.IsChecked == true)
                    text.Text = d.gora_dol_szyfruj_specjalne(text.Text.ToLower());
                if (down_up.IsChecked == true)
                    text.Text = d.dol_gora_szyfruj_specjalne(text.Text.ToLower());
            }
        }

        private void encrypt(object sender, RoutedEventArgs e)
        {
            if (step.IsChecked == true)
            {
                if (remove_white.IsChecked == true)
                {
                    MessageBoxResult result = MessageBox.Show("Filtrowanie hasła z niedozwolonych znaków.");
                    string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
                    password.Text = pass;
                    result = MessageBox.Show("Filtrowanie tesktu do szyfrowania z niedozwolonych znaków.");
                    string plain_text = Regex.Replace(text.Text.ToLower(), "[^a-z]", "");
                    text.Text = plain_text;
                    if (pass == "" || pass == "wpiszhaso" || plain_text == "")
                    {
                        if (pass == "" || pass == "wpiszhaso")
                        {
                            result = MessageBox.Show("Podaj hasło");
                            password.Text = "Wpisz hasło";
                        }
                        if (plain_text == "")
                        {
                            result = MessageBox.Show("Podaj tekst.");
                        }
                    }
                    else
                    {
                        result = MessageBox.Show("Ustawienie macierzy.");
                        if (result == MessageBoxResult.OK)
                        {
                            d.set_matrix(pass);
                            show_matrix_separate_thread();
                        }
                        if (horizontal.IsChecked == true)
                        {
                            result = MessageBox.Show("Stworzenie postaci pośredniej.");
                            if (result == MessageBoxResult.OK)
                                text.Text = d.poziomo_przejsciowa(plain_text);

                            result = MessageBox.Show("Stworzenie szyfru.");
                            if (result == MessageBoxResult.OK)
                                text.Text = d.poziomo_szyforwanie(plain_text);
                        }
                        else if (up_down.IsChecked == true)
                        {
                            result = MessageBox.Show("Stworzenie postaci pośredniej.");
                            if (result == MessageBoxResult.OK)
                                text.Text = d.gora_dol_przejsciowa(plain_text);

                            result = MessageBox.Show("Stworzenie szyfru.");
                            if (result == MessageBoxResult.OK)
                                text.Text = d.gora_dol_szyforwanie(plain_text);
                        }
                        else if (down_up.IsChecked == true)
                        {
                            result = MessageBox.Show("Stworzenie postaci pośredniej.");
                            if (result == MessageBoxResult.OK)
                                text.Text = d.dol_gora_przejsciowa(plain_text);

                            result = MessageBox.Show("Stworzenie szyfru.");
                            if (result == MessageBoxResult.OK)
                                text.Text = d.dol_gora_szyfrowanie(plain_text);
                        }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Filtrowanie hasła z niedozwolonych znaków.");
                    string pass = Regex.Replace(password.Text.ToLower(), "[^a-z]", "");
                    if (result == MessageBoxResult.OK)
                    {
                        password.Text = pass;
                    }

                    string plain_text = text.Text.ToLower();
                    result = MessageBox.Show("Filtrowanie tekstu z cyfr(zamiana każdej cyfry na znak _).");
                    if (result == MessageBoxResult.OK)
                    {
                        text.Text = plain_text;
                    }
                    if (pass == "" || pass == "wpiszhaso" || plain_text == "")
                    {
                        if (pass == "" || pass == "wpiszhaso")
                        {
                            result = MessageBox.Show("Podaj hasło");
                            password.Text = "Wpisz hasło";
                        }
                        if (plain_text == "")
                        {
                            result = MessageBox.Show("Podaj tekst.");
                        }
                    }
                    else
                    {
                        result = MessageBox.Show("Ustawienie macierzy.");
                        if (result == MessageBoxResult.OK)
                        {
                            d.set_matrix(pass);
                            show_matrix_separate_thread();
                        }
                        result = MessageBox.Show("Podział tekstu na tokeny.(Każdy token rozdzielony znakiem nowej lini)");
                        if (result == MessageBoxResult.OK)
                        {
                            var tmp = d.tokens_raw(plain_text);
                            string tokens = "";
                            for (int i = 0; i < tmp.Length; i++)
                            {
                                tokens += tmp[i];
                                tokens += "\n";
                            }
                            text.Text = tokens;
                        }
                        if (horizontal.IsChecked == true)
                        {
                            result = MessageBox.Show("Stworzenie postaci pośredniej tokentów bez znaków spacjalnych i odtworzenie kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.poziomo_przejsciowa_specjalne(plain_text);
                            }
                            result = MessageBox.Show("Syforowanie tokenów które nie zawierają znaków specjalnych i odtworzenie pierwotnej kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.poziomo_szyfruj_specjalne(plain_text);
                            }
                        }
                        if (up_down.IsChecked == true)
                        {
                            result = MessageBox.Show("Stworzenie postaci pośredniej tokentów bez znaków spacjalnych i odtworzenie kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.gora_dol_przejsciowa_specjalne(plain_text);
                            }
                            result = MessageBox.Show("Syforowanie tokenów które nie zawierają znaków specjalnych i pierwotnej kolejności ich wystąpień");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.gora_dol_szyfruj_specjalne(plain_text);
                            }
                        }
                        if (down_up.IsChecked == true)
                        {
                            result = MessageBox.Show("Stworzenie postaci pośredniej tokentów bez znaków spacjalnych i odtworzenie kolejności ich wystąpień.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.dol_gora_przejsciowa_specjalne(plain_text);
                            }
                            result = MessageBox.Show("Syforowanie tokenów które nie zawierają znaków specjalnych i odtworzenie poprzedniej postaci tekstu.");
                            if (result == MessageBoxResult.OK)
                            {
                                text.Text = d.dol_gora_szyfruj_specjalne(plain_text);
                            }
                        }
                    }
                }
            }
            else
            {
                if (remove_white.IsChecked == true)
                    enryptAllRemoveWhite();
                else
                    encryptAllRewrite();
            }
        } 

        private void load_about(object sender, RoutedEventArgs e)
        {
            var about = new about();
            about.Show();
        } 
    }
}