using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Configurations
{
    public class DatabaseSettings
    {
        public string connectionString { get; set; } = null!;
        public string databaseName { get; set; } = null!;
        public string productCollection { get; set; } = null!;
        public string cartCollection { get; set; } = null!;
        public string userCollection { get; set; } = null!;
    }
}