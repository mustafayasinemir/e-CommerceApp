using Microsoft.Maui.Controls;

namespace eCommerce.UI.Pages
{
    public partial class PaymentPage : ContentPage
    {
        public PaymentPage()
        {
            InitializeComponent();
        }

        private void OnCardNumberChanged(object sender, TextChangedEventArgs e)
        {
            CardNumberLabel.Text = FormatCardNumber(e.NewTextValue);
        }

        private void OnExpiryDateChanged(object sender, TextChangedEventArgs e)
        {
            ExpiryDateLabel.Text = e.NewTextValue;
        }

        private void OnCVVChanged(object sender, TextChangedEventArgs e)
        {
            CVVLabel.Text = e.NewTextValue;
        }

        private void OnPayButtonClicked(object sender, EventArgs e)
        {
            // �deme i�lemini ger�ekle�tirme kodu buraya gelecek
            DisplayAlert("�deme", "�deme s�reci ba�lat�ld�.", "Tamam");
        }

        private string FormatCardNumber(string cardNumber)
        {
            // Kart numaras�n� 4 haneli gruplara ay�rma
            if (string.IsNullOrWhiteSpace(cardNumber))
                return string.Empty;

            cardNumber = cardNumber.Replace(" ", "");
            return string.Join(" ", cardNumber.Select((c, i) => i > 0 && i % 4 == 0 ? " " + c : c.ToString()));
        }
    }
}
