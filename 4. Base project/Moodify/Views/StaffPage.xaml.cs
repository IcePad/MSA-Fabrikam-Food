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
            //Login of staff to access the staff page. 
            //use hardcoded password
            if (passInput.Text == "2828") {
                App.RootPage.Detail = new NavigationPage(new StaffOptionPage());
                App.MenuIsPresented = false;
            } else {
                //Alert user if wrong
                DisplayAlert("Alert", "Incorrect password", "OK");
            }
        }
    }
}
