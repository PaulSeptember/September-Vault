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
	public partial class ShowWifiPage : ContentPage
	{
        public bool showPassword = false;
       
        

       

        public ShowWifiPage(Field toShow)
        {
            InitializeComponent();
            Title = "Network " + toShow.name;
            
            url.Text = "Additional info: " + toShow.url;
            password.Text = " : " + toShow.password;
            login.Text = "SSID: " + toShow.login;
            
        }
    }
}