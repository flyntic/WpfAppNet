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
using System.Net.Http;
using System.IO;
namespace WpfAppNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client;
        public string _respond;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void LoadPage(object sender, RoutedEventArgs e)
        {
            using (client = new HttpClient())
                try
                {
                    var respond = client.GetAsync(addressPageTextBox.Text);

                    _respond = respond.Result.Content.ReadAsStringAsync().Result;
                    pageTextBox.Text = _respond;
                    resultLabel.Content = respond.Result.StatusCode.ToString();

                }
                catch
                {
                    MessageBox.Show("Неудачная загрузка");
                }
        }
        private async Task<bool> save()
        {
            using (StreamWriter outputFile = new StreamWriter(new FileStream("page.html", FileMode.Create, FileAccess.Write)))
            {
                outputFile.WriteAsync(_respond).Wait();
            }
            return true;
            
        }

        private void SavePage(object sender, RoutedEventArgs e)
        {
            bool result = save().Result;

            if(result==true)
             MessageBox.Show("Файл успешно сохранен");
        }
    }
}
