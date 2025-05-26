namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Label labelDays;
        private System.Windows.Forms.TextBox textBoxDays;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label labelCapacity;
        private System.Windows.Forms.ComboBox comboBoxCapacity;
        private System.Windows.Forms.CheckBox checkBoxSafe;
        private System.Windows.Forms.CheckBox checkBoxBreakfast;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.TextBox textBoxTotal;
        private System.Windows.Forms.Button buttonCalculate;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.labelDays = new System.Windows.Forms.Label();
            this.textBoxDays = new System.Windows.Forms.TextBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelCapacity = new System.Windows.Forms.Label();
            this.comboBoxCapacity = new System.Windows.Forms.ComboBox();
            this.checkBoxSafe = new System.Windows.Forms.CheckBox();
            this.checkBoxBreakfast = new System.Windows.Forms.CheckBox();
            this.labelTotal = new System.Windows.Forms.Label();
            this.textBoxTotal = new System.Windows.Forms.TextBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.labelDays.AutoSize = true;
            this.labelDays.Location = new System.Drawing.Point(30, 30);
            this.labelDays.Name = "labelDays";
            this.labelDays.Size = new System.Drawing.Size(155, 15);
            this.labelDays.TabIndex = 0;
            this.labelDays.Text = "Количество дней проживания";

            this.textBoxDays.Location = new System.Drawing.Point(200, 27);
            this.textBoxDays.Name = "textBoxDays";
            this.textBoxDays.Size = new System.Drawing.Size(100, 23);
            this.textBoxDays.TabIndex = 1;

            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(30, 70);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(102, 15);
            this.labelCategory.TabIndex = 2;
            this.labelCategory.Text = "Категория номера";

            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Items.AddRange(new object[] { "1", "2", "3" });
            this.comboBoxCategory.Location = new System.Drawing.Point(200, 67);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(100, 23);
            this.comboBoxCategory.TabIndex = 3;

            this.labelCapacity.AutoSize = true;
            this.labelCapacity.Location = new System.Drawing.Point(30, 110);
            this.labelCapacity.Name = "labelCapacity";
            this.labelCapacity.Size = new System.Drawing.Size(112, 15);
            this.labelCapacity.TabIndex = 4;
            this.labelCapacity.Text = "Вместимость номера";

            this.comboBoxCapacity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCapacity.FormattingEnabled = true;
            this.comboBoxCapacity.Items.AddRange(new object[] { "1", "2", "3" });
            this.comboBoxCapacity.Location = new System.Drawing.Point(200, 107);
            this.comboBoxCapacity.Name = "comboBoxCapacity";
            this.comboBoxCapacity.Size = new System.Drawing.Size(100, 23);
            this.comboBoxCapacity.TabIndex = 5;

            this.checkBoxSafe.AutoSize = true;
            this.checkBoxSafe.Location = new System.Drawing.Point(30, 150);
            this.checkBoxSafe.Name = "checkBoxSafe";
            this.checkBoxSafe.Size = new System.Drawing.Size(49, 19);
            this.checkBoxSafe.TabIndex = 6;
            this.checkBoxSafe.Text = "Сейф";

            this.checkBoxBreakfast.AutoSize = true;
            this.checkBoxBreakfast.Location = new System.Drawing.Point(30, 180);
            this.checkBoxBreakfast.Name = "checkBoxBreakfast";
            this.checkBoxBreakfast.Size = new System.Drawing.Size(72, 19);
            this.checkBoxBreakfast.TabIndex = 7;
            this.checkBoxBreakfast.Text = "Завтрак";

            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(30, 220);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(47, 15);
            this.labelTotal.TabIndex = 8;
            this.labelTotal.Text = "Сумма";

            this.textBoxTotal.Location = new System.Drawing.Point(200, 217);
            this.textBoxTotal.Name = "textBoxTotal";
            this.textBoxTotal.ReadOnly = true;
            this.textBoxTotal.Size = new System.Drawing.Size(100, 23);
            this.textBoxTotal.TabIndex = 9;

            this.buttonCalculate.Location = new System.Drawing.Point(30, 260);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(270, 30);
            this.buttonCalculate.TabIndex = 10;
            this.buttonCalculate.Text = "Рассчитать";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.ButtonCalculate_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 320);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.textBoxTotal);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.checkBoxBreakfast);
            this.Controls.Add(this.checkBoxSafe);
            this.Controls.Add(this.comboBoxCapacity);
            this.Controls.Add(this.labelCapacity);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.labelCategory);
            this.Controls.Add(this.textBoxDays);
            this.Controls.Add(this.labelDays);
            this.Name = "Form1";
            this.Text = "Гостиничный калькулятор";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(textBoxDays.Text);
                int category = int.Parse(comboBoxCategory.SelectedItem?.ToString() ?? "0");
                int capacity = int.Parse(comboBoxCapacity.SelectedItem?.ToString() ?? "0");

                decimal basePrice = category * 1000 + capacity * 500;
                decimal total = days * basePrice;

                if (checkBoxSafe.Checked) total += 200 * days;
                if (checkBoxBreakfast.Checked) total += 300 * days;

                textBoxTotal.Text = total.ToString("C");
            }
            catch
            {
                textBoxTotal.Text = "0,00 ₽";
                MessageBox.Show("Проверьте правильность ввода данных");
            }
        }
    }
}
