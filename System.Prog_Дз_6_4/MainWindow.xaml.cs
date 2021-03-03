using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace System.Prog_Дз_6_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = @"C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SourceTextBlock.Text = dialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog2 = new CommonOpenFileDialog();
            dialog2.InitialDirectory = @"C:\\Users";
            dialog2.IsFolderPicker = true;
            if (dialog2.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ReceiverTextBlock.Text = dialog2.FileName;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string folderName1 = SourceTextBlock.Text;
            string folderName2 = ReceiverTextBlock.Text;

            Task task = new Task(() => Task4(folderName1, folderName2));
            task.Start();
            task.Wait();
        }
        private void Task4(string folderName1, string folderName2)
        {
            DirectoryInfo directory = new DirectoryInfo(folderName1);
            FileInfo[] files = directory.GetFiles("*.txt");

            Regex copyFileRegex = new Regex(@".*[(]\d[)]");

            FileInfo file;
            string str = "";
            string text = "";
            for (int i = 0; i < files.Length; i++)
            {
                file = files[i];
                if (!copyFileRegex.IsMatch(file.Name))
                {
                    str = file.Name.Remove(file.Name.Length - 4);
                    for (int j = 0; j < files.Length; j++)
                    {
                        if (files[j].Name.Contains(str) && copyFileRegex.IsMatch(files[j].Name))
                        {
                            using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                            {
                                StreamReader sr = new StreamReader(fs);
                                text = sr.ReadToEnd();
                                sr.Close();
                            }
                            using (FileStream fs = new FileStream(folderName2 + "\\" + file.Name, FileMode.Create, FileAccess.Write))
                            {
                                StreamWriter sw = new StreamWriter(fs);
                                sw.WriteLine(text);
                                sw.Close();
                            }
                            using (StreamWriter sw1 = new StreamWriter("Info.txt", true, System.Text.Encoding.Default))
                            {
                                sw1.WriteLine($"{file.Name} copy to {folderName2}");
                            }
                        }
                    }
                }
            }
        }
    }
}
