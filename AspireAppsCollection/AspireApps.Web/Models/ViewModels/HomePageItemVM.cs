using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspireApps.ViewModels
{
    public class HomePageItemVM
    {
        public string ImageURL { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
    }

    public class AppVM
    {
        public String Category { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String StoreURL { get; set; }
        public String ImageURL { get; set; }
        public Boolean Active { get; set; }
    }
}