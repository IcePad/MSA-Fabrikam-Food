using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class StaffOptionPage : ContentPage {
        public StaffOptionPage() {
            InitializeComponent();
        }

        //Button to open up page to input food into the database
        public async void add_Clicked(object sender, EventArgs e) {
            App.RootPage.Detail = new NavigationPage(new FoodInputPage());
            App.MenuIsPresented = false;
        }
        //Button to open up page to update food on the database
        public async void update_Clicked(object sender, EventArgs e) {
            App.RootPage.Detail = new NavigationPage(new UpdateFoodItemPage());
            App.MenuIsPresented = false;
        }
        //Button to open up page to clear order to clear order
        public async void clearOrder_Clicked(object sender, EventArgs e) {
            App.RootPage.Detail = new NavigationPage(new ClearOrderPage());
            App.MenuIsPresented = false;
        }
        //Button to open up page to delete food from the database
        public async void delete_Clicked(object sender, EventArgs e) {
            App.RootPage.Detail = new NavigationPage(new DeleteFoodItemPage());
            App.MenuIsPresented = false;
        }
    }
}
