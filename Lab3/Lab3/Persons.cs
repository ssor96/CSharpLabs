using System;
using System.Collections.ObjectModel;

namespace Lab3
{
    class Persons : ObservableCollection<Person>
    {
        public void Load()
        {
            Add(new Person()
            {
                Name = "Вася",
                PhoneNumber = "3223322",
                Email = "zzz.net",
                Birthday = new DateTime(1996, 01, 23),
                Comment = "zbs chel"
            });
            Add(new Person()
            {
                Name = "Анфиса",
                PhoneNumber = "3223322",
                Email = "zzz.net",
                Birthday = new DateTime(1996, 01, 27),
                Comment = "zbs chel"
            });
            Add(new Person()
            {
                Name = "Андрей",
                PhoneNumber = "3223322",
                Email = "zzz.net",
                Birthday = new DateTime(1996, 04, 04),
                Comment = "zbs chel"
            });
            Add(new Person()
            {
                Name = "Маша",
                PhoneNumber = "3223322",
                Email = "zzz.net",
                Birthday = new DateTime(1996, 04, 04),
                Comment = "zbs chel"
            });
        }
        ~Persons()
        {
            //FileStream fs = File.Create(".contactsBase.bin");
            //BinaryWriter writer = new BinaryWriter(fs);
            //UnicodeEncoding uen = new UnicodeEncoding();
            //byte[] buffer = 
        }
    }
}
