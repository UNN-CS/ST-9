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

        public Form1()
        {
            InitializeComponent();
            CreateControls();
            ArrangeControls();
            SetupEventHandlers();
        }

        private void CreateControls()
        {
            // Поле для количества дней
            daysTextBox = new TextBox
            {
                Name = "daysTextBox",
                AccessibleName = "daysTextBox",
                Size = new Size(150, 25),
                Location = new Point(150, 50)
            };

            // Выпадающие списки
            categoryComboBox = new ComboBox
            {
                Name = "categoryComboBox",
                AccessibleName = "categoryComboBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(150, 25),
                Location = new Point(150, 90)
            };
            categoryComboBox.Items.AddRange(new object[] { "Эконом", "Стандарт", "Люкс" });

            capacityComboBox = new ComboBox
            {
                Name = "capacityComboBox",
                AccessibleName = "capacityComboBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(150, 25),
                Location = new Point(150, 130)
            };
            capacityComboBox.Items.AddRange(new object[] { "1 человек", "2 человека", "3 человека" });

            // Чекбоксы
            safeCheckBox = new CheckBox
            {
                Name = "safeCheckBox",
                AccessibleName = "safeCheckBox",
                Text = "Сейф",
                AutoSize = true,
                Location = new Point(150, 170)
            };

            breakfastCheckBox = new CheckBox
            {
                Name = "breakfastCheckBox",
                AccessibleName = "breakfastCheckBox",
                Text = "Завтрак",
                AutoSize = true,
                Location = new Point(150, 200)
            };

            // Поле результата
            sumTextBox = new TextBox
            {
                Name = "sumTextBox",
                AccessibleName = "sumTextBox",
                ReadOnly = true,
                Size = new Size(150, 25),
                Location = new Point(150, 240),
                BackColor = Color.White
            };

            // Кнопки
            calculateButton = new Button
            {
                Name = "calculateButton",
                AccessibleName = "calculateButton",
                Text = "Рассчитать",
                Size = new Size(120, 35),
                Location = new Point(100, 290)
            };

            clearButton = new Button
            {
                Name = "clearButton",
                AccessibleName = "clearButton",
                Text = "Очистить",
                Size = new Size(120, 35),
                Location = new Point(250, 290)
            };
        }

        private void ArrangeControls()
        {
            // Добавляем метки
            var labels = new[]
            {
                new Label { Text = "Количество дней:", Location = new Point(20, 50), AutoSize = true },
                new Label { Text = "Категория номера:", Location = new Point(20, 90), AutoSize = true },
                new Label { Text = "Количество гостей:", Location = new Point(20, 130), AutoSize = true },
                new Label { Text = "Итоговая сумма:", Location = new Point(20, 240), AutoSize = true }
            };

            foreach (var label in labels)
            {
                this.Controls.Add(label);
            }

            // Добавляем основные элементы
            this.Controls.Add(daysTextBox);
            this.Controls.Add(categoryComboBox);
            this.Controls.Add(capacityComboBox);
            this.Controls.Add(safeCheckBox);
            this.Controls.Add(breakfastCheckBox);
            this.Controls.Add(sumTextBox);
            this.Controls.Add(calculateButton);
            this.Controls.Add(clearButton);
        }

        private void SetupEventHandlers()
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
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                int days = int.Parse(daysTextBox.Text);
                int category = categoryComboBox.SelectedIndex + 1;
                int capacity = capacityComboBox.SelectedIndex + 1;

                decimal basePrice = GetBasePrice(category);
                decimal capacityMultiplier = GetCapacityMultiplier(capacity);
                decimal extras = GetExtrasPrice();

                decimal total = (basePrice * capacityMultiplier + extras) * days;
                sumTextBox.Text = total.ToString("C");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(daysTextBox.Text) || !int.TryParse(daysTextBox.Text, out _))
            {
                MessageBox.Show("Введите корректное количество дней", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (categoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите категорию номера", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (capacityComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите количество гостей", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}