using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Moodify.DataModels;
using Plugin.Media;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moodify.Views
{
	public partial class EmotionPage : ContentPage{
        //Variables
        double total = 0;
        List<OrderModel> preCart = new List<OrderModel>();
        OrderModel tOrder = new OrderModel();
        string tStatus = "feelings";

        public EmotionPage()
		{
			InitializeComponent();

		}

        private async void TakePicture_Clicked(object sender, EventArgs e) {
            //Ensure's the user is logged in
            if (App.loggedIn == true) {
                takePicture();
            } else {
                await DisplayAlert("Alert", "Please log in via Log In page", "OK");
                App.RootPage.Detail = new NavigationPage(new HomePage());
            }

        }

        private async void takePicture() {
            //Check to see if camera is available
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                await DisplayAlert("No Camera", ":( No camera available", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions {
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                Directory = "Moodify",
                Name = $"{DateTime.UtcNow}.jpg",
                CompressionQuality = 92
            });

            if (file == null)
                return;

            try {
                //Progress bar
                UploadingIndicator.IsRunning = true;
                string emotionKey = "037d37af34ba425c8a29cf8560bd82ee";
                EmotionServiceClient emotionClient = new EmotionServiceClient(emotionKey);
                var emotionResults = await emotionClient.RecognizeAsync(file.GetStream());
                //Progress bar
                UploadingIndicator.IsRunning = false;
                //Getting first element of JSON file
                //ToRankedList() organizes the list. 
                getMood(emotionResults[0].Scores);
                Device.BeginInvokeOnMainThread(() => {
                    TakePicBtn.Text = "Retake photo!";
                    TakePicBtn.FontSize = 15;
                    accept_btn.Text = tStatus + " Menu!";
                    accept_btn.IsVisible = true;
                });

            } catch (Exception ex) {
                errorLabel.Text = ex.Message;
            }
        }
            

        private async void Accept_Clicked(object sender, EventArgs e) {
            //Removing all other moods from the foodItemModel 
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            if (tStatus == "Happy") {
                foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Happy");
            }
            if (tStatus == "Angry") {
                foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Angry");
            }
            if (tStatus == "Contempt") {
                foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Contempt");
            }
            if (tStatus == "Disgusted") {
                foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Disgusted");
            }
            if (tStatus == "Neutral") {
                foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Neutral");
            }
            if (tStatus == "Sad") {
                foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Sad");
            }
            if (tStatus == "Surprised") {
                foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Surprised");
            }
            foodList.ItemsSource = foodItems;
        }

        private void getMood(Scores score) {
            //Return which mood is the highest
            if (score.Anger >= 0.60) {
                tStatus = "Angry";
            } else if (score.Contempt >= 0.60) {
                tStatus = "Contempt";
            } else if (score.Disgust >= 0.60) {
                tStatus = "Disgusted";
            } else if (score.Fear >= 0.60) {
                tStatus = "Scared";
            } else if (score.Happiness >= 0.60) {
                tStatus = "Happy";
            } else if (score.Sadness >= 0.60) {
                tStatus = "Sad";
            } else if (score.Surprise >= 0.60) {
                tStatus = "Surprised";
            } else {
                tStatus = "Neutral";
            }
        }


        public async void addtoCardBtn(object sender, EventArgs e) {
            //This adds item to the cart while also totaling the value
            //Ensures user is logged in
            if (App.loggedIn == true) {
                var mi = ((Button)sender);
                await DisplayAlert("Alert", mi.Text + " added to your cart!", "OK");
                List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
                foreach (FoodItemModel item in foodItems) {
                    if (item.Name == mi.Text) {
                        total = total + item.EmotionPrice;
                        cartTotal.Text = "Total: $" + total.ToString();
                        string name = App.currentName;
                        tOrder = new OrderModel() {
                            Name = App.currentName,
                            FoodName = item.Name,
                            Price = item.EmotionPrice
                        };
                        preCart.Add(tOrder);
                    }
                }
            } else {
                await DisplayAlert("Alert", "Please log in via Log In page", "OK");
                App.RootPage.Detail = new NavigationPage(new HomePage());
            }
        }

        //When user pressed the cart button, enables them to view cart
        private async void cartBtn(object sender, EventArgs e) {
            if (App.loggedIn == true) {
                viewCart();
            } else {
                await DisplayAlert("Alert", "Please log in via home page", "OK");
                App.RootPage.Detail = new NavigationPage(new HomePage());
            }
        }

        //The cart button will display a display alert showing them their cart
        public async void viewCart() {
            string cartList = "";
            foreach (OrderModel item in preCart) {
                cartList = cartList + item.FoodName + " $" + item.Price + Environment.NewLine;
            }
            var answer = await DisplayAlert("Complete purchase?", cartList, "Yes", "No");
            if (answer == true) {
                foreach (OrderModel item in preCart) {
                    await AzureManager.AzureManagerInstance.AddOrderModel(item);
                    App.RootPage.Detail = new NavigationPage(new EmotionPage());
                    await DisplayAlert("Alert", "Purchase Successful!", "OK");
                }
            }
            if (answer == false) {
            }
        }
        //This button clears the cart and total string. 
        public async void clearbtn(object sender, EventArgs e) {
            preCart = new List<OrderModel>();
            total = 0;
            cartTotal.Text = "Total: $0.00";
        }



    }
}
