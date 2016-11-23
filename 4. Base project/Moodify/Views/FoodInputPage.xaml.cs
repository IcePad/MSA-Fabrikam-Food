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
            //Progress bar
            ProgressIndicator.IsRunning = true;
            //Check to ensure a correct mood was entered and alert the staff member if not
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
                //Progress bar
                ProgressIndicator.IsRunning = false;
            } else {
                //Check to see if feilds are empty
                if (nameInput.Text == null || priceInput.Text == null || Double.Parse(priceInput.Text) <= 2) {
                    await DisplayAlert("Alert", "Please ensure all fields are filled out and valid!" + Environment.NewLine + "*Price must be greater than $2.*", "OK");
                    //Progress bar
                    ProgressIndicator.IsRunning = false;
                } else {
                    //Get userinput 
                    var nameText = nameInput.Text;
                    var priceText = Double.Parse(priceInput.Text);
                    //Create discounted price 
                    var emotionPriceText = priceText - 2;
                    //Create heavily discounted price of Fabrikam Special for challenge 
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
                    //Insert item into database
                    await AzureManager.AzureManagerInstance.AddFoodItemModel(FoodItem);
                    await DisplayAlert("Alert", "Successfully Inserted new food item!", "OK");
                    //Progress bar
                    ProgressIndicator.IsRunning = false;
                    //Refresh the page
                    App.RootPage.Detail = new NavigationPage(new FoodInputPage());
                    App.MenuIsPresented = false;
                }
            }
        }

    }
}
