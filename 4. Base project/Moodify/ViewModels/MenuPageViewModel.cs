using Moodify.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Moodify
{
	public class MenuPageViewModel
	{
		public ICommand GoHomeCommand { get; set; }
		public ICommand GoEmotionCommand { get; set; }
        public ICommand GoFoodMenuCommand { get; set;  }
        public ICommand GoStaffCommand { get; set; }
        public ICommand GoFoodInputCommand { get; set; }
        public ICommand GoOrderCommand { get; set; }
        public ICommand GoMapCommand { get; set; }
        public ICommand AboutUsCommand { get; set; }
        public string name { get; set; }
        public bool logg { get; set;  }

        public MenuPageViewModel()
		{
            name = App.currentName;
            logg = App.loggedIn;
			GoHomeCommand = new Command(GoHome);
			GoEmotionCommand = new Command(GoEmotion);
            GoFoodMenuCommand = new Command(GoFoodMenu);
            GoOrderCommand = new Command(GoOrder);
            GoStaffCommand = new Command(GoStaff);
            GoFoodInputCommand = new Command(GoFoodInput);
            GoMapCommand = new Command(GoMapInput);
            AboutUsCommand = new Command(AboutUsInput);
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

        void GoOrder(object obj) {
            App.RootPage.Detail = new NavigationPage(new OrderPage());
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

        void GoMapInput(object obj) {
            App.RootPage.Detail = new NavigationPage(new MapPage());
            App.MenuIsPresented = false;
        }

        void AboutUsInput(object obj) {
            App.RootPage.Detail = new NavigationPage(new AboutUsPage());
            App.MenuIsPresented = false;
        }
    }
}
