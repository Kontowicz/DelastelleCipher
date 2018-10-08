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
using System.Windows.Shapes;

namespace cipher
{
    /// <summary>
    /// Interaction logic for about.xaml
    /// </summary>
    public partial class about : Window
    {
        public about()
        {
            InitializeComponent();
            this.Title = "About";
            Grid my_grid = new Grid();
            ColumnDefinition column_first = new ColumnDefinition();
            ColumnDefinition column_second = new ColumnDefinition();
            
            my_grid.ColumnDefinitions.Add(column_first);
            my_grid.ColumnDefinitions.Add(column_second);
 
            RowDefinition row_first = new RowDefinition();
            RowDefinition row_second = new RowDefinition();

            my_grid.RowDefinitions.Add(row_first);
            my_grid.RowDefinitions.Add(row_second);

            Viewbox w = new Viewbox();
            
            TextBox tmp = new TextBox();
            tmp.AcceptsReturn = true;
            string contents = File.ReadAllText(@"../../about.txt");
            tmp.Text = contents;
            w.Child = tmp;
            Grid.SetColumnSpan(w, 2);
            Grid.SetRowSpan(w, 2);
            Grid.SetRow(w, 0);
            Grid.SetColumn(w, 0);
            my_grid.Children.Add(w);
            this.Content = my_grid;
        }
    }
}
