using Microsoft.Office.Interop.Excel;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace _2april
{
    /// <summary>
    /// Interaction logic for OutPatient_bill.xaml
    /// </summary>
    public partial class OutPatient_bill : UserControl
    {
        public int id;
        public string name;
        public string Date;
        public int consultation_charges;
        public string doctor_name;
        public int total;
        public int billno;
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        public OutPatient_bill()
        {
            InitializeComponent();
        }
        public void add_OutpatientBilling()
        {
            id = Convert.ToInt32(id_p.Text);
            name = name_p.Text;
            Date = date_p.Text;
            doctor_name = doctor.Text;
            billno = Convert.ToInt32(bill_no.Text);
            consultation_charges = Convert.ToInt32(consult_charges.Text);
            total = Convert.ToInt32(tot.Text);
        }
        public void foo()
        {
            int a;
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();

            cmd = new SqlCommand("select max(BillNo) from OutPatientBilling", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == null)
                {
                    bill_no.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    bill_no.Text = a.ToString();
                }
                con.Close();
            }
        }

        public void db_to_box()
        {

            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();

            cmd = new SqlCommand("select * from OutPatient where ID like '" + id_p.Text + "%'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                doctor.Text = dr["DoctorName"].ToString();
                name_p.Text = dr["Name"].ToString();
                date_p.Text = dr["Date"].ToString();

                con.Close();
            }
            else
            {
                MessageBox.Show("data not found");
            }
        }
        public void show()
        {
            add_OutpatientBilling();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();



            cmd.CommandText = "SET IDENTITY_INSERT OutPatientBilling ON insert into OutPatientBilling (Name,Date,DoctorName,ConsultationCharges,Total,BillNo,ID) values(@name,@Date,@doctor_name,@consultation_charges,@total,@billno,@id)";
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@doctor_name", doctor_name);
            cmd.Parameters.AddWithValue("@consultation_charges", consultation_charges);
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@billno", billno);
            cmd.Parameters.AddWithValue("@id", id);


            //cmd.Connection = con;
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("add successfully");

            }
            binddatagrid();
        }
        private void binddatagrid()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from OutPatientBilling ORDER BY BillNo";
            cmd.Connection = con;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("OutPatientBilling");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db_to_box();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(id_p.Text))
            {
                MessageBox.Show(" Must enter ID");
            }
            else if (string.IsNullOrEmpty(name_p.Text))
            {
                MessageBox.Show(" Must enter Name");
            }
            else if (string.IsNullOrEmpty(doctor.Text))
            {
                MessageBox.Show(" Must enter Doctor Name");

            }
            else if (string.IsNullOrEmpty(consult_charges.Text))
            {
                MessageBox.Show(" Must enter Consultation charges");

            }
            else if (string.IsNullOrEmpty(tot.Text))
            {
                MessageBox.Show(" Must click on total");

            }
            else
            {

                show();
            }
        }

        private void on_load(object sender, RoutedEventArgs e)
        {

            foo();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete  from OutPatientBilling where ID like '" + dlete.Text + "%'", con);
            MessageBox.Show("Deleted By  " + dlete.Text);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("OutPatientBilling");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
            binddatagrid();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            binddatagrid();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < z1.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = z1.Columns[j].Header;
            }
            for (int i = 0; i < z1.Columns.Count; i++)
            {
                for (int j = 0; j < z1.Items.Count; j++)
                {
                    TextBlock b = z1.Columns[i].GetCellContent(z1.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            tot.Text = consult_charges.Text;
        }


    }
}
