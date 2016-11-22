using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodify.DataModels {
    class FoodItemModel {

        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public double EmotionPrice { get; set; }
        public string Mood { get; set; }

    }
}
