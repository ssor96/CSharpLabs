using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Lab3
{
    class MainViewModel : INotifyPropertyChanged
    {
        Persons _persons = new Persons();

        ObservableCollection<PersonViewModel> _personViewModels = new ObservableCollection<PersonViewModel>();

        CollectionViewSource _cvs = new CollectionViewSource();

        public string Alphabet { get; } = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        string _startSymb;

        public ICollectionView Persons
        {
            get
            {
                return _cvs.View;
            }
        }

        public MainViewModel()
        {
            _persons = new Persons();
            _persons.Load();

            foreach(Person p in _persons)
            {
                _personViewModels.Add(new PersonViewModel(p));
            }

            _persons.CollectionChanged += _persons_CollectionChanged;

            _cvs.Source = _personViewModels;
            _cvs.Filter += new FilterEventHandler(_cvs_Filter);
            _cvs.View.CurrentChanged += View_CurrentChanged;

            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete)
            {
                CanExecuteProperty = false
            };
        }

        public PersonViewModel SelectedPerson
        {
            get
            {
                return (PersonViewModel)_cvs.View.CurrentItem;
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string _filter = "";

        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                _cvs.View.Refresh();
            }
        }

        bool _nearestBirthdays = false;

        public bool NearestBirthdays
        {
            get
            {
                return _nearestBirthdays;
            }
            set
            {
                _nearestBirthdays = value;
                _cvs.View.Refresh();
            }
        }

        void _cvs_Filter(object sender, FilterEventArgs e)
        {
            PersonViewModel p = (PersonViewModel)e.Item;
            if (_nearestBirthdays)
            {
                if (p.Birthday != null)
                {
                    var now = DateTime.Now.DayOfYear;
                    var date = DateTime.Parse(p.Birthday).DayOfYear;
                    var weekLater = now + 7;
                    if (now > date || date > weekLater)
                    {
                        e.Accepted = false;
                        return;
                    }
                }
            }
            if (p.Name != null)
            {
                if (_startSymb != null)
                {
                    if (!p.Name.StartsWith(_startSymb))
                    {
                        e.Accepted = false;
                        return;
                    }
                }
                e.Accepted = p.Name.Contains(Filter);
            }
        }

        void _persons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                _personViewModels.Add(new PersonViewModel((Person)e.NewItems[0]));
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var p = (Person)e.OldItems[0];
                var v = _personViewModels.FirstOrDefault(x => x.Person == p);
                _personViewModels.Remove(v);
            }
        }

        void View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedPerson");
            DeleteCommand.CanExecuteProperty = (SelectedPerson != null);
        }

        void Add()
        {

        }

        void Delete()
        {
            if (SelectedPerson != null)
            {
                _persons.Remove(SelectedPerson.Person);
            }
        }

        void ChangeStartSymb(char c)
        {
            if (_startSymb == c.ToString())
            {
                _startSymb = null;
            }
            else
            {
                _startSymb = c.ToString();
            }
            _cvs.View.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand AddCommand { get; set; }

        public DelegateCommand DeleteCommand { get; set; }

        public ICommand ChangeStartSymbCommand { get { return new DelegateCommandWithArgs<char>(ChangeStartSymb); } }
    }
}
