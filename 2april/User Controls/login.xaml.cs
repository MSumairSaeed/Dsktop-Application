using System;
using System.Collections.Generic;
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

namespace _2april.User_Controls
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial  class login : UserControl
    {

        public login()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Click_1(object sender, RoutedEventArgs e)
        {
            person obj = new person();
            obj.fname = user.Text;
            obj.lname = last.Text;
            obj.pass = pass.Text;
            obj.email = email.Text;
            obj.sex = gender.Text;
            obj.dob1 = dob.Text;
            obj.save_person();

            
            Data dat = Data.Instance;
            dat.addData(obj);
             
        }

    }
}
