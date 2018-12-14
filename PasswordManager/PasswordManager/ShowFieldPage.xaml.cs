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
	public partial class ShowFieldPage : ContentPage
	{
		public ShowFieldPage(Field toShow)
		{
            InitializeComponent();
            name.Text ="Description: " + toShow.name;
            password.Text ="Password: " + toShow.password;
            url.Text ="URL: " + toShow.url;
            login.Text = "Login: " + toShow.login;
        }
	}
}