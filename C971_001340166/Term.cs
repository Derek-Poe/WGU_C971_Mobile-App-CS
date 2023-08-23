using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace C971_001340166
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
