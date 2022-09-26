using _2april.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace _2april.User_Controls
{
    /// <summary>
    /// Interaction logic for patient_module.xaml
    /// </summary>
    public partial class patient_module : UserControl
    {
        public int id;
        public string name;
        public string address;
        public string gender;
        public string doc_name;
        public string contact_no;
        public string department;
        public string date;
        public int age;
        public string dp;
        public string doc;
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public patient_module()
        {
            InitializeComponent();
        }
        public void add_Outpatient_reg()
        {
            id = Convert.ToInt32(id_p.Text);
            name = name_p.Text;
            address = address_p.Text;
            gender = this.gender_p.SelectionBoxItem.ToString();
            doc_name = this.doctor_name_p.SelectionBoxItem.ToString();
            contact_no = contact_p.Text;
            department = this.department_p.SelectionBoxItem.ToString();
            date = date_p.Text;
            age = Convert.ToInt32(age_p.Text);

        }
        private void binddatagrid()
        {
            OutPatientDb ob = new OutPatientDb();
            ob.GridShow(z1);
        }
        private void submit_p_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true; //www.ahmetcansever.com
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < z1.Columns.Count; j++) //Başlıklar için
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; //Başlığın Kalın olması için
                sheet1.Columns[j + 1].ColumnWidth = 15; //Sütun genişliği ayarı
                myRange.Value2 = z1.Columns[j].Header;
            }
            for (int i = 0; i < z1.Columns.Count; i++)
            { //www.ahmetcansever.com
                for (int j = 0; j < z1.Items.Count; j++)
                {
                    TextBlock b = z1.Columns[i].GetCellContent(z1.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }

        }
        public void show()
        {
            add_Outpatient_reg();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SET IDENTITY_INSERT OutPatient ON insert into OutPatient (ID,Name,Address,Gender,DoctorName,ContactNo,Department,Date,Age) values(@id,@name,@address,@gender,@doc_name,@contact_no,@department,@date,@age)";
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@doc_name", doc_name);
            cmd.Parameters.AddWithValue("@contact_no", contact_no);
            cmd.Parameters.AddWithValue("@department", department);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@age", age);


            //  cmd.Connection = con;
            try
            {
                cmd.ExecuteNonQuery();

                MessageBox.Show("add successfully");
                cleanTextbox();
                foo();

            }
            catch (SqlException e)
            {
                string msg = "Insert Error:";
                msg += e.Message;
                MessageBox.Show(msg);
            }
        }
        public void cleanTextbox()
        {

            name_p.Text = null;
            address_p.Text = null;
            gender_p.Text = null;
            doctor_name_p.Text = null;
            contact_p.Text = null;
            department_p.Text = null;
            date_p.Text = null;
            age_p.Text = null;
        }
        private void submit_p_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(age_p.Text))
            {
                MessageBox.Show(" Must enter age");
            }
            else if (string.IsNullOrEmpty(name_p.Text))
            {
                MessageBox.Show(" Must enter name");

            }
            else if (string.IsNullOrEmpty(doctor_name_p.Text))
            {
                MessageBox.Show(" Must enter DoctorName");

            }

            else
            {
                show();
                binddatagrid();
            }
        }

        public void foo()
        {
            int a;
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();

            cmd = new SqlCommand("select max(ID) from OutPatient", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == null)
                {
                    id_p.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    id_p.Text = a.ToString();
                }
                con.Close();
            }
        }

        private void on_load(object sender, RoutedEventArgs e)
        {
            foo();
        }

        private void submit_p_Copy4_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete  from OutPatient where ID like '" + dlete.Text + "%'", con);
            MessageBox.Show("Deleted By  " + dlete.Text);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("OutPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
            binddatagrid();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            binddatagrid();
        }

        private void press(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void age_p_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text != "." && isNumber(e.Text) == false)
            {
                e.Handled = true;
            }
            else if (e.Text == ".")
            {
                if (((System.Windows.Controls.TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    e.Handled = true;
                }
            }
        }
        private bool isNumber(string p)
        {
            int output;
            return int.TryParse(p, out output);
            throw new NotImplementedException();
        }

        private void submit_p_Copy3_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from OutPatient where DoctorName like '" + search.Text + "%'", con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("OutPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
        }
        public void doctor_combo_show()
        {
            doctor_name_p.Items.Clear();
            string Sql = "select * from doctorName";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;

            conn.Open();
            SqlCommand cmd = new SqlCommand(Sql, conn);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                String sname = (string)DR["DoctorName"];
                doctor_name_p.Items.Add(DR[0]);
            }
        }
        private void combo_load(object sender, RoutedEventArgs e)
        {
            doctor_combo_show();
        }
        public void doctor_combo_insert()
        {
            doc = add_Doctor.Text;


            doctor_name_p.Items.Clear();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " insert into doctorName (Doctorname) values(@doc)";
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@doc", doc);
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("added doctor");

            }


        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            doctor_combo_insert();
            doctor_combo_show();
        }
        public void department_combo_show()
        {
            department_p.Items.Clear();
            string Sql = "select * from DePartment";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;

            conn.Open();
            SqlCommand cmd = new SqlCommand(Sql, conn);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                String sname = (string)DR["department"];
                department_p.Items.Add(DR[0]);
            }
        }
        private void department_load(object sender, RoutedEventArgs e)
        {
            department_combo_show();
        }
        public void department_combo_insert()
        {
            dp = add_department.Text;


            department_p.Items.Clear();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " insert into DePartment (department) values(@dp)";
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@dp", dp);
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("added department");

            }


        }
        private void Add_Department(object sender, RoutedEventArgs e)
        {
            department_combo_insert();
            department_combo_show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void delete_doctor1(object sender, RoutedEventArgs e)
        {
            string a = delete_doctor.Text;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete   from doctorName where Doctorname like '" + a + "%'", con);
            MessageBox.Show("Deleted By  " + delete_doctor);
            cmd.ExecuteNonQuery();
            delete_doctor.Text = null;
            doctor_combo_show();



        }

        private void Delete_Department(object sender, RoutedEventArgs e)
        {
            string a = delete_department.Text;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete   from DePartment where department like '" + a + "%'", con);
            MessageBox.Show("Deleted By  " + delete_department);
            cmd.ExecuteNonQuery();
            delete_department.Text = null;
            department_combo_show();
        }




    }
}
