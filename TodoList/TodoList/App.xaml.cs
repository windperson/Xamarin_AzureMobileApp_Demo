using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Abstractions;
using TodoList.Services;
using Xamarin.Forms;

namespace TodoList
{
	public partial class App : Application
	{
	    public static ICloudService CloudService { get; set; }

        public App ()
		{
            CloudService = new AzureCloudService();

			InitializeComponent();

			MainPage = new NavigationPage(new Pages.EntryPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
