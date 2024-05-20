namespace eCommerce.UI
{
    public partial class AppShell : Shell
    {
        private readonly bool _isAdmin = false;
        private readonly bool _isCustomer = false;

        public bool IsAdmin => _isAdmin;
        public AppShell()
        {
            var role = Preferences.Get("role", "customer");
            if (role == "customer")
                _isCustomer = true;
            
            if(role=="admin")
                _isAdmin = true;

            InitializeComponent();
            BindingContext = this;
        }
    }
}
