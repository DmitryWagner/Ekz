﻿using Ekz.ClassFolder;
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


namespace Ekz.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для UserListPage.xaml
    /// </summary>
    public partial class UserListPage : Page
    {
        public UserListPage()
        {
            InitializeComponent();
            ListUserDG.ItemsSource = DBEntities.GetContext().User
                .ToList().OrderBy(u => u.UserId);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListUserDG.ItemsSource = DBEntities.GetContext()
                .User.Where(u => u.UserName
                .StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.UserName);
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
                    User user = ListUserDG.SelectedItem as User;
                    VariableClass.UserId = user.UserId;
                    NavigationService.Navigate(new EditUserPage(user));
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

            User user = ListUserDG.SelectedItem as User;
            VariableClass.UserId = user.UserId;
            NavigationService.Navigate(new EditUserPage(user));
        }

        private void DeleteM_Click(object sender, RoutedEventArgs e)
        {
            User user = ListUserDG.SelectedItem as User;

            if (ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите пользователя" +
                    "для удаления");
            }
            else
            {
                if (MBClass.QuestionMB("Удалить" +
                    $"пользователя с логином" +
                    $"{user.UserName}?"))
                {
                    DBEntities.GetContext().User.Remove(user);
                    DBEntities.GetContext().SaveChanges();

                    MessageBox.Show("Пользователь удален");
                    ListUserDG.ItemsSource = DBEntities.GetContext()
                        .User.ToList().OrderBy(u => u.UserName);
                }
            }
        }
    }
}
