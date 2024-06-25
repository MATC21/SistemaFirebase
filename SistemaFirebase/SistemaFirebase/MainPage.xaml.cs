using Firebase.Auth;
using Newtonsoft.Json;
using SistemaFirebase.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SistemaFirebase
{
    public partial class MainPage : ContentPage
    {
        public string WebAPIKey = "AIzaSyCf-QCWIi-xJ4DogQ-Vm32OU5r-OSqnBTU";
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            string emaili = email.Text.Trim();
            string passwordi = password.Text.Trim();

            if (!IsValidEmail(emaili))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "Ok");
                return;
            }

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));

            try
            {   
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(emaili, passwordi);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedContent);

                await Navigation.PushAsync(new InicioPage());

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            string email = newemail.Text.Trim();
            string password = newpassword.Text.Trim();

            if (!IsValidEmail(email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "Ok");
                return;
            }

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                string gettoken = auth.FirebaseToken;

                await App.Current.MainPage.DisplayAlert("Alert", gettoken, "Ok");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
