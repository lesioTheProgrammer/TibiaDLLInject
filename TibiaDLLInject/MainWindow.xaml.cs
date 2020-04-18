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

namespace TibiaDLLInject

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tibia_Inject m_client;
        Tibia_Memory_Reader memory_reader; // on jest nulem

        public MainWindow()
        {
            InitializeComponent();
            Tibia_Debug.SetLogSpace(this.textBox);

            memory_reader = new Tibia_Memory_Reader();
        }

        bool _stopAsyncTask = true;
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
            if (listBox.SelectedIndex < 0 ||
                listBox.SelectedIndex >= Process.GetProcessesByName("tibia").Length)
                return;
            
            m_client = new Tibia_Inject(Process.GetProcessesByName("tibia")[listBox.SelectedIndex]);
            Tibia_Debug.Log("Injected into :: " + m_client.cHandle.ToString());

            var task = RefresItems(TimeSpan.FromSeconds(10));

            button1.IsEnabled = false;
        }



        public async Task RefresItems(TimeSpan interval)
        {
            var cHandleOnceForAll = m_client.cHandle;
            var baseAddress = m_client.baseAddress;

            _stopAsyncTask = true;

            while (_stopAsyncTask)
            {
                onBotRefresh(cHandleOnceForAll, baseAddress);
                await Task.Delay(interval);
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

            Tibia_Debug.Log(cHandle.ToString());
            Tibia_Debug.Log(baseAddr.ToString());






        }
    }
}
