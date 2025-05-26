using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private TextBox daysTextBox;
        private ComboBox categoryComboBox;
        private ComboBox capacityComboBox;
        private CheckBox safeCheckBox;
        private CheckBox breakfastCheckBox;
        private TextBox sumTextBox;
        private Button calculateButton;
        private Button clearButton;
        private Label detailsLabel;

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Настройка формы
            this.Text = "Гостиничный калькулятор";
            this.ClientSize = new Size(400, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.LightGray;

            // Создание элементов управления
            CreateControls();

            // Расположение элементов
            ArrangeControls();

            // Добавление обработчиков событий
            AddEventHandlers();
        }

        private void CreateControls()
        {
            // Заголовок - теперь сохраняем как поле класса
            var titleLabel = new Label
            {
                Text = "Гостиничный калькулятор",
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Name = "titleLabel",
                AccessibleName = "Заголовок калькулятора"
            };
            this.Controls.Add(titleLabel); // Добавляем заголовок сразу

            // Поля ввода
            daysTextBox = new TextBox
            {
                Size = new Size(100, 20),
                Name = "daysTextBox",
                AccessibleName = "Поле ввода количества дней"
            };

            // Выпадающие списки
            categoryComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(100, 20),
                Name = "categoryComboBox",
                AccessibleName = "Выбор категории номера"
            };

            capacityComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(100, 20),
                Name = "capacityComboBox",
                AccessibleName = "Выбор вместимости номера"
            };

            // Чекбоксы
            safeCheckBox = new CheckBox
            {
                Text = "Сейф (+500 руб/день)",
                AutoSize = true,
                Name = "safeCheckBox",
                AccessibleName = "Дополнительно сейф"
            };

            breakfastCheckBox = new CheckBox
            {
                Text = "Завтрак (+800 руб/день)",
                AutoSize = true,
                Name = "breakfastCheckBox",
                AccessibleName = "Дополнительно завтрак"
            };

            // Поле результата
            sumTextBox = new TextBox
            {
                ReadOnly = true,
                Size = new Size(150, 20),
                BackColor = Color.White,
                Name = "sumTextBox",
                AccessibleName = "Итоговая сумма"
            };

            // Кнопки
            calculateButton = new Button
            {
                Text = "Рассчитать",
                Size = new Size(100, 30),
                BackColor = Color.LightGreen,
                Name = "calculateButton",
                AccessibleName = "Кнопка рассчитать"
            };

            clearButton = new Button
            {
                Text = "Очистить",
                Size = new Size(100, 30),
                BackColor = Color.LightCoral,
                Name = "clearButton",
                AccessibleName = "Кнопка очистить"
            };

            // Детали расчета
            detailsLabel = new Label
            {
                AutoSize = false,
                Size = new Size(350, 80),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Name = "detailsLabel",
                AccessibleName = "Детали расчета"
            };

            // Заполнение выпадающих списков
            categoryComboBox.Items.AddRange(new object[] { "1 - Эконом", "2 - Стандарт", "3 - Люкс" });
            capacityComboBox.Items.AddRange(new object[] { "1", "2", "3" });
        }

        private void ArrangeControls()
        {
            int y = 20;
            int labelWidth = 200;
            int controlX = 220;

            // Заголовок
            var titleLabel = (Label)this.Controls[0];
            titleLabel.Location = new Point((this.ClientSize.Width - titleLabel.Width) / 2, y);
            y += 40;

            // Метки и поля ввода
            AddControlPair("Количество дней проживания:", daysTextBox, ref y, labelWidth, controlX);
            AddControlPair("Категория номера:", categoryComboBox, ref y, labelWidth, controlX);
            AddControlPair("Вместимость номера:", capacityComboBox, ref y, labelWidth, controlX);

            // Чекбоксы
            safeCheckBox.Location = new Point(20, y);
            this.Controls.Add(safeCheckBox);
            y += 30;

            breakfastCheckBox.Location = new Point(20, y);
            this.Controls.Add(breakfastCheckBox);
            y += 40;

            // Кнопки
            calculateButton.Location = new Point(50, y);
            this.Controls.Add(calculateButton);

            clearButton.Location = new Point(200, y);
            this.Controls.Add(clearButton);
            y += 50;

            // Результат
            AddControlPair("Итоговая сумма:", sumTextBox, ref y, labelWidth, controlX);
            y += 40;

            // Детали расчета
            var detailsTitle = new Label
            {
                Text = "Детали расчета:",
                Location = new Point(20, y),
                AutoSize = true
            };
            this.Controls.Add(detailsTitle);
            y += 20;

            detailsLabel.Location = new Point(20, y);
            this.Controls.Add(detailsLabel);
        }

        private void AddControlPair(string labelText, Control control, ref int y, int labelWidth, int controlX)
        {
            var label = new Label
            {
                Text = labelText,
                Location = new Point(20, y),
                Size = new Size(labelWidth, 20),
                TextAlign = ContentAlignment.MiddleRight
            };
            this.Controls.Add(label);

            control.Location = new Point(controlX, y);
            this.Controls.Add(control);
            y += 30;
        }

        private void AddEventHandlers()
        {
            calculateButton.Click += CalculateButton_Click;
            clearButton.Click += ClearButton_Click;
            daysTextBox.KeyPress += DaysTextBox_KeyPress;
        }

        private void DaysTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            daysTextBox.Clear();
            categoryComboBox.SelectedIndex = -1;
            capacityComboBox.SelectedIndex = -1;
            safeCheckBox.Checked = false;
            breakfastCheckBox.Checked = false;
            sumTextBox.Clear();
            detailsLabel.Text = string.Empty;
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                int days = int.Parse(daysTextBox.Text);
                int category = categoryComboBox.SelectedIndex + 1;

                // Безопасное получение значения вместимости
                int capacity = 1; // Значение по умолчанию
                if (capacityComboBox.SelectedItem != null)
                {
                    capacity = int.Parse(capacityComboBox.SelectedItem.ToString());
                }

                decimal basePrice = GetBasePrice(category);
                decimal capacityMultiplier = GetCapacityMultiplier(capacity);
                decimal extras = GetExtrasPrice();

                decimal dailyPrice = (basePrice * capacityMultiplier) + extras;
                decimal total = dailyPrice * days;

                sumTextBox.Text = total.ToString("C");
                ShowCalculationDetails(days, basePrice, capacityMultiplier, extras, dailyPrice, total);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчёта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(daysTextBox.Text) || !int.TryParse(daysTextBox.Text, out int days) || days <= 0)
            {
                MessageBox.Show("Введите корректное количество дней (целое число больше 0)", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (categoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите категорию номера", "Ошибка выбора", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (capacityComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите вместимость номера", "Ошибка выбора", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private decimal GetBasePrice(int category)
        {
            return category switch
            {
                1 => 1000,   // Эконом
                2 => 2000,   // Стандарт
                3 => 5000,   // Люкс
                _ => 0
            };
        }

        private decimal GetCapacityMultiplier(int capacity)
        {
            return capacity switch
            {
                1 => 1.0m,
                2 => 1.2m,
                3 => 1.5m,
                _ => 1.0m
            };
        }

        private decimal GetExtrasPrice()
        {
            decimal extras = 0;
            if (safeCheckBox.Checked) extras += 500;
            if (breakfastCheckBox.Checked) extras += 800;
            return extras;
        }

        private void ShowCalculationDetails(int days, decimal basePrice, decimal capacityMultiplier, decimal extras, decimal dailyPrice, decimal total)
        {
            string details = $"Базовая цена: {basePrice:C}\n" +
                           $"Коэффициент вместимости: {capacityMultiplier}\n" +
                           $"Доп. услуги: {extras:C}\n" +
                           $"Цена за день: {dailyPrice:C}\n" +
                           $"Количество дней: {days}\n" +
                           $"Итого: {total:C}";

            detailsLabel.Text = details;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}