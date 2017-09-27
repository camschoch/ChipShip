using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models.ViewModels
{
    public class WalmartApiViewModel
    {
        public int numItems { get; set; }
        public int start { get; set; }
        public int totalResults { get; set; }
        public string query { get; set; }
        public string sort { get; set; }
        public List<ItemsItems>Items { get; set; }
    }
    public class ItemsItems
    {
        public int itemId { get; set; }
        public float salePrice { get; set; }
        public string name { get; set; }
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
        public string thumbnailImage { get; set; }        
    }
}