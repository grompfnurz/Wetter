using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;

namespace Wetter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly string apiKey = "9c8f1f56fae50371dadc4f1e0fafe5c3";
        private string requestURL = "http://api.openweathermap.org/data/2.5/weather";




        public MainWindow()
        {

            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            InitializeComponent();

            UpdateUI("Berlin");
            
            

        }


        public void UpdateUI(string city) {

            WetterKartenEmpfang result = GetWetterDaten(city);

            string finalImage = "sun.png";
            string currentWeather = result.weather[0].main.ToLower();

            if (currentWeather.Contains("cloud"))
            {
                finalImage = "cloud.png";
            }
            else if (currentWeather.Contains("rain"))
            {
                finalImage = "rain.png";
            }
            else if (currentWeather.Contains("snow"))
            {
                finalImage = "snow.png";
            }

            backgroundImage.ImageSource = new BitmapImage(new Uri("Images/" + finalImage, UriKind.Relative));

            labelTemperature.Content = result.main.temp.ToString("F0") + "°C";
            labelInfo.Content = result.weather[0].main;
            
            
            
          




        }




        public WetterKartenEmpfang GetWetterDaten(string city) {
            HttpClient httpClient = new HttpClient();
            var finalUri = requestURL + "?q=" + city + "&appid=" + apiKey + "&units=metric";
            HttpResponseMessage httpResponse = httpClient.GetAsync(finalUri).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            WetterKartenEmpfang wetterKartenEmpfang = JsonConvert.DeserializeObject<WetterKartenEmpfang>(response);
            //Console.WriteLine(wetterKartenEmpfang);
            return wetterKartenEmpfang;

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = searchText.Text;
            UpdateUI(query);
        }
    }
}

