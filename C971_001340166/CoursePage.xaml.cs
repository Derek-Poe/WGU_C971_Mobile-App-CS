using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace C971_001340166
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        private Course course;
        private string currentCourse;
        public CoursePage(string CurrentCourse)
        {
            InitializeComponent();
            currentCourse = CurrentCourse;
            course = DataConn.conn.FindWithQuery<Course>($"SELECT * FROM Course WHERE Name = '{currentCourse}';");
            updateInfo();
        }
        private void updateInfo()
        {
            course = DataConn.conn.FindWithQuery<Course>($"SELECT * FROM Course WHERE ID = '{course.ID}';");
            lab_course_dates.Text = $"{course.Name}\n{course.Start.ToString("MM/dd/yyyy")} - {course.End.ToString("MM/dd/yyyy")}";
            lab_course_instructorName.Text = course.Instructor;
            lab_course_instructorPhone.Text = course.InstructorPhone;
            lab_course_instructorEmail.Text = course.InstructorEmail;
            lab_course_status.Text = course.Status;
            lab_course_notifications.Text = (course.Notifications) ? "Notifications Enabled" : "Notifications Disabled";
            lab_course_notes.Text = course.Notes;
        }
        protected override void OnAppearing()
        {
            updateInfo();
        }
        private async void btnFunc_course_assessments(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentPage(course.Name, course.ID));
        }
        private async void btnFunc_course_editCourse(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseModPage("mod", course.Name, course.TermID));
        }
        private async void btnFunc_course_shareNotes(object sender, EventArgs e)
        {
            await Share.RequestAsync($"{course.Name} Notes:\n\n{course.Notes}");
        }
    }
}