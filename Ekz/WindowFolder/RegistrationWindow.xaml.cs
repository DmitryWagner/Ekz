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
using System.Windows.Shapes;


namespace Ekz.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTb.Focus();
            }
            else if (DBEntities.GetContext()
                .User.FirstOrDefault(u =>
                u.UserName == LoginTb.Text) != null)
            {
                MBClass.ErrorMB("Такой логин уже существует");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordPsb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(RepeatPasswordPsb.Password))
            {
                MBClass.ErrorMB("Введите повторно пароль");
                RepeatPasswordPsb.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().User.Add(new User()
                    {
                        UserName = LoginTb.Text,
                        UserPassword = PasswordPsb.Password,
                        RoleId = 2
                    });
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InfoMB("Вы успешно зарегистрировались");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }
    }
}
