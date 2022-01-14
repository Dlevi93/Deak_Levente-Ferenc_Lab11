using System;
using Deak_Levente_Ferenc_Lab10;
using Deak_LeventeFerenc_Lab10.Models;
using Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deak_LeventeFerenc_Lab10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            slist.Date = DateTime.UtcNow;
            await App.Database.SaveShopListAsync(slist);
            await Navigation.PopAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(slist);
            await Navigation.PopAsync();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopl = (ShopList)BindingContext;

            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }

        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage((ShopList)
                BindingContext)
            {
                BindingContext = new Product()
            });
        }
    }
}