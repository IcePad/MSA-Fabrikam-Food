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
            HappyFood();
            AngryFood();
            ContemptFood();
            DisgustedFood();
            ScaredFood();
            HappyFood();
            NeutralFood();
            SadFood();
            SurprisedFood();


        }

        private async void HappyFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Happy");
            happyMenuList.ItemsSource = foodItems;
        }

        private async void AngryFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Angry");
            angryMenuList.ItemsSource = foodItems;
        }

        private async void ContemptFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Contempt");
            ContemptMenuList.ItemsSource = foodItems;
        }

        private async void DisgustedFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Disgusted");
            DisgustedMenuList.ItemsSource = foodItems;
        }

        private async void ScaredFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Scared");
            ScaredMenuList.ItemsSource = foodItems;
        }

        private async void NeutralFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Neutral");
            NeutralMenuList.ItemsSource = foodItems;
        }

        private async void SadFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Sad");
            SadMenuList.ItemsSource = foodItems;
        }

        private async void SurprisedFood() {
            List<FoodItemModel> foodItems = await AzureManager.AzureManagerInstance.GetFoodItemModels();
            foodItems.RemoveAll(FoodItemModel => FoodItemModel.Mood != "Surprised");
            SurprisedMenuList.ItemsSource = foodItems;
        }

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
            } else {
                await DisplayAlert("Alert", "Please log in via home page", "OK");
                App.RootPage.Detail = new NavigationPage(new HomePage());
            }
        }

        public async void cartBtn(object sender, EventArgs e) {
            string cartList = "";
            foreach (OrderModel item in preCart) {
                cartList = cartList + item.FoodName + " $" + item.Price + Environment.NewLine;
            }
            var answer = await DisplayAlert("Complete purchase?", cartList, "Yes", "No");
            if (answer == true) {
                foreach (OrderModel item in preCart) {
                    await AzureManager.AzureManagerInstance.AddOrderModel(item);
                }
            }
            if (answer == false) {
            }
        }

        public async void clearbtn(object sender, EventArgs e) {
            preCart = new List<OrderModel>();
            cartTotal.Text = "Total: $0.00";
        }
    }

       


    
}

