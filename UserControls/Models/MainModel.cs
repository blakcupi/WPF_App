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
        public string CrawlResult = string.Empty; // Crawling Tab
        public bool IsBusy = false; // Crawling Tab


        public bool IsChecked = false; // ToggleSwitch Tab

        public double Value = 0; // Etc. Tab

        public int StarValue = 0; // Etc. Tab
    }
}
