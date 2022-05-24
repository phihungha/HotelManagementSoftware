using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagementSoftware.UI.Windows
{
    /// <summary>
    /// Interaction logic for MaintenanceEditWindow.xaml
    /// </summary>
    public partial class MaintenanceEditWindow : Window
    {
        public MaintenanceEditWindow()
        {
            InitializeComponent();
            DataContext = new ItemsVM();
        }

        
    }

    public class ItemsVM : ObservableObject
    {
        public ObservableCollection<Item> Items { get; set; }
        public ItemsVM()
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
