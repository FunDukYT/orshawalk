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
        public int x; // координата х
        public int x1; // временная координата 
        public int y; // координата у
        public int y1; // временная координата у
        public static int mode; // цветовой режим
        public int mode1; // временная переменная для цветового режима
        public string street_name; // название улицы
        public int cordsv; // параметр видимости координатов
        public int cordsv1; // временный параметр видимости координатов

        public void respawn() // возврат к нулевым координатам
        {
            x = 0;
            y = 0;
            constx.Content = x;
            consty.Content = y;
            streetCheck();
        }
        public void modeCheck() // проверка и установка цветов цветового режима
        {
            if (mode == 1)
            {
                MWindow.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
                modeB.Content = "🌙";
                street.Foreground = new SolidColorBrush(Colors.White);
                street_label.Foreground = new SolidColorBrush(Colors.White);
                constx.Foreground = new SolidColorBrush(Colors.White);
                consty.Foreground = new SolidColorBrush(Colors.White);
                xxx.Foreground = new SolidColorBrush(Colors.White);
                yyy.Foreground = new SolidColorBrush(Colors.White);
                name.Foreground = new SolidColorBrush(Colors.White);
                Places.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
            }
            else if (mode == 0)
            {
                MWindow.Background = new SolidColorBrush(Colors.White);
                modeB.Content = "☀";
                street.Foreground = new SolidColorBrush(Colors.Black);
                street_label.Foreground = new SolidColorBrush(Colors.Black);
                constx.Foreground = new SolidColorBrush(Colors.Black);
                consty.Foreground = new SolidColorBrush(Colors.Black);
                xxx.Foreground = new SolidColorBrush(Colors.Black);
                yyy.Foreground = new SolidColorBrush(Colors.Black);
                name.Foreground = new SolidColorBrush(Colors.Black);
                Places.Background = new SolidColorBrush(Colors.White);
            }
        }
        private void modeB_Click(object sender, RoutedEventArgs e) // кнопка смены цветового режима
        {
            if (mode == 0)
            {
                MWindow.Background = new SolidColorBrush(Color.FromRgb(57,57,57));
                modeB.Content = "🌙";
                mode = 1;
                street.Foreground = new SolidColorBrush(Colors.White);
                street_label.Foreground = new SolidColorBrush(Colors.White);
                constx.Foreground = new SolidColorBrush(Colors.White);
                consty.Foreground = new SolidColorBrush(Colors.White);
                xxx.Foreground = new SolidColorBrush(Colors.White);
                yyy.Foreground = new SolidColorBrush(Colors.White);
                name.Foreground = new SolidColorBrush(Colors.White);
                Places.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
            }
            else if (mode == 1)
            {
                MWindow.Background = new SolidColorBrush(Colors.White);
                modeB.Content = "☀";
                mode = 0;
                street.Foreground = new SolidColorBrush(Colors.Black);
                street_label.Foreground = new SolidColorBrush(Colors.Black);
                constx.Foreground = new SolidColorBrush(Colors.Black);
                consty.Foreground = new SolidColorBrush(Colors.Black);
                xxx.Foreground = new SolidColorBrush(Colors.Black);
                yyy.Foreground = new SolidColorBrush(Colors.Black);
                name.Foreground = new SolidColorBrush(Colors.Black);
                Places.Background = new SolidColorBrush(Colors.White);
            }
        }
        public void cordsvCheck()
        {
            if (cordsv == 0)
            {
                consty.Visibility = Visibility.Hidden;
                constx.Visibility = Visibility.Hidden;
                xxx.Visibility = Visibility.Hidden;
                yyy.Visibility = Visibility.Hidden;
            }
            else if (cordsv == 1)
            {
                consty.Visibility = Visibility.Visible;
                constx.Visibility = Visibility.Visible;
                xxx.Visibility = Visibility.Visible;
                yyy.Visibility = Visibility.Visible;
            }
        }
        public void streetChange() // запрос заведений из базы данных по критерию улицы
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
        public void streetCheck() // проверка улицы по координатам
        {
            if (x >= 1 && x <= 2 && y <= 4 && y >= 1)
            {
                street_name = "Молокова";
                streetChange();
            }
            else if (y == 1 && x <= 0 && x >= -7 || x <= -1 && x >= -5 && y == -1)
            {
                street_name = "Ф. Держинского";
                streetChange();
            }
            else if (x >= 3 && x <= 6 && y == 0)
            {
                street_name = "Пакгаузная";
                streetChange();
            }
            else
            {
                street_name = "null";
                streetChange();
            }
        }
        public void imageCheck() // смена изображения
        {
            try
            {
                String stringPath = "Assets/" + x + y + ".jpg";
                MapView.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
            }
            catch
            {
                String stringPath = "Assets/interface.png";
                MapView.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
            }
        }

        public void moveUp() // передвижение вперед
        {
            y++;
            consty.Content = y;
            streetCheck();
            imageCheck();
        }
        public void moveDown() // передвижение назад
        {
            y--;
            consty.Content = y;
            streetCheck();
            imageCheck();
        }
        public void moveRight() // передвижение вправо
        {
            x++;
            constx.Content = x;
            streetCheck();
            imageCheck();
        }
        public void moveLeft() // передвижение влево
        {
            x--;
            constx.Content = x;
            streetCheck();
            imageCheck();
        }
        public void save() // сохранение координатов и цветового режима
        {
            using (StreamWriter writer = new StreamWriter(@"./save.save"))
            {
                writer.WriteLine(x);
                writer.WriteLine(y);
                writer.WriteLine(mode);
                writer.WriteLine(cordsv);
            }
        }
        public void load() // загрузка сохраненных координатов и настроек
        {
            using (StreamReader sr = new StreamReader(@"./save.save"))
            {
                x = int.Parse(sr.ReadLine());
                y = int.Parse(sr.ReadLine());
                mode = int.Parse(sr.ReadLine());
                cordsv = int.Parse(sr.ReadLine());
            }
            y1 = y;
            x1 = x;
            mode1 = mode;
            cordsv1 = cordsv;
        }
        public void DragWindow(object sender, MouseButtonEventArgs e) // перемещение окна с помощью мыши
        {
            this.DragMove();
        }
        public void closeB_Red(object sender, MouseEventArgs e) 
        {
            closeB.Foreground = new SolidColorBrush(Colors.Red);
        }
        public void closeB_Black(object sender, MouseEventArgs e)
        {
            closeB.Foreground = new SolidColorBrush(Colors.Black);
        }
        public void maxB_Blue(object sender, MouseEventArgs e)
        {
            maxB.Foreground = new SolidColorBrush(Colors.Blue);
        }
        public void maxB_Black(object sender, MouseEventArgs e)
        {
            maxB.Foreground = new SolidColorBrush(Colors.Black);
        }
        public void minB_Blue(object sender, MouseEventArgs e)
        {
            minB.Foreground = new SolidColorBrush(Colors.Blue);
        }
        public void minB_Black(object sender, MouseEventArgs e)
        {
            minB.Foreground = new SolidColorBrush(Colors.Black);
        }
        public void modeB_Yellow(object sender, MouseEventArgs e)
        {
            modeB.Foreground = new SolidColorBrush(Colors.Yellow);
        }
        public void modeB_Black(object sender, MouseEventArgs e)
        {
            modeB.Foreground = new SolidColorBrush(Colors.Black);
        }
        public void helpB_Blue(object sender, MouseEventArgs e)
        {
            helpB.Foreground = new SolidColorBrush(Colors.Blue);
        }
        public void helpB_Black(object sender, MouseEventArgs e)
        {
            helpB.Foreground = new SolidColorBrush(Colors.Black);
        }
        public void cordsB_Blue(object sender, MouseEventArgs e)
        {
            cordsB.Foreground = new SolidColorBrush(Colors.Blue);
        }
        public void cordsB_Black(object sender, MouseEventArgs e)
        {
            cordsB.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void Window_Key(object sender, KeyEventArgs e) // горячие клавиши в приложении
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
            else if (e.Key == Key.R)
            {
                respawn();
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                save();
            }
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


        private void closeB_Click(object sender, RoutedEventArgs e) 
        {
            if (y1 != y && x1 != x || mode1 != mode || cordsv1 != cordsv)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить перед закрытием?", "Выход", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        save();
                        this.Close();
                        break;
                    case MessageBoxResult.No:
                        this.Close();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                this.Close();
            }
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

        private void respawnB_Click(object sender, RoutedEventArgs e)
        {
            respawn();
        }

        private void helpB_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            help.Show();
        }
        private void cordsB_Click(object sender, RoutedEventArgs e)
        {
            if(cordsv == 1)
            {
                consty.Visibility = Visibility.Hidden;
                constx.Visibility = Visibility.Hidden;
                xxx.Visibility = Visibility.Hidden;
                yyy.Visibility = Visibility.Hidden;
                cordsv = 0;
            }
            else if(cordsv == 0)
            {
                consty.Visibility = Visibility.Visible;
                constx.Visibility = Visibility.Visible;
                xxx.Visibility = Visibility.Visible;
                yyy.Visibility = Visibility.Visible;
                cordsv = 1;
            }
        }
        public MainWindow()
        {
            load();
            InitializeComponent();
            DateTime time = DateTime.Now;
            Time.Content = time.ToString();
            cordsvCheck();
            streetCheck();
            modeCheck();
            consty.Content = y;
            constx.Content = x;
        }

    }
}
