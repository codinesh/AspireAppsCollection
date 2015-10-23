using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspireApps.Models
{
    public class HomePageItem : TableEntity
    {
        public HomePageItem() { }
        public HomePageItem(string partitionKey, int rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = RowKey;
        }

        public HomePageItem(string partitionKey, int rowKey, string imageName, string description, string heading)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = RowKey;
            this.ImageName = imageName;
            this.Description = description;
            this.Heading = heading;
        }

        public string ImageName { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
    }

    public class App : TableEntity
    {
        public App() { }
        public App(string partitionKey, int rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = RowKey;
        }

        public App(string partitionKey, int rowKey, string name, string description, string imageUrl, string storeUrl, bool active)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = RowKey;
            this.Name = name;
            this.Description = description;
            this.StoreURL = storeUrl;
            this.ImageURL = imageUrl;
        }

        public String Name { get; set; }
        public string Description { get; set; }
        public string StoreURL { get; set; }
        public string ImageURL { get; set; }
        public Boolean Active { get; set; }
    }
}