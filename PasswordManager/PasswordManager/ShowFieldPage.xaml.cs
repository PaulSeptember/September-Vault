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
        public bool showPassword = false;
        string Password;
        async void Reveal(object sender, EventArgs args)
        {
            showPassword = !showPassword;
            UpdatePassword();
        }

        void UpdatePassword()
        {
            string temp = "Password:";
            if (showPassword)
                password.Text = "Password: " + Password;
            else
            {
                Password = "Password:";
                for (int i = 0; i < Password.Length; i++)
                {
                    temp += "*";
                }
                password.Text = temp;
            }
        }

		public ShowFieldPage(Field toShow)
		{
            InitializeComponent();
            name.Text ="Description: " + toShow.name;
            url.Text ="URL: " + toShow.url;
            Password = toShow.password;
            login.Text = "Login: " + toShow.login;
            UpdatePassword();
        }
	}
}