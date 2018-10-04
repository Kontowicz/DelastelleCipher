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
            this.Title = "Matrix";

            Grid my_grid = new Grid();
            ColumnDefinition column_first = new ColumnDefinition();
            ColumnDefinition column_second = new ColumnDefinition();
            ColumnDefinition column_third = new ColumnDefinition();
            ColumnDefinition column_fourth = new ColumnDefinition();
            ColumnDefinition column_fifth = new ColumnDefinition();
            ColumnDefinition column_sixth = new ColumnDefinition();
            ColumnDefinition column_seventh = new ColumnDefinition();

            my_grid.ColumnDefinitions.Add(column_first);
            my_grid.ColumnDefinitions.Add(column_second);
            my_grid.ColumnDefinitions.Add(column_third);
            my_grid.ColumnDefinitions.Add(column_fourth);
            my_grid.ColumnDefinitions.Add(column_fifth);
            my_grid.ColumnDefinitions.Add(column_sixth);
            my_grid.ColumnDefinitions.Add(column_seventh);

            RowDefinition row_first = new RowDefinition();
            RowDefinition row_second = new RowDefinition();
            RowDefinition row_third = new RowDefinition();
            RowDefinition row_fourth = new RowDefinition();
            RowDefinition row_fifth = new RowDefinition();
            RowDefinition row_sixth = new RowDefinition();
            RowDefinition row_seventh = new RowDefinition();

            my_grid.RowDefinitions.Add(row_first);
            my_grid.RowDefinitions.Add(row_second);
            my_grid.RowDefinitions.Add(row_third);
            my_grid.RowDefinitions.Add(row_fourth);
            my_grid.RowDefinitions.Add(row_fifth);
            my_grid.RowDefinitions.Add(row_sixth);
            my_grid.RowDefinitions.Add(row_seventh);

            GridSplitter[] row_spliter = new GridSplitter[6];
            for(int i = 0; i < 6; i++)
            {
                row_spliter[i] = new GridSplitter();
                row_spliter[i].Height = 10;
                row_spliter[i].HorizontalAlignment = HorizontalAlignment.Stretch;
                row_spliter[i].VerticalAlignment = VerticalAlignment.Top;
                row_spliter[i].IsEnabled = false;
                Grid.SetColumn(row_spliter[i], 1);
                Grid.SetRow(row_spliter[i], i+1);
                Grid.SetColumnSpan(row_spliter[i], 5);
                my_grid.Children.Add(row_spliter[i]);
            }

            GridSplitter[] column_spliter = new GridSplitter[6];
            for (int i = 0; i < 5; i++)
            {
                column_spliter[i] = new GridSplitter();
                column_spliter[i].Width = 10;
                column_spliter[i].HorizontalAlignment = HorizontalAlignment.Left;
                column_spliter[i].VerticalAlignment = VerticalAlignment.Stretch;
                column_spliter[i].IsEnabled = false;
                Grid.SetColumn(column_spliter[i], i+1);
                Grid.SetRow(column_spliter[i], 1);
                Grid.SetRowSpan(column_spliter[i], 5);
                my_grid.Children.Add(column_spliter[i]);
            }

            column_spliter[5] = new GridSplitter();
            column_spliter[5].Width = 10;
            column_spliter[5].HorizontalAlignment = HorizontalAlignment.Right;
            column_spliter[5].IsEnabled = false;
            Grid.SetColumn(column_spliter[5], 5);
            Grid.SetRow(column_spliter[5], 1);
            Grid.SetRowSpan(column_spliter[5], 5);
            my_grid.Children.Add(column_spliter[5]);

            Viewbox[] view_box_vertical = new Viewbox[5];
            for(int i = 0; i<5; i++)
            {
                view_box_vertical[i] = new Viewbox();
                Label tmp = new Label();
                tmp.Content = (i+1).ToString();
                view_box_vertical[i].Child = tmp;
                view_box_vertical[i].Visibility = Visibility.Visible;
                view_box_vertical[i].VerticalAlignment = VerticalAlignment.Center;
                view_box_vertical[i].HorizontalAlignment = HorizontalAlignment.Right;
                view_box_vertical[i].Margin = new Thickness(0, 0, 5, 0);
                Grid.SetRow(view_box_vertical[i], i+1);
                Grid.SetColumn(view_box_vertical[i], 0);
                my_grid.Children.Add(view_box_vertical[i]);
                this.Content = my_grid;
            }

            Viewbox[] view_box_horizontal = new Viewbox[5];
            for(int i = 0; i < 5; i++)
            {
                view_box_vertical[i] = new Viewbox();
                Label tmp = new Label();
                tmp.Content = (i + 1).ToString();
                view_box_vertical[i].Child = tmp;
                view_box_vertical[i].Visibility = Visibility.Visible;
                view_box_vertical[i].VerticalAlignment = VerticalAlignment.Bottom;
                view_box_vertical[i].HorizontalAlignment = HorizontalAlignment.Center;
                view_box_vertical[i].Margin = new Thickness(0, 0, 0, 5);
                Grid.SetRow(view_box_vertical[i], 0);
                Grid.SetColumn(view_box_vertical[i], i + 1);
                my_grid.Children.Add(view_box_vertical[i]);
                this.Content = my_grid;
            }

            int cnt = 0;
            Viewbox[] view_box_val = new Viewbox[25];
            for (int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    view_box_val[cnt] = new Viewbox();
                    Label tmp = new Label();
                    tmp.Content = (cnt + 1).ToString();
                    view_box_val[cnt].Child = tmp;
                    view_box_val[cnt].Visibility = Visibility.Visible;
                    view_box_val[cnt].VerticalAlignment = VerticalAlignment.Bottom;
                    view_box_val[cnt].HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(view_box_val[cnt], i+1);
                    Grid.SetColumn(view_box_val[cnt], j+1);
                    my_grid.Children.Add(view_box_val[cnt]);
                    this.Content = my_grid;
                    ++cnt;
                }
            }
            this.Show();
        }
    }
}
