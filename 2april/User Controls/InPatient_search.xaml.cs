using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace _2april.User_Controls
{
    /// <summary>
    /// Interaction logic for InPatient_search.xaml
    /// </summary>
    public partial class InPatient_search : UserControl
    {
        public InPatient_search()
        {
            InitializeComponent();
        }
        public void func()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from InPatient where Name like '" + inpatientsearch.Text + "%'", con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("InPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            func();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            login1 obj = new login1();
            obj.Show();

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
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            binddatagrid();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            inpatientsearch.Clear();
        }
    }
}
