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
    /// Логика взаимодействия для ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        List<Schedules> schedulesList;


        public ManagerMainWindow()
        {
            InitializeComponent();
            try
            {
                List<string> sortList = new List<string>() { "Дата", "Цена", "Эконом места", "Статус: подтвержден", "Статус не подтвержден" };
                ComboBoxAirportFrom.ItemsSource = DBEntities.GetContext().Airports.ToList();
                ComboBoxAirportTo.ItemsSource = DBEntities.GetContext().Airports.ToList();
                schedulesList = DBEntities.GetContext().Schedules.ToList();
                drawDataGrid();
            }
            catch
            {
                MessageBox.Show("Warning 500\n Потеряно соединение с базой данных");
            }
        }

        public void drawDataGrid()
        {
            DataGridShuldesList.Items.Clear();
            foreach (Schedules dataGridItem in schedulesList)
            {
                DataGridShuldesList.Items.Add(dataGridItem);
            }
        }

        private void clickCancelFlight(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridShuldesList.SelectedItems.Count > 0)
                {
                    foreach (Schedules shedulesEdit in DataGridShuldesList.SelectedItems)
                    {
                        if (shedulesEdit.Confirmed)
                            shedulesEdit.Confirmed = false;
                        else
                            shedulesEdit.Confirmed = true;

                    }
                }
                DBEntities.GetContext().SaveChanges();
            }
            catch
            {
                    MessageBox.Show("Warning 500\n Потеряно соединение с базой данных");
                    LogService log = new LogService();
                    log.DataTime = DateTime.Now;
                    log.TYPE = "WARNING";
                    log.description = "Потеряно соединение с базой данных при быстром редактировании (Активности рейса) Confirmed на странице менеджера";
                    log.IdUser = int.Parse(Application.Current.Resources["UserId"].ToString());
                    DBEntities.GetContext().LogService.Add(log);
                    DBEntities.GetContext().SaveChanges();
            }
            drawDataGrid();
        }
        private void clickEdit(object sender, RoutedEventArgs e)
        {
            AMONIC.RolePage.Manager.SheduleEditWindow newPage = new AMONIC.RolePage.Manager.SheduleEditWindow(DataGridShuldesList.SelectedItem as Schedules);
            newPage.Owner = this;
            newPage.Show();
        }

        private void clickImport(object sender, RoutedEventArgs e)
        {

        }

        private void selectionChangedComboBoxAiraports(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxAirportFrom.SelectedIndex == ComboBoxAirportTo.SelectedIndex)
            {
                MessageBox.Show("Место вылета и прибытия не могут совпадать");
                ComboBoxAirportTo.SelectedItem = null;
            }
        }



        private void selectionChangedComboBoxSortBy(object sender, SelectionChangedEventArgs e)
        {
            //ComboBoxAirportFrom.ItemsSource = DBEntities.GetContext().Airports.Where(i => i.ID != int.Parse(ComboBoxAirportTo.SelectedValue.ToString())).ToList();
        }

        private void clickReset(object sender, RoutedEventArgs e)
        {
            ComboBoxAirportFrom.SelectedItem = null;
            ComboBoxAirportTo.SelectedItem = null;
            DatePickerOutbound.SelectedDate = null;
            TextBoxFlightNyber.Text = "";
            schedulesList = DBEntities.GetContext().Schedules.ToList();
            drawDataGrid();
        }

        private void clickApply(object sender, RoutedEventArgs e)
        {
            try
            { 
                if (ComboBoxAirportFrom.SelectedIndex != -1)
                {
                    int idAirportsFrom = int.Parse(ComboBoxAirportFrom.SelectedValue.ToString());
                    schedulesList = schedulesList.Where(i => i.Routes.Airports.ID == idAirportsFrom).ToList();
                }
                if (ComboBoxAirportTo.SelectedIndex != -1)
                {
                    int idAirportsTo = int.Parse(ComboBoxAirportTo.SelectedValue.ToString());
                    schedulesList = schedulesList.Where(i => i.Routes.Airports1.ID == idAirportsTo).ToList();
                }

                DateTime dateToSelect;
                
                if (DateTime.TryParse(DatePickerOutbound.SelectedDate.ToString(), out dateToSelect))
                {
                    schedulesList = schedulesList.Where(i => i.Date == dateToSelect).ToList();
                }

                if (TextBoxFlightNyber.Text.Length > 1)
                {
                    schedulesList = schedulesList.Where(i => i.Aircrafts.MakeModel.Contains(TextBoxFlightNyber.Text)).ToList();
                }
            }
            catch
            {
                MessageBox.Show("Warning 422\n Ошибка при обработке поиска");
                    ComboBoxAirportFrom.SelectedItem = null;
                    ComboBoxAirportTo.SelectedItem = null;
                    DatePickerOutbound.SelectedDate = null;
                    TextBoxFlightNyber.Text = "";
                    schedulesList = DBEntities.GetContext().Schedules.ToList();
                    drawDataGrid();
                LogService log = new LogService();
                log.DataTime = DateTime.Now;
                log.TYPE = "WARNING";
                log.description = "Ошибка при обработке функции фильтрации на странице менеджера";
                log.IdUser = int.Parse(Application.Current.Resources["UserId"].ToString());
                DBEntities.GetContext().LogService.Add(log);
                DBEntities.GetContext().SaveChanges();
            }

            drawDataGrid();
        }

    }
}
