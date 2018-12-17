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
	public partial class DatabaseEditPage : ContentPage
	{
        string file;
        Database database;

	    public DatabaseEditPage(string filename)
		{
			InitializeComponent ();
            this.file = filename;
            Caption.Text = "Database " + filename;
            kostyl();           
		}

        async void kostyl()
        {
            var str = await DependencyService.Get<IFileWorker>().LoadTextAsync(file);
            database = new Database(str);
            databasesList.ItemsSource = database.getForList();   
        }

        async void UpdateList()
        {
            databasesList.ItemsSource = database.getForList();     
        }

        async void DeleteExist(object sender, EventArgs args)
        {
            string name = (string)((MenuItem)sender).BindingContext;
            database.Remove(name);
            Save();
            UpdateList();
        }

        async public void CreateNew(object sender, SelectedItemChangedEventArgs args)
        {
            //TODO: different types of fields
            await Navigation.PushAsync(new NewFieldPage(file,this));
        }

        async public void add(Field temp)
        {
            database.Add(temp);
            UpdateList();
        }

        async public void DatabaseSelect(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null) return;
            string selectedName = (string)args.SelectedItem;
            databasesList.SelectedItem = null;
            await Navigation.PushAsync(new ShowFieldPage(database.findByName(selectedName)));
        }

        async public void Save()
        {
            await DependencyService.Get<IFileWorker>().SaveTextAsync(file, database.getForSave());
        }
	}
}