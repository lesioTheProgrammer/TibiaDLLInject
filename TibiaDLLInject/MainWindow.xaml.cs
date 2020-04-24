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
using System.Diagnostics;
using System.Windows.Forms.Integration;
using System.Threading;
using System.Windows.Forms;
using TibiaDLLInject.Injecting;
using TibiaDLLInject.Injecting.Piping;

namespace TibiaDLLInject

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            Tibia_Debug.SetLogSpace(this.textBox);
            memory_reader = new Tibia_Memory_Reader();
            button2.IsEnabled = false;
        }
        // global Variables:
        #region
        bool _stopAsyncTask = true;
        TibiaProcess tibia_process = new TibiaProcess();
        Tibia_Memory_Reader memory_reader;
        Injector_Using_Dll _instance = new Injector_Using_Dll();
        #endregion
        private void button_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            foreach (var item in Process.GetProcessesByName("tibia"))
            {
                listBox.Items.Add(item.MainWindowTitle);
            }

            _stopAsyncTask = false;
        }



        private  void button1_Click(object sender, RoutedEventArgs e)
        {

            var tibiaInjectedm_client = tibia_process.GetChangingIndexOfProcess(listBox.SelectedIndex);
            Tibia_Debug.Log("Injected into :: " + tibiaInjectedm_client.cHandle.ToString());
            var task = RefresItems(TimeSpan.FromSeconds(0.5), tibiaInjectedm_client);
            button1.IsEnabled = false;
            button2.IsEnabled = true;
            button.Content = "Stop the bot";
        }



        public async Task RefresItems(TimeSpan interval, Tibia_Inject tibiaInjectedm_client)
        {
            _stopAsyncTask = true;
            while (_stopAsyncTask)
            {
                tibiaInjectedm_client = tibia_process.GetChangingIndexOfProcess(listBox.SelectedIndex);
                onBotRefresh(tibiaInjectedm_client.cHandle, tibiaInjectedm_client.baseAddress);
                await Task.Delay(interval);
                Tibia_Debug.ClearLogConsole();
            }
        }

        private void onBotRefresh(IntPtr cHandle, IntPtr baseAddr)
        {
            int hp = memory_reader.GetHealth(cHandle, (Int32)baseAddr);
            int mp = memory_reader.GetMana(cHandle, (Int32)baseAddr);
            int hp_max = memory_reader.GetMaxHealth(cHandle, (Int32)baseAddr);
            int mp_max = memory_reader.GetMaxMana(cHandle, (Int32)baseAddr);

           

             Tibia_Debug.Log(hp.ToString());
             Tibia_Debug.Log(mp.ToString());
             Tibia_Debug.Log(hp_max.ToString());
             Tibia_Debug.Log(mp_max.ToString());
        }



        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // get items from first market dropdown
            var tibiaInjectedm_client = tibia_process.GetChangingIndexOfProcess(listBox.SelectedIndex);
            listBox3.ItemsSource = memory_reader.GetFirstDialogBoxList(tibiaInjectedm_client.cHandle,
                (Int32)tibiaInjectedm_client.baseAddress);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var tibiaInjectedm_client = tibia_process.GetChangingIndexOfProcess(listBox.SelectedIndex);
            string processName = "tibia";
            int index = listBox.SelectedIndex;
            _instance.Inject(processName, index);

            PipeServer.

        }
    }
}
