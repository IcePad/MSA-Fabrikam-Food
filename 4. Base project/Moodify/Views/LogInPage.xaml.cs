using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class LogInPage : ContentPage {
        //Variables
        bool nameInDb = false;

        public LogInPage() {
            InitializeComponent();
        }

        private void UserLogIn_Clicked(object sender, EventArgs e) {
            
            checkName();
            
        }

        private async void checkName() {
            //Check to see the name is in the database
            bool nameNotInDb = false;
            //Progress bar
            ProgressIndicator.IsRunning = true;
            List<UserModel> userItems = await AzureManager.AzureManagerInstance.GetUserModels();
            foreach (var user in userItems) {
                if (user.UserName == loginName.Text) {
                    checkPassword();
                    nameNotInDb = true; 
                }
            }
            if(nameNotInDb == false) {
                await DisplayAlert("Alert", "User name is not regiestered!", "OK");
                //Progress bar
                ProgressIndicator.IsRunning = false;
            }
        }

        private async void checkPassword() {
            bool correctPass = false; 
            //Check to see the password is correct
            List<UserModel> userItems = await AzureManager.AzureManagerInstance.GetUserModels();
            foreach (var user in userItems) {
                //Decode the password
                string s = Encoding.UTF8.GetString(user.EncryptedPass, 0, user.EncryptedPass.Length); 
                if (user.UserName == loginName.Text && s == loginPass.Text) {
                        App.currentName = loginName.Text;
                        App.loggedIn = true;
                        await DisplayAlert("Alert", "Successfully logged in!", "OK");
                        correctPass = true;
                        App.RootPage.Master = new MenuPage();
                        App.MenuIsPresented = true;
                }  
            }
            if(correctPass == false) {
                await DisplayAlert("Alert", "Incorrect Password!", "OK");
            }
            //Progress bar
            ProgressIndicator.IsRunning = false;
        }


    }
}
