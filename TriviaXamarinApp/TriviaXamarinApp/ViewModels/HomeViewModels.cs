using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Views;
namespace TriviaXamarinApp.ViewModels
{
    class HomeViewModels : ModelViewBase
    {
       private bool isEnable;
        public bool IsEnable { get => isEnable; set { if (isEnable != value) { isEnable = value; OnPropertyChanged("IsEnable"); } } }

        public ICommand NavigateToPage { get; }
        

        public HomeViewModels()
        {
            NavigateToPage = new Command<string>(MoveToPage);
        }

        private async void MoveToPage(string obj)
        {
            Page p = null;
            switch(obj)
            {
                case "play":
                    p = new Play();
                    p.BindingContext = new PlayViewModels();
                    break;
                case "signIn":
                    p = new Login();
                    p.BindingContext = new LoginViewModels();
                    break;
                case "signUp":
                    p = new SignUp();
                    p.BindingContext = new SignUpViewModels();
                    break;

            }


            
            await Application.Current.MainPage.Navigation.PushAsync(p);



            
        }
    }

}