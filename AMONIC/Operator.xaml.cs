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
    public partial class Operator : Window
    {
        List<Schedules> schedulesListReturn;
        List<Schedules> schedulesList;

        public Operator()
        {
            InitializeComponent();
            try
            {
                ComboBoxAirportFrom.ItemsSource = DBEntities.GetContext().Airports.ToList();
                ComboBoxAirportTo.ItemsSource = DBEntities.GetContext().Airports.ToList();
                ComboBoxCabinType.ItemsSource = DBEntities.GetContext().CabinTypes.ToList();
                schedulesList = DBEntities.GetContext().Schedules.Where(i => i.Confirmed).ToList();
                schedulesListReturn = DBEntities.GetContext().Schedules.Where(i => i.Confirmed).ToList();
                drawDataGrid();
            }
            catch
            {
                MessageBox.Show("Warning 500\n Потеряно соединение с базой данных");
            }
        }

        private void selectionChangedComboBoxAiraports(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxAirportFrom.SelectedIndex == ComboBoxAirportTo.SelectedIndex)
            {
                MessageBox.Show("Место вылета и прибытия не могут совпадать");
                ComboBoxAirportTo.SelectedItem = null;
            }
        }

        private void clickApply(object sender, RoutedEventArgs e)
        {
            schedulesListReturn = DBEntities.GetContext().Schedules.Where(i => i.Confirmed).ToList();
            schedulesList = DBEntities.GetContext().Schedules.Where(i => i.Confirmed).ToList();
            try
            {
                if (ComboBoxAirportFrom.SelectedIndex != -1 && ComboBoxAirportTo.SelectedIndex != -1)
                {
                    int idAirportsFrom = int.Parse(ComboBoxAirportFrom.SelectedValue.ToString());
                    int idAirportsTo = int.Parse(ComboBoxAirportTo.SelectedValue.ToString());

                    schedulesList = schedulesList.Where(i => i.Routes.Airports1.ID == idAirportsTo).ToList();
                    schedulesList = schedulesList.Where(i => i.Routes.Airports.ID == idAirportsFrom).ToList();

                    if (radioButtonReturn.IsChecked == true)
                    {
                        schedulesListReturn = schedulesListReturn.Where(i => i.Routes.Airports.ID == idAirportsTo).ToList();
                        schedulesListReturn = schedulesListReturn.Where(i => i.Routes.Airports1.ID == idAirportsFrom).ToList();
                    }
                    else
                    {
                        schedulesListReturn = null;
                    }
                }

                DateTime dateToSelect;

                if (DateTime.TryParse(DatePickerOutbound.SelectedDate.ToString(), out dateToSelect))
                {
                    schedulesList = schedulesList.Where(i => i.Date == dateToSelect).ToList();
                }

                if (DateTime.TryParse(DatePickerReturn.SelectedDate.ToString(), out dateToSelect))
                {
                    schedulesListReturn = schedulesListReturn.Where(i => i.Date == dateToSelect).ToList();
                }

            }
            catch
            {
                MessageBox.Show("Warning 422\n Ошибка при обработке поиска");
                ComboBoxAirportFrom.SelectedItem = null;
                ComboBoxAirportTo.SelectedItem = null;
                DatePickerOutbound.SelectedDate = null;
                schedulesList = DBEntities.GetContext().Schedules.Where(i => i.Confirmed).ToList();
                schedulesListReturn = DBEntities.GetContext().Schedules.Where(i => i.Confirmed).ToList();
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

        private void clickReset(object sender, RoutedEventArgs e)
        {
            ComboBoxAirportFrom.SelectedItem = null;
            ComboBoxAirportTo.SelectedItem = null;
            DatePickerOutbound.SelectedDate = null;
            schedulesList = DBEntities.GetContext().Schedules.Where(i => i.Confirmed).ToList();
            schedulesListReturn = null;
            drawDataGrid();
        }

        public void drawDataGrid()
        {
            DataGridShuldesList.Items.Clear();
            DataGridReturnShuldesList.Items.Clear();
            foreach (Schedules dataGridItem in schedulesList)
            {
                DataGridShuldesList.Items.Add(dataGridItem);
            }

            if (radioButtonReturn.IsChecked == true)
            {
                DataGridReturnShuldesList.Items.Clear();
                foreach (Schedules dataGridItem2 in schedulesListReturn)
                {
                    DataGridReturnShuldesList.Items.Add(dataGridItem2);
                }
            }

        }

        private void clickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonBlockFlightClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int countPassagers;
                if (int.TryParse(TextBoxCountPassagers.Text, out countPassagers))
                {
                    Schedules selectedRace = DataGridShuldesList.SelectedItem as Schedules;

                    if (radioButtonReturn.IsChecked == true)
                    {
                        Schedules selectedRaceReturn = DataGridReturnShuldesList.SelectedItem as Schedules;
                        (new BookingWindows(selectedRace, selectedRaceReturn)).Show();
                    }
                    else
                    {
                        (new BookingWindows(selectedRace, null)).Show();
                    }
                }
                else
                {
                    MessageBox.Show("Колличество пассажиров должно быть - целым положительным числом.");
                }
            }
            catch
            {
                MessageBox.Show("Warning 500\n Произошла ошибка при введении бронировании билетов");
            }
        }
    }
}
