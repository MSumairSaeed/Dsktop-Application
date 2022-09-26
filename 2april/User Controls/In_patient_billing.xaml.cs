using Microsoft.Office.Interop.Excel;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace _2april.User_Controls
{
    /// <summary>
    /// Interaction logic for In_patient_billing.xaml
    /// </summary>
    public partial class In_patient_billing : UserControl
    {
        public int idI;
        public string nameI;
        public int ageI;
        public string genderI;
        public int Room_charges;
        public int Doctor_fees;
        public string admissionDate;
        public string dischargeDate;
        public int pathology;
        public int misc;
        public int TOTal;

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public In_patient_billing()
        {
            InitializeComponent();
        }
        public void inpatient_billing()
        {
            idI = Convert.ToInt32(p_id.Text);
            ageI = Convert.ToInt32(p_age.Text);
            Room_charges = Convert.ToInt32(room_charges.Text);
            Doctor_fees = Convert.ToInt32(doc_fees.Text);
            nameI = name.Text;
            genderI = this.gender.SelectionBoxItem.ToString();
            admissionDate = doa.Text;
            dischargeDate = dod.Text;
            pathology = Convert.ToInt32(pathol.Text);
            misc = Convert.ToInt32(mis.Text);
            TOTal = Convert.ToInt32(total.Text);

        }

        public void db_to_box()
        {

            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();

            cmd = new SqlCommand("select * from InPatient where ID like '" + p_id.Text + "%'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                name.Text = dr["Name"].ToString();
                gender.Text = dr["Gender"].ToString();
                p_age.Text = dr["Age"].ToString();
                doa.Text = dr["RegDate"].ToString();

                con.Close();
            }
            else
            {
                MessageBox.Show("data not found");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(p_id.Text))
            {
                MessageBox.Show(" Must Enter ID");
            }
            else if (string.IsNullOrEmpty(p_age.Text))
            {
                MessageBox.Show(" Must enter age");
            }
            else if (string.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show(" Must enter name");

            }
            else if (string.IsNullOrEmpty(room_charges.Text))
            {
                MessageBox.Show(" Must enter RoomCharges");

            }
            else if (string.IsNullOrEmpty(doc_fees.Text))
            {
                MessageBox.Show(" Must enter Doctor Fees");

            }


            else if (string.IsNullOrEmpty(pathol.Text))
            {
                MessageBox.Show(" Must enter Pathology");

            }
            else if (string.IsNullOrEmpty(mis.Text))
            {
                MessageBox.Show(" Must enter Miscallaunce");

            }
            else
            {

                inpatient_billing();
                db_to_box();
                show();
                clear_textbox();

            }
        }
        public void show()
        {
            inpatient_billing();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();



            cmd.CommandText = "insert into InPatientBilling (Name,Age,Gender,RoomCharges,DoctorFees,Pathology,Miscallaunce,DateOfAdmission,DateOfDischarge,ID,Total) values(@nameI,@ageI,@genderI,@Room_charges,@Doctor_fees,@pathology,@misc,@admissionDate,@dischargeDate,@idI,@TOTal)";
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@nameI", nameI);
            cmd.Parameters.AddWithValue("@ageI", ageI);
            cmd.Parameters.AddWithValue("@genderI", genderI);
            cmd.Parameters.AddWithValue("@Room_charges", Room_charges);
            cmd.Parameters.AddWithValue("@Doctor_fees", Doctor_fees);
            cmd.Parameters.AddWithValue("@pathology", pathology);
            cmd.Parameters.AddWithValue("@misc", misc);

            cmd.Parameters.AddWithValue("@admissionDate", admissionDate);
            cmd.Parameters.AddWithValue("@dischargeDate", dischargeDate);
            cmd.Parameters.AddWithValue("@idI", idI);
            cmd.Parameters.AddWithValue("@TOTal", TOTal);


            //cmd.Connection = con;
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("add successfully");

            }
            binddatagrid();
        }
        public void clear_textbox()
        {
           p_id.Text=" ";
    p_age.Text=" ";
          room_charges.Text=" ";
           doc_fees.Text=" ";
            name.Text=" ";
            gender.Text = " ";
          doa.Text=" ";
           dod.Text=" ";
           pathol.Text=" ";
         mis.Text=" ";
          total.Text=" ";
        }
        private void binddatagrid()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from InPatientBilling ORDER BY DateOfAdmission";
            cmd.Connection = con;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("InPatientBilling");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            db_to_box();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete  from InPatientBilling where ID like '" + dlete.Text + "%'", con);
            MessageBox.Show("Deleted By  " + dlete.Text);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("InPatientBilling");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
            binddatagrid();
            clear_textbox();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            binddatagrid();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // total.Text = (a + b).ToString();
            Room_charges = Convert.ToInt32(room_charges.Text);
            Doctor_fees = Convert.ToInt32(doc_fees.Text);
            pathology = Convert.ToInt32(pathol.Text);
            misc = Convert.ToInt32(mis.Text);
            total.Text = (Room_charges + Doctor_fees + pathology + misc).ToString();



        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
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

        private void Misc_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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

        
    }


}
