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
using System.IO;

namespace _2april
{
    public class person
    {
        public string fname;
        public string lname;
        public string email;
        public string pass;
        public string rpass;
        public string sex;
        public string dob1;

        public void save_person()
        {

            using (StreamWriter obj = new StreamWriter("test1.txt", true))
            {
                obj.WriteLine("First name is"+" "+fname);
                obj.WriteLine("Last name is" + " " + lname);
                obj.WriteLine("Email is" + " " + email);
                obj.WriteLine("Password  is" + " " + pass);

                obj.WriteLine("Date Of Birth is" + " " + dob1);
                obj.WriteLine("Sex is" + " " + sex);
                obj.WriteLine();
                obj.WriteLine();
                MessageBox.Show("Successfully save in file");
              



        }
        }
    }
}
