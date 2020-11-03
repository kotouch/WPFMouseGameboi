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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using System.Windows.Forms;
    using MessageBox = System.Windows.Forms.MessageBox;
    using System.Threading;
using System.IO;

namespace WPFMouseGame
    {
    
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        public partial class MainWindow : Window
        {
            public static Random _random = new Random();
            int playerScore = 0;
            public static int xMouseLocation = 20;
            public static int yMouseLocation = 20;

            public MainWindow()
            {
                InitializeComponent();

                Thread crazyMouseThread = new Thread(new ThreadStart(CrazyMouseThread));
                crazyMouseThread.Start();


            StreamReader sr = new StreamReader("Test_File.txt");

            var line = sr.ReadLine();

            MessageBox.Show(line);

        }

            public void CrazyMouseThread()
            {
                int moveX = 0;
                int moveY = 0;

                while (true)
                {
                    moveX = _random.Next(xMouseLocation) - (xMouseLocation / 2);
                    moveY = _random.Next(yMouseLocation) - (yMouseLocation / 2);

                    System.Windows.Forms.Cursor.Position = new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X + moveX, System.Windows.Forms.Cursor.Position.Y + moveY);
                    Thread.Sleep(50);
                }
            }

            public void xTheButton_Click(object sender, RoutedEventArgs e)
            {
                if (xTheButton.Height <= 100)
                {
                MessageBox.Show("K get outta here tryhard.");

                xMouseLocation = 1;
                yMouseLocation = 1;


            }
                else
                {
                    //Increment Score
                    playerScore++;
                    xMyScore.Text = playerScore.ToString();

                    //Increment Randomness
                    xMouseLocation = xMouseLocation + 2;
                    yMouseLocation = yMouseLocation + 2;

                    //Shrink the Target
                    xTheButton.Height = xTheButton.Height - 10;
                    xTheButton.Width = xTheButton.Width - 14;
                }

            }

        private void Time_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();
        }

        private int increment = 0;
        private void dtTicker(object sender, EventArgs e)
        {
            increment++;

            Time.Content = increment.ToString();

            if (increment == 10)
            {
                MessageBox.Show("YOU LOST TRY AGAIN, sPress Enter to continue.");

                xMouseLocation = 1;
                yMouseLocation = 1;

                
            }
        }

        private void SaveScore_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter File = new StreamWriter("Test_File.txt");
            File.Write($"{playerScore}");
            File.Close();
            Environment.Exit(0);
        }
    }
}