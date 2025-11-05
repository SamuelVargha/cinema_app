using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CinemaApp.Model;
using CinemaApp.Repositories;
using FontAwesome.Sharp;

namespace CinemaApp.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        //fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }


        public string Caption {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        // --> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowMovieViewCommand { get; }
        public ICommand ShowScreeningViewCommand { get; }
        public ICommand ShowReservationViewCommand { get; }
        public ICommand ShowAboutViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            // initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowMovieViewCommand = new ViewModelCommand(ExecuteShowMovieViewCommand);
            ShowScreeningViewCommand = new ViewModelCommand(ExecuteShowScreeningViewCommand);
            ShowReservationViewCommand = new ViewModelCommand(ExecuteReservationViewCommand);
            ShowAboutViewCommand = new ViewModelCommand(ExecuteAboutViewCommand);

            //default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowMovieViewCommand(object obj)
        {
            CurrentChildView = new MovieViewModel();
            Caption = "Movies";
            Icon = IconChar.Film;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Home";
            Icon = IconChar.Home;
        }

        private void ExecuteShowScreeningViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Screenings";
            Icon = IconChar.Tv;
        }

        private void ExecuteReservationViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Reservations";
            Icon = IconChar.List;
        }

        private void ExecuteAboutViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "About";
            Icon = IconChar.Info;
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if(user != null)
            {

                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"{user.FirstName} {user.LastName}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName="Invalid user, not logged in";

            }
        }
    }
}
