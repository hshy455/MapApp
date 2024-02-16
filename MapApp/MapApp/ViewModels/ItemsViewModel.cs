using MapApp.Models;
using MapApp.Resources;
using MapApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;
        private string _searchQuery;
        private ObservableCollection<Item> _filteredItems;

        public ObservableCollection<Item> Items { get; }
        public ObservableCollection<Item> FilteredItems
        {
            get => _filteredItems;
            set => SetProperty(ref _filteredItems, value);
        }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                SetProperty(ref _searchQuery, value);
                FilterItems();
            }
        }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            FilteredItems = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
                FilteredItems = Items;
                FilterItems(); // Filter items initially
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem()
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            Data.SelectedToilet = item.Toilet;
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        private void FilterItems()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                FilteredItems = Items;
            }
            else
            {
                var filtered = Items.Where(item =>
                    item.Text.ToLower().Contains(SearchQuery.ToLower()) ||
                    item.Description.ToLower().Contains(SearchQuery.ToLower()));
                foreach (var item in filtered)
                {
                    Console.WriteLine(item.Text);
                }
                FilteredItems = new ObservableCollection<Item>(filtered);
            }
        }
    }
}