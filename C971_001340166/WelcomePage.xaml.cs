using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace C971_001340166
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }
        private async void btnFunc_welcome_start(object sender, EventArgs e)
        {
            DataConn.startConnection();
            await Navigation.PushAsync(new TermPage());
        }
    }
}
