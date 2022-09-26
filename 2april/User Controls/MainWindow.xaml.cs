using System.Windows;
using System.Windows.Controls;

namespace _2april.User_Controls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModels obj = new ViewModels();
            DataContext = obj;


        }
        private void sam(UserControl user)
        {
            this.asdfg.Children.Clear();
            this.asdfg.Children.Add(user);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            sam(new login());
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            sam(new uc2());
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            //finl obj = new finl();
            //sam(obj);

        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                            "The system manages daily activities of the hospital/clinic in all its departments – Pharmacy, Laboratory, Radiology, Physiotherapy, Records, Out Patient Departments (OPD) and all wards – Medical & Surgical, Maternity, Intensive Care Unit (ICU), Operating Theatre, Special side wards and isolations wards.\n" +
                            "Thank You for using it!\nDesigned by: Sarmad saeed- sarmadsaeed13@gmail.com",
                            "About",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                            );
        }
        private void EnterUserLogoutTime() { }
        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to close application?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                EnterUserLogoutTime();
                this.Close();
            }
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            //billing_module obj = new billing_module();
            //sam(obj);
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            InPatient obj = new InPatient();
            sam(obj);
        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            patient_module obj = new patient_module();
            sam(obj);
        }

        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            Doctor_info obj = new Doctor_info();
            sam(obj);
        }

        private void MenuItem_Click_12(object sender, RoutedEventArgs e)
        {
            Room_info obj = new Room_info();
            sam(obj);
        }

        private void MenuItem_Click_13(object sender, RoutedEventArgs e)
        {
            In_patient_billing obj8 = new In_patient_billing();
            sam(obj8);
        }

        private void MenuItem_Click_14(object sender, RoutedEventArgs e)
        {
            OutPatient_bill obj10 = new OutPatient_bill();
            sam(obj10);
        }

        private void MenuItem_Click_15(object sender, RoutedEventArgs e)
        {
            InPatient_search obj1 = new InPatient_search();
            sam(obj1);
        }

        private void MenuItem_Click_16(object sender, RoutedEventArgs e)
        {
            OutPatient_search obj2 = new OutPatient_search();
            sam(obj2);
        }

        private void MenuItem_Click_17(object sender, RoutedEventArgs e)
        {
            login1 obj = new login1();
            obj.Show();
            this.Close();
        }

        private void loginRecord_Click(object sender, RoutedEventArgs e)
        {
            Login_Record_Show ob = new Login_Record_Show();
            sam(ob);
        }

        //private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    this.menu_Main.Width = this.grid_Main.ActualWidth;
        //}
    }
}
