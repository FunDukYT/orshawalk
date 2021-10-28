using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public void modeCheck()
        {
            if(MainWindow.mode == 0)
            {
                HWindow.Background = new SolidColorBrush(Colors.White);
                textBox.Foreground = new SolidColorBrush(Colors.Black);
                textBox.Background = new SolidColorBrush(Colors.White);
            }
            else if(MainWindow.mode == 1)
            {
                HWindow.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
                textBox.Foreground = new SolidColorBrush(Colors.White);
                textBox.Background = new SolidColorBrush(Color.FromRgb(57, 57, 57));
            }
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
        private void maxB_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
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
        private void closeB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
        
        public Help()
        {
            InitializeComponent();
            modeCheck();
            FileStream fs = new FileStream(@"./Help.rtf", FileMode.Open);
            textBox.Selection.Load(fs, DataFormats.Rtf);
            fs.Close();
        }

    }
}
