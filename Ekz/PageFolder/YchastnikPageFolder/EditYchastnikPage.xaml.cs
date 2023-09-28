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
    /// Логика взаимодействия для EditYchastnikPage.xaml
    /// </summary>
    public partial class EditYchastnikPage : Page
    {
        Meropriyatie raspisanie = new Meropriyatie();
        public EditYchastnikPage(Meropriyatie raspisanie)
        {
            InitializeComponent();
            DataContext = raspisanie;
            this.raspisanie.MeropriyatieId = raspisanie.MeropriyatieId;
            GenderCb.ItemsSource = DBEntities.GetContext().Gender.ToList();
         

        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                int index2 = GenderCb.SelectedIndex + 1;
                raspisanie = DBEntities.GetContext().Meropriyatie
                    .FirstOrDefault(u => u.MeropriyatieId == raspisanie.MeropriyatieId);
               raspisanie.Name= NameTb.Text;
                raspisanie.GenderId = index2;
                raspisanie.Date = DateTime.Parse(DateTb.Text);
                raspisanie.Email = EmailTb.Text;
                raspisanie.PhoneNumber= PhoneTb.Text;
                raspisanie.Country = CountryCb.Text;

                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Данные успешно отредактированы");
                NavigationService.Navigate(new YchastnikListPage());
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
