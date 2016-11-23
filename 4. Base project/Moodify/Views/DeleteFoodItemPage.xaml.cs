using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class DeleteFoodItemPage : ContentPage {
        public DeleteFoodItemPage() {
            InitializeComponent();
        }

        private async void deleteFood_Clicked(object sender, EventArgs e) {
            //Variables 
            bool itemInDb = false;
            //Progress bar
            ProgressIndicator.IsRunning = true;
            //Get the items in the foodItemModel table 
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            //Check to see if the name entered equals the text field. If so delete that item from table
            //Also change bool of itemInDb to true.
            foreach (FoodItemModel item in foodItems) {
                if (item.Name == nameInput.Text) {
                    await AzureManager.AzureManagerInstance.DeleteFoodItemModel(item);
                    await DisplayAlert("Alert", nameInput.Text + " has been successfully deleted!" , "OK");
                    itemInDb = true;
                }
            }
            //No item in db, alert user. 
            if(itemInDb == false) {
                await DisplayAlert("Alert", nameInput.Text + " is not currently on the menu!", "OK");
            }
            //Progress bar
            ProgressIndicator.IsRunning = false;
        }
    }
}
