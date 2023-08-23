using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;

namespace C971_001340166
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
    {
        public TermPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            updateTermsList();
            foreach (Course course in DataConn.conn.Table<Course>().ToList())
            {
                if(course.Notifications && (course.Start.Year == DateTime.Now.Year && course.Start.Month == DateTime.Now.Month && course.Start.Day == DateTime.Now.Day))
                {
                    CrossLocalNotifications.Current.Show("Course Alert", $"{course.Name} starts today!");
                }
                if (course.Notifications && (course.End.Year == DateTime.Now.Year && course.End.Month == DateTime.Now.Month && course.End.Day == DateTime.Now.Day))
                {
                    CrossLocalNotifications.Current.Show("Course Alert", $"{course.Name} ends today!");
                }
            }
            foreach (Assessment assessment in DataConn.conn.Table<Assessment>().ToList())
            {
                if (assessment.Notifications && (assessment.Start.Year == DateTime.Now.Year && assessment.Start.Month == DateTime.Now.Month && assessment.Start.Day == DateTime.Now.Day))
                {
                    CrossLocalNotifications.Current.Show("Assessment Alert", $"{assessment.Name} starts today!");
                }
                if (assessment.Notifications && (assessment.End.Year == DateTime.Now.Year && assessment.End.Month == DateTime.Now.Month && assessment.End.Day == DateTime.Now.Day))
                {
                    CrossLocalNotifications.Current.Show("Assessment Alert", $"{assessment.Name} ends today!");
                }
            }
        }
        private void updateTermsList()
        {
            listView_terms_terms.ItemsSource = DataConn.conn.Table<Term>().ToList().Select(term => term.Name);
        }
        protected override void OnAppearing()
        {
            updateTermsList();
        }
        private async void btnFunc_term_newTerm(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermModPage("new", ""));
        }
        private async void listViewFunc_term_termSelected(object sender, EventArgs e)
        {
            if (listView_terms_terms.SelectedItem == null) return;
            string selectedTerm = listView_terms_terms.SelectedItem.ToString();
            listView_terms_terms.SelectedItem = null;
            await Navigation.PushAsync(new TermViewPage(selectedTerm));
        }
    }
}