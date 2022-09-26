using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2april
{
    public class ViewModels : System.ComponentModel.IDataErrorInfo
    {
        public ViewModels()
        {
            /* Set default age */
        }

        public string name { get; set; }

        public string age { get; set; }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "name":
                        if (this.name ==null)
                            return "The Name Must Not Be Empty";
                        break;
                    case "age":
                        if (this.age == null)
                            return "The Age Must Not Be Empty";
                        break;
                }

                return string.Empty;
            }
        }

    }
}
