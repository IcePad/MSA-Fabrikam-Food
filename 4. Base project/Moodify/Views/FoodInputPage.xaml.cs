using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class FoodInputPage : ContentPage {
        public FoodInputPage() {
            InitializeComponent();
        }

        public async void insert_Clicked(object sender, EventArgs e) {
            var i = moodInput.Text;
            if (i != "Happy"
                && i != "Angry"
                && i != "Contempt"
                && i != "Disgusted"
                && i != "Scared"
                && i != "Neutral"
                && i != "Sad"
                && i != "Surprised") {
                await DisplayAlert("Alert", "Invalid Mood! \n Please enter \n Happy, Angry, Contempt, \n Disgusted, Scared, Neutral,\n Sad or Surprised!", "OK");
            } else {

                var nameText = nameInput.Text;
                var priceText = Double.Parse(priceInput.Text);
                var emotionPriceText = priceText - 2;
                if (nameInput.Text == "Fabrikam Special") {
                    emotionPriceText = 5;
                }
                var moodText = moodInput.Text;
                FoodItemModel FoodItem = new FoodItemModel() {
                    Name = nameText,
                    Price = priceText,
                    EmotionPrice = emotionPriceText,
                    Mood = moodText
                };
                await AzureManager.AzureManagerInstance.AddFoodItemModel(FoodItem);
                await DisplayAlert("Alert", "Successfully Inserted new food item!", "OK");
                App.RootPage.Detail = new NavigationPage(new FoodInputPage());
                App.MenuIsPresented = false;
            }
        }

    }
}
