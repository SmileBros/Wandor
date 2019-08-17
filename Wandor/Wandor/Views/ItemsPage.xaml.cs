using System;
using System.ComponentModel;
using Wandor.Models;
using Wandor.ViewModels;
using Xamarin.Forms;

namespace Wandor.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        //private ItemsPageViewModel ViewModel => BindingContext as ItemsPageViewModel;

        public ItemsPage() {
            InitializeComponent();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args) {
            //if (!(args.SelectedItem is Item item))
            //    return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            //// Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        private async void AddItem_Clicked(object sender, EventArgs e) {
            //await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            //if (ViewModel.Items.Count == 0)
            //    ViewModel.LoadItemsCommand.Execute(null);
        }
    }
}
