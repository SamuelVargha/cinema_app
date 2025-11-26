using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CinemaApp.View;
using CinemaApp.Model;

namespace CinemaApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private List<UserAccountModel> userAccounts;

        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            userAccounts = new List<UserAccountModel>();

            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainView(userAccounts);
                    mainView.Show();
                    loginView.Close();
                }
            };
        }
    }
}
