using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.Models
{
    public class MainModel
    {
        public string StatusText = "준비"; // 공통


        public string SearchText = string.Empty; // Search Tab


        public string UriText = string.Empty; // Crawling Tab
        public string CrawlResult = string.Empty;



        public double Quantity = 1;
        public string Password = string.Empty;
        public int CurrentPage = 1;
        public int TotalPages = 12;
        public bool IsBusy = false;
        public bool NotificationsEnabled = true;
        public int Score = 3;
        public string FilePath = string.Empty;
        public string FolderPath = string.Empty;
    }
}
