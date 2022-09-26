using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2april
{
    class Data
    {
        private static Data instance;

   private Data() {}

   public static List<person> dat;
   public static Data Instance
   {
      get 
      {
         if (instance == null)
         {
             instance = new Data();
             dat = new List<person>();
         }
         return instance;
      }
   }

        public void addData(person p)
   {
       dat.Add(p);
   }

    }
}
