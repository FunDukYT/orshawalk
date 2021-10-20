using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SQLite;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int x;
        public int y;
        public int mode;
        public string street_name;

        public void modeCheck()
        {
            if (mode == 1)
            {
                MWindow.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
                modeB.Content = "Темная";
                street.Foreground = new SolidColorBrush(Colors.White);
                street_label.Foreground = new SolidColorBrush(Colors.White);
                constx.Foreground = new SolidColorBrush(Colors.White);
                consty.Foreground = new SolidColorBrush(Colors.White);
                xxx.Foreground = new SolidColorBrush(Colors.White);
                yyy.Foreground = new SolidColorBrush(Colors.White);
                Places.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
            }
            else if (mode == 0)
            {
                MWindow.Background = new SolidColorBrush(Colors.White);
                modeB.Content = "Светлая";
                street.Foreground = new SolidColorBrush(Colors.Black);
                street_label.Foreground = new SolidColorBrush(Colors.Black);
                constx.Foreground = new SolidColorBrush(Colors.Black);
                consty.Foreground = new SolidColorBrush(Colors.Black);
                xxx.Foreground = new SolidColorBrush(Colors.Black);
                yyy.Foreground = new SolidColorBrush(Colors.Black);
                Places.Background = new SolidColorBrush(Colors.White);
            }
        }
        public void streetChange()
        {
            string s1 = "'" + street_name + "'";
            SQLiteConnection conn = new SQLiteConnection("Data Source=places.db; Version=3;");
            SQLiteCommand sqlcmd = conn.CreateCommand();
            conn.Open();
            sqlcmd.CommandText = "SELECT Заведение,Тип,Дом FROM places_1 WHERE Улица LIKE " + s1;
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlcmd.CommandText, conn);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            Places.ItemsSource = ds.Tables[0].DefaultView;
            conn.Close();
            street.Content = street_name;
        }

        public void moveUp()
        {
            y++;
            consty.Content = y;
            streetCheck();
        }
        public void moveDown()
        {
            y--;
            consty.Content = y;
            streetCheck();
        }
        public void moveRight()
        {
            x++;
            constx.Content = x;
            streetCheck();
        }
        public void moveLeft()
        {
            x--;
            constx.Content = x;
            streetCheck();
        }
        public void streetCheck()
        {
            if (x >= 1 && x <= 2 && y <= 4 && y >= 1)
            {
                street_name = "Молокова";
                streetChange();
            }
            else if (y == 1 && x <= 0 && x >= -3)
            {
                street_name = "Ф. Держинского";
                streetChange();
            }
            else if (x == 3 && y == 0)
            {
                street_name = "Пакгаузная";
                streetChange();
            }
            else
            {
                street_name = "В разработке";
                streetChange();
            }
        }
        public void save()
        {
            using (StreamWriter writer = new StreamWriter(@"./save.save"))
            {
                writer.WriteLine(x);
                writer.WriteLine(y);
                writer.WriteLine(mode);
            }
        }
        public void load()
        {
            using (StreamReader sr = new StreamReader(@"./save.save"))
            {
                x = int.Parse(sr.ReadLine());
                y = int.Parse(sr.ReadLine());
                mode = int.Parse(sr.ReadLine());
            }
        }
        private void Window_Key(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                moveUp();
            }
            else if (e.Key == Key.Down)
            {
                moveDown();
            }
            else if (e.Key == Key.Right)
            {
                moveRight();
            }
            else if (e.Key == Key.Left)
            {
                moveLeft();
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                save();
            }
        }
        public MainWindow()
        {
            load();
            InitializeComponent();
            streetCheck();
            modeCheck();
            consty.Content = y;
            constx.Content = x;
        }
        private void forwardB_Click(object sender, RoutedEventArgs e)
        {
            moveUp();
        }

        private void backB_Click(object sender, RoutedEventArgs e)
        {
            moveDown();
        }

        private void leftB_Click(object sender, RoutedEventArgs e)
        {
            moveLeft();
        }

        private void rightB_Click(object sender, RoutedEventArgs e)
        {
            moveRight();
        }

        private void modeB_Click(object sender, RoutedEventArgs e)
        {
            if (mode == 0)
            {
                MWindow.Background = new SolidColorBrush(Color.FromRgb(57,57,57));
                modeB.Content = "Темная";
                mode = 1;
                street.Foreground = new SolidColorBrush(Colors.White);
                street_label.Foreground = new SolidColorBrush(Colors.White);
                constx.Foreground = new SolidColorBrush(Colors.White);
                consty.Foreground = new SolidColorBrush(Colors.White);
                xxx.Foreground = new SolidColorBrush(Colors.White);
                yyy.Foreground = new SolidColorBrush(Colors.White);
                Places.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
            }
            else if (mode == 1)
            {
                MWindow.Background = new SolidColorBrush(Colors.White);
                modeB.Content = "Светлая";
                mode = 0;
                street.Foreground = new SolidColorBrush(Colors.Black);
                street_label.Foreground = new SolidColorBrush(Colors.Black);
                constx.Foreground = new SolidColorBrush(Colors.Black);
                consty.Foreground = new SolidColorBrush(Colors.Black);
                xxx.Foreground = new SolidColorBrush(Colors.Black);
                yyy.Foreground = new SolidColorBrush(Colors.Black);
                Places.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void closeB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maxB_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                maxB.Content = "🔳";
            }
            else
            {
                this.WindowState = WindowState.Normal;
                maxB.Content = "🔲";
            }
        }

        private void minB_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void saveB_Click(object sender, RoutedEventArgs e)
        {
            save();
        }
    }
}
