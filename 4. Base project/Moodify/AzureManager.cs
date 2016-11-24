using Microsoft.WindowsAzure.MobileServices;
using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodify {
    class AzureManager {
        private static AzureManager instance;
        private MobileServiceClient client;
        public IMobileServiceTable<FoodItemModel> foodItemModelTable;
        private IMobileServiceTable<UserModel> userModelTable;
        private IMobileServiceTable<OrderModel> orderModelTable;

        private AzureManager() {
            this.client = new MobileServiceClient("https://moodtimelinee.azurewebsites.net/");
            this.foodItemModelTable = this.client.GetTable<FoodItemModel>();
            this.userModelTable = this.client.GetTable<UserModel>();
            this.orderModelTable = this.client.GetTable<OrderModel>();
        }

        public MobileServiceClient AzureClient {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance {
            get {
                if (instance == null) {
                    instance = new AzureManager();
                }

                return instance;
            }
        }


        //Add fooditems
        public async Task AddFoodItemModel(FoodItemModel foodItemModel) {
            await this.foodItemModelTable.InsertAsync(foodItemModel);
        }

        //Get foodItems
        public async Task<List<FoodItemModel>> GetFoodItemModels() {
            return await this.foodItemModelTable.ToListAsync();
        }

        //delete foodItems
        public async Task DeleteFoodItemModel(FoodItemModel foodItemModel) {
            await foodItemModelTable.DeleteAsync(foodItemModel); ;
        }
        //Update foodItems
        public async Task UpdateFoodItemModel(FoodItemModel foodItemModel) {
            await foodItemModelTable.UpdateAsync(foodItemModel);
        }

        //Add users
        public async Task AddUserModel(UserModel userModel) {
            await this.userModelTable.InsertAsync(userModel);
        }

        //get users
        public async Task<List<UserModel>> GetUserModels() {
            return await this.userModelTable.ToListAsync();
        }

        //add orders
        public async Task AddOrderModel(OrderModel orderModel) {
            await this.orderModelTable.InsertAsync(orderModel);
        }

        //get orders
        public async Task<List<OrderModel>> GetOrderModels() {
            return await this.orderModelTable.ToListAsync();
        }

        //delete foodItems
        public async Task DeleteOrderModel(OrderModel orderModel) {
            await orderModelTable.DeleteAsync(orderModel); ;
        }
    }
}
