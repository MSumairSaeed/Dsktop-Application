using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace _2april.User_Controls
{
    /// <summary>
    /// Interaction logic for Show_Meeting.xaml
    /// </summary>
    public partial class Show_Meeting : UserControl
    {
        public Show_Meeting()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
        public void foo()
        {
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from OutPatient where DoctorName like '" + doc_name.Text + "%'", con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("OutPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
        }

    }
}
