using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace LocalLendAppMobileCA.DataAccess
{
    [Table("Item")]

    class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ItemID { get; set; }

        [MaxLength(25)]
        public string ItemName { get; set; }

        [MaxLength(140)]
        public string ItemDescription { get; set; }

        public string ItemImage { get; set; }

        public string Availability { get; set; }

        public Item()
        { }

        public Item(string itemName, string itemDescription, string itemImage, string availability)
        {
            ItemName = itemName;
            ItemDescription = itemDescription;
            ItemImage = itemImage;
            Availability = availability;
        }
    }
}