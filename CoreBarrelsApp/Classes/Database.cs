using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBarrelsApp
{
    public class Database
    {
        public static void LoadDB()
        {
            using (var DB = new AppDbContext())
            {
            };
        }
    }
}
