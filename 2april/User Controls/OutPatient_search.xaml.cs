using _2april.User_Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2april
{
    /// <summary>
    /// Interaction logic for OutPatient_search.xaml
    /// </summary>
    public partial class OutPatient_search : UserControl
    {
        public OutPatient_search()
        {
            InitializeComponent();
        }
        public void search_outpatient_by_name()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from OutPatient where Name like '" + outpatientsearch.Text + "%'", con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("OutPatient");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            search_outpatient_by_name();
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            outpatientsearch.Clear();
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void exit_outpatient_search(object sender, RoutedEventArgs e)
        {
            
          
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
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            binddatagrid();
        }
    }
}
