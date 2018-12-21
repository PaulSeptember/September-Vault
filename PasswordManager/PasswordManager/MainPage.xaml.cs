using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
           
           
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await UpdateFileList();
        }
        async Task UpdateFileList()
        {
            // получаем все файлы
            filesList.ItemsSource = await DependencyService.Get<IFileWorker>().GetFilesAsync();
            // снимаем выделение
            filesList.SelectedItem = null;
        }
        async void FileSelect(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null) return;
            string filename = (string)args.SelectedItem;
            await Navigation.PushAsync(new DatabaseOpenPage(filename));
            filesList.SelectedItem = null;
            await UpdateFileList();

        }
       
        private async void ToAbout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
        private async void ToCreate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateNewPage());
           
        }
        private async void ToSettings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
        private async void ToChoose(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChoosePage());
            
        }
    }
}
