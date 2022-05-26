using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class MaintenanceEditWindowVM : ObservableValidator
    {
        public ObservableCollection<Item> Items { get; set; }
        public MaintenanceEditWindowVM()
        {
            Items = new ObservableCollection<Item>();
            Items.Add(new Item("Lightbulb", 3, "20W lightbulb"));
            Items.Add(new Item("TV", 1, "Samsung TV"));
            Items.Add(new Item("Lightbulb", 3, "20W lightbulb"));
            Items.Add(new Item("Lightbulb", 3, "20W lightbulb"));
            Items.Add(new Item("TV", 1, "Samsung TV"));
            Items.Add(new Item("TV", 1, "Samsung TV"));
        }
    }
    public class Item
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }

        public Item(string? name, int quantity, string? description)
        {
            Name = name;
            Quantity = quantity;
            Description = description;
        }
    }
}
