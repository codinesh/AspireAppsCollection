using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspireApps.ViewModels
{
    public class IndexVM
    {
        public IndexVM() { }
        public List<HomePageItemVM> HomePageItems { get; set; }
        public List<AppVM> AndroidAppList { get; set; }
        public List<AppVM> WindowsAppList { get; set; }
    }
}