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
    /// Логика взаимодействия для EditUsersWindow.xaml
    /// </summary>
    public partial class EditUsersWindow : Window
    {

        private int selectedRoleId;
        private Users selectEditUsers;

        public EditUsersWindow(Users editUser)
        {
            InitializeComponent();

            ComboBoxOffice.ItemsSource = DBEntities.GetContext().Offices.ToList();

            selectEditUsers = editUser;

            TextBoxEmail.Text = editUser.Email;
            TextBoxFirstName.Text = editUser.FirstName;
            TextBoxLasttName.Text = editUser.LastName;
            ComboBoxOffice.SelectedValue = editUser.OfficeID;
            DataPickerBirthday.SelectedDate = editUser.Birthdate;

            List<Roles> roleList = DBEntities.GetContext().Roles.ToList();

            foreach (Roles roleItem in roleList)
            {
                RadioButton newRole = new RadioButton();
                newRole.Content = roleItem.Title;
                newRole.Uid = roleItem.ID.ToString();
                if (editUser.RoleID == roleItem.ID)
                    newRole.IsChecked = true;
                newRole.Checked += (s, e) => RadioButton_Checked(s, e); 

               StackPanelRole.Children.Add(newRole);
            }

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

            if (MessageError == "")
            {

                try
                {

                    selectEditUsers.Email = TextBoxEmail.Text;
                    selectEditUsers.FirstName = TextBoxFirstName.Text;
                    selectEditUsers.LastName = TextBoxLasttName.Text;
                    selectEditUsers.Birthdate = dateBirthday;
                    selectEditUsers.OfficeID = int.Parse(ComboBoxOffice.SelectedValue.ToString());
                    selectEditUsers.Active = true;
                    selectEditUsers.RoleID = selectedRoleId;

                    DBEntities.GetContext().SaveChanges();

                    MessageBox.Show("Пользователь успешно создан");

                    LogService log = new LogService();
                    log.DataTime = DateTime.Now;
                    log.TYPE = "ADD_USER";
                    log.description = "Отредакктирован пользователь" + selectEditUsers.ID;
                    log.IdUser = int.Parse(Application.Current.Resources["UserId"].ToString());

                    DBEntities.GetContext().LogService.Add(log);
                    DBEntities.GetContext().SaveChanges();

                    this.Close();

                }
                catch
                {
                    MessageBox.Show("Warning 500\n Потеряно соединение с базой данных");
                }

            }
            else
            {
                MessageBox.Show(MessageError);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            selectedRoleId = int.Parse((sender as RadioButton).Uid.ToString());
            MessageBox.Show(selectedRoleId.ToString());
        }
    }
}
