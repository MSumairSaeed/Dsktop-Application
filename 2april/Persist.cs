using System;
using System.Data.SqlClient;
using System.Windows;

namespace _2april
{
    class Persist
    {
        private string connetionString = null;
        private SqlConnection connection;
        private string sql = null;

        public void OpenDBConnection()
        {
            /*
            * Data Source=localhost;   it is host name , in our case it is localhost or local
            * Initial Catalog=test;    it is DB name
            * User ID=sa;              it is user id for SQL server
            * Password=12345678        it is user id password for SQL server
            */
            connetionString = "Data Source=localhost;Initial Catalog=master;User ID=sarmadsaeed;Password=sarmadsaeed123";
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                MessageBox.Show("Connection opened successfully ");

            }
            catch
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }
        public void ReadPerson(person p)
        {
            try
            {
                SqlCommand command;
                sql = "select * from Person";
                command = new SqlCommand(sql, connection);
                //command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    p.fname = reader["fisrt"].ToString();
                    p.lname = reader["age"].ToString();
                }

                command.Dispose();
                MessageBox.Show("Retrieved Succesful... ");

            }
            catch
            {
                MessageBox.Show("Can not read from DB ! ");

            }

        }

        public void insertPerson(person p)
        {
            try
            {
                String query = "INSERT INTO dbo.Person (cnic,name) VALUES(@cnic,@name)";

                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.Add("@cnic", p.fname);
                //command.Parameters.Add("@name", p.lname);

                command.ExecuteNonQuery();
                command.Dispose();
                MessageBox.Show("Record inserted into DB ! ");

            }
            catch
            {
                MessageBox.Show("Can not insert into DB ! ");
            }
        }

        public void closeConnection()
        {
            connection.Close();
        }

        public Persist()
        {

        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged(string PropertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        //    }
        //}
    }
}
