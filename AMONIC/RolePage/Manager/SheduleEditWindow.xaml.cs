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

namespace AMONIC.RolePage.Manager
{
    /// <summary>
    /// Логика взаимодействия для SheduleEditWindow.xaml
    /// </summary>
    public partial class SheduleEditWindow : Window
    {

        private Schedules editshedulesSelectedItem;

        public SheduleEditWindow(Schedules editshedules)
        {
            InitializeComponent();
            editshedulesSelectedItem = editshedules;
            TextBlockFrom.Text = editshedules.Routes.Airports.IATACode;
            TextBlockTo.Text = editshedules.Routes.Airports1.IATACode;
            TextBlockAircraft.Text = editshedules.Aircrafts.Name;
            DatePickerEditDate.SelectedDate = editshedules.Date;
            TextBoxTime.Text = editshedules.Time.ToString();
            TextBoxPrice.Text = editshedules.EconomyPrice.ToString();
        }

        private void clickUpdate(object sender, RoutedEventArgs e)
        {
            string warningMassage = "";
            DateTime dateToSelect;

            if (!DateTime.TryParse(DatePickerEditDate.SelectedDate.ToString(), out dateToSelect))
            {
                warningMassage = "Выберите дату!";
            }
            TimeSpan time;
            if (!TimeSpan.TryParse(TextBoxTime.Text, out time))
            {
                warningMassage += "Время введено не корректно";
            }

            Decimal price;
            if (!Decimal.TryParse(TextBoxPrice.Text, out price))
            {
                warningMassage += "Цена должна быть целым положительным числом!";
            }
            if (warningMassage != "")
            {
                MessageBox.Show(warningMassage);
            }
            else
            {
                try
                {
                    editshedulesSelectedItem.Date = dateToSelect;
                    editshedulesSelectedItem.Time = time;
                    editshedulesSelectedItem.EconomyPrice = price;
                    //editshedulesSelectedItem.BuisnesPrice = Math.Round(price + price/100*30);
                    //editshedulesSelectedItem.FirstClass = Math.Round(price + (price / 100 * 30)/100*35);
                    DBEntities.GetContext().SaveChanges();

                    MessageBox.Show("Обвновление данных прошло успешно");
                    this.Close();

                }
                catch
                {
                    MessageBox.Show("Warning 500\n Не придвиденная ошибка соединения с БД");
                    LogService log = new LogService();
                    log.DataTime = DateTime.Now;
                    log.TYPE = "WARNING";
                    log.description = "Ошибка при обновлении данных о рейсе страница: SheduleEditWindow";
                    log.IdUser = int.Parse(Application.Current.Resources["UserId"].ToString());
                    DBEntities.GetContext().LogService.Add(log);
                    DBEntities.GetContext().SaveChanges();
                }
            }
        }

        private void clickCancel(object sender, RoutedEventArgs e)
        {

        }



    }
}
