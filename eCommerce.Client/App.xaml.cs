﻿namespace eCommerce.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SignupPage();
        }
    }
}
