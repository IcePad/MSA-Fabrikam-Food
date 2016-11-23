using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class UpdateFoodItemPage : ContentPage {
        public UpdateFoodItemPage() {
            InitializeComponent();
        }


        public async void updateFoodItem_Clicked(object sender, EventArgs e) {
            //Variables
            bool itemInDb = false;
            var i = moodInput.Text;
            //Progress bar
            ProgressIndicator.IsRunning = true;
            
            if (i != "Happy"
                && i != "Angry"
                && i != "Contempt"
                && i != "Disgusted"
                && i != "Scared"
                && i != "Neutral"
                && i != "Sad"
                && i != "Surprised") {
                await DisplayAlert("Alert", "Invalid Mood! \n Please enter \n Happy, Angry, Contempt, \n Disgusted, Scared, Neutral,\n Sad or Surprised!", "OK");
                //Progress bar
                ProgressIndicator.IsRunning = false;
            } else {
                //Check to see if feilds are empty
                if (nameInput.Text == null || priceInput.Text == null || Double.Parse(priceInput.Text) <= 2) {
                    await DisplayAlert("Alert", "Please ensure all fields are filled out!" + Environment.NewLine + "*Price must be greater than $2.*", "OK");
                    //Progress bar
                    ProgressIndicator.IsRunning = false;
                } else {
                    //Get food objects
                    List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
                    //Check to see if user input text matches any food in database
                    foreach (var food in foodItems) {
                        if (food.Name == nameInput.Text) {
                            UpdateFoodTable();
                            itemInDb = true;
                        }
                    }
                    if (itemInDb == false) {
                        //alert if not
                        await DisplayAlert("Alert", nameInput.Text + " is not on the menu!", "OK");
                    }
                }
            }
        }
           
        public async void UpdateFoodTable() {
            //select all food objects which match the input name
            var person = await AzureManager.AzureManagerInstance.foodItemModelTable
                    .Where(FoodItemModel => FoodItemModel.Name == nameInput.Text)
                    .ToListAsync();
            //get new price
            var priceText = Double.Parse(priceInput.Text);
            //Get emotion prices
            var emotionPriceText = priceText - 2;
            if (nameInput.Text == "Fabrikam Special") {
                emotionPriceText = 5;
            }
            //Change value for rows which need to be updated
            foreach (var item in person) {
                item.Name = nameInput.Text;
                item.Price = priceText;
                item.EmotionPrice = emotionPriceText;
                item.Mood = moodInput.Text;
                //Update the record
                await AzureManager.AzureManagerInstance.UpdateFoodItemModel(item);
            }

            await DisplayAlert("Alert", nameInput.Text + " has been successfully updated!", "OK");
            //Progress bar
            ProgressIndicator.IsRunning = false;
        }

    }
}
