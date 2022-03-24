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

namespace AMONIC.RolePage.Admin
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            ComboBoxOffice.ItemsSource = DBEntities.GetContext().Offices.ToList();
        }

        private void clickButtonCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void clickButtonSave(object sender, RoutedEventArgs e)
        {
            string MessageError = "";
            if (TextBoxEmail.Text.Length < 0)
                MessageError = "Заполните Email";
            if (TextBoxFirstName.Text.Length < 0)
                MessageError += "Заполните имя";
            if (TextBoxLasttName.Text.Length < 0)
                MessageError += "Заполните фамилию";
            if (ComboBoxOffice.SelectedIndex == -1)
                MessageError += "Выберите офис";

            DateTime dateBirthday;
            if (!DateTime.TryParse(DataPickerBirthday.Text, out dateBirthday))
            {
                MessageError += "Выберите дату рождения";
            }

            if (PasswordBoxPassword.Password.ToString().Length < 0)
            {
                MessageError += "Введите пароль";
            }

            if (MessageError == "")
            {

                try
                {
                Users newUsers = new Users();
                newUsers.Email = TextBoxEmail.Text;
                newUsers.FirstName = TextBoxFirstName.Text;
                newUsers.LastName = TextBoxLasttName.Text;
                newUsers.Birthdate = dateBirthday;
                newUsers.Password = PasswordBoxPassword.Password.ToString();
                newUsers.OfficeID = int.Parse(ComboBoxOffice.SelectedValue.ToString());
                newUsers.Active = true;
                newUsers.RoleID = 2;

                DBEntities.GetContext().Users.Add(newUsers);
                DBEntities.GetContext().SaveChanges();

                MessageBox.Show("Пользователь успешно создан");

                    LogService log = new LogService();
                    log.DataTime = DateTime.Now;
                    log.TYPE = "ADD_USER";
                    log.description = "Создан пользователь пользователь" + newUsers.ID;
                    log.IdUser = int.Parse(Application.Current.Resources["UserId"].ToString());

                    DBEntities.GetContext().LogService.Add(log);
                    DBEntities.GetContext().SaveChanges();

                    this.Close();

                }
                catch
                {
                    MessageBox.Show("Warning 500\n Потеряно соединение с базой данных");
                    MessageBox.Show("Ваша учетная запись была заблокирована!");
                    LogService log = new LogService();
                    log.DataTime = DateTime.Now;
                    log.TYPE = "WARNING";
                    log.description = "Потеряно соединение с базой данных при создании пользователя";
                    log.IdUser = int.Parse(Application.Current.Resources["UserId"].ToString());
                    DBEntities.GetContext().LogService.Add(log);
                    DBEntities.GetContext().SaveChanges();
                }

            }
            else
            {
                MessageBox.Show(MessageError);
            }


        }

        
    }
}
