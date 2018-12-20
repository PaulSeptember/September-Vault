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
	public partial class NewFieldPage : ContentPage
	{
        string _file;
        Database database;
        DatabaseEditPage parent;
        bool visible = true;
		public NewFieldPage (string file, DatabaseEditPage parent)
		{
			InitializeComponent ();
            _file = file;
            this.parent = parent;
            getDatabase();
        }

        async void getDatabase()
        {
            var str = await DependencyService.Get<IFileWorker>().LoadTextAsync(_file);
            database = new Database(str);
        }

        async void Reveal()
        {
            visible = !visible;
            passwordEntry.IsPassword = visible;
        }

        async void Save(object sender, EventArgs args)
        {
            Field _field = new Field();
            _field.name = nameEntry.Text;
            _field.login = loginEntry.Text;
            _field.password = passwordEntry.Text;
            _field.url = urlEntry.Text;
            _field.icon = "key";
            database.Add(_field);
            parent.add(_field);

            await DependencyService.Get<IFileWorker>().SaveTextAsync(_file, database.getForSave());
            await Navigation.PopAsync();
            return;
        }
	}
}