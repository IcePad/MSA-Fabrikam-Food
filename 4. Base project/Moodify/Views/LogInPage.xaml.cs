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

        private async void UserLogIn_Clicked(object sender, EventArgs e) {
            checkName();
            if(nameInDb == false) {
                await DisplayAlert("Alert", "Incorrect user name!", "OK");
            } else {
                checkPassword();
            }
        }

        private async void checkName() {
            List<UserModel> userItems = await AzureManager.AzureManagerInstance.GetUserModels();
            foreach (var user in userItems) {
                if (user.UserName == loginName.Text) {
                    nameInDb = true; 
                }
            }
        }

        private async void checkPassword() {
            bool correctPass = false; 
            List<UserModel> userItems = await AzureManager.AzureManagerInstance.GetUserModels();
            foreach (var user in userItems) {
                if (user.UserName == loginName.Text) {
                    if(user.Password == loginPass.Text) {
                        await DisplayAlert("Alert", "Successfully logged in!", "OK");
                        correctPass = true; 
                    }
                }
            }
            if(correctPass == false) {
                await DisplayAlert("Alert", "Incorrect Password!", "OK");
            }
        }


    }
}
