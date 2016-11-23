using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class LogInPage : ContentPage {
        bool nameInDb = false;

        public LogInPage() {
            InitializeComponent();
        }

        private void UserLogIn_Clicked(object sender, EventArgs e) {
            checkName();
        }

        private async void checkName() {
            bool nameNotInDb = false; 
            List<UserModel> userItems = await AzureManager.AzureManagerInstance.GetUserModels();
            foreach (var user in userItems) {
                if (user.UserName == loginName.Text) {
                    checkPassword();
                    nameNotInDb = true; 
                }
            }
            if(nameNotInDb == false) {
                await DisplayAlert("Alert", "User name is not regiestered!", "OK");
            }
        }

        private async void checkPassword() {
            bool correctPass = false; 
            
            List<UserModel> userItems = await AzureManager.AzureManagerInstance.GetUserModels();
            foreach (var user in userItems) {
                string s = Encoding.UTF8.GetString(user.EncryptedPass, 0, user.EncryptedPass.Length); 
                if (user.UserName == loginName.Text && s == loginPass.Text) {
                        App.currentName = loginName.Text;
                        App.loggedIn = true;
                        await DisplayAlert("Alert", "Successfully logged in!", "OK");
                        correctPass = true;
                        App.RootPage.Master = new MenuPage();
                        App.MenuIsPresented = false;
                }  
            }
            if(correctPass == false) {
                await DisplayAlert("Alert", "Incorrect Password!", "OK");
            }
        }


    }
}
