using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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

namespace Mastermind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string TitelAppears1;
        private string TitelAppears2;
        private string TitelAppears3;
        private string TitelAppears4;
        int attempts = 0;

        private DispatcherTimer timer;
        DateTime clicked;
        TimeSpan elapsedTime;
        public MainWindow()
        {
            InitializeComponent();
            TitelAppearsAbove();

            ComboBox1.Items.Add("rood");
            ComboBox1.Items.Add("geel");
            ComboBox1.Items.Add("groen");
            ComboBox1.Items.Add("oranje");
            ComboBox1.Items.Add("wit");
            ComboBox1.Items.Add("blauw");

            ComboBox2.Items.Add("rood");
            ComboBox2.Items.Add("geel");
            ComboBox2.Items.Add("groen");
            ComboBox2.Items.Add("oranje");
            ComboBox2.Items.Add("wit");
            ComboBox2.Items.Add("blauw");

            ComboBox3.Items.Add("rood");
            ComboBox3.Items.Add("geel");
            ComboBox3.Items.Add("groen");
            ComboBox3.Items.Add("oranje");
            ComboBox3.Items.Add("wit");
            ComboBox3.Items.Add("blauw");

            ComboBox4.Items.Add("rood");
            ComboBox4.Items.Add("geel");
            ComboBox4.Items.Add("groen");
            ComboBox4.Items.Add("oranje");
            ComboBox4.Items.Add("wit");
            ComboBox4.Items.Add("blauw");


            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += startcountdown;
        }

        private void startcountdown(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - clicked;
            Tijd.Text = $"{elapsedTime.Seconds}:{elapsedTime.Milliseconds.ToString().PadLeft(3, '0')}";
        }

        public void TitelAppearsAbove()
        {
            Random rnd = new Random();
            string[] TitelAppears = new string[] { "rood", "geel", "groen", "oranje", "wit", "blauw" };
            TitelAppears1 = TitelAppears[rnd.Next(0, TitelAppears.Length)];
            TitelAppears2 = TitelAppears[rnd.Next(0, TitelAppears.Length)];
            TitelAppears3 = TitelAppears[rnd.Next(0, TitelAppears.Length)];
            TitelAppears4 = TitelAppears[rnd.Next(0, TitelAppears.Length)];

            this.Title = $"Mastermind ({TitelAppears1},{TitelAppears2},{TitelAppears3},{TitelAppears4})";
        }

        private void toggledebug(DebuggerDisplayAttribute displayAttribute) 
        {
            Debug.Text = "Hallo";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            clicked = DateTime.Now;

            string kleur1 = ComboBox1.SelectedItem?.ToString();
            string kleur2 = ComboBox2.SelectedItem?.ToString();
            string kleur3 = ComboBox3.SelectedItem?.ToString();
            string kleur4 = ComboBox4.SelectedItem?.ToString();


            attempts++;
            this.Title = $"Mastermind ({TitelAppears1},{TitelAppears2},{TitelAppears3},{TitelAppears4}, poging: {attempts})";

            string[] correcteCode = { TitelAppears1, TitelAppears2, TitelAppears3, TitelAppears4 };
            string[] gokken = { kleur1, kleur2, kleur3, kleur4 };

            // reset als er iets niet meer klopt
            ResetBorder();


            for (int i = 0; i < 4; i++)
            {
                if (gokken[i] == correcteCode[i])
                {

                    SetBorderColor(i, Brushes.DarkRed);
                }
                else if (correcteCode.Contains(gokken[i]))
                {

                    SetBorderColor(i, Brushes.Wheat);
                }
            }
        }

        private void ResetBorder()
        {
            kleur1Border.BorderBrush = Brushes.Gray;
            kleur2Border.BorderBrush = Brushes.Gray;
            kleur3Border.BorderBrush = Brushes.Gray;
            kleur4Border.BorderBrush = Brushes.Gray;
        }

        private void SetBorderColor(int index, Brush color)
        {
            switch (index)
            {
                case 0:
                    kleur1Border.BorderBrush = color;
                    break;
                case 1:
                    kleur2Border.BorderBrush = color;
                    break;
                case 2:
                    kleur3Border.BorderBrush = color;
                    break;
                case 3:
                    kleur4Border.BorderBrush = color;
                    break;
            }
        }
        private void SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            // pakt wat de gelescteerde kleur is
            string kleur1 = ComboBox1.SelectedItem.ToString();
            if (kleur1 == null) return;
            switch (kleur1)
            {
                case "rood":
                    Kleur1.Background = Brushes.Red;
                    break;

                case "geel":
                    Kleur1.Background = Brushes.Yellow;
                    break;

                case "groen":
                    Kleur1.Background = Brushes.Green;
                    break;

                case "oranje":
                    Kleur1.Background = Brushes.Orange;
                    break;

                case "wit":
                    Kleur1.Background = Brushes.White;
                    break;

                case "blauw":
                    Kleur1.Background = Brushes.Blue;
                    break;
            }
            TextBlock1.Text = $"Gekozen kleur: {kleur1}";
        }
        private void SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            string kleur2 = ComboBox2.SelectedItem.ToString();

            if (kleur2 == null) return;
            switch (kleur2)
            {
                case "rood":
                    Kleur2.Background = Brushes.Red;
                    break;

                case "geel":
                    Kleur2.Background = Brushes.Yellow;
                    break;

                case "groen":
                    Kleur2.Background = Brushes.Green;
                    break;

                case "oranje":
                    Kleur2.Background = Brushes.Orange;
                    break;

                case "wit":
                    Kleur2.Background = Brushes.White;
                    break;

                case "blauw":
                    Kleur2.Background = Brushes.Blue;
                    break;
            }
            TextBlock2.Text = $"Gekozen kleur: {kleur2}";
        }
        private void SelectionChanged3(object sender, SelectionChangedEventArgs e)
        {
            string kleur3 = ComboBox3.SelectedItem.ToString();

            if (kleur3 == null) return;
            switch (kleur3)
            {
                case "rood":
                    Kleur3.Background = Brushes.Red;
                    break;

                case "geel":
                    Kleur3.Background = Brushes.Yellow;
                    break;

                case "groen":
                    Kleur3.Background = Brushes.Green;
                    break;

                case "oranje":
                    Kleur3.Background = Brushes.Orange;
                    break;

                case "wit":
                    Kleur3.Background = Brushes.White;
                    break;

                case "blauw":
                    Kleur3.Background = Brushes.Blue;
                    break;
            }
            TextBlock3.Text = $"Gekozen kleur: {kleur3}";
        }

        private void SelectionChanged4(object sender, SelectionChangedEventArgs e)
        {
            string kleur4 = ComboBox4.SelectedItem.ToString();
            if (kleur4 == null) return;
            switch (kleur4)
            {
                case "rood":
                    Kleur4.Background = Brushes.Red;
                    break;

                case "geel":
                    Kleur4.Background = Brushes.Yellow;
                    break;

                case "groen":
                    Kleur4.Background = Brushes.Green;
                    break;

                case "oranje":
                    Kleur4.Background = Brushes.Orange;
                    break;

                case "wit":
                    Kleur4.Background = Brushes.White;
                    break;

                case "blauw":
                    Kleur4.Background = Brushes.Blue;
                    break;
            }
            TextBlock4.Text = $"Gekozen kleur: {kleur4}";
        }



    }
}
