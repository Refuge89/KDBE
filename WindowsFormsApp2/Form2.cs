using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KargatumDBE
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.general_host;   // Хост
            textBox2.Text = Properties.Settings.Default.general_root;   // Пользователь
            textBox3.Text = Properties.Settings.Default.general_pass;   // Пароль
            textBox4.Text = Properties.Settings.Default.general_bd;     // БД
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Переменные
            string host = textBox1.Text;
            string login = textBox2.Text;
            string password = textBox3.Text;
            string bd = textBox4.Text;

            // Подключение
            string Connect = string.Format("Database={0};Data Source={1};User Id={2};Password={3}", bd, host, login, password);
            MySqlConnection myConnection = new MySqlConnection(Connect);
            try
            {
                myConnection.Open();

                // Сохранение переменных
                Properties.Settings.Default.general_host = host;
                Properties.Settings.Default.general_root = login;
                Properties.Settings.Default.general_pass = password;
                Properties.Settings.Default.general_bd = bd;
                Properties.Settings.Default.Save();

                // Закрытие формы при успешном подключении
                Close();
            }
            catch (MySqlException mySqlException)
            {
                MessageBox.Show(mySqlException.Message.ToString());
            }
        }

        private void Connect_BD(string host, string login, string password, string bd)
        {
            string Connect = string.Format("Database={0};Data Source={1};User Id={2};Password={3}", bd, host, login, password);
            MySqlConnection myConnection = new MySqlConnection(Connect);
        }
    }
}
