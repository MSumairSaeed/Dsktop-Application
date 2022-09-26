using _2april.Windows;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Cryptography;
namespace _2april.User_Controls
{
    /// <summary>
    /// Interaction logic for login1.xaml
    /// </summary>
    public partial class login1 : Window
    {
        public string NAME;
        public string PASSWORD;
        public string TYPE;
        public int a;
        public login1()
        {
            InitializeComponent();
            pass.PasswordChar = '*';
        }
        protected static Boolean Authentication(string NAME,string PASSWORD, string TYPE)
        {

            string sqlstring;
            sqlstring = "Select Name, Password,Type from Login where Name='" + NAME + "' and Password ='" + PASSWORD + "' and Type='" + TYPE + "'";

            //Name='" + NAME + "' and
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();


            if (reader.Read())
                return true;
            else
                return false;
        }
        //public void encrypt()
        //{
        //    byte[] data = UTF8Encoding.UTF8.GetBytes(PASSWORD);
        //    using(MD5CryptoServiceProvider md5=new MD5CryptoServiceProvider)
        //    {
        //        byte[] keys=md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
        //        using()
        //        {

        //        }
        //    }
        //}
        public void add_login()
        {
            NAME = name.Text;
           // textblock.Text = pass.Password;
            PASSWORD = pass.Password;
            TYPE =this.type.SelectionBoxItem.ToString();

           

            if (Authentication(NAME,PASSWORD, TYPE) == true)
            {
                MessageBox.Show("Match successfully");

                //a = Convert.ToInt32(type.Text);
                if (TYPE == "Admin")
                {

                    //Duration dur = new Duration(TimeSpan.FromSeconds(10));
                    //DoubleAnimation dban = new DoubleAnimation(200.0, dur);
                    //pb1.BeginAnimation(ProgressBar.ValueProperty, dban);

                    //int milliseconds = 10000;
                    //Thread.Sleep(milliseconds);
                    MainWindow obj = new MainWindow();
                    obj.Show();
                    this.Close();
                }
                if (TYPE == "Doctor")
                {
                    MainWindowDoctor obj = new MainWindowDoctor();
                    obj.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Not Match");

            }

        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            add_login();  
        }
        private void loadprogressbar()
        {

        }
        private void AddUserControl(UserControl user)
        {


            this.logingrid.Children.Clear();
            this.logingrid.Children.Add(user);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SignUp obj = new SignUp();
            obj.Show();
            this.Close();
        }
    }
}
