using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Moodify.DataModels;
using Plugin.Media;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moodify.Views
{
	public partial class EmotionPage : ContentPage
	{
        string tStatus = "feelings";

        public EmotionPage()
		{
			InitializeComponent();

		}

        private async void TakePicture_Clicked(object sender, EventArgs e) {
            if (App.loggedIn == true) {
                takePicture();
            } else {
                await DisplayAlert("Alert", "Please log in via home page", "OK");
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
                    accept_btn.Text = "You're " + tStatus + "! Click here to see items to match your mood!";
                    accept_btn.IsVisible = true;
                });
                var temp = emotionResults[0].Scores;
                EmotionModel emo = new EmotionModel() {
                    Anger = temp.Anger,
                    Contempt = temp.Contempt,
                    Disgust = temp.Disgust,
                    Fear = temp.Fear,
                    Happiness = temp.Happiness,
                    Neutral = temp.Neutral,
                    Sadness = temp.Sadness,
                    Surprise = temp.Surprise,
                    Date = DateTime.Now
                };
                //Inserting into the database. 
                await AzureManager.AzureManagerInstance.AddEmotionModel(emo);
            } catch (Exception ex) {
                errorLabel.Text = ex.Message;
            }
        }
            
        

        private async void Accept_Clicked(object sender, EventArgs e) {
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

    }
}
