﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Moodify.DataModels {
    class UserModel {

        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] EncryptedPass { get; set; }
    }
}
