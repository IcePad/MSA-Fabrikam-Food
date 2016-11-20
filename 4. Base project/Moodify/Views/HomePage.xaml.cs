using Android.Content.PM;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Moodify.DataModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Moodify
{
	public partial class HomePage : ContentPage
	{
        string tStatus = "feelings";
    
		public HomePage()
		{
			InitializeComponent();
		}

        private async void TakePicture_Clicked(object sender, EventArgs e) {

          

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
                    accept_btn.Text = "You're " + tStatus;
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


                await AzureManager.AzureManagerInstance.AddEmotionModel(emo);


            } catch (Exception ex) {
                errorLabel.Text = ex.Message;
            }


            image.Source = ImageSource.FromStream(() => {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

            Device.BeginInvokeOnMainThread(() => {
                accept_btn.IsVisible = true;
            });



        }

        private void Accept_Clicked(object sender, EventArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                accept_btn.IsVisible = false;
            });
        }

        private void getMood(Scores score) {
            if (score.Anger >= 0.60) {
                tStatus = "Angry";
            }
            else if (score.Contempt >= 0.60) {
                tStatus = "Contempt";
            }
            else if (score.Disgust >= 0.60) {
                tStatus = "Disgust";
            }
            else if (score.Fear >= 0.60) {
                tStatus = "Fear";
            }
            else if (score.Happiness >= 0.60) {
                tStatus = "Happy";
            }
            else if (score.Sadness >= 0.60) {
                tStatus = "Sad";
            }
            else if (score.Surprise >= 0.60) {
                tStatus = "Surprised";
            } else {
                tStatus = "Neutral";
            }
        }
    }
}
