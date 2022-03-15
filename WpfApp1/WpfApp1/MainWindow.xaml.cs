using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)     //Добавление
        {
            using (HotelEntities db = new HotelEntities())
            {
                Customer c = new Customer();
                c.Age = 100;
                c.Email = "someemail#mail.ru";
                c.FirstName = "Peter";
                c.LastName = "Pen";
                c.PassportID = 123456;
                c.Phone = "7-999-999-99-99";
                db.Customers.Add(c);
                db.SaveChanges();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   //Изменение
        {
            using (HotelEntities db = new HotelEntities())
            {
                Customer p1 = db.Customers.Where((customer) => customer.FirstName == "Peter").FirstOrDefault();
                p1.Age = 3000; //Изменяем
                db.SaveChanges();
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)   //Удаление
        {
            using (HotelEntities db = new HotelEntities())
            {
                Customer p1 = db.Customers.Where((customer) => customer.FirstName == "Peter").FirstOrDefault();
                if (p1 != null)
                {
                    db.Customers.Remove(p1);
                    db.SaveChanges();

                }
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)   //Добавление в таблицу Booking (Пример с DateTime и TimeSpan)
        {
            using (HotelEntities db = new HotelEntities())
            {
                Booking b = new Booking();
                b.ArrivalDate = new DateTime(2001, 01, 20);
                b.ArrivalTime = new TimeSpan(12, 30, 0);
                b.DepartureDate = new DateTime(2001, 01, 20);
                b.DepartureTime = new TimeSpan(12, 30, 0);

                b.CustomerId = db.Customers.Where(customer => customer.FirstName == "Peter").FirstOrDefault().Id;
                b.RoomId = db.Rooms.FirstOrDefault().Id;
                db.Bookings.Add(b);
                db.SaveChanges();
            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)   //Вывод данных из соединения 2-х таблиц (Booking и Customers)
        {
            using(HotelEntities db = new HotelEntities())
            {
                var bookings = db.Bookings.Join(db.Customers,
                        booking => booking.CustomerId,
                        customer => customer.Id,
                        (booking, customer) => new
                        {
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Phone = customer.Phone,
                            ArrivalDate = booking.ArrivalDate,
                            DepartureDate = booking.DepartureDate,
                        });

                foreach(var b in bookings)
                {
                    tbOutput.Text += string.Format("({0} {1}) Phone: {2} \n ArrivalDate: {3} DepartureDate: {4}\n--------\n",
		            b.FirstName, b.LastName, b.Phone,
		            b.ArrivalDate, b.DepartureDate);
                }
                    
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)   //Получение данных из 3-х таблиц
        {
            using(HotelEntities db = new HotelEntities())
            {
                var bookings = from booking in db.Bookings
                    join customer in db.Customers on booking.CustomerId equals customer.Id
                    join room in db.Rooms on booking.RoomId equals room.Id
                    select new
                    {
                        Name = customer.FirstName,
                        Price = room.Price,
                        ArrivalDate = booking.ArrivalDate,
                        DepartureDate = booking.DepartureDate
                    };

                foreach(var b in bookings)
                {
                    tbOutput.Text += string.Concat("Name: ", b.Name,
		            "\nPrice: ", b.Price,
		            "\nArrivalDate: ", b.ArrivalDate,
                    "\nDepartureDate: ", b.DepartureDate,
                    "\n-----+++++++++----\n");
                }
            }
        }


    }
}
