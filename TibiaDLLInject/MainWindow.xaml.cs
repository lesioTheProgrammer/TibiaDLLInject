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

            //keys
            m_input = new Tibia_Input();
            Keys[] _ks = new Keys[] { Keys.F1, Keys.F2,
                Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7,
                Keys.F8, Keys.F9, Keys.F10, Keys.F11, Keys.F12 };
            foreach (Keys _k in _ks)
            {
                comboBox.Items.Add(_k);
                comboBox1.Items.Add(_k);
            }
            _ks = null;

        }
        // global Variables:
        #region
        bool _stopAsyncTask = true;
        Tibia_Inject m_client;
        Tibia_Memory_Reader memory_reader;
        Tibia_Input m_input;
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
            var task = RefresItems(TimeSpan.FromSeconds(0.5));
            button1.IsEnabled = false;
            button2.IsEnabled = true;
            button.Content = "Stop the bot";

        }



        public async Task RefresItems(TimeSpan interval)
        {
            _stopAsyncTask = true;
            while (_stopAsyncTask)
            {
                GetChangingIndexOfProcess();
                onBotRefresh(m_client.cHandle, m_client.baseAddress);
                await Task.Delay(interval);
                Tibia_Debug.ClearLogConsole();
            }
        }

        private void onBotRefresh(IntPtr cHandle, IntPtr baseAddr)
        {
            Tibia_Debug.Log("Injected into :: " + m_client.cHandle.ToString());

            int hp = memory_reader.GetHealth(cHandle, (Int32)baseAddr);
            int mp = memory_reader.GetMana(cHandle, (Int32)baseAddr);
            int hp_max = memory_reader.GetMaxHealth(cHandle, (Int32)baseAddr);
            int mp_max = memory_reader.GetMaxMana(cHandle, (Int32)baseAddr);

            if (checkBox.IsChecked == true)
            {
                healthChecker(hp, hp_max);
            }

             Tibia_Debug.Log(hp.ToString());
             Tibia_Debug.Log(mp.ToString());
             Tibia_Debug.Log(hp_max.ToString());
             Tibia_Debug.Log(mp_max.ToString());
        }



        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // get items from first market dropdown
            GetChangingIndexOfProcess();
            listBox3.ItemsSource = memory_reader.GetFirstDialogBoxList(m_client.cHandle,
                (Int32)m_client.baseAddress);
        }


        private void healthChecker(int _hp, int _hpmax)
        {
            // bieda klawiszowa do tego nie dzialajaca xddd
            int _hpBar1;
            int _hpBar2;
            if (!int.TryParse(textBox1.Text, out _hpBar1))
                _hpBar1 = int.MaxValue;

            if (!int.TryParse(textBox2.Text, out _hpBar2))
                _hpBar2 = int.MaxValue;

            if (_hp <= _hpBar1 && _hp > _hpBar2)
                m_input.SendKeystroke(m_client.cHWND, (Keys)comboBox.SelectedItem);
            else if (_hp <= _hpBar2 && _hp > 0)
                m_input.SendKeystroke(m_client.cHWND, (Keys)comboBox1.SelectedItem);
        }
    }
}
