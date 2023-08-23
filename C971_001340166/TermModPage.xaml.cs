using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Plugin.LocalNotifications;

namespace C971_001340166
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermModPage : ContentPage
    {
        private Term selectedTerm = new Term();
        private string modType;
        public TermModPage(String ModType, string selectedTermName)
        {
            InitializeComponent();
            modType = ModType;
            if (modType == "new")
            {
                lab_termMod_title.Text = "New Term";
                datePicker_termMod_start.Date = DateTime.Now;
                datePicker_termMod_end.Date = DateTime.Now.AddMonths(6);
            }
            else
            {
                selectedTerm = DataConn.conn.FindWithQuery<Term>($"SELECT * FROM Term WHERE Name = '{selectedTermName}';");
                lab_termMod_title.Text = "Modify Term";
                entry_termMod_termName.Text = selectedTerm.Name;
                datePicker_termMod_start.Date = selectedTerm.Start;
                datePicker_termMod_end.Date = selectedTerm.End;
                btn_termMod_delete.IsVisible = true;
            }
        }
        private async void btnFunc_termMod_save(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(entry_termMod_termName.Text))
            {
                await DisplayAlert("Invalid Name", $"The Term Name must be entered.", "OK");
                return;
            }
            if (DataConn.conn.FindWithQuery<Term>($"SELECT * FROM Term WHERE Name = '{entry_termMod_termName.Text}';") != null && selectedTerm.Name != entry_termMod_termName.Text)
            {
                await DisplayAlert("Invalid Name", $"{entry_termMod_termName.Text} already exists as a term name.", "OK");
                return;
            }
            if (datePicker_termMod_start.Date > datePicker_termMod_end.Date)
            {
                await DisplayAlert("Invalid Dates", "The Start Date cannot be after the End Date.", "OK");
                return;
            }
            if (modType == "new")
            {
                DataConn.conn.Insert(new Term{Name = entry_termMod_termName.Text, Start = datePicker_termMod_start.Date, End = datePicker_termMod_end.Date });
            }
            else
            {
                selectedTerm.Name = entry_termMod_termName.Text;
                selectedTerm.Start = datePicker_termMod_start.Date;
                selectedTerm.End = datePicker_termMod_end.Date;
                DataConn.conn.Update(selectedTerm);
            }
            await Navigation.PopAsync();
        }
        private async void btnFunc_termMod_delete(object sender, EventArgs e)
        {
            DataConn.conn.Delete(selectedTerm);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();
        }
    }
}