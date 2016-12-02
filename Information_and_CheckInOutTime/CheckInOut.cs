using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Information_and_CheckInOutTime
{
    public partial class CheckInOut : Form
    {
        public CheckInOut()
        {
            InitializeComponent();
            document.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }

        private DateTime start = DateTime.Now;
        private DateTime end;
        private TimeSpan t;
        private string res = "";
        private double pay = 0;
        PrintDocument document = new PrintDocument();
        PrintDialog dialog = new PrintDialog();

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            txtRes.Text = "";
            txtCheckOut.Text = "";
            string currentDate = DateTime.Now.ToString("dd/MM/yyyy  HH\\:mm\\:ss");
            txtCheckIn.Text = currentDate;
            end = DateTime.Now;
            t = end.Subtract(start);

            btnCheckOut.Enabled = true;
            btnCheckIn.Enabled = false;
            btnPrint.Enabled = false;
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            int day = (t.Days * 24) * 60;
            int hour = t.Hours * 60;
            int mn = t.Minutes;
            int totalMin = day + hour + mn;
            int myMin = 0;

            txtRes.Text = "";
            string currentDate = DateTime.Now.ToString("dd/MM/yyyy  HH\\:mm\\:ss");
            txtCheckOut.Text = currentDate;

            btnCheckOut.Enabled = false;
            btnCheckIn.Enabled = true;
            btnPrint.Enabled = true;

            if (totalMin <= 120)
            {
                pay = 0;
            }

            if (totalMin > 120)
            {
                myMin = totalMin-120;
                int count = 0;
                if (myMin <= 30)
                {
                    pay = 1000;
                } else
                {
                    for(int i = myMin; i >= 1; i -= 30)
                    {
                        count++;
                    }
                    pay = count * 1000;
                }
               
            }

            //MessageBox.Show("" + totalMin);

            res = " Start date : " + start.ToString("dd/MM/yyyy  HH\\:mm\\:ss")
                + System.Environment.NewLine + " End date : " + end.ToString("dd/MM/yyyy  HH\\:mm\\:ss")
                + System.Environment.NewLine + "--------------------------------------------------------"
                + System.Environment.NewLine + " Usage Time : " + t.Days + "d : " + t.Hours + "h : " + t.Minutes + "m"
                + System.Environment.NewLine + " Free 2 hours. (2*60 = 120mn)"
                + System.Environment.NewLine + " Amount of Paid : " + min2Hour(myMin)
                + System.Environment.NewLine + "--------------------------------------------------------"
                + System.Environment.NewLine + "      Total Paid : " + pay + " Riel";
            txtRes.AppendText(res);
        }

        private void CheckInOut_Load(object sender, EventArgs e)
        {
            timer.Enabled = true;
            //timer.Interval = 1;

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lbTimer.Text = "Date: " + DateTime.Now.ToString("dd/MM/yyyy") + " - Time: " + DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss");
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(txtRes.Text, new Font("Arial", 20, FontStyle.Regular), Brushes.Black, 20, 20);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dialog.Document = document;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                document.Print();
            }
        }

        private string min2Hour(int mn)
        {
            int rMN = 0, rH = 0;
            for(int i = mn; i >= 1; i -= 60)
            {
                if (i >= 60)
                    rH++;
                else
                    rMN = i;
            }
            string HourMin = rH + "h : " + rMN + "mn"; 
            return HourMin;
        }

    }
}
