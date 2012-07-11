using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Facebook;
using System.IO.IsolatedStorage;

namespace PhoneApp1
{
    public partial class Login : PhoneApplicationPage
    {
        string oauthUrl = "https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&display=touch&response_type=token";
        string facebookAppId = "183642511732233";
        string redirectUri = "https://www.facebook.com/connect/login_success.html";

        public Login()
        {
            InitializeComponent();
            webBrowser1.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(webBrowser1_Navigated);
            webBrowser1.Navigate(new Uri(String.Format(oauthUrl, facebookAppId, redirectUri)));
        }

        void webBrowser1_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.ToString().StartsWith(redirectUri))
            {
                var client = new FacebookClient();
                var result = client.ParseOAuthCallbackUrl(e.Uri);
                var accessToken = result.AccessToken;

                var navUri = String.Format("/Welcome.xaml?access_token={0}", accessToken);
                NavigationService.Navigate(new Uri(navUri, UriKind.Relative));
            }
        }
    }
}