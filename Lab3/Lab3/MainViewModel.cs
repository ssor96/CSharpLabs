﻿using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Controls;
using System.Diagnostics;
using System.Threading;

namespace Lab3
{
    class MainViewModel : INotifyPropertyChanged
    {
        Persons _persons = new Persons();

        ObservableCollection<PersonViewModel> _personViewModels = new ObservableCollection<PersonViewModel>();

        CollectionViewSource _cvs = new CollectionViewSource();

        ObservableCollection<PersonViewModel> _searchResults = new ObservableCollection<PersonViewModel>();

        public IEnumerable SearchResults
        {
            get
            {
                return _searchResults;
            }
        }

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
                _searchResults.Add(new PersonViewModel(p));
            }

            _persons.CollectionChanged += _persons_CollectionChanged;

            _cvs.Source = _personViewModels;
            _cvs.Filter += new FilterEventHandler(_cvs_Filter);
            _cvs.View.CurrentChanged += View_CurrentChanged;

            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete);
            ChangeCommand = new DelegateCommand(Change);

            //var textchanges = Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(
            //    h => TextBoxSearch.TextChanged += h,
            //    h => TextBoxSearch.TextChanged -= h
            //    ).Select(x => ((TextBox)x.Sender).Text);

            //var processedTextChanges = textchanges.Throttle(TimeSpan.FromMilliseconds(300)).Where(text => !string.IsNullOrWhiteSpace(text) && text.Length >= 3).DistinctUntilChanged();

            //processedTextChanges.Select(Search).Switch().ObserveOn(DispatcherScheduler.Current).Subscribe(OnSearchResult);
        }

        //private void OnSearchResult(List<PersonViewModel> list)
        //{
        //    Trace.WriteLine("OnSearchResult: " + Thread.CurrentThread.ManagedThreadId);
        //    _searchResults.Clear();
        //    list.ForEach(_searchResults.Add);
        //}

        //public IObservable<List<PersonViewModel>> Search(string filter)
        //{
        //    Trace.WriteLine(Thread.CurrentThread.ManagedThreadId + ": search " + filter);
        //    //if (filter.Length <= 3) 
        //    //    Thread.Sleep(5000);
        //    var filteredList = _personViewModels.Where(x => x.Name.ToLower().Contains(filter.ToLower())).ToList();
        //    Trace.WriteLine("Search end " + filter);
        //    return Observable.Return(filteredList);
        //}

        ~MainViewModel()
        {
            _persons.Dump();
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
                Console.WriteLine("!!!");
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
                    if (p.Birthday[0] < '0' || p.Birthday[0] > '9')
                    {
                        e.Accepted = false;
                        return;
                    }
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
            ChangeCommand.CanExecuteProperty = (SelectedPerson != null);
        }

        void Add()
        {
            var newPersonViewModel = new PersonViewModel(new Person());
            if (WindowManager.ShowDialog(newPersonViewModel, "add"))
            {
                _persons.Add(newPersonViewModel.Person);
            }
        }

        void Delete()
        {
            if (SelectedPerson != null)
            {
                _persons.Remove(SelectedPerson.Person);
            }
        }

        void Change()
        {
            if (SelectedPerson != null)
            {
                WindowManager.ShowDialog(SelectedPerson, "change");
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

        public DelegateCommand ChangeCommand { get; set; }

        public ICommand ChangeStartSymbCommand { get { return new DelegateCommandWithArgs<char>(ChangeStartSymb); } }
    }
}







































