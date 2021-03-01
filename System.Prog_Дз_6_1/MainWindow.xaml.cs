using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace System.Prog_Дз_6_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Task<int> task1;
        Task<int> task2;
        Task<int> task3;
        Task<int> task4;
        Task<int> task5;

        public static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        public CancellationToken token = cancelTokenSource.Token;


        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            task1 = new Task<int>(
                () => NumberOfOffers()
            );

            task2 = task1.ContinueWith(NumberOfCharacters);
            task3 = task2.ContinueWith(NumberOfWords);
            task4 = task3.ContinueWith(TheNumberOfInterrogativeSentences);
            task5 = task4.ContinueWith(NumberOfExclamationSentences);
            Task task6 = task5.ContinueWith(IfFileOrListBox);
            task1.Start();



        }
        public void IfFileOrListBox(Task t)
        {
            Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (WriteInFileCheckBox.IsChecked == true)
                {
                    using (StreamWriter sw = new StreamWriter("Info.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine($"NumberOfOffers {task1.Result}");
                        sw.WriteLine($"NumberOfCharacters {task2.Result}");
                        sw.WriteLine($"NumberOfWords {task3.Result}");
                        sw.WriteLine($"TheNumberOfInterrogativeSentences {task4.Result}");
                        sw.WriteLine($"NumberOfExclamationSentences {task5.Result}");
                    }
                }
                else
                {
                    listBox.Items.Add($"NumberOfOffers {task1.Result}");
                    listBox.Items.Add($"NumberOfCharacters {task2.Result}");
                    listBox.Items.Add($"NumberOfWords {task3.Result}");
                    listBox.Items.Add($"TheNumberOfInterrogativeSentences {task4.Result}");
                    listBox.Items.Add($"NumberOfExclamationSentences {task5.Result}");
                }
            }));

        }
        public int NumberOfOffers()
        {
            if (token.IsCancellationRequested)
            {
                MessageBox.Show("Operation Stop");
                return 0;
            }
            string msg = "";
            Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                msg = textBox.Text;
            }));
            string[] words = msg.Split(new char[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
        public int NumberOfCharacters(Task t)
        {
            if (token.IsCancellationRequested)
            {
                MessageBox.Show("Operation Stop");
                return 0;
            }
            string msg = "";
            Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                msg = textBox.Text;
            }));
            return msg.Length;
        }
        public int NumberOfWords(Task t)
        {
            if (token.IsCancellationRequested)
            {
                MessageBox.Show("Operation Stop");
                return 0;
            }
            string msg = "";
            Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                msg = textBox.Text;
            }));
            string[] words = msg.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
        public int TheNumberOfInterrogativeSentences(Task t)
        {
            if (token.IsCancellationRequested)
            {
                MessageBox.Show("Operation Stop");
                return 0;
            }
            string msg = "";
            Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                msg = textBox.Text;
            }));
            string[] words = msg.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
        public int NumberOfExclamationSentences(Task t)
        {
            if (token.IsCancellationRequested)
            {
                MessageBox.Show("Operation Stop");
                return 0;
            }
            string msg = "";
            Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                msg = textBox.Text;
            }));
            string[] words = msg.Split(new char[] { '!' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        private void Task2ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            cancelTokenSource.Cancel();
        }

        private void Task3Button_Click(object sender, RoutedEventArgs e)
        {
            task1 = new Task<int>(
                () => NumberOfOffers()
            );

            task2 = task1.ContinueWith(NumberOfCharacters);
            task3 = task2.ContinueWith(NumberOfWords);
            task4 = task3.ContinueWith(TheNumberOfInterrogativeSentences);
            task5 = task4.ContinueWith(NumberOfExclamationSentences);
            Task task6 = task5.ContinueWith(IfFileOrListBox);
            task1.Start();
        }
        public void IfFileOrListBoxForTask3(Task t)
        {
            Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (WriteInFileCheckBox.IsChecked == true)
                {
                    using (StreamWriter sw = new StreamWriter("Info.txt", true, System.Text.Encoding.Default))
                    {
                        if (checkBox1.IsChecked == true)
                            sw.WriteLine($"NumberOfOffers {task1.Result}");
                        if (checkBox2.IsChecked == true)
                            sw.WriteLine($"NumberOfCharacters {task2.Result}");
                        if (checkBox3.IsChecked == true)
                            sw.WriteLine($"NumberOfWords {task3.Result}");
                        if (checkBox4.IsChecked == true)
                            sw.WriteLine($"TheNumberOfInterrogativeSentences {task4.Result}");
                        if (checkBox5.IsChecked == true)
                            sw.WriteLine($"NumberOfExclamationSentences {task5.Result}");
                    }
                }
                else
                {
                    if (checkBox1.IsChecked == true)
                        listBox.Items.Add($"NumberOfOffers {task1.Result}");
                    if (checkBox2.IsChecked == true)
                        listBox.Items.Add($"NumberOfCharacters {task2.Result}");
                    if (checkBox3.IsChecked == true)
                        listBox.Items.Add($"NumberOfWords {task3.Result}");
                    if (checkBox4.IsChecked == true)
                        listBox.Items.Add($"TheNumberOfInterrogativeSentences {task4.Result}");
                    if (checkBox5.IsChecked == true)
                        listBox.Items.Add($"NumberOfExclamationSentences {task5.Result}");
                }
            }));

        }
    }
}
