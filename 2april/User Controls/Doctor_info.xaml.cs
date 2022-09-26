using Microsoft.Office.Interop.Excel;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace _2april
{
    /// <summary>
    /// Interaction logic for Doctor_info.xaml
    /// </summary>
    public partial class Doctor_info : UserControl
    {
        public int idI;
        public string nameI;
        public string genderI;
        public string contact_noI;
        public string department;
        public int ageI;

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public Doctor_info()
        {
            InitializeComponent();
        }
        public void add_DoctorInformation()
        {
            idI = Convert.ToInt32(id.Text);

            nameI = name.Text;
            genderI = this.gender.SelectionBoxItem.ToString();
            genderI = gender.Text;
            contact_noI = contact.Text;

            department = this.departm.SelectionBoxItem.ToString();

            ageI = Convert.ToInt32(age.Text);
        }
        private void binddatagrid()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from DoctorInformation ORDER BY ID";
            cmd.Connection = con;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("DoctorInformation");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;

        }

        public void show()
        {
            add_DoctorInformation();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();



            cmd.CommandText = "SET IDENTITY_INSERT DoctorInformation ON  insert into DoctorInformation (Name,Age,ContactNo,Gender,Department,ID) values(@nameI,@ageI,@contact_noI,@genderI,@department,@idI)";
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@nameI", nameI);
            cmd.Parameters.AddWithValue("@ageI", ageI);
            cmd.Parameters.AddWithValue("@contact_noI", contact_noI);
            cmd.Parameters.AddWithValue("@genderI", genderI);

            cmd.Parameters.AddWithValue("@department", department);
            cmd.Parameters.AddWithValue("@idI", idI);

            //cmd.Connection = con;
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("add successfully");
                foo();
            }
        }
        public void foo()
        {
            int a;
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();

            cmd = new SqlCommand("select max(ID) from DoctorInformation", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == " ")
                {
                    id.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    id.Text = a.ToString();
                }
                con.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            department = this.departm.SelectionBoxItem.ToString();

            if (string.IsNullOrEmpty(department))
            {
                MessageBox.Show(" Must Select Department");
            }
            else if (string.IsNullOrEmpty(age.Text))
            {
                MessageBox.Show(" Must enter age");
            }
            else if (string.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show(" Must enter name");

            }
            else
            {
                show();
                binddatagrid();
            }
        }

        private void on_load(object sender, RoutedEventArgs e)
        {
            foo();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            department_combo_insert();
            department_combo_show();
            add_department.Clear();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {


            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete  from DoctorInformation where ID like '" + dlete.Text + "%'", con);
            MessageBox.Show("Deleted By  " + dlete.Text);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("DoctorInformation");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
            binddatagrid();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            binddatagrid();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void update_doctor_Click_5(object sender, RoutedEventArgs e)
        {
            add_DoctorInformation();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("set identity_insert DoctorInformation on update DoctorInformation set Name='" + nameI + "',Age='" + ageI + "',Gender='" + genderI + "',ContactNo='" + contact_noI + "',Department='" + department + "'WHERE ID='" + idI + "' set identity_insert DoctorInformation off", con);

            MessageBox.Show("update successfully");
            //insert into DoctorInformation (Name,Age,ContactNo,Gender,Department,ID) values(@nameI,@ageI,@contact_noI,@genderI,@department,@idI)";
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("DoctorInformation");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
            foo();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
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

        private void age_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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

        public void department_combo_show()
        {
            departm.Items.Clear();
            string Sql = "select * from DePartment";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;

            conn.Open();
            SqlCommand cmd = new SqlCommand(Sql, conn);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                String sname = (string)DR["department"];
                departm.Items.Add(DR[0]);
            }
        }
        private void combo_load(object sender, RoutedEventArgs e)
        {
            department_combo_show();
        }
        public void department_combo_insert()
        {
            string dp;
            dp = add_department.Text;


            departm.Items.Clear();
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
    }





}
