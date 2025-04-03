using System;
using System.Windows;

namespace Pr3Part3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик нажатия кнопки "Вычислить"
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем значения из текстовых полей
            if (!double.TryParse(DurationTextBox.Text, out double duration) || duration <= 0)
            {
                MessageBox.Show("Неверное значение длительности разговора. Введите положительное число.");
                return;
            }

            if (!double.TryParse(RateTextBox.Text, out double rate) || rate <= 0)
            {
                MessageBox.Show("Неверное значение цены за минуту. Введите положительное число.");
                return;
            }

            // Проверяем, выбран ли день недели
            string selectedDay = GetSelectedDay();
            if (string.IsNullOrEmpty(selectedDay))
            {
                MessageBox.Show("Выберите день недели.");
                return;
            }

            // Определяем скидку за выходной день
            double discountWeekend = IsWeekend(selectedDay) ? 0.15 : 0;

            // Расчет базовой стоимости
            double baseCost = duration * rate;

            // Применяем скидку за выходной день
            double discountedCost = baseCost * (1 - discountWeekend);

            // Применяем скидку за длительный разговор (>30 минут)
            if (duration > 30)
            {
                double extraMinutes = duration - 30;
                double reducedRate = rate * 0.7; // 30% скидка
                discountedCost = (30 * rate) + (extraMinutes * reducedRate);
            }

            // Отображаем результат
            ResultTextBlock.Text = $"Стоимость разговора: {discountedCost:C2}";
        }

        // Метод для получения выбранного дня недели
        private string GetSelectedDay()
        {
            if (Monday.IsChecked == true) return "понедельник";
            if (Tuesday.IsChecked == true) return "вторник";
            if (Wednesday.IsChecked == true) return "среда";
            if (Thursday.IsChecked == true) return "четверг";
            if (Friday.IsChecked == true) return "пятница";
            if (Saturday.IsChecked == true) return "суббота";
            if (Sunday.IsChecked == true) return "воскресенье";
            return null;
        }

        // Метод для проверки, является ли день выходным
        private bool IsWeekend(string day)
        {
            return day == "суббота" || day == "воскресенье";
        }
    }
}