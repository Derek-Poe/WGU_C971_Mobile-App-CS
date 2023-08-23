using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace C971_001340166
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Instructor { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Status { get; set; }
        public bool Notifications { get; set; }
        public string Notes { get; set; }
        public int TermID { get; set; }
    }
}
