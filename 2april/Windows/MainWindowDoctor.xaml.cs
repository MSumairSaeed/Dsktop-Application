using _2april.User_Controls;
using System.Windows;
using System.Windows.Controls;

namespace _2april.Windows
{
    /// <summary>
    /// Interaction logic for MainWindowDoctor.xaml
    /// </summary>
    public partial class MainWindowDoctor : Window
    {
        public MainWindowDoctor()
        {
            InitializeComponent();
        }
        private void sam(UserControl user)
        {
            this.asdfg.Children.Clear();
            this.asdfg.Children.Add(user);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            login1 obj = new login1();
            obj.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void meetings_Click(object sender, RoutedEventArgs e)
        {
            sam(new Show_Meeting());
        }
    }
}
