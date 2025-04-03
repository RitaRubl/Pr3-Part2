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
            // Проверка на пустой ввод длительности
            if (string.IsNullOrWhiteSpace(DurationTextBox.Text))
            {
                MessageBox.Show("Введите длительность разговора.");
                return;
            }

            // Проверка на пустой ввод цены за минуту
            if (string.IsNullOrWhiteSpace(RateTextBox.Text))
            {
                MessageBox.Show("Введите цену за минуту.");
                return;
            }

            // Проверка корректности ввода длительности
            if (!double.TryParse(DurationTextBox.Text, out double duration) || duration <= 0)
            {
                MessageBox.Show("Неверное значение длительности разговора. Введите положительное число.");
                return;
            }

            // Проверка корректности ввода цены за минуту
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

            // Расчет базовой стоимости
            double baseCost;

            // Применяем скидку за длительный разговор (>30 минут)
            if (duration > 30)
            {
                double extraMinutes = duration - 30; // Количество минут после 30-й
                double reducedRate = rate * 0.7; // Цена за минуту со скидкой 30%
                baseCost = (30 * rate) + (extraMinutes * reducedRate); // Суммируем стоимость первых 30 минут и дополнительных минут
            }
            else
            {
                baseCost = duration * rate; // Если разговор <= 30 минут, просто умножаем на цену за минуту
            }

            // Применяем скидку за выходной день
            double discountWeekend = IsWeekend(selectedDay) ? 0.15 : 0;
            double discountedCost = baseCost * (1 - discountWeekend);

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