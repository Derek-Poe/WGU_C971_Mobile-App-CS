using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_001340166
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseModPage : ContentPage
    {
        private Course selectedCourse = new Course();
        private string modType;
        private int termID;
        public CourseModPage(String ModType, string selectedCourseName, int TermID)
        {
            InitializeComponent();
            modType = ModType;
            termID = TermID;
            if (modType == "new")
            {
                lab_courseMod_title.Text = "New Course";
                datePicker_courseMod_start.Date = DateTime.Now;
                datePicker_courseMod_end.Date = DateTime.Now.AddMonths(1);
            }
            else
            {
                selectedCourse = DataConn.conn.FindWithQuery<Course>($"SELECT * FROM Course WHERE Name = '{selectedCourseName}';");
                lab_courseMod_title.Text = "Modify Course";
                entry_courseMod_courseName.Text = selectedCourse.Name;
                datePicker_courseMod_start.Date = selectedCourse.Start;
                datePicker_courseMod_end.Date = selectedCourse.End;
                entry_courseMod_instructorName.Text = selectedCourse.Instructor;
                entry_courseMod_instructorPhone.Text = selectedCourse.InstructorPhone;
                entry_courseMod_instructorEmail.Text = selectedCourse.InstructorEmail;
                picker_courseMod_status.SelectedItem = selectedCourse.Status;
                entry_courseMod_notes.Text = selectedCourse.Notes;
                checkbox_courseMod_notifications.IsChecked = selectedCourse.Notifications;
                btn_courseMod_delete.IsVisible = true;
            }
        }
        private async void btnFunc_courseMod_save(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(entry_courseMod_courseName.Text))
            {
                await DisplayAlert("Invalid Name", $"The Course Name must be entered.", "OK");
                return;
            }
            if (DataConn.conn.FindWithQuery<Course>($"SELECT * FROM Course WHERE Name = '{entry_courseMod_courseName.Text}';") != null && selectedCourse.Name != entry_courseMod_courseName.Text)
            {
                await DisplayAlert("Invalid Name", $"{entry_courseMod_courseName.Text} already exists as a course name.", "OK");
                return;
            }
            if (String.IsNullOrWhiteSpace(entry_courseMod_instructorName.Text) || String.IsNullOrWhiteSpace(entry_courseMod_instructorPhone.Text) || String.IsNullOrWhiteSpace(entry_courseMod_instructorEmail.Text))
            {
                await DisplayAlert("Invalid Instructor Info", $"All instructor information must be entered.", "OK");
                return;
            }
            if (datePicker_courseMod_start.Date > datePicker_courseMod_end.Date)
            {
                await DisplayAlert("Invalid Dates", "The Start Date cannot be after the End Date.", "OK");
                return;
            }
            if(picker_courseMod_status.SelectedIndex == -1)
            {
                await DisplayAlert("Invalid Course Status", "A course status must be selected.", "OK");
                return;
            }
            if (modType == "new")
            {
                DataConn.conn.Insert(new Course { Name = entry_courseMod_courseName.Text, Start = datePicker_courseMod_start.Date, End = datePicker_courseMod_end.Date, Instructor = entry_courseMod_instructorName.Text, InstructorPhone = entry_courseMod_instructorPhone.Text, InstructorEmail = entry_courseMod_instructorEmail.Text, Notifications = checkbox_courseMod_notifications.IsChecked, Notes = entry_courseMod_notes.Text, Status = picker_courseMod_status.Items[picker_courseMod_status.SelectedIndex], TermID = termID});
            }
            else
            {
                selectedCourse.Name = entry_courseMod_courseName.Text;
                selectedCourse.Start = datePicker_courseMod_start.Date;
                selectedCourse.End = datePicker_courseMod_end.Date;
                selectedCourse.Instructor = entry_courseMod_instructorName.Text; 
                selectedCourse.InstructorPhone = entry_courseMod_instructorPhone.Text; 
                selectedCourse.InstructorEmail = entry_courseMod_instructorEmail.Text; 
                selectedCourse.Notifications = checkbox_courseMod_notifications.IsChecked; 
                selectedCourse.Notes = entry_courseMod_notes.Text; 
                selectedCourse.Status = picker_courseMod_status.Items[picker_courseMod_status.SelectedIndex];
                DataConn.conn.Update(selectedCourse);
            }
            await Navigation.PopAsync();
        }
        private async void btnFunc_courseMod_delete(object sender, EventArgs e)
        {
            DataConn.conn.Delete(selectedCourse);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();
        }
    }
}