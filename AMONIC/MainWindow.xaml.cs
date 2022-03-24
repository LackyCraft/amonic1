using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AMONIC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool EnableAuth = true;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.Resources["Count"] = "0";
        }

        private void clickAuth(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Users> userSearch = DBEntities.GetContext().Users.Where(i => (i.Email == TextBoxUsername.Text && i.Password == PasswordBoxPassword.Password.ToString())).ToList();
                if (userSearch.Count > 0)
                {
                    if (bool.Parse(userSearch[0].Active.ToString()))
                    {
                        Application.Current.Resources["UserId"] = userSearch[0].ID.ToString();
                        Application.Current.Resources["RoleId"] = userSearch[0].RoleID.ToString();

                        LogService log = new LogService();
                        log.DataTime = DateTime.Now;
                        log.TYPE = "LOGIN";
                        log.IdUser = int.Parse(Application.Current.Resources["UserId"].ToString());

                        DBEntities.GetContext().LogService.Add(log);
                        DBEntities.GetContext().SaveChanges();

                        if (Application.Current.Resources["RoleId"].ToString() == "1")
                        {
                            AdminMainWindow AdminPage = new AdminMainWindow();
                            AdminPage.Show();
                        }
                        if (Application.Current.Resources["RoleId"].ToString() == "2")
                        {
                            (new UserMainWindow()).Show();
                        }

                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Ваша учетная запись была заблокирована!");
                        LogService log = new LogService();
                        log.DataTime = DateTime.Now;
                        log.TYPE = "ACCESS_DENIED";
                        log.description = "Учетная запись заблокирована";
                        log.IdUser = int.Parse(userSearch[0].ID.ToString());
                        DBEntities.GetContext().LogService.Add(log);
                        DBEntities.GetContext().SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Веден неверный логин или пароль!");
                }
            }
            catch
            {
                MessageBox.Show("Warning 500\n Потеряно соединение с базой данных");
            }

        }

        private void clickExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
