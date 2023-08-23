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
    public partial class AssessmentPage : ContentPage
    {
        private int courseID;
        public AssessmentPage(string courseName, int CourseID)
        {
            InitializeComponent();
            courseID = CourseID;
            lab_assessment_title.Text = $"{courseName} Assessments";
            updateInfo();
        }
        private void updateInfo()
        {
            listView_assessment_assessments.ItemsSource = DataConn.conn.Table<Assessment>().ToList().Where(assessment => assessment.CourseID == courseID).ToList().Select(assessment => assessment.Name);
        }
        protected override void OnAppearing()
        {
            updateInfo();
        }
        private async void listViewFunc_assessment_assessmentSelected(object sender, EventArgs e)
        {
            if (listView_assessment_assessments.SelectedItem == null) return;
            string selectedAssessment = listView_assessment_assessments.SelectedItem.ToString();
            listView_assessment_assessments.SelectedItem = null;
            await Navigation.PushAsync(new AssessmentViewPage(selectedAssessment));
        }
        private async void btnFunc_assessment_newAssessment(object sender, EventArgs e)
        {
            if (DataConn.conn.FindWithQuery<Assessment>($"SELECT * FROM Assessment WHERE Type = 'Objective' AND CourseID = '{courseID}';") != null && DataConn.conn.FindWithQuery<Assessment>($"SELECT * FROM Assessment WHERE Type = 'Performance' AND CourseID = '{courseID}';") != null)
            {
                await DisplayAlert("Assessments Full", "There may only be 1 Objective assessment and 1 performance assessment for a course.", "OK");
                return;
            }
            await Navigation.PushAsync(new AssessmentMod("new", "", courseID));
        }
    }
}