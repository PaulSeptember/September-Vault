﻿using System;
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
        bool showed = false;

	    public DatabaseEditPage(string filename)
		{
			InitializeComponent ();
            this.file = filename;
            Title = "Database " + filename;
            kostyl();
            
            

        }

        async void kostyl()
        {
            var str = await DependencyService.Get<IFileWorker>().LoadTextAsync(file);
            database = new Database(str);
            databasesList.ItemsSource = database.getForList();
            UpdateCat();
        }

        async void UpdateCat()
        {
            if (database.getForList().Length > 0)
                Cat.Source = "sleeping_cat_bellow";
            else
                Cat.Source = "sad_cat_bellow";
            if (database.getForList().Length > 6)
                Cat.IsEnabled = false;
        }

        async void UpdateList()
        {
            databasesList.ItemsSource = database.getForList();
            UpdateCat();       
        }

        async void DeleteExist(object sender, EventArgs args)
        {
            string name = ((Field)((MenuItem)sender).BindingContext).name;
            database.Remove(name);
            Save();
            UpdateList();
        }

        

        async public void CreateNew(object sender, SelectedItemChangedEventArgs args)
        {
            showed = !showed;
            ButtonAdd.Image = showed? "images_rotated":"images";
            button0.IsVisible = showed;
            button1.IsVisible = showed;
            button2.IsVisible = showed;
            button3.IsVisible = showed;

            //await Navigation.PushAsync(new NewFieldPage(file,this));
        }
        async public void NewKey(object sender, SelectedItemChangedEventArgs args)
        {
            await Navigation.PushAsync(new NewFieldPage(file, this));
        }
        async public void NewWifi(object sender, SelectedItemChangedEventArgs args)
        {
            await Navigation.PushAsync(new AddWifiPage(file, this));
        }
        async public void NewCard(object sender, SelectedItemChangedEventArgs args)
        {
            await Navigation.PushAsync(new AddCardPage(file, this));
        }
        async public void NewSecure(object sender, SelectedItemChangedEventArgs args)
        {
            await Navigation.PushAsync(new AddSecureNotePage(file, this));
        }


        async public void add(Field temp)
        {
            database.Add(temp);
            UpdateList();
        }

        async public void DatabaseSelect(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null) return;
            string selectedName = ((Field)args.SelectedItem).name;
            if (((Field)args.SelectedItem).type == "secure")
            {
                databasesList.SelectedItem = null;
                await Navigation.PushAsync(new ShowSecureNotePage(database.findByName(selectedName)));
            }
            if (((Field)args.SelectedItem).type == "wifi")
            {
                databasesList.SelectedItem = null;
                await Navigation.PushAsync(new ShowWifiPage(database.findByName(selectedName)));
            }
            if (((Field)args.SelectedItem).type == "card")
            {
                databasesList.SelectedItem = null;
                await Navigation.PushAsync(new ShowCardPage(database.findByName(selectedName)));
            }
            if (((Field)args.SelectedItem).type == "key")
            {
                databasesList.SelectedItem = null;
                await Navigation.PushAsync(new ShowFieldPage(database.findByName(selectedName)));
            }
        }

        async public void Save()
        {
            await DependencyService.Get<IFileWorker>().SaveTextAsync(file, database.getForSave());
        }
	}
}