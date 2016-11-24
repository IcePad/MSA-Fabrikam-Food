using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class FoodMenuPage : ContentPage {
        //Variables
        double total = 0;
        List<OrderModel> preCart = new List<OrderModel>();
        OrderModel emo = new OrderModel();

        public FoodMenuPage() {
            InitializeComponent();
            getMenu();

            
        }


        private async void getMenu() {
            //Progress bar
            ProgressIndicator.IsRunning = true;
            //Methods to popluate the listviews with respective menu
            HappyFood();
            AngryFood();
            ContemptFood();
            DisgustedFood();
            ScaredFood();
            HappyFood();
            NeutralFood();
            SadFood();
            SurprisedFood();
            //Progress bar
            ProgressIndicator.IsRunning = false;
        }
        //Methods to popluate the listviews with respective menu
        private async void HappyFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Happy");
            happyMenuList.ItemsSource = foodItems;
        }
        //Methods to popluate the listviews with respective menu
        private async void AngryFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Angry");
            angryMenuList.ItemsSource = foodItems;
        }
        //Methods to popluate the listviews with respective menu
        private async void ContemptFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Contempt");
            ContemptMenuList.ItemsSource = foodItems;
        }
        //Methods to popluate the listviews with respective menu
        private async void DisgustedFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Disgusted");
            DisgustedMenuList.ItemsSource = foodItems;
        }
        //Methods to popluate the listviews with respective menu
        private async void ScaredFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Scared");
            ScaredMenuList.ItemsSource = foodItems;
        }
        //Methods to popluate the listviews with respective menu
        private async void NeutralFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Neutral");
            NeutralMenuList.ItemsSource = foodItems;
        }
        //Methods to popluate the listviews with respective menu
        private async void SadFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Sad");
            SadMenuList.ItemsSource = foodItems;
        }
        //Methods to popluate the listviews with respective menu
        private async void SurprisedFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Surprised");
            SurprisedMenuList.ItemsSource = foodItems;
        }

        //Button event to add item to cart and add subtotal
        public async void addBtn(object sender, EventArgs e) {
            if (App.loggedIn == true) {
                var mi = ((Button)sender);
                await DisplayAlert("Alert", mi.Text + " added to your cart!", "OK");
                List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
                foreach (FoodItemModel item in foodItems) {
                    if (item.Name == mi.Text) {
                        total = total + item.Price;
                        cartTotal.Text = "Total: $" + total.ToString();
                        string name = App.currentName;
                        emo = new OrderModel() {
                            Name = App.currentName,
                            FoodName = item.Name,
                            Price = item.Price
                        };
                        preCart.Add(emo);
                    }
                }
                //Check to ensure user is logged in
            } else {
                await DisplayAlert("Alert", "Please log in via Log In page", "OK");
                App.RootPage.Detail = new NavigationPage(new HomePage());
            }
        }

        //Displays the cart
        public async void cartBtn(object sender, EventArgs e) {
            if (App.loggedIn == true) {
                string cartList = "";
                foreach (OrderModel item in preCart) {
                    cartList = cartList + item.FoodName + " $" + item.Price + Environment.NewLine;
                }
                //Ask user if he wants to confirm the purchase
                //If yes, inserts new order into order menu
                var answer = await DisplayAlert("Complete purchase?", cartList, "Yes", "No");
                if (answer == true) {
                    foreach (OrderModel item in preCart) {
                        await AzureManager.AzureManagerInstance.AddOrderModel(item);
                        App.RootPage.Detail = new NavigationPage(new OrderPage());
                        App.MenuIsPresented = false;
                    }
                }
                if (answer == false) {
                }
            } else {
                await DisplayAlert("Alert", "Please log in via Log In page", "OK");
                App.RootPage.Detail = new NavigationPage(new HomePage());
            }

        }

        //Clears cart and the total string.
        public async void clearbtn(object sender, EventArgs e) {
            preCart = new List<OrderModel>();
            cartTotal.Text = "Total: $0.00";
        }
    }

       


    
}

