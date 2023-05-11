using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace rfid_with_login
{
    public partial class Form1 : Form
    {
        string portname, tapespeed;
        string[] ports = SerialPort.GetPortNames();

        public Form1()
        {
            InitializeComponent();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            timer1.Start();
            portname = cbPort.Text;
            tapespeed = cbTapeSpeed.Text;

            try
            {
            serialPort1.PortName= portname;
            serialPort1.BaudRate = Convert.ToInt32(tapespeed);

            serialPort1.Open();

            label1.Text = "Connected";
            label1.ForeColor= Color.Green;
            }
            catch 
            {
                serialPort1.Close();
                serialPort1.Open();
                MessageBox.Show("Connection is already open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();

                label1.Text = "Disconnected";
                label1.ForeColor = Color.Red;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string result;
            result = serialPort1.ReadExisting();
            if (result != "")
            {
                lblRfidUid.Text = result;
                if (lblRfidUid.Text == "2172407421")
                {
                    Form2 fr = new Form2();
                    fr.Show();
                    this.Hide();
                }
                
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in ports)
            {
                cbPort.Items.Add(port);
            }
            cbTapeSpeed.Items.Add("2400");
            cbTapeSpeed.Items.Add("4800");
            cbTapeSpeed.Items.Add("9600");
            cbTapeSpeed.Items.Add("19200");
            cbTapeSpeed.Items.Add("115200");

            cbPort.SelectedIndex = 0;
            cbTapeSpeed.SelectedIndex = 2;
        }
    }
}
