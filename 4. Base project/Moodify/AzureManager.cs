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
        private IMobileServiceTable<EmotionModel> emotionModelTable;
        private IMobileServiceTable<FoodItemModel> foodItemModelTable;
        private IMobileServiceTable<UserModel> userModelTable;

        private AzureManager() {
            this.client = new MobileServiceClient("http://moodtimelinee.azurewebsites.net/");
            this.emotionModelTable = this.client.GetTable<EmotionModel>();
            this.foodItemModelTable = this.client.GetTable<FoodItemModel>();
            this.userModelTable = this.client.GetTable<UserModel>();
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

        public async Task AddEmotionModel(EmotionModel emotionModel) {
            await this.emotionModelTable.InsertAsync(emotionModel);
        }

        public async Task<List<EmotionModel>> GetEmotionModels() {
            return await this.emotionModelTable.ToListAsync();
        }

        public async Task AddFoodItemModel(FoodItemModel foodItemModel) {
            await this.foodItemModelTable.InsertAsync(foodItemModel);
        }

        public async Task<List<FoodItemModel>> GetFoodItemModels() {
            return await this.foodItemModelTable.ToListAsync();
        }

        public async Task AddUserModel(UserModel userModel) {
            await this.userModelTable.InsertAsync(userModel);
        }

        public async Task<List<UserModel>> GetUserModels() {
            return await this.userModelTable.ToListAsync();
        }
    }
}
