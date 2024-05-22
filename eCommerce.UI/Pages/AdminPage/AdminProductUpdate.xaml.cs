using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using eCommerce.UI.Models;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class AdminProductUpdate : ContentPage, INotifyPropertyChanged
    {
        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { SetProperty(ref products, value); }
        }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set { SetProperty(ref selectedProduct, value); }
        }

        public AdminProductUpdate()
        {
            InitializeComponent();
            BindingContext = this;

            // Load products initially
            LoadProducts();
        }

        private async void LoadProducts()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("https://api.example.com/api/admin/products");

                if (response != null)
                {
                    Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async void OnUpdateProductButtonClicked(object sender, EventArgs e)
        {
            if (SelectedProduct == null)
            {
                await DisplayAlert("Hata", "L�tfen g�ncellenecek bir �r�n se�in.", "Tamam");
                return;
            }

            // Update the product
            bool success = await UpdateProductAsync(SelectedProduct);

            if (success)
            {
                await DisplayAlert("Ba�ar�l�", "�r�n ba�ar�yla g�ncellendi.", "Tamam");

                // Refresh product list
                LoadProducts();
            }
            else
            {
                await DisplayAlert("Hata", "�r�n g�ncellenirken bir hata olu�tu.", "Tamam");
            }
        }

        private async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"https://api.example.com/api/admin/products/{product.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
