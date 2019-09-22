using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Wandor.Models;
using Wandor.Services;
using Wandor.Views;
using Xamarin.Forms;

namespace Wandor.ViewModels
{
    public class ItemsPageViewModel : BaseViewModel
    {
        private readonly IDataStore<Item> _dataStore;

        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsPageViewModel(IDataStore<Item> DataStore)
        {
            _dataStore = DataStore;

            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await _dataStore.AddItemAsync(newItem);
            });
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _dataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
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
    }
}
