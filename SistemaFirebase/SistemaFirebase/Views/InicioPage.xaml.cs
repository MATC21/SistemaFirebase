using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SistemaFirebase.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioPage : ContentPage
    {
        public string WebAPIKey = "AIzaSyCf-QCWIi-xJ4DogQ-Vm32OU5r-OSqnBTU";
        public InicioPage()
        {
            InitializeComponent();
            GetProfileInformationAndRefreshToken();
        }

        private async void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));

            try
            {
                var savedfirebaseauth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);

                MyUsername.Text = savedfirebaseauth.User.Email;

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "ho no! El Token Caduco ", "Ok");
            }
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            App.Current.MainPage =  new NavigationPage();  
        }
    }
}