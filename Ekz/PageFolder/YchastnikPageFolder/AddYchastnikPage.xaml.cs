using Ekz.ClassFolder;
using Ekz.DataFolder;
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

namespace Ekz.PageFolder.YchastnikPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddYchastnikPage.xaml
    /// </summary>
    public partial class AddYchastnikPage : Page
    {
        public AddYchastnikPage()
        {
            InitializeComponent();
            GenderCb.ItemsSource = DBEntities.GetContext().Gender.ToList();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                int index2 = GenderCb.SelectedIndex + 1;
                DBEntities.GetContext().Meropriyatie.Add(new Meropriyatie()
                {
                    Name = NameTb.Text,
                GenderId = index2,
                Date = DateTime.Parse(DateTb.Text),
                Email = EmailTb.Text,
                PhoneNumber = PhoneTb.Text,
                Country = CountryCb.Text
            });
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Расписание успешно добавлено");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
                throw;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
