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
    public partial class AssessmentViewPage : ContentPage
    {
        private string currentAssessment;
        private Assessment assessment;
        public AssessmentViewPage(string CurrentAssessment)
        {
            InitializeComponent();
            currentAssessment = CurrentAssessment;
            assessment = DataConn.conn.FindWithQuery<Assessment>($"SELECT * FROM Assessment WHERE Name = '{currentAssessment}';");
        }
        private void updateInfo()
        {
            assessment = DataConn.conn.FindWithQuery<Assessment>($"SELECT * FROM Assessment WHERE ID = '{assessment.ID}';");
            lab_assessment_dates.Text = $"{assessment.Name}\n{assessment.Start.ToString("MM/dd/yyyy")} - {assessment.End.ToString("MM/dd/yyyy")}";
            lab_assessment_notifications.Text = (assessment.Notifications) ? "Notifications Enabled" : "Notifications Disabled";
            lab_assessment_type.Text = $"Assessment Type - {assessment.Type}";
        }
        protected override void OnAppearing()
        {
            updateInfo();
        }
        private async void btnFunc_assessment_editAssessment(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentMod("mod", assessment.Name, assessment.CourseID));
        }
    }
}