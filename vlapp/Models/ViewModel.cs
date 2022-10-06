using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vlapp
{
    internal class ViewModel
    {
        public List<MenuItemData> ItemList
        {
            get
            {
                return new List<MenuItemData>
                {
                    new MenuItemData(){IsItemSelected= true , MenuText ="Videos" },
                    new MenuItemData(){IsItemSelected= false , MenuText ="Arrangement" },
                    new MenuItemData(){IsItemSelected= false , MenuText ="Brightness" }
                };
            }
        }

    }

    public class MenuItemData
    {
        public bool IsItemSelected { get; set; }

        public string MenuText { get; set; }
    }
}
