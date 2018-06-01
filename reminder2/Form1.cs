using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;
namespace reminder2
{
   
    public partial class Form1 : Form
    {
        Timer t = new Timer();
        
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Documents\d_reminder.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 se = new Form2();
            se.Show();
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("reminder2", Application.ExecutablePath.ToString());
        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 se = new Form3();
            se.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            disp_data();
            t.Interval = 1000; 

            t.Tick += new EventHandler(this.t_Tick);
            t.Start(); 
        }
       
        public void t_Tick(object sender, EventArgs e)
        {
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;
            string time = "";
            
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }
            
            label1.Text = time;

            var curtim = time;
            var curdt = DateTime.Now.ToString("MM-dd-yyyy");

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from Table2 where date='" + curdt + "' AND time='" + curtim + "'";

            var taskn = cmd.ExecuteReader();
            if (taskn.HasRows)
            {
                taskn.Read();
                var taskvl = taskn.GetString(0);
                con.Close();
                MessageBox.Show(taskvl);
            }
            con.Close();

            }
       
        public void disp_data()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Table2";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
          con.Close();
          disp_data();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
  
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Table2 where date='" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {}
    }
}
