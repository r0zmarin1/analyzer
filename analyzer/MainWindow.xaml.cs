using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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

namespace analyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ServiceResults> ServicesResults { get; set; }
        public ServiceResults ServiceResults { get; set; }
        private DispatcherTimer timer = null;
        HttpClient httpClient = new HttpClient();



        public MainWindow()
        {
            InitializeComponent();
            httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
            timerStart();
        }

        public void timerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e) //к таймеру относится 
        {
            Thread thread = new Thread(GetResultsFromLedetect);
            thread.Start();
        }
        private void NewAnalys(object sender, RoutedEventArgs e)
        {

        }

        public async void GetResultsFromLedetect()
        {
            var responce = await httpClient.GetAsync($"analyzer/Ledetect");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            else
            {
                var result = await responce.Content.ReadFromJsonAsync<ServiceResults>();
                ServiceResults = new ServiceResults
                {
                    Code = result.Code,
                    PatientId = result.PatientId,
                    Result = result.Result,
                    Progress = result.Progress
                };
                ServicesResults.Add(ServiceResults);
            }
        }
    }
}