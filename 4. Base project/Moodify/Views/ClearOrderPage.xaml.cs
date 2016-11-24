using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class ClearOrderPage : ContentPage {
        //Variables
        string total;
        double calTotal = 0;
        bool inDb = false;
        string n;

        public ClearOrderPage() {
            InitializeComponent();

        }




        public async void getOrders_Clicked(object sender, EventArgs e) {
            if (customerName != null ) {
                //Progress bar
                ProgressIndicator.IsRunning = true;
                //Get orders from OrderModel table
                List<OrderModel> orderItems = await AzureManager.AzureManagerInstance.GetOrderModels();
                foreach (var item in orderItems) {
                    if(customerName.Text == item.Name) {
                        inDb = true;
                    }
                }

                if (inDb == true) {
                    //removes all orders not pertaining to the name in the entry field.
                    orderItems.RemoveAll(OrderModel => OrderModel.Name != customerName.Text);
                    foreach (OrderModel item in orderItems) {
                        calTotal = calTotal + item.Price;
                    }

                    //Displays user name and current total of orders.
                    orderList.ItemsSource = orderItems;
                    total = "Total: $" + calTotal;
                    totalDisplay.Text = total;
                    clearBtn.IsVisible = true;
                    //Progress bar
                    ProgressIndicator.IsRunning = false;
                    n = customerName.Text;
                } else {
                    await DisplayAlert("Alert", "Customer does not have any orders", "OK");
                    //Progress bar
                    ProgressIndicator.IsRunning = false;
                }
            } else {
                await DisplayAlert("Alert", "Please enter customer name!", "OK");
                //Progress bar
                ProgressIndicator.IsRunning = false;
            }
        }

        public async void clearOrders_Clicked(object sender, EventArgs e) {
            //Variables 
            bool itemInDb = false;
            //Progress bar
            ProgressIndicator.IsRunning = true;
            //Get the items in the OrderModel table 
            List<OrderModel> orderItems = await AzureManager.AzureManagerInstance.GetOrderModels();
            //Check to see if the name entered equals the text field. If so delete that order from table
            //Also change bool of itemInDb to true.
            foreach (OrderModel order in orderItems) {
                if (order.Name == n) {
                    await AzureManager.AzureManagerInstance.DeleteOrderModel(order);
                    itemInDb = true;
                }
            }
            App.RootPage.Detail = new NavigationPage(new ClearOrderPage());
            await DisplayAlert("Alert", n + " order has been cleared!", "OK");
            //No item in db, alert user. 
            if (itemInDb == false) {
                await DisplayAlert("Alert", n + " has no orders to clear!", "OK");
            }
            //Progress bar
            ProgressIndicator.IsRunning = false;
        }
    }



}

