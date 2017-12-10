using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cuentas_x_Cobrar.Models
{
    public class Account
    {
        [JsonProperty("AccountId")]
        public int AccountId { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("AccountType")]
        public string AccountType { get; set; }

        [JsonProperty("AllowTransactions")]
        public bool AllowTransactions { get; set; }
        [JsonProperty("Level")]
        public string Level { get; set; }
        [JsonProperty("ParentAccount")]
        public string ParentAccount { get; set; }
        [JsonProperty("Balance")]
        public string Balance { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
    }

    public class EntryModelRequest
    {
        [JsonProperty("EntryId")]
        public int EntryId { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("AuxiliarId")]
        public int AuxiliarId { get; set; }

        [JsonProperty("AccountId")]
        public int AccountId { get; set; }

        //[JsonProperty("Account")]
        //public Account Account { get; set; }

        [JsonProperty("MovementType")]
        public string MovementType { get; set; }

        [JsonProperty("EntryDate")]
        public DateTime EntryDate { get; set; }

        [JsonProperty("EntryAmount")]
        public double EntryAmount { get; set; }

        [JsonProperty("CurreyncyType")]
        public string CurreyncyType { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}