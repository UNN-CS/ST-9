namespace HotelCalculatorUI
{
    public partial class MainForm : Form
    {
        // Константы для расчета цены
        private const int DailyRateMultiplier = 10000;
        private const int RoomCategoryMultiplier = 1000;
        private const int GuestCapacityMultiplier = 100;
        private const int SafeDepositFee = 10;
        private const int BreakfastFee = 1;

        public MainForm()
        {
            InitializeComponent();
            ConfigureControls();
        }

        private void ConfigureControls()
        {
            // Настройка элементов управления
            calcBtn.Click += CalculateButton_Click;
            this.AcceptButton = calcBtn; // Enter для расчета
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                int totalPrice = CalculateTotalPrice();
                price.Text = totalPrice.ToString();
            }
            catch (FormatException)
            {
                ShowError("Пожалуйста, введите корректные числовые значения");
            }
            catch (InvalidYesNoValueException)
            {
                ShowError("Пожалуйста, укажите 'да' или 'нет' для дополнительных услуг");
            }
            catch (Exception ex)
            {
                ShowError($"Произошла ошибка: {ex.Message}");
            }
        }

        private int CalculateTotalPrice()
        {
            // Основной расчет стоимости
            int daysCount = ParseIntValue(days.Text, "количество дней");
            int roomCategory = ParseIntValue(category.Text, "категория номера");
            int guestCount = ParseIntValue(capacity.Text, "количество гостей");

            bool hasSafeDeposit = ParseYesNoValue(safe.Text, "сейф");
            bool hasBreakfast = ParseYesNoValue(breakfast.Text, "завтрак");

            return daysCount * DailyRateMultiplier
                 + roomCategory * RoomCategoryMultiplier
                 + guestCount * GuestCapacityMultiplier
                 + (hasSafeDeposit ? SafeDepositFee : 0)
                 + (hasBreakfast ? BreakfastFee : 0);
        }

        private int ParseIntValue(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new FormatException($"Поле '{fieldName}' не заполнено");

            if (!int.TryParse(input, out int result) || result < 0)
                throw new FormatException($"Некорректное значение для {fieldName}");

            return result;
        }

        private bool ParseYesNoValue(string input, string serviceName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new InvalidYesNoValueException($"Не указано значение для {serviceName}");

            string lowerInput = input.ToLower().Trim();
            
            return lowerInput switch
            {
                "да" => true,
                "нет" => false,
                _ => throw new InvalidYesNoValueException($"Недопустимое значение '{input}' для {serviceName}")
            };
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка ввода", 
                          MessageBoxButtons.OK, 
                          MessageBoxIcon.Error);
            price.Text = string.Empty;
        }
    }

    // Кастомное исключение для неверных значений Да/Нет
    public class InvalidYesNoValueException : Exception
    {
        public InvalidYesNoValueException(string message) : base(message) { }
    }
}