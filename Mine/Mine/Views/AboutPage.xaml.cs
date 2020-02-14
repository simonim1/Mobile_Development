using System.ComponentModel;
using Xamarin.Forms;
using System;


namespace Mine.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    /// <summary>
    /// About Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        /// <summary>
        /// Constructor for About Page
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Data Source Toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          void DataSource_Toggled(object sender, EventArgs e)
        {
            //// Flip the settings
            if (DataSourceValue.IsToggled == true)
            {
                MessagingCenter.Send(this, "SetDataSource", 1);
            }
            else
            {
                MessagingCenter.Send(this, "SetDataSource", 0);
            }
        }
    }


}