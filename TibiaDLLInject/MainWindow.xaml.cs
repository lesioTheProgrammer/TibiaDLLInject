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


namespace TibiaDLLInject

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tibia_Inject m_client;
        Tibia_Memory_Reader memory_reader;

        public MainWindow()
        {
            InitializeComponent();
            Tibia_Debug.SetLogSpace(this.textBox);
            foreach (Process p in Process.GetProcessesByName("tibia"))
                Tibia_Debug.Log(p.Id.ToString());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            foreach (var item in Process.GetProcessesByName("tibia"))
            {
                listBox.Items.Add(item.MainWindowTitle);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex < 0 &&
                listBox.SelectedIndex >= Process.GetProcessesByName("tibia").Length)
                return;

            m_client = new Tibia_Inject(Process.GetProcessesByName("tibia")[listBox.SelectedIndex]);
            Tibia_Debug.Log("Injected into :: " + m_client.cHandle.ToString());


        }

        private void onBotRefresh(object sender, EventArgs e)
        {
            int hp = memory_reader.GetHealth(m_client.cHandle, (Int32)m_client.baseAddress);
            int mp = memory_reader.GetMana(m_client.cHandle, (Int32)m_client.baseAddress);
            int hp_max = memory_reader.GetMaxHealth(m_client.cHandle, (Int32)m_client.baseAddress);
            int mp_max = memory_reader.GetMaxMana(m_client.cHandle, (Int32)m_client.baseAddress);

            Tibia_Debug.Log(hp.ToString());
            Tibia_Debug.Log(mp.ToString());
            Tibia_Debug.Log(hp_max.ToString());
            Tibia_Debug.Log(mp_max.ToString());
        }
    }
}
