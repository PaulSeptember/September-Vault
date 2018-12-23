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
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public MasterDetailPage1()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPage1MenuItem;
            if (item == null)
                return;
            Page page = null;
            switch (item.Id){
                case 0:
                    page = new MainPage();
                    break;
                case 1:
                    page = new SettingsPage();
                    break;
                case 2:
                    page = new MainPage();
                    break;
                case 3:
                    page = new AboutPage();
                    break;
            }

                //var page = new SettingsPage();// (Page)Activator.CreateInstance(item.TargetType);
                //page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;
           
            MasterPage.ListView.SelectedItem = null;
        }
    }
}