using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private TextBox txtDays;
        private TextBox txtCategory;
        private TextBox txtCapacity;
        private TextBox txtSafe;
        private TextBox txtBreakfast;
        private Label lblSum;
        private Button btnCalculate;

        public Form1()
        {
            InitializeComponent();
            CreateForm();
        }

        private void CreateForm()
        {
            this.Text = "Гостиничный калькулятор";
            this.ClientSize = new Size(300, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblDays = new Label();
            lblDays.Text = "Количество дней проживания:";
            lblDays.Location = new Point(10, 20);
            lblDays.AutoSize = true;
            lblDays.Name = "lblDays";

            txtDays = new TextBox();
            txtDays.Location = new Point(10, 50);
            txtDays.Size = new Size(200, 20);
            txtDays.Name = "txtDays";

            Label lblCategory = new Label();
            lblCategory.Text = "Категория номера (1-3):\n1-Эконом, 2-Стандарт, 3-Люкс";
            lblCategory.Location = new Point(10, 80);
            lblCategory.AutoSize = true;
            lblCategory.Name = "lblCategory";

            txtCategory = new TextBox();
            txtCategory.Location = new Point(10, 120);
            txtCategory.Size = new Size(200, 20);
            txtCategory.Name = "txtCategory";

            Label lblCapacity = new Label();
            lblCapacity.Text = "Вместимость номера (1-3):";
            lblCapacity.Location = new Point(10, 150);
            lblCapacity.AutoSize = true;
            lblCapacity.Name = "lblCapacity";

            txtCapacity = new TextBox();
            txtCapacity.Location = new Point(10, 180);
            txtCapacity.Size = new Size(200, 20);
            txtCapacity.Name = "txtCapacity";

            Label lblSafe = new Label();
            lblSafe.Text = "Сейф (да/нет):\n+200 руб/сутки";
            lblSafe.Location = new Point(10, 210);
            lblSafe.AutoSize = true;
            lblSafe.Name = "lblSafe";

            txtSafe = new TextBox();
            txtSafe.Location = new Point(10, 240);
            txtSafe.Size = new Size(200, 20);
            txtSafe.Name = "txtSafe";

            Label lblBreakfast = new Label();
            lblBreakfast.Text = "Завтрак (да/нет):\n+300 руб/сутки";
            lblBreakfast.Location = new Point(10, 270);
            lblBreakfast.AutoSize = true;
            lblBreakfast.Name = "lblBreakfast";

            txtBreakfast = new TextBox();
            txtBreakfast.Location = new Point(10, 300);
            txtBreakfast.Size = new Size(200, 20);
            txtBreakfast.Name = "txtBreakfast";

            lblSum = new Label();
            lblSum.Text = "Сумма: 0 руб.";
            lblSum.Location = new Point(10, 330);
            lblSum.AutoSize = true;
            lblSum.Font = new Font(lblSum.Font, FontStyle.Bold);
            lblSum.Name = "lblSum";

            btnCalculate = new Button();
            btnCalculate.Text = "Рассчитать";
            btnCalculate.Location = new Point(10, 360);
            btnCalculate.Size = new Size(100, 30);
            btnCalculate.Click += BtnCalculate_Click;
            btnCalculate.Name = "btnCalculate";

            this.Controls.Add(lblDays);
            this.Controls.Add(txtDays);
            this.Controls.Add(lblCategory);
            this.Controls.Add(txtCategory);
            this.Controls.Add(lblCapacity);
            this.Controls.Add(txtCapacity);
            this.Controls.Add(lblSafe);
            this.Controls.Add(txtSafe);
            this.Controls.Add(lblBreakfast);
            this.Controls.Add(txtBreakfast);
            this.Controls.Add(lblSum);
            this.Controls.Add(btnCalculate);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtDays.Text, out int days) || days <= 0)
                {
                    MessageBox.Show("Введите корректное количество дней (целое число больше 0)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCategory.Text, out int category) || category < 1 || category > 3)
                {
                    MessageBox.Show("Введите категорию номера (1, 2 или 3)\n1-Эконом, 2-Стандарт, 3-Люкс", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity < 1 || capacity > 3)
                {
                    MessageBox.Show("Введите вместимость номера (1, 2 или 3)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string safeInput = txtSafe.Text.Trim().ToLower();
                if (safeInput != "да" && safeInput != "нет")
                {
                    MessageBox.Show("Введите 'да' или 'нет' для сейфа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool hasSafe = safeInput == "да";

                string breakfastInput = txtBreakfast.Text.Trim().ToLower();
                if (breakfastInput != "да" && breakfastInput != "нет")
                {
                    MessageBox.Show("Введите 'да' или 'нет' для завтрака", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool hasBreakfast = breakfastInput == "да";

                decimal basePricePerDay = 0;
                switch (category)
                {
                    case 1: basePricePerDay = 1000; break;
                    case 2: basePricePerDay = 2000; break;
                    case 3: basePricePerDay = 5000; break;
                }

                decimal capacityDiscount = 1.0m;
                switch (capacity)
                {
                    case 2: capacityDiscount = 0.9m; break;
                    case 3: capacityDiscount = 0.8m; break;
                }

                decimal safePrice = hasSafe ? 200 * days : 0;
                decimal breakfastPrice = hasBreakfast ? 300 * days : 0;

                decimal total = (basePricePerDay * capacityDiscount * days) + safePrice + breakfastPrice;

                lblSum.Text = $"Сумма: {total:N0} руб.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
