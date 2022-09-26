using _2april.User_Controls;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace _2april.Windows
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public string NAME;
        public string PASSWORD;
        public string repassword;
        public string TYPE;
        public SignUp()
        {
            InitializeComponent();
        }

        public void add_SignUp()
        {

            NAME = name.Text;
            PASSWORD = password.Password;

            TYPE = this.type.SelectionBoxItem.ToString();
        }

        public void signupTodb()
        {
            add_SignUp();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Login (Name,Password,Type) values(@NAME,@PASSWORD,@TYPE)";
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@NAME", NAME);
            cmd.Parameters.AddWithValue("@PASSWORD", PASSWORD);
            cmd.Parameters.AddWithValue("@TYPE", TYPE);


            cmd.Connection = con;
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("add successfully");
                login1 obj = new login1();
                obj.Show();
                this.Close();
            }


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            login1 obj = new login1();
            obj.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            signupTodb();
        }

        private void Content_TextChanged(object sender, RoutedEventArgs e)
        {
            if (password.Password.Equals(repass.Password))
                signup.IsEnabled = true;
            else
                signup.IsEnabled = false;
        }
    }
}
