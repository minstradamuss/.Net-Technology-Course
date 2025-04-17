using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;

namespace AtomicHeart
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly Stack<LocationModel> _backStack = new();
        private readonly Stack<LocationModel> _forwardStack = new();
        private LocationModel _currentLocation;
        private int _currentImageIndex = 0;

        public ObservableCollection<LocationModel> Locations { get; set; }

        public LocationModel CurrentLocation
        {
            get => _currentLocation;
            set
            {
                if (_currentLocation != value)
                {
                    if (_currentLocation != null) _backStack.Push(_currentLocation);
                    _currentLocation = value;
                    _currentImageIndex = 0;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentImage));
                }
            }
        }

        public string CurrentImage =>
            CurrentLocation?.ImagePaths != null && CurrentLocation.ImagePaths.Count > 0
                ? CurrentLocation.ImagePaths[_currentImageIndex]
                : null;

        public ICommand NavigateCommand => new RelayCommand<LocationModel>(location =>
        {
            if (location != null)
            {
                _forwardStack.Clear();
                CurrentLocation = location;
            }
        });

        public ICommand BackCommand => new RelayCommand(() =>
        {
            if (_backStack.Count > 0)
            {
                _forwardStack.Push(_currentLocation);
                _currentLocation = _backStack.Pop();
                _currentImageIndex = 0;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(CurrentImage));
            }
        });

        public ICommand ForwardCommand => new RelayCommand(() =>
        {
            if (_forwardStack.Count > 0)
            {
                _backStack.Push(_currentLocation);
                _currentLocation = _forwardStack.Pop();
                _currentImageIndex = 0;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(CurrentImage));
            }
        });

        public ICommand NextImageCommand => new RelayCommand(() =>
        {
            if (CurrentLocation?.ImagePaths == null) return;
            if (_currentImageIndex < CurrentLocation.ImagePaths.Count - 1)
            {
                _currentImageIndex++;
                OnPropertyChanged(nameof(CurrentImage));
            }
        });

        public ICommand PrevImageCommand => new RelayCommand(() =>
        {
            if (CurrentLocation?.ImagePaths == null) return;
            if (_currentImageIndex > 0)
            {
                _currentImageIndex--;
                OnPropertyChanged(nameof(CurrentImage));
            }
        });

        public MainViewModel()
        {
            Locations = new ObservableCollection<LocationModel>
            {
                new() { 
                    Name = "Челомей", 
                    ImagePaths = new List<string> {
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\1.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\2.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\3.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\4.png"
                    }, 
                    Description = "Город, расположенный на летающей платформе «Икар» и представляющий собой конструкторское бюро машиностроения."
                },
                new() {
                    Name = "Вавилов",
                    ImagePaths = new List<string> {
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\5.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\6.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\7.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\8.jpg"
                    },
                    Description = "Экспериментальный ботанический комплекс."
                },
                new() {
                    Name = "Павлов",
                    ImagePaths = new List<string> {
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\9.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\10.jpg"
                    },
                    Description = "Экспериментальный комплекс изучения физики и химии тела, антропогенеза и медицины."
                },
                new() {
                    Name = "Нептун",
                    ImagePaths = new List<string> {
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\11.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\12.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\13.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\14.jpg"
                    },
                    Description = "Экспериментальный подводный исследовательский комплекс, расположенный на дне озера Лазурь."
                },
                new() {
                    Name = "ВДНХ",
                    ImagePaths = new List<string> {
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\15.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\16.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\17.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\18.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\19.jpg"
                    },
                    Description = "Главная выставка Предприятия 3826."
                },
                new() {
                    Name = "Менделеев",
                    ImagePaths = new List<string> {
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\20.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\21.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\22.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\23.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\24.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\25.jpg"
                    },
                    Description = "Экспериментальный химико-технологический комплекс."
                },
                new() {
                    Name = "Театр имени Майи Плисецкой",
                    ImagePaths = new List<string> {
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\26.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\27.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\28.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\29.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\30.jpg"
                    },
                    Description = "Основанный в 1948 году и расположенный на территории Предприятия первый в мире роботизированный театр."
                },
                new() {
                    Name = "Гостиница «Лёгкая»",
                    ImagePaths = new List<string> {
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\31.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\32.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\33.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\34.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\35.jpg"
                    },
                    Description = "Уникальный центр отдыха и восстановления, в первую очередь созданный для поддержания сил сотрудников Предприятия 3826, а также простых граждан и интуристов."
                },
                new() {
                    Name = "Лаборатория «Шар»",
                    ImagePaths = new List<string> {
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\36.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\37.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\38.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\39.jpg",
                    "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\AtomicHeart\\AtomicHeart\\View\\Imgs\\40.jpg"
                    },
                    Description = "Мобильная лаборатория профессора Лебедева."
                },
            };
            CurrentLocation = Locations[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
