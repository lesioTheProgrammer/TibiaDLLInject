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
        

        public MainWindow()
        {
            InitializeComponent();
            Tibia_Debug.SetLogSpace(this.textBox);
            memory_reader = new Tibia_Memory_Reader();
        }
        // global Variables:
        #region
        bool _stopAsyncTask = true;
        Tibia_Inject m_client;
        Tibia_Memory_Reader memory_reader; 
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


        private int GetChangingIndexOfProcess()
        {  
            // mclient Update method
            if (listBox.SelectedIndex < 0 ||
               listBox.SelectedIndex >= Process.GetProcessesByName("tibia").Length)
                return listBox.SelectedIndex;

            m_client = new Tibia_Inject(Process.GetProcessesByName("tibia")[listBox.SelectedIndex]);
            Tibia_Debug.Log("Injected into :: " + m_client.cHandle.ToString());
            return listBox.SelectedIndex;
        }

        private  void button1_Click(object sender, RoutedEventArgs e)
        {
            GetChangingIndexOfProcess();
            var task = RefresItems(TimeSpan.FromSeconds(1));
            button1.IsEnabled = false;
        }



        public async Task RefresItems(TimeSpan interval)
        {
            _stopAsyncTask = true;
            while (_stopAsyncTask)
            {
                GetChangingIndexOfProcess();
                onBotRefresh(m_client.cHandle, m_client.baseAddress);
                await Task.Delay(interval);
            }
        }

        private void onBotRefresh(IntPtr cHandle, IntPtr baseAddr)
        {
            Tibia_Debug.Log("Injected into :: " + m_client.cHandle.ToString());

            int hp = memory_reader.GetHealth(cHandle, (Int32)baseAddr);
            int mp = memory_reader.GetMana(cHandle, (Int32)baseAddr);
            int hp_max = memory_reader.GetMaxHealth(cHandle, (Int32)baseAddr);
            int mp_max = memory_reader.GetMaxMana(cHandle, (Int32)baseAddr);

             Tibia_Debug.Log(hp.ToString());
             Tibia_Debug.Log(mp.ToString());
             Tibia_Debug.Log(hp_max.ToString());
             Tibia_Debug.Log(mp_max.ToString());

            Tibia_Debug.Log("End of stage");
        }
    }
}
