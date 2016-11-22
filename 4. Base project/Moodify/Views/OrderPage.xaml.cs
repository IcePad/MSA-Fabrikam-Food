using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class OrderPage : ContentPage {
        string total;
        double calTotal = 0;
        string name = App.currentName;

        public OrderPage() {
            InitializeComponent();
            getOrder();
            
        }

        public async void getOrder() {
            List<OrderModel> orderItems = await AzureManager.AzureManagerInstance.GetOrderModels();
            orderItems.RemoveAll(OrderModel => OrderModel.Name != App.currentName);
            foreach (OrderModel item in orderItems) {
                calTotal = calTotal + item.Price;
            }

            orderList.ItemsSource = orderItems;
            total = "Total: $" + calTotal;
            nameDisplay.Text = name;
            totalDisplay.Text = total;
        }

     

    }
}
