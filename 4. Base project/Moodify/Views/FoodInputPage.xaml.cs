using Moodify.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Moodify.Views {
    public partial class FoodInputPage : ContentPage {
        public FoodInputPage() {
            InitializeComponent();
        }

        public async void insert_Clicked(object sender, EventArgs e) {
            var nameText = nameInput.Text;
            var priceText = Double.Parse(priceInput.Text);
            var moodText = moodInput.Text;

            FoodItemModel FoodItem = new FoodItemModel() {
                Name = nameText,
                Price = priceText,
                Mood = moodText
            };

            await AzureManager.AzureManagerInstance.AddFoodItemModel(FoodItem);

        }

    }
}
