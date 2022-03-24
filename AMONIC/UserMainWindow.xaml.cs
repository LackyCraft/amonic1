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
    /// Логика взаимодействия для UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {

        private List<LogService> thisUsers;

        public UserMainWindow()
        {
            InitializeComponent();
            int idActivUser = int.Parse(Application.Current.Resources["UserId"].ToString());
            thisUsers = DBEntities.GetContext().LogService.Where(i => i.Users.ID == idActivUser).ToList();
            DataGridLogUsers.ItemsSource = thisUsers;
            TextBlockHello.Text = "Hi " + thisUsers[0].Users.FirstName + ", Welcome to AMONIC Airlines.";
            TextBlockCountCrashes.Text = "number of crashes: " + thisUsers.Where(i => (i.TYPE == "ACCESS_DENIED" || i.TYPE == "WARNING")).ToList().Count;
        }

        private void clickButtonExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
