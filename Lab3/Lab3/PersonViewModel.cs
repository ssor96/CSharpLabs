using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab3
{
    class PersonViewModel : INotifyPropertyChanged
    {
        Person _p;


        public Person Person
        {
            get
            {
                return _p;
            }
        }

        public PersonViewModel(Person p)
        {
            _p = p;

            //UpdateCommand = new DelegateCommand(ChangeName, CanChangeName);
        }


        public string Name
        {
            get
            {
                return _p.Name;
            }
            set
            {
                _p.Name = value;
                OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get
            {
                return _p.Birthday.HasValue ? _p.Birthday.Value.Date.ToString("dd-MM-yyyy") : "не указан";
            }
            set
            {
                try
                {
                    _p.Birthday = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    _p.Birthday = null;
                }
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return _p.Email;
            }
            set
            {
                _p.Email = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _p.PhoneNumber;
            }
            set
            {
                _p.PhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get
            {
                return _p.Comment;
            }
            set
            {
                _p.Comment = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmpty
        {
            get
            {
                bool isEmpty = (Email == null || Email == "") &&
                        (Name == null || Name == "") &&
                        (PhoneNumber == null || PhoneNumber == "");

                return isEmpty;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
