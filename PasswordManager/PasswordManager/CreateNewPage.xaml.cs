using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class CreateNewPage : ContentPage
    {
        public CreateNewPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        // сохранение текста в файл

        async void Reveal(object sender, EventArgs args)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
        }

        async void Random(object sender, EventArgs args)
        {
            PasswordEntry.Text = "1234";
            ConfirmPasswordEntry.Text = "1234";
            PasswordEntry.IsPassword = false;
            ConfirmPasswordEntry.IsPassword = false;
        }
        async void Save(object sender, EventArgs args)
        {
            string filename = fileNameEntry.Text;
            if (string.IsNullOrEmpty(filename))
            {
                await DisplayAlert("Error", "Empty filename", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Empty password field", "Ok");
                return;
            }
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Passwords does not match", "Ok");
                return;
            }
            // если файл существует
            if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
            {
                // запрашиваем разрешение на перезапись
                bool isRewrited = await DisplayAlert("Подверждение", "File with same name already exists. Rewrite?", "Да", "Нет");
                if (isRewrited == false) return;
            }
            // перезаписываем файл
            string Database = PasswordEntry.Text + '\n';
            await DependencyService.Get<IFileWorker>().SaveTextAsync(fileNameEntry.Text, Database);
            await DisplayAlert("Success", "Saved correctly.", "Ok");
            PasswordEntry.Text = "";
            ConfirmPasswordEntry.Text = "";
            fileNameEntry.Text = "";
            
            await Navigation.PushAsync(new DatabaseEditPage(filename));
            Navigation.RemovePage(this);
            // обновляем список файлов
        }
        
    }
}