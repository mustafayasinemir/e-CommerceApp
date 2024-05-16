using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eCommerce.UI.Models
{
    public class ShoppingCartItem : INotifyPropertyChanged
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("totalAmount")]
        public int TotalAmount { get; set; }

        [JsonProperty("qty")]

        private int qty;

        public int Qty
        {
            get { return qty; }
            set
            {
                if (qty != value)
                {
                    qty = value;
                    OnPropertyChanged();
                }

            }
        }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
