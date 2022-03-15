using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{

    class PeopleContext : DbContext
    {
        public PeopleContext() : base("connectionString")
        {

        }
        public DbSet<Person> Persons { get; set; }

    }
}
