using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace _2april.User_Controls
{
    /// <summary>
    /// Interaction logic for Login_Record_Show.xaml
    /// </summary>
    public partial class Login_Record_Show : UserControl
    {
        public Login_Record_Show()
        {
            InitializeComponent();
        }
        private void binddatagrid()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conlog"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Login";
            cmd.Connection = con;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable("Login");
            ad.Fill(dt);
            z1.ItemsSource = dt.DefaultView;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            binddatagrid();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true; //www.ahmetcansever.com
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < z1.Columns.Count; j++) //Başlıklar için
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; //Başlığın Kalın olması için
                sheet1.Columns[j + 1].ColumnWidth = 30; //Sütun genişliği ayarı
                myRange.Value2 = z1.Columns[j].Header;
            }
            for (int i = 0; i < z1.Columns.Count; i++)
            { //www.ahmetcansever.com
                for (int j = 0; j < z1.Items.Count; j++)
                {
                    TextBlock b = z1.Columns[i].GetCellContent(z1.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }

        private void z1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
