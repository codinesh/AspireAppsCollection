using AspireApps.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspireApps.Web.Controllers
{
    public class AspireAppsDataController : ApiController
    {
        // GET: api/HomeData
        [HttpGet, ActionName("HomePageItems")]
        public IEnumerable<HomePageItem> GetHomePageItems()
        {
            var HomePageItems = GetStorageItemsFromTable<HomePageItem>("HomePageItems").ToList();
            var HomePageBlobImages = GetStorageItemsFromBlob("homepageimages");
            foreach (HomePageItem item in HomePageItems)
            {
                if (HomePageBlobImages.Any(i => i.Name == item.ImageName))
                {
                    item.ImageName = HomePageBlobImages.Where(i => i.Name == item.ImageName).First<CloudBlockBlob>().Uri.AbsoluteUri;
                }
            }
            return HomePageItems;
        }
        public IEnumerable<App> GetAppList()
        {
            var apps = GetStorageItemsFromTable<App>("AppsList").ToList();
            var HomePageBlobImages = GetStorageItemsFromBlob("appimages");
            foreach (App app in apps)
            {
                if (HomePageBlobImages.Any(i => i.Name == app.ImageURL))
                {
                    app.ImageURL = HomePageBlobImages.Where(i => i.Name == app.ImageURL).First<CloudBlockBlob>().Uri.AbsoluteUri;
                }
            }
            return apps;
        }

        private IEnumerable<CloudBlockBlob> GetStorageItemsFromBlob(string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            List<CloudBlockBlob> list = new List<CloudBlockBlob>();
            try
            {
                foreach (IListBlobItem item in container.ListBlobs(null, false))
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        list.Add(blob);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        private IEnumerable<T> GetStorageItemsFromTable<T>(string tableName) where T : TableEntity, new()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);
            TableQuery<T> query = new TableQuery<T>();
            return table.ExecuteQuery<T>(query).ToList<T>();
        }
    }
}
