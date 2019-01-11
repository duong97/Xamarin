using System;
using System.Collections.Generic;
using System.Text;

namespace ProductChecker.Models
{
    public enum MenuItemType
    {
        Homepage, // Show items soon expire or sale out
        Import,
        Export,
        Check,
        List, // List product
        Report,
        Setting,
        About,
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
