using _2april.User_Controls;
using excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace _2april.Models
{
    public class OutPatientDb : patient_module
    {
       
        public void GridShow(DataGrid z1)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from OutPatient ORDER BY ID";
            cmd.Connection = con;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("OutPatient");
            ad.Fill(dt);

            z1.ItemsSource = dt.DefaultView;
        }
        public void dleteoutpatient(string a)
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete  from OutPatient where ID like '" + a + "%'", con);
            MessageBox.Show("Deleted By  " + dlete.Text);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("OutPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
        }
    }
}
