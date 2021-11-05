using System;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int x; // координата х
        public int x1; // временная координата x
        public int y; // координата у
        public int y1; // временная координата у
        public static int mode; // цветовой режим
        public int mode1; // временная переменная для цветового режима
        public string street_name; // название улицы
        public int cordsv; // параметр видимости координатов
        public int cordsv1; // временный параметр видимости координатов
        public string modeS; // временный параметр цветового режима

        public void respawn() // возврат к нулевым координатам
        {
            x = 0;
            y = 0;
            constx.Content = x;
            consty.Content = y;
            streetCheck();
            imageChange();
        }
        public void modeCheck() // проверка и установка цветов цветового режима
        {
            if (mode == 1)
            {
                MWindow.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
                String stringPath = "Assets/dark-black.png";
                mode_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
                street.Foreground = new SolidColorBrush(Colors.White);
                street_label.Foreground = new SolidColorBrush(Colors.White);
                constx.Foreground = new SolidColorBrush(Colors.White);
                consty.Foreground = new SolidColorBrush(Colors.White);
                xxx.Foreground = new SolidColorBrush(Colors.White);
                yyy.Foreground = new SolidColorBrush(Colors.White);
                name.Foreground = new SolidColorBrush(Colors.White);
                Places.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
                Time.Foreground = new SolidColorBrush(Colors.White);
                modeS = "dark";
            }
            else if (mode == 0)
            {
                MWindow.Background = new SolidColorBrush(Colors.Azure);
                String stringPath = "Assets/light-black.png";
                mode_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
                street.Foreground = new SolidColorBrush(Colors.Black);
                street_label.Foreground = new SolidColorBrush(Colors.Black);
                constx.Foreground = new SolidColorBrush(Colors.Black);
                consty.Foreground = new SolidColorBrush(Colors.Black);
                xxx.Foreground = new SolidColorBrush(Colors.Black);
                yyy.Foreground = new SolidColorBrush(Colors.Black);
                name.Foreground = new SolidColorBrush(Colors.Black);
                Places.Background = new SolidColorBrush(Colors.Azure);
                Time.Foreground = new SolidColorBrush(Colors.Black);
                modeS = "light";
            }
        }
        private void modeB_Click(object sender, RoutedEventArgs e) // кнопка смены цветового режима
        {
            if (mode == 0)
            {
                MWindow.Background = new SolidColorBrush(Color.FromRgb(57,57,57));
                String stringPath = "Assets/dark-black.png";
                mode_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
                mode = 1;
                street.Foreground = new SolidColorBrush(Colors.White);
                street_label.Foreground = new SolidColorBrush(Colors.White);
                constx.Foreground = new SolidColorBrush(Colors.White);
                consty.Foreground = new SolidColorBrush(Colors.White);
                xxx.Foreground = new SolidColorBrush(Colors.White);
                yyy.Foreground = new SolidColorBrush(Colors.White);
                name.Foreground = new SolidColorBrush(Colors.White);
                Places.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
                Time.Foreground = new SolidColorBrush(Colors.White);
                modeS = "dark";
            }
            else if (mode == 1)
            {
                MWindow.Background = new SolidColorBrush(Colors.Azure);
                String stringPath = "Assets/light-black.png";
                mode_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
                mode = 0;
                street.Foreground = new SolidColorBrush(Colors.Black);
                street_label.Foreground = new SolidColorBrush(Colors.Black);
                constx.Foreground = new SolidColorBrush(Colors.Black);
                consty.Foreground = new SolidColorBrush(Colors.Black);
                xxx.Foreground = new SolidColorBrush(Colors.Black);
                yyy.Foreground = new SolidColorBrush(Colors.Black);
                name.Foreground = new SolidColorBrush(Colors.Black);
                Places.Background = new SolidColorBrush(Colors.Azure);
                Time.Foreground = new SolidColorBrush(Colors.Black);
                modeS = "light";
            }
        }
        public void streetChange() // запрос заведений из базы данных по критерию улицы
        {
            string s1 = "'" + street_name + "'";
            SQLiteConnection conn = new SQLiteConnection("Data Source=places.db; Version=3;");
            SQLiteCommand sqlcmd = conn.CreateCommand();
            conn.Open();
            sqlcmd.CommandText = "SELECT Заведение,Тип,Дом,Описание,Время_Работы FROM places_1 WHERE Улица LIKE " + s1;
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlcmd.CommandText, conn);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            Places.ItemsSource = ds.Tables[0].DefaultView;
            conn.Close();
            street.Content = street_name;
        }
        public void imageChange() // смена изображения
        {
            try
            {
                String stringPath = "Images/" + x + y + ".jpg";
                MapView.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
            }
            catch
            {
                String stringPath = "Assets/empty.png";
                MapView.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
            }
        }
        public void streetCheck() // проверка улицы по координатам
        {
            if (x >= 0 && x <= 1 && y <= 60 && y >= 1)
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

        public void moveUp() // передвижение вперед
        {
            y++;
            consty.Content = y;
            streetCheck();
            imageChange();
        }
        public void moveDown() // передвижение назад
        {
            y--;
            consty.Content = y;
            streetCheck();
            imageChange();
        }
        public void moveRight() // передвижение вправо
        {
            x++;
            constx.Content = x;
            streetCheck();
            imageChange();
        }
        public void moveLeft() // передвижение влево
        {
            x--;
            constx.Content = x;
            streetCheck();
            imageChange();
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
            try
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
            catch // если файл сохранения отсутствует, программа создаст новый
            {
                using (StreamWriter writer = new StreamWriter(@"./save.save"))
                {
                    writer.WriteLine(0);
                    writer.WriteLine(0);
                    writer.WriteLine(0);
                    writer.WriteLine(0);
                }
            }
        }
        public void DragWindow(object sender, MouseButtonEventArgs e) // перемещение окна с помощью мыши
        {
            this.DragMove();
        }
        public void closeB_Red(object sender, MouseEventArgs e) 
        {
            String stringPath = "Assets/close-enter.png";
            close_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
        }
        public void closeB_Black(object sender, MouseEventArgs e)
        {
            String stringPath = "Assets/close.png";
            close_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
        }
        public void maxB_Blue(object sender, MouseEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                String stringPath = "Assets/max-blue.png";
                max_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;

            }
            else
            {
                String stringPath = "Assets/max2-blue.png";
                max_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
            }
        }
        public void maxB_Black(object sender, MouseEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                String stringPath = "Assets/max-black.png";
                max_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;

            }
            else
            {
                String stringPath = "Assets/max2-black.png";
                max_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
            }
        }
        public void minB_Blue(object sender, MouseEventArgs e)
        {
            String stringPath = "Assets/min-blue.png";
            min_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
        }
        public void minB_Black(object sender, MouseEventArgs e)
        {
            String stringPath = "Assets/min-black.png";
            min_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
        }
        public void modeB_Yellow(object sender, MouseEventArgs e)
        {
            String stringPath = "Assets/" + modeS + "-yellow.png";
            mode_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
        }
        public void modeB_Black(object sender, MouseEventArgs e)
        {
            String stringPath = "Assets/" + modeS + "-black.png";
            mode_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
        }
        public void helpB_Blue(object sender, MouseEventArgs e)
        {
            String stringPath = "Assets/help-blue.png";
            help_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
        }
        public void helpB_Black(object sender, MouseEventArgs e)
        {
            String stringPath = "Assets/help-black.png";
            help_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
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
            else if (e.Key == Key.F1)
            {
                help();
            }
            else if (e.Key == Key.X)
            {
                cordsChange();
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
        private void left_upB_Click(object sender, RoutedEventArgs e)
        {
            moveUp();
            moveLeft();
        }

        private void left_downB_Click(object sender, RoutedEventArgs e)
        {
            moveDown();
            moveLeft();
        }

        private void right_downB_Click(object sender, RoutedEventArgs e)
        {
            moveRight();
            moveDown();
        }

        private void right_upB_Click(object sender, RoutedEventArgs e)
        {
            moveRight();
            moveUp();
        }


        private void closeB_Click(object sender, RoutedEventArgs e) 
        {
            if (y1 != y || x1 != x || mode1 != mode || cordsv1 != cordsv)
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
                Time.Visibility = Visibility.Visible;
                String stringPath = "Assets/max2-black.png";
                max_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;

            }
            else
            {
                this.WindowState = WindowState.Normal;
                Time.Visibility = Visibility.Hidden;
                String stringPath = "Assets/max-black.png";
                max_im.Source = new ImageSourceConverter().ConvertFromString(stringPath) as ImageSource;
            }
        }

        private void minB_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void cordsB_Click(object sender, RoutedEventArgs e)
        {
            cordsChange();      
        }
        public void cordsChange()
        {
            if (cordsv == 0)
            {
                xxx.Visibility = Visibility.Visible;
                yyy.Visibility = Visibility.Visible;
                constx.Visibility = Visibility.Visible;
                consty.Visibility = Visibility.Visible;
                cordsv = 1;
            }
            else if (cordsv == 1)
            {
                xxx.Visibility = Visibility.Hidden;
                yyy.Visibility = Visibility.Hidden;
                constx.Visibility = Visibility.Hidden;
                consty.Visibility = Visibility.Hidden;
                cordsv = 0;
            }
        }
        public void cordsCheck()
        {
            if (cordsv == 1)
            {
                xxx.Visibility = Visibility.Visible;
                yyy.Visibility = Visibility.Visible;
                constx.Visibility = Visibility.Visible;
                consty.Visibility = Visibility.Visible;
            }
            else if (cordsv == 0)
            {
                xxx.Visibility = Visibility.Hidden;
                yyy.Visibility = Visibility.Hidden;
                constx.Visibility = Visibility.Hidden;
                consty.Visibility = Visibility.Hidden;
            }
        }
        public void help() // вызов справки
        {
            Help help = new Help();
            help.Show();
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
            help();
        }
        public void cordsShow()
        {
            constx.Content = x;
            consty.Content = y;
        }
        public void timeShow() // показ времени
        {
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate { Time.Content = DateTime.Now.ToString("HH:mm:ss"); }, this.Dispatcher);
        }

        public MainWindow()
        {
            load();
            InitializeComponent();
            imageChange();
            streetCheck();
            modeCheck();
            cordsShow();
            cordsCheck();
            timeShow();
        }
    }
}
