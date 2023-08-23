using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_001340166
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermViewPage : ContentPage
    {
        private string currentTermName;
        private Term term;
        private int courseCount;
        public TermViewPage(string currentTerm)
        {
            InitializeComponent();
            currentTermName = currentTerm;
            term = DataConn.conn.FindWithQuery<Term>($"SELECT * FROM Term WHERE Name = '{currentTermName}';");
            updateInfo();  
        }
        private async void updateInfo()
        {
            term = DataConn.conn.FindWithQuery<Term>($"SELECT * FROM Term WHERE ID = '{term.ID}';");
            lab_termView_dates.Text = $"{term.Name}\n{term.Start.ToString("MM/dd/yyyy")} - {term.End.ToString("MM/dd/yyyy")}";
            listView_termView_courses.ItemsSource = DataConn.conn.Table<Course>().ToList().Where(course => course.TermID == term.ID).Select(course => course.Name);
            courseCount = DataConn.conn.Table<Course>().ToList().Where(course => course.TermID == term.ID).Count();
        }
        protected override void OnAppearing()
        {
            updateInfo();
        }
        private async void btnFunc_term_editTerm(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermModPage("mod", term.Name));
        }
        private async void btnFunc_termView_newCourse(object sender, EventArgs e)
        {
            if (courseCount >= 6)
            {
                await DisplayAlert("Term Full", "No more than 6 courses may be added to a term.", "OK");
                return;
            }
            await Navigation.PushAsync(new CourseModPage("new", "", term.ID));
        }
        private async void listViewFunc_termView_courseSelected(object sender, EventArgs e)
        {
            if (listView_termView_courses.SelectedItem == null) return;
            string selectedCourse = listView_termView_courses.SelectedItem.ToString();
            listView_termView_courses.SelectedItem = null;
            await Navigation.PushAsync(new CoursePage(selectedCourse));
        }
    }
}