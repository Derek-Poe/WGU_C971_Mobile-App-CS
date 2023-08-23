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
    public partial class AssessmentMod : ContentPage
    {
        private Assessment selectedAssessment = new Assessment();
        private string modType;
        private int courseID;
        public AssessmentMod(String ModType, string selectedAssessmentName, int CourseID)
        {
            InitializeComponent();
            modType = ModType;
            courseID = CourseID;
            if (modType == "new")
            {
                lab_assessmentMod_title.Text = "New Assessment";
                datePicker_assessmentMod_start.Date = DateTime.Now.AddHours(1);
                datePicker_assessmentMod_end.Date = DateTime.Now.AddHours(3);
            }
            else
            {
                selectedAssessment = DataConn.conn.FindWithQuery<Assessment>($"SELECT * FROM Assessment WHERE Name = '{selectedAssessmentName}';");
                lab_assessmentMod_title.Text = "Modify Assessment";
                entry_assessmentMod_assessmentName.Text = selectedAssessment.Name;
                datePicker_assessmentMod_start.Date = selectedAssessment.Start;
                datePicker_assessmentMod_end.Date = selectedAssessment.End;
                picker_assessmentMod_type.SelectedItem = selectedAssessment.Type;
                checkbox_assessmentMod_notifications.IsChecked = selectedAssessment.Notifications;
                btn_assessmentMod_delete.IsVisible = true;
            }
        }
        private async void btnFunc_assessmentMod_save(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(entry_assessmentMod_assessmentName.Text))
            {
                await DisplayAlert("Invalid Name", $"The Assessment Name must be entered.", "OK");
                return;
            }
            if (DataConn.conn.FindWithQuery<Term>($"SELECT * FROM Term WHERE Name = '{entry_assessmentMod_assessmentName.Text}';") != null && selectedAssessment.Name != entry_assessmentMod_assessmentName.Text)
            {
                await DisplayAlert("Invalid Name", $"{entry_assessmentMod_assessmentName.Text} already exists as an assessment name.", "OK");
                return;
            }
            if (datePicker_assessmentMod_start.Date > datePicker_assessmentMod_end.Date)
            {
                await DisplayAlert("Invalid Dates", "The Start Date cannot be after the End Date.", "OK");
                return;
            }
            if (picker_assessmentMod_type.SelectedIndex == -1)
            {
                await DisplayAlert("Invalid Assessment Type", "An Assessment Type must be selected.", "OK");
                return;
            }
            if (DataConn.conn.FindWithQuery<Assessment>($"SELECT * FROM Assessment WHERE Type = '{picker_assessmentMod_type.Items[picker_assessmentMod_type.SelectedIndex]}' AND CourseID = '{courseID}'") != null && picker_assessmentMod_type.Items[picker_assessmentMod_type.SelectedIndex] != selectedAssessment.Type)
            {
                await DisplayAlert("Invalid Type", $"An assessment with the selected type ({picker_assessmentMod_type.Items[picker_assessmentMod_type.SelectedIndex]}) already exists. Only 1 assessment type is allowed per course.", "OK");
                return;
            }
            if (modType == "new")
            {
                DataConn.conn.Insert(new Assessment { Name = entry_assessmentMod_assessmentName.Text, Start = datePicker_assessmentMod_start.Date, End = datePicker_assessmentMod_end.Date, Notifications = checkbox_assessmentMod_notifications.IsChecked, Type = picker_assessmentMod_type.Items[picker_assessmentMod_type.SelectedIndex], CourseID = courseID });
            }
            else
            {
                selectedAssessment.Name = entry_assessmentMod_assessmentName.Text;
                selectedAssessment.Start = datePicker_assessmentMod_start.Date;
                selectedAssessment.End = datePicker_assessmentMod_end.Date;
                selectedAssessment.Notifications = checkbox_assessmentMod_notifications.IsChecked;
                selectedAssessment.Type = picker_assessmentMod_type.Items[picker_assessmentMod_type.SelectedIndex];
                DataConn.conn.Update(selectedAssessment);
            }
            await Navigation.PopAsync();
        }
        private async void btnFunc_assessmentMod_delete(object sender, EventArgs e)
        {
            DataConn.conn.Delete(selectedAssessment);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();
        }
    }
}