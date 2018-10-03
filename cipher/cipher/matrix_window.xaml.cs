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

namespace cipher
{
    /// <summary>
    /// Interaction logic for matrix_window.xaml
    /// </summary>
    public partial class matrix_window : Window
    {
        public matrix_window()
        {
            InitializeComponent();
            Label[] viewboxes = new Label[1];
            for(int i = 0; i < 1; ++i)
            {
                viewboxes[i] = new Label();

                System.Console.WriteLine("test");
                viewboxes[i].Content = "tmp";
                viewboxes[i].Visibility = Visibility.Visible;
                Grid.SetRow(viewboxes[i], 3);
                Grid.SetColumn(viewboxes[i], 2);
            }
        }
    }
}
