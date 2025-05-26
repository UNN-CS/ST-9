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
            // ��������� �����
            this.Text = "����������� �����������";
            this.ClientSize = new Size(400, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.LightGray;

            // �������� ��������� ����������
            CreateControls();

            // ������������ ���������
            ArrangeControls();

            // ���������� ������������ �������
            AddEventHandlers();
        }

        private void CreateControls()
        {
            // ��������� - ������ ��������� ��� ���� ������
            var titleLabel = new Label
            {
                Text = "����������� �����������",
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Name = "titleLabel",
                AccessibleName = "��������� ������������"
            };
            this.Controls.Add(titleLabel); // ��������� ��������� �����

            // ���� �����
            daysTextBox = new TextBox
            {
                Size = new Size(100, 20),
                Name = "daysTextBox",
                AccessibleName = "���� ����� ���������� ����"
            };

            // ���������� ������
            categoryComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(100, 20),
                Name = "categoryComboBox",
                AccessibleName = "����� ��������� ������"
            };

            capacityComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(100, 20),
                Name = "capacityComboBox",
                AccessibleName = "����� ����������� ������"
            };

            // ��������
            safeCheckBox = new CheckBox
            {
                Text = "���� (+500 ���/����)",
                AutoSize = true,
                Name = "safeCheckBox",
                AccessibleName = "������������� ����"
            };

            breakfastCheckBox = new CheckBox
            {
                Text = "������� (+800 ���/����)",
                AutoSize = true,
                Name = "breakfastCheckBox",
                AccessibleName = "������������� �������"
            };

            // ���� ����������
            sumTextBox = new TextBox
            {
                ReadOnly = true,
                Size = new Size(150, 20),
                BackColor = Color.White,
                Name = "sumTextBox",
                AccessibleName = "�������� �����"
            };

            // ������
            calculateButton = new Button
            {
                Text = "����������",
                Size = new Size(100, 30),
                BackColor = Color.LightGreen,
                Name = "calculateButton",
                AccessibleName = "������ ����������"
            };

            clearButton = new Button
            {
                Text = "��������",
                Size = new Size(100, 30),
                BackColor = Color.LightCoral,
                Name = "clearButton",
                AccessibleName = "������ ��������"
            };

            // ������ �������
            detailsLabel = new Label
            {
                AutoSize = false,
                Size = new Size(350, 80),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Name = "detailsLabel",
                AccessibleName = "������ �������"
            };

            // ���������� ���������� �������
            categoryComboBox.Items.AddRange(new object[] { "1 - ������", "2 - ��������", "3 - ����" });
            capacityComboBox.Items.AddRange(new object[] { "1", "2", "3" });
        }

        private void ArrangeControls()
        {
            int y = 20;
            int labelWidth = 200;
            int controlX = 220;

            // ���������
            var titleLabel = (Label)this.Controls[0];
            titleLabel.Location = new Point((this.ClientSize.Width - titleLabel.Width) / 2, y);
            y += 40;

            // ����� � ���� �����
            AddControlPair("���������� ���� ����������:", daysTextBox, ref y, labelWidth, controlX);
            AddControlPair("��������� ������:", categoryComboBox, ref y, labelWidth, controlX);
            AddControlPair("����������� ������:", capacityComboBox, ref y, labelWidth, controlX);

            // ��������
            safeCheckBox.Location = new Point(20, y);
            this.Controls.Add(safeCheckBox);
            y += 30;

            breakfastCheckBox.Location = new Point(20, y);
            this.Controls.Add(breakfastCheckBox);
            y += 40;

            // ������
            calculateButton.Location = new Point(50, y);
            this.Controls.Add(calculateButton);

            clearButton.Location = new Point(200, y);
            this.Controls.Add(clearButton);
            y += 50;

            // ���������
            AddControlPair("�������� �����:", sumTextBox, ref y, labelWidth, controlX);
            y += 40;

            // ������ �������
            var detailsTitle = new Label
            {
                Text = "������ �������:",
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

                // ���������� ��������� �������� �����������
                int capacity = 1; // �������� �� ���������
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
                MessageBox.Show($"������ �������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(daysTextBox.Text) || !int.TryParse(daysTextBox.Text, out int days) || days <= 0)
            {
                MessageBox.Show("������� ���������� ���������� ���� (����� ����� ������ 0)", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (categoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("�������� ��������� ������", "������ ������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (capacityComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("�������� ����������� ������", "������ ������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private decimal GetBasePrice(int category)
        {
            return category switch
            {
                1 => 1000,   // ������
                2 => 2000,   // ��������
                3 => 5000,   // ����
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
            string details = $"������� ����: {basePrice:C}\n" +
                           $"����������� �����������: {capacityMultiplier}\n" +
                           $"���. ������: {extras:C}\n" +
                           $"���� �� ����: {dailyPrice:C}\n" +
                           $"���������� ����: {days}\n" +
                           $"�����: {total:C}";

            detailsLabel.Text = details;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}