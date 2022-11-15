using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inchidere_Proces
{
    public partial class Form1 : Form
    {
        int diferentaOra;
        int diferentaMinut;
        int oraSchimbul1= Properties.Settings.Default.oraSchimbul1;
        int oraSchimbul2= Properties.Settings.Default.oraSchimbul2;
        int oraSchimbul3= Properties.Settings.Default.oraSchimbul3;
        int minutSchimbul123 = Properties.Settings.Default.minutSchimbul123;
        //int minutSchimbul2 = Properties.Settings.Default.minutSchimbul2;
        //int minutSchimbul3 = Properties.Settings.Default.minutSchimbul3;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IncarcareSetari();

        }

        public int Ora()
        {
           int ora = DateTime.Now.Hour;
            return ora;
        }
        public int Minut()
        {
            int minut = DateTime.Now.Minute;
            return minut;

        }

        public void IncarcareSetari()
        {
            txt_schimbul1.Text = oraSchimbul1.ToString() +" : "+ minutSchimbul123.ToString();
            txt_schimbul2.Text = oraSchimbul2.ToString() + " : "+ minutSchimbul123.ToString(); ;
            txt_schimbul3.Text = oraSchimbul3.ToString() + " : "+ minutSchimbul123.ToString(); ;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((Ora() == oraSchimbul1 || Ora() == oraSchimbul2 || Ora() == oraSchimbul3) && Minut() == minutSchimbul123)
            {
                var procesDeInchis = Process.GetProcesses().Where(pro => pro.ProcessName == "notepad"); 

                foreach (var process in procesDeInchis)
                {
                    process.Kill();
                }
                RestartCalc();
            }
        }

        private int DiferentaOra()
        {
            if (Ora() < (oraSchimbul1 + 1))
            {
                diferentaOra = (oraSchimbul1) - Ora();
            }
            else if ((Ora() >= oraSchimbul1 + 1) && (Ora() < oraSchimbul2 + 1))
            {
                diferentaOra = oraSchimbul2 - Ora();
            }
            else if (Ora() >= oraSchimbul2 + 1 && Ora() < oraSchimbul3 + 1)
            {
                diferentaOra = oraSchimbul3 - Ora();
            }
            return diferentaOra;
        }
        private int DiferentaMinut()
        {
            if (Minut() <= 55)
            {
                diferentaMinut = 55 - Minut();
            }
            else
            {
                diferentaMinut =60+55- Minut();
            }
           
            return diferentaMinut;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            label1.Text = DiferentaOra().ToString()+" ore - "+ DiferentaMinut().ToString()+" minute.";
        }
        private void Form_Shown(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Minimized;

            
            this.Hide();
        }
        private void RestartCalc()
        {
            System.Diagnostics.Process.Start("shutdown.exe", "-r -t 30 ");
        }
    }
}
