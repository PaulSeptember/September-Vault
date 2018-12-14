using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DatabaseOpenPage : ContentPage
	{
        string file;
        string password;
        public DatabaseOpenPage(string filename)
		{
			InitializeComponent ();
            file = filename;
            databaseName.Text = "Database " + file;
            getPassword();
            
        }

        async void EncryptDatabase(object sender, EventArgs args)
        {
            
            if (!string.Equals(passwordEntry.Text,password))
            {
                await DisplayAlert("Error", "Wrong password", "Ok");
                passwordEntry.Text = "";
            }
            else
            {
                await DisplayAlert("Success", "Access granted!", "Ok");
                await Navigation.PushAsync(new DatabaseEditPage(file));
            }

        }
        async void getPassword()
        {
            password = await DependencyService.Get<IFileWorker>().LoadTextAsync(file);
            password = password.Split('\n')[0];
        }
    }
}