using Ekz.ClassFolder;
using Ekz.DataFolder;
using Ekz.PageFolder.AdminPageFolder;
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
    /// Логика взаимодействия для YchastnikListPage.xaml
    /// </summary>
    public partial class YchastnikListPage : Page
    {
        public YchastnikListPage()
        {
            InitializeComponent();
            ListUserDG.ItemsSource = DBEntities.GetContext().Meropriyatie
                .ToList().OrderBy(u => u.MeropriyatieId);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListUserDG.ItemsSource = DBEntities.GetContext()
                .Meropriyatie.Where(u => u.Name
                .StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.Name);
            if (ListUserDG.Items.Count <= 0)
            {
                MBClass.ErrorMB("Данные не найдены");
            }
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    Meropriyatie user = ListUserDG.SelectedItem as Meropriyatie;
                    VariableClass.UserId = user.MeropriyatieId;
                    NavigationService.Navigate(new EditYchastnikPage(user));
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void EditM_Click(object sender, RoutedEventArgs e)
        {
            if (ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "пользователя для редактирования");
            }

            Meropriyatie user = ListUserDG.SelectedItem as Meropriyatie;
            VariableClass.UserId = user.MeropriyatieId;
            NavigationService.Navigate(new EditYchastnikPage(user));
        }

        private void DeleteM_Click(object sender, RoutedEventArgs e)
        {
            Meropriyatie user = ListUserDG.SelectedItem as Meropriyatie;

            if (ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите пользователя" +
                    "для удаления");
            }
            else
            {
                if (MBClass.QuestionMB("Удалить" +
                    $"пользователя с логином" +
                    $"{user.Name}?"))
                {
                    DBEntities.GetContext().Meropriyatie.Remove(user);
                    DBEntities.GetContext().SaveChanges();

                    MessageBox.Show("Пользователь удален");
                    ListUserDG.ItemsSource = DBEntities.GetContext()
                        .Meropriyatie.ToList().OrderBy(u => u.Name);
                }
            }
        }
    }
}
