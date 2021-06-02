using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using Xamarin.Forms;
using TriviaXamarinApp.Views;
namespace TriviaXamarinApp.ViewModels
{
    class LoginViewModels:ModelViewBase
    {
        //הגדרת משתנים
        private string email;
        private string password;
        //אוביקט פרוקסי בו יש את הפעולות מתקיית סרוויס
        private TriviaWebAPIProxy proxy;

        //properties
        public string Email { get=> email; set { if (email != value) { email = value; OnPropertyChanged("Email"); } } }
        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChanged("Password"); } } }
        public ICommand LoginCommand { get; }

        public LoginViewModels()
        {
           proxy = TriviaWebAPIProxy.CreateProxy();
            LoginCommand = new Command(Login);
        }

        private async void Login()
        {
            User user = null;
            //קריאה לפעולת לוגין מסרוויס
            user = await proxy.LoginAsync(email, Password);
            if (user == null)
            {
                //הודעת שגיאה
                await Application.Current.MainPage.DisplayAlert("Login failed", "User name or password invalid", "OK");
            }
            else
            {
                Page p = new Play();
                p.BindingContext = new PlayViewModels();
                await Application.Current.MainPage.Navigation.PushAsync(p);
            }
        }
    }
}
