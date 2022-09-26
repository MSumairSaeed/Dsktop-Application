using _2april.User_Controls;
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

namespace _2april
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        public void AddUserControl(UserControl user)
        {
            this.signup.Children.Clear();

            this.signup.Children.Add(user);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          //  AddUserControl(new SignUp());
        }
    }
}
