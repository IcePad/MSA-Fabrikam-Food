using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Moodify.Views {
    public partial class StaffPage : ContentPage {
        public StaffPage() {
            InitializeComponent();
        }

        private void loginClicked(object sender, EventArgs e) {
            if (passInput.Text == "2828") {
                App.RootPage.Detail = new NavigationPage(new FoodInputPage());
                App.MenuIsPresented = false;
            } else {
                DisplayAlert("Alert", "Incorrect password", "OK");
            }
        }
    }
}
