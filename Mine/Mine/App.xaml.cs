﻿using Xamarin.Forms;
using Mine.Services;
using Mine.Views;
using Mine.ViewModels;

namespace Mine
{
    /// <summary>
    /// Main Application entry point
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Default App Constructor
        /// </summary>
        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            // DependencyService.Register<DatabaseService>();
            var temp=ViewModels.ItemIndexViewModel.Instance;
            // Call the Main Page to open
            MainPage = new MainPage();
        }

        /// <summary>
        /// On Startup code if needed
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// On Sleep code if needed
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// On App Resume code if needed
        /// </summary>
        protected override void OnResume()
        {
        }
    }
}