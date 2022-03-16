using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Contact selectedContact;

        public ObservableCollection<Contact> Contacts { get; set; }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Contact contact = new Contact();
                        Contacts.Insert(0, contact);
                        SelectedContact = contact;
                    }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        Contact phone = obj as Contact;
                        if (phone != null)
                        {
                            Contacts.Remove(phone);
                        }
                    },
                    (obj) => Contacts.Count > 0));
            }
        }

        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                OnPropertyChanged("SelectedContact");
            }
        }

        public ApplicationViewModel()
        {
            Contacts = new ObservableCollection<Contact>
            {
                new Contact {Name="Фльберт", Surname="Марков", Phone="89111234567"},
                new Contact {Name="Машка", Surname="Кузнецова", Phone="89113476529"},
                new Contact {Name="Татьяна", Surname="Смирнова", Phone="89101123454"},
                new Contact {Name="Александр", Surname="Капитонов", Phone="891135839523"}
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
