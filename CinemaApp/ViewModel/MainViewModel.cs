using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Movie> movies;

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

            movies = new ObservableCollection<Movie>
            {
                new Movie
                {
                    Title = "Iron Man 2",
                    PosterPath = "/CinemaApp;component/Images/iron_man_2.jpg",
                    Description = "Tony Stark faces growing pressure from the government, rivals, and his own declining health as he continues to operate as Iron Man, leading to new alliances and dangerous enemies.",
                    Screenings = new List<Screening>
                    {
                        new Screening { Date = "2025-03-01", Time = "18:30", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-01", Time = "21:00", Hall = "Hall 2" },
                    }
                },

                new Movie
                {
                    Title = "The Revenant",
                    PosterPath = "/CinemaApp;component/Images/revenant.jpg",
                    Description = "A frontiersman fights to survive in the brutal wilderness after being left for dead, pushing himself through extreme conditions driven by determination and resilience.",
                    Screenings = new List<Screening>
                    {
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 2" },
                    }
                },
                new Movie
                {
                    Title = "Fight Club",
                    PosterPath = "/CinemaApp;component/Images/fight_club.jpg",
                    Description = "A disillusioned office worker forms an underground fight club with a mysterious stranger, unraveling into a dark exploration of identity, rebellion, and modern masculinity.",
                    Screenings = new List<Screening>
                    {
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 2" },
                    }
                },
                new Movie
                {
                    Title = "Who Killed Captain Alex",
                    PosterPath = "/CinemaApp;component/Images/alex.jpg",
                    Description = "Uganda’s first action movie follows a chaotic mission to uncover the fate of a special forces commander, blending over-the-top action, humor, and the unforgettable commentary of VJ Emmie.",
                    Screenings = new List<Screening>
                    {
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 2" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 2" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 2" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 2" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 1" },
                    }
                }
            };

            // initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowMovieViewCommand = new ViewModelCommand(ExecuteShowMovieViewCommand);
            ShowScreeningViewCommand = new ViewModelCommand(ExecuteShowScreeningViewCommand);
            ShowReservationViewCommand = new ViewModelCommand(ExecuteReservationViewCommand);
            ShowAboutViewCommand = new ViewModelCommand(ExecuteAboutViewCommand);

            //default view
            ExecuteShowMovieViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowMovieViewCommand(object obj)
        {
            CurrentChildView = new MovieViewModel(movies);
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
            CurrentChildView = new ScreeningsViewModel(movies);
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
