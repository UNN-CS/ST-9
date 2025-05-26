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
            this.ClientSize = new Size(450, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 245, 249);
            this.Font = new Font("Segoe UI", 9);

            // Создание элементов управления
            CreateControls();

            // Расположение элементов
            ArrangeControls();

            // Добавление обработчиков событий
            AddEventHandlers();
        }

        private void CreateControls()
        {
            // Поля ввода
            daysTextBox = new TextBox
            {
                Size = new Size(150, 25),
                Name = "daysTextBox",
                AccessibleName = "Поле ввода количества дней",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Выпадающие списки
            categoryComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(150, 25),
                Name = "categoryComboBox",
                AccessibleName = "Выбор категории номера",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White
            };

            capacityComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(150, 25),
                Name = "capacityComboBox",
                AccessibleName = "Выбор вместимости номера",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White
            };

            // Чекбоксы
            safeCheckBox = new CheckBox
            {
                Text = "Сейф",
                AutoSize = true,
                Name = "safeCheckBox",
                AccessibleName = "Дополнительно сейф",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            breakfastCheckBox = new CheckBox
            {
                Text = "Завтрак",
                AutoSize = true,
                Name = "breakfastCheckBox",
                AccessibleName = "Дополнительно завтрак",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            // Поле результата
            sumTextBox = new TextBox
            {
                ReadOnly = true,
                Size = new Size(150, 25),
                BackColor = Color.White,
                Name = "sumTextBox",
                AccessibleName = "Итоговая сумма",
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215)
            };

            // Кнопки
            calculateButton = new Button
            {
                Text = "Рассчитать",
                Size = new Size(120, 35),
                Name = "calculateButton",
                AccessibleName = "Кнопка рассчитать",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            clearButton = new Button
            {
                Text = "Очистить",
                Size = new Size(120, 35),
                Name = "clearButton",
                AccessibleName = "Кнопка очистить",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(230, 230, 230),
                ForeColor = Color.FromArgb(64, 64, 64),
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };

            // Заполнение выпадающих списков
            categoryComboBox.Items.AddRange(new object[] { "1", "2", "3" });
            capacityComboBox.Items.AddRange(new object[] { "1", "2", "3" });
        }

        private void ArrangeControls()
        {
            // Заголовок
            var titleLabel = new Label
            {
                Text = "Гостиничный калькулятор",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215),
                AutoSize = true,
                Location = new Point((this.ClientSize.Width - 250) / 2, 20)
            };
            this.Controls.Add(titleLabel);

            int y = 70;
            int labelWidth = 180;
            int controlX = 200;

            // Метки и поля ввода
            AddControlPair("Количество дней", daysTextBox, ref y, labelWidth, controlX+10);
            AddControlPair("Категория номера:", categoryComboBox, ref y, labelWidth, controlX+10);
            AddControlPair("Количество мест", capacityComboBox, ref y, labelWidth, controlX+10);

            // Чекбоксы
            safeCheckBox.Location = new Point(controlX, y);
            this.Controls.Add(safeCheckBox);
            y += 30;

            breakfastCheckBox.Location = new Point(controlX, y);
            this.Controls.Add(breakfastCheckBox);
            y += 40;

            // Группа для кнопок
            var buttonPanel = new Panel
            {
                Location = new Point((this.ClientSize.Width - 260) / 2, y),
                Size = new Size(260, 50),
                BackColor = Color.Transparent
            };

            calculateButton.Location = new Point(0, 0);
            clearButton.Location = new Point(140, 0);

            buttonPanel.Controls.Add(calculateButton);
            buttonPanel.Controls.Add(clearButton);
            this.Controls.Add(buttonPanel);
            y += 60;

            // Результат
            AddControlPair("Стоимость:", sumTextBox, ref y, labelWidth, controlX+10);
            y += 40;
        }

        private void AddControlPair(string labelText, Control control, ref int y, int labelWidth, int controlX)
        {
            var label = new Label
            {
                Text = labelText,
                Location = new Point(30, y),
                Size = new Size(labelWidth, 25),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(64, 64, 64)
            };
            this.Controls.Add(label);

            control.Location = new Point(controlX, y);
            this.Controls.Add(control);
            y += 35;
        }

        private void AddEventHandlers()
        {
            calculateButton.Click += CalculateButton_Click;
            clearButton.Click += ClearButton_Click;
            daysTextBox.KeyPress += DaysTextBox_KeyPress;

            // Эффекты при наведении на кнопки
            calculateButton.MouseEnter += (s, e) => calculateButton.BackColor = Color.FromArgb(0, 90, 180);
            calculateButton.MouseLeave += (s, e) => calculateButton.BackColor = Color.FromArgb(0, 120, 215);

            clearButton.MouseEnter += (s, e) => clearButton.BackColor = Color.FromArgb(210, 210, 210);
            clearButton.MouseLeave += (s, e) => clearButton.BackColor = Color.FromArgb(230, 230, 230);
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