using Android.Content.PM;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Moodify.DataModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using Moodify.Views;

namespace Moodify
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		}

        private void SignUp_Clicked(object sender, EventArgs e) {
            App.RootPage.Detail = new NavigationPage(new SignUpPage());
            App.MenuIsPresented = false;
        }

        private void LogIn_Clicked(object sender, EventArgs e) {
            App.RootPage.Detail = new NavigationPage(new LogInPage());
            App.MenuIsPresented = false;
        }



    }
}
