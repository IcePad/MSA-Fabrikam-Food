﻿using Moodify.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Moodify
{
	public class MenuPageViewModel
	{
		public ICommand GoHomeCommand { get; set; }
		public ICommand GoEmotionCommand { get; set; }
        public ICommand GoThirdCommand { get; set;  }
        public ICommand GoStaffCommand { get; set; }
        public ICommand GoFoodInputCommand { get; set; }
        public string name { get; set; }
        public bool logg { get; set;  }

        public MenuPageViewModel()
		{
            name = App.currentName;
            logg = App.loggedIn;
			GoHomeCommand = new Command(GoHome);
			GoEmotionCommand = new Command(GoEmotion);
            GoThirdCommand = new Command(GoFoodMenu);
            GoStaffCommand = new Command(GoStaff);
            GoFoodInputCommand = new Command(GoFoodInput);
		}

		void GoHome(object obj)
		{

            App.RootPage.Detail = new NavigationPage(new HomePage());
			App.MenuIsPresented = false;
		}

		void GoEmotion(object obj)
		{
            App.RootPage.Detail = new NavigationPage(new EmotionPage());
            App.MenuIsPresented = false;
		}

        void GoFoodMenu(object obj) {
            App.RootPage.Detail = new NavigationPage(new FoodMenuPage());
            App.MenuIsPresented = false; 
        }

        void GoStaff(object obj) {
            App.RootPage.Detail = new NavigationPage(new StaffPage());
            App.MenuIsPresented = false;
        }

        void GoFoodInput(object obj) {
            App.RootPage.Detail = new NavigationPage(new FoodInputPage());
            App.MenuIsPresented = false; 
        }
    }
}
