using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace C971_001340166
{
    public class DataConn
    {
        public static SQLiteConnection conn;
        public static void startConnection()
        {            
            conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "planner.db3"));
            //
            //conn.CreateCommand("DROP TABLE Term;").ExecuteNonQuery(); conn.CreateCommand("DROP TABLE Course;").ExecuteNonQuery(); conn.CreateCommand("DROP TABLE Assessment;").ExecuteNonQuery();
            //
            if (conn.CreateCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Term';").ExecuteScalar<string>() == null)
            {
                conn.CreateTables<Term, Course, Assessment>();
                DataConn.conn.Insert(new Term { Name = "Spring Term", Start = DateTime.Now, End = DateTime.Now.AddMonths(6) });
                DataConn.conn.Insert(new Course { Name = "Example - 101", Start = DateTime.Now, End = DateTime.Now.AddDays(10), Instructor = "Derek Poe", InstructorEmail = "dpoe8@wgu.edu", InstructorPhone = "(123) 456-7890" , Status = "Completed", Notifications = true, Notes = "These are example notes for the Example - 101 course.", TermID = 1});
                DataConn.conn.Insert(new Assessment { Name = "Example OA", Start = DateTime.Now.AddDays(2), End = DateTime.Now.AddDays(2).AddHours(2), Notifications = true, Type = "Objective", CourseID = 1 });
                DataConn.conn.Insert(new Assessment { Name = "Example PA", Start = DateTime.Now.AddDays(3), End = DateTime.Now.AddDays(3).AddHours(2), Notifications = true, Type = "Performance", CourseID = 1 });
            }
        }
    }
}
