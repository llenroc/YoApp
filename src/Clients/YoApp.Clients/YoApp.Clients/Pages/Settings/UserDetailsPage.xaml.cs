﻿using Autofac;
using Xamarin.Forms;
using YoApp.Clients.Helpers;
using YoApp.Clients.ViewModels.Settings;

namespace YoApp.Clients.Pages.Settings
{
    public partial class UserDetailsPage : ContentPage, IPageService
    {
        public UserDetailsPage()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<UserDetailsPageViewModel>(
                new TypedParameter(typeof(IPageService), this));
        }
    }
}
