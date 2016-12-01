using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Information_and_CheckInOutTime
{
    public partial class frmInfo : Form
    {
        public frmInfo()
        {
            InitializeComponent();
        }

        private int MaxDay(int year, int month)
        {
            int maxDay = DateTime.DaysInMonth(year, month);
            return maxDay;
        }

        private void createDay()
        {
            cbDay.Items.Clear();
            int year = int.Parse("" + cbYear.SelectedItem);
            int month = cbMonth.SelectedIndex + 1;
            if (month >= 1)
            {
               int maxDay = DateTime.DaysInMonth(year, month);
               for (int i = 1; i <= maxDay; i++)
                    cbDay.Items.Add(i);
                cbDay.SelectedItem = DateTime.Now.Day;
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string firstname = txtFirstname.Text;
            string lastname = txtLastname.Text;
            string fullname = firstname + " " + lastname;
            string phone = txtPhone.Text;
            string dob = "" + cbDay.SelectedItem + "/" + (cbMonth.SelectedIndex + 1) + "/"+ cbYear.SelectedItem;
            string gender = "";
            if (rdMale.Checked)
                gender = "Male";
            else if (rdFemale.Checked)
                gender = "Female";
            else
                gender = "Other";
            string country = cbCountry.SelectedItem + "";
            string[] rData = { id, fullname, gender, dob, phone, country };
            ListViewItem list = new ListViewItem(rData);
            listPeople.Items.Add(list);

            txtFirstname.Text = "";
            txtLastname.Text = "";
            txtPhone.Text = "";
            txtID.Text = "";
  
        }

        private void frmInfo_Load(object sender, EventArgs e)
        {
            int nYear = DateTime.Now.Year;
            int stYear = nYear - 200;
            int edYear = nYear + 200;
            for (int i = stYear; i <= edYear; i++)
                cbYear.Items.Add(i);
            cbYear.SelectedItem = DateTime.Now.Year;
            cbMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            createDay();
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            createDay();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem list in listPeople.SelectedItems)
                listPeople.Items.Remove(list);
        }
    }
}
