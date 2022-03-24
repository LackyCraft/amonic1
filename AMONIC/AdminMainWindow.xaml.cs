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

namespace AMONIC
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {

        List<Users> usersList = DBEntities.GetContext().Users.ToList();

        public AdminMainWindow()
        {
            InitializeComponent();
            drawDataGrid();
        }

        public void drawDataGrid()
        {
            DataGridUsersList.Items.Clear();
            foreach (Users uesrItem in usersList)
            {
                DataGridUsersList.Items.Add(uesrItem);
                ComboBoxType.ItemsSource = DBEntities.GetContext().Offices.ToList();
            }
        }

        private void clickExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void selectionChangedComboBoxType(object sender, SelectionChangedEventArgs e)
        {
            usersList = DBEntities.GetContext().Users.ToList();
            usersList = usersList.Where(i => i.OfficeID == int.Parse(ComboBoxType.SelectedValue.ToString())).ToList();
            drawDataGrid();
        }

        private void statusUser(object sender, RoutedEventArgs e)
        {
            if(DataGridUsersList.SelectedItems.Count > 0){
                try
                {
                    for (int i =0;i< DataGridUsersList.SelectedItems.Count; i++)
                    {
                        (DataGridUsersList.SelectedItems[i] as Users).Active = !(DataGridUsersList.SelectedItems[i] as Users).Active;
                        DBEntities.GetContext().SaveChanges();

                        LogService log = new LogService();
                        log.DataTime = DateTime.Now;
                        log.TYPE = "EDIT_USER";
                        log.description = "Отредактирован пользователь (Edit Active)" + (DataGridUsersList.SelectedItems[i] as Users).ID.ToString() + "; Activity = " + (DataGridUsersList.SelectedItems[i] as Users).Active.ToString();
                        log.IdUser = int.Parse(Application.Current.Resources["UserId"].ToString());

                        DBEntities.GetContext().LogService.Add(log);
                        DBEntities.GetContext().SaveChanges();


                    }
                }
                catch
                {
                    MessageBox.Show("Warning 500\n Потеряно соединение с базой данных");
                    LogService log = new LogService();
                    log.DataTime = DateTime.Now;
                    log.TYPE = "WARNING";
                    log.description = "Потеряно соединение с базой данных на странице администратора";
                    log.IdUser = int.Parse(Application.Current.Resources["UserId"].ToString());
                    DBEntities.GetContext().LogService.Add(log);
                    DBEntities.GetContext().SaveChanges();
                }
                drawDataGrid();
            } 
        }

        private void changeRoleSelectedUser(object sender, RoutedEventArgs e)
        {
            if (DataGridUsersList.SelectedItems.Count > 0)
            {
                AMONIC.RolePage.Admin.EditUsersWindow newPage = new AMONIC.RolePage.Admin.EditUsersWindow(DataGridUsersList.SelectedItem as Users);
                newPage.Owner = this;
                newPage.Show();
                drawDataGrid();
            }
        }

        private void clickAddUsers(object sender, RoutedEventArgs e)
        {
            (new RolePage.Admin.AddUserWindow()).Show();
        }
    }
}
