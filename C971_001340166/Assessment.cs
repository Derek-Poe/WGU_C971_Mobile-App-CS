using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace C971_001340166
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CourseID { get; set; }
        public bool Notifications { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }
    }
}
