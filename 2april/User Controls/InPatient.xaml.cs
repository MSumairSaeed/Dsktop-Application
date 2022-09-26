
using _2april.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace _2april
{
    /// <summary>
    /// Interaction logic for InPatient.xaml
    /// </summary>
    public partial class InPatient : UserControl
    {
        public int idI;
        public string nameI;
        public string addressI;
        public string genderI;
        public string contact_noI;

        public int ageI;
        public int roomI;
        public string regDateI;
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public InPatient()
        {
            InitializeComponent();
            //id_p.Text = Convert.ToString(amt);

        }
        public void add_Inpatient_reg()
        {
            //Models.InPatientDb obj = new Models.InPatientDb();
            //obj.disp();
            idI = Convert.ToInt32(id_p.Text);

            nameI = name.Text;
            addressI = address.Text;
            genderI = this.gender.SelectionBoxItem.ToString();
            contact_noI = contact_no.Text;
            //  regDateI=datepikr.SelectedDate.Value.ToShortDateString();
            regDateI = datepikr.Text;
            ageI = Convert.ToInt32(age.Text);
            roomI = Convert.ToInt32(room_p.Text);

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
        public void binddatagrid()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from InPatient ORDER BY ID";
            cmd.Connection = con;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("InPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;

        }

        public void show()
        {
            add_Inpatient_reg();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();



            cmd.CommandText = "SET IDENTITY_INSERT InPatient ON  insert into InPatient (Name,Age,Gender,Address,ContactNo,RegDate,RoomNo,ID) values(@nameI,@ageI,@genderI,@addressI,@contact_noI,@regDateI,@roomI,@idI)";
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@nameI", nameI);
            cmd.Parameters.AddWithValue("@ageI", ageI);
            cmd.Parameters.AddWithValue("@genderI", genderI);
            cmd.Parameters.AddWithValue("@addressI", addressI);
            cmd.Parameters.AddWithValue("@contact_noI", contact_noI);
            cmd.Parameters.AddWithValue("@regDateI", regDateI);
            cmd.Parameters.AddWithValue("@roomI", roomI);
            cmd.Parameters.AddWithValue("@idI", idI);

            //cmd.Connection = con;
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("add successfully");
                foo();
                room_no_auto_increment();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(room_p.Text))
            {
                MessageBox.Show(" Must enter room no");
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(room_p.Text))
            {
                MessageBox.Show(" Must enter room no");
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
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            binddatagrid();
        }
        public void foo()
        {
            int a;
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();

            cmd = new SqlCommand("select max(ID) from InPatient", con);
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
        public void room_no_auto_increment()
        {
            int a;
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();

            cmd = new SqlCommand("select max(RoomNo) from InPatient", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == null)
                {
                    room_p.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    room_p.Text = a.ToString();
                }
                con.Close();
            }
        }

        private void on_load(object sender, RoutedEventArgs e)
        {
            foo();
            room_no_auto_increment();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            InPatientDb obj = new InPatientDb();
            obj.dltPatient(dlete.Text);
            binddatagrid();

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
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

        private void room_p_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            add_Inpatient_reg();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("set identity_insert InPatient on update InPatient set Name='" + nameI + "',Age='" + ageI + "',Gender='" + genderI + "',Address='" + addressI + "',ContactNo='" + contact_noI + "',RegDate='" + regDateI + "',RoomNo='" + roomI + "'WHERE ID='" + idI + "' set identity_insert InPatient off", con);

            MessageBox.Show("update successfully");
            //  update InPatient set Name='" + nameI + "',Age='" + ageI + "',Gender=" + genderI + ",Address='" + addressI + "' where EmpId=" + txtEmpId.Text

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("InPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
            foo();
            room_no_auto_increment();
            //binddatagrid();

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from InPatient where RoomNo like '" + search.Text + "'", con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("InPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete * from InPatient", con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("InPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
        }



        //private bool isNumber(string p)
        //{
        //    int output;
        //    return int.TryParse(p, out output);
        //    throw new NotImplementedException();
        //}

    }
}
