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
    class SignUpViewModels : ModelViewBase
    {
        //הגדרת משתנים
        private string email;
        private string password;
        //אובביקט פרוקסי בו יש את הפעולות מתקיית סרוויס
        private TriviaWebAPIProxy proxy;

        //properties
        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged("UserName"); } } }
        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChanged("Password"); } } }
        public ICommand SignUpCommand { get; }

        public SignUpViewModels()
        {
            proxy = TriviaWebAPIProxy.CreateProxy();
            SignUpCommand = new Command(SignUp);
        }

        private async void SignUp()
        {
            bool isRegister = false;
            User u = new User { Email= Email, Password=Password};
            
            //קריאה לפעולת לוגין מסרוויס
            isRegister = await proxy.RegisterUser(u);
            if (!isRegister)
            {
                //הודעת שגיאה
                await Application.Current.MainPage.DisplayAlert("login failed", "user name or password invalid", "OK");
            }
            else
            {
                Page p = new Login();
                p.BindingContext = new LoginViewModels();
                await Application.Current.MainPage.Navigation.PushAsync(p);
            }
        }
    }
}
