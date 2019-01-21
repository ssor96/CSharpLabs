using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class PersonViewModel
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
                //OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get
            {
                return _p.Birthday.Value.Date.ToString("dd-MM-yyyy");
            }
            set
            {
                _p.Birthday = DateTime.Parse(value);
                //OnPropertyChanged();
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
                //OnPropertyChanged();
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
                //OnPropertyChanged();
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
                //OnPropertyChanged();
            }
        }
    }
}
