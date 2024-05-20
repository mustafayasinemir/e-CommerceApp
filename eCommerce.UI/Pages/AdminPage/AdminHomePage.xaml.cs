using System;
using Microsoft.Maui.Controls;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class AdminHomePage : ContentPage
    {
        public AdminHomePage()
        {
            InitializeComponent();
        }

        private async void OnAddCategoryButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminCategoryAdded());
        }

        //private async void OnUpdateCategoryButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new AdminCategoryUpdated());
        //}

        private async void OnRemoveCategoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminCategoryRemove());
        }



        private async void OnAddProductButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminProductAdded());
        }

        //private async void OnUpdateProductButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new AdminProductUpdated());
        //}

        //private async void OnDeleteProductButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new AdminProductDeleted());
        //}
    }
}
