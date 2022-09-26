using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
namespace _2april.Models
{
    public class InPatientDb : InPatient
    {
        public void disp()
        {
            MessageBox.Show("SHow db");
        }
        public void dltPatient(string a)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete  from InPatient where ID like '" + a + "%'", con);
            MessageBox.Show("Deleted By  " + dlete.Text);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("InPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;



        }

    }
}
