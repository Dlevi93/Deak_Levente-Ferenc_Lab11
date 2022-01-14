using System;
using System.Collections.Generic;
using Deak_Levente_Ferenc_Lab10;
using Deak_LeventeFerenc_Lab10.Models;
using Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deak_LeventeFerenc_Lab10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        ShopList sl;

        public ProductPage(ShopList slist)
        {
            InitializeComponent();
            sl = slist;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.SaveProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.DeleteProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Product p;
            if (e.SelectedItem != null)
            {
                p = e.SelectedItem as Product;
                var lp = new ListProduct()
                {
                    ShopListID = sl.ID,
                    ProductID = p.ID
                };
                await App.Database.SaveListProductAsync(lp);
                p.ListProducts = new List<ListProduct> { lp };

                await Navigation.PopAsync();
            }
        }
    }
}