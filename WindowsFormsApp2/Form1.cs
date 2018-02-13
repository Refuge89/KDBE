using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KargatumDBE
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            statusStrip1.Items.Clear();
            
            // Попытка подключения по сохранённым данным
            string bd = Properties.Settings.Default.general_bd;
            string host = Properties.Settings.Default.general_host;
            string login = Properties.Settings.Default.general_root;
            string password = Properties.Settings.Default.general_pass;
            try
            {
                statusStrip1.Items.Clear();
                statusStrip1.Items.Add("Состояние подключения: Нет сохранённых данных");
            }
            catch
            {
                string Connect = string.Format("Database={0};Data Source={1};User Id={2};Password={3}", bd, host, login, password);
                MySqlConnection myConnection = new MySqlConnection(Connect);
                try
                {
                    myConnection.Open();
                    statusStrip1.Items.Clear();
                    statusStrip1.Items.Add("Состояние подключения: Есть подключение!");
                }
                catch
                {
                    statusStrip1.Items.Clear();
                    statusStrip1.Items.Add("Состояние подключения: Подключения нет!");
                }
            }
        }

        private void КоннектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 ifrm = new Form2();
            ifrm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Подключение
            string bd       = Properties.Settings.Default.general_bd;
            string host     = Properties.Settings.Default.general_host;
            string login    = Properties.Settings.Default.general_root;
            string password = Properties.Settings.Default.general_pass;
            string Connect  = string.Format("Database={0};Data Source={1};User Id={2};Password={3}", bd, host, login, password);
            MySqlConnection myConnection = new MySqlConnection(Connect);
            myConnection.Open();

            string entry_str = textBox1.Text;
            string res_myCommand = string.Format("SELECT entry, name, description, displayid FROM `item_template` WHERE `entry` = '{0}'", entry_str);

            MySqlCommand myCommand = new MySqlCommand(res_myCommand, myConnection);
            try
            {
                statusStrip1.Items.Clear();
                statusStrip1.Items.Add(res_myCommand);
            }
            catch (MySqlException ex)
            {
                statusStrip1.Items.Clear();
                statusStrip1.Items.Add(ex.ToString());
            }
            MySqlDataReader reader = myCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    textBox1.Text = reader[0].ToString(); // Номер
                    textBox2.Text = reader[1].ToString(); // Название
                    textBox3.Text = reader[2].ToString(); // Подпись
                    textBox4.Text = reader[3].ToString(); // Номер дисплея
                }
                reader.Close(); // закрываем reader
            }
            else
            {
                string error_entry = string.Format("Предмет с номером '{0}' не найден!", entry_str);
                statusStrip1.Items.Clear();
                statusStrip1.Items.Add(error_entry);
            }        
            myConnection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
