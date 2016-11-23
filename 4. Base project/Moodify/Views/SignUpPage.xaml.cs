using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace Moodify.Views {
    public partial class SignUpPage : ContentPage {


        public SignUpPage() {
            InitializeComponent();
        }

        private async void NewSignUp_Clicked(object sender, EventArgs e) {
            //Check if user name or password is null
            if (userNameInput.Text != null && userNameInput.Text.Length >= 3) {
                checkPassword();
            } else {
                await DisplayAlert("Alert", "Please enter a user name with atleast 3 letters!", "OK");
            } 
        }

        private async void checkPassword() {
            if (passwordInput.Text != null) {
                checkNameAvailabiltiy();
            } else {
                await DisplayAlert("Alert", "Please enter a password!", "OK");
            }
        }

        private async void checkNameAvailabiltiy() {
            bool sameName = false;
            List<UserModel> userItems = await AzureManager.AzureManagerInstance.GetUserModels();
            foreach (var user in userItems) {
                if (user.UserName == userNameInput.Text) {
                    await DisplayAlert("Alert", "Please use another user name!", "OK");
                    sameName = true;
                }
            }
            if (sameName == false) {
                insertUserIntoDatabase();
            }
        }

        private async void insertUserIntoDatabase() {
            string newName = userNameInput.Text;
            string newPass = passwordInput.Text;



            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(newPass);
            string s = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            UserModel userItem = new UserModel() {
                UserName = newName,
                Password = s,
                EncryptedPass = buffer
            };
            await AzureManager.AzureManagerInstance.AddUserModel(userItem);
            await DisplayAlert("Alert", "Successfully signed up!", "OK");
        }

    }
}
