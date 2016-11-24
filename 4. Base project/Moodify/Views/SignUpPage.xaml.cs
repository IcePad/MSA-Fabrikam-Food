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
            //Progress bar
            ProgressIndicator.IsRunning = true;
            //Check if user name is null
            if (userNameInput.Text != null ) {
                //Check to see if name is greater than 3 characters
                if (userNameInput.Text.Length >= 3) {
                       //CHeck passwords
                    checkPassword();
                } else {
                    await DisplayAlert("Alert", "Please enter a user name with atleast 3 characters!", "OK");
                    //Progress bar
                    ProgressIndicator.IsRunning = false;
                }
            } else {
                await DisplayAlert("Alert", "Please enter a user name!", "OK");
                //Progress bar
                ProgressIndicator.IsRunning = false;
            } 
        }

        private async void checkPassword() {
            //Check if user name is null
            if (passwordInput.Text != null && passwordInput2.Text != null) {
                //Check to see if name is greater than 3 characters
                if (passwordInput.Text.Length >= 6 && passwordInput2.Text.Length >= 6 ) {
                    //Check if passwords match one another
                    if(passwordInput.Text == passwordInput2.Text) {
                        //check if name is already being used
                        checkNameAvailability();
                    } else {
                        await DisplayAlert("Alert", "Ensure passwords are the same!", "OK");
                        //Progress bar
                        ProgressIndicator.IsRunning = false;
                    }
                }else {
                    await DisplayAlert("Alert", "Please enter a password with atleast 6 charcters!", "OK");
                    //Progress bar
                    ProgressIndicator.IsRunning = false;
                }

            } else {
                await DisplayAlert("Alert", "Please enter a password!", "OK");
                //Progress bar
                ProgressIndicator.IsRunning = false;
            }
        }

        private async void checkNameAvailability() {
            bool sameName = false;
            //Get names from database
            List<UserModel> userItems = await AzureManager.AzureManagerInstance.GetUserModels();
            //Check against each name to ensure they dont match.
            foreach (var user in userItems) {
                if (user.UserName == userNameInput.Text) {
                    await DisplayAlert("Alert", "Please use another user name!", "OK");
                    sameName = true;
                    //Progress bar
                    ProgressIndicator.IsRunning = false;
                }
            }
            if (sameName == false) {
                //Insert into database after encoding password
                insertUserIntoDatabase();

            }
        }

        private async void insertUserIntoDatabase() {
            string newName = userNameInput.Text;
            string newPass = passwordInput.Text;
            //Encode the password
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(newPass);
            //Create userModel item
            UserModel userItem = new UserModel() {
                UserName = newName,
                EncryptedPass = buffer
            };
            //Insert item into the UserModel table
            await AzureManager.AzureManagerInstance.AddUserModel(userItem);
            //Log straight into the current account
            App.currentName = newName;
            App.loggedIn = true;
            await DisplayAlert("Alert", "Successfully signed up!", "OK");
            App.RootPage.Master = new MenuPage();
            App.MenuIsPresented = true;
            //Progress bar
            ProgressIndicator.IsRunning = false;
        }

    }
}
