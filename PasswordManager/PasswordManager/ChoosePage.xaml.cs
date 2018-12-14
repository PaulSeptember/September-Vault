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
	public partial class ChoosePage : ContentPage
	{
		public ChoosePage ()
		{
			InitializeComponent();
		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await UpdateFileList();
        }
        async void FileSelect(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null) return;
            // получаем выделенный элемент
            string filename = (string)args.SelectedItem;
            await Navigation.PushAsync(new DatabaseOpenPage(filename));
            // загружем текст в текстовое поле
            //textEditor.Text = await DependencyService.Get<IFileWorker>().LoadTextAsync((string)args.SelectedItem);
            // устанавливаем название файла
            // снимаем выделение
            filesList.SelectedItem = null;

        }
        async void Delete(object sender, EventArgs args)
        {
            // получаем имя файла
            string filename = (string)((MenuItem)sender).BindingContext;
            // удаляем файл из списка
            await DependencyService.Get<IFileWorker>().DeleteAsync(filename);
            // обновляем список файлов
            await UpdateFileList();
        }
        // обновление списка файлов
        async Task UpdateFileList()
        {
            // получаем все файлы
            filesList.ItemsSource = await DependencyService.Get<IFileWorker>().GetFilesAsync();
            // снимаем выделение
            filesList.SelectedItem = null;
        }
    }
}