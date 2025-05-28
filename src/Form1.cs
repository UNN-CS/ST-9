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
            this.Text = "����������� �����������";
            this.ClientSize = new Size(300, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblDays = new Label();
            lblDays.Text = "���������� ���� ����������:";
            lblDays.Location = new Point(10, 20);
            lblDays.AutoSize = true;
            lblDays.Name = "lblDays";

            txtDays = new TextBox();
            txtDays.Location = new Point(10, 50);
            txtDays.Size = new Size(200, 20);
            txtDays.Name = "txtDays";

            Label lblCategory = new Label();
            lblCategory.Text = "��������� ������ (1-3):\n1-������, 2-��������, 3-����";
            lblCategory.Location = new Point(10, 80);
            lblCategory.AutoSize = true;
            lblCategory.Name = "lblCategory";

            txtCategory = new TextBox();
            txtCategory.Location = new Point(10, 120);
            txtCategory.Size = new Size(200, 20);
            txtCategory.Name = "txtCategory";

            Label lblCapacity = new Label();
            lblCapacity.Text = "����������� ������ (1-3):";
            lblCapacity.Location = new Point(10, 150);
            lblCapacity.AutoSize = true;
            lblCapacity.Name = "lblCapacity";

            txtCapacity = new TextBox();
            txtCapacity.Location = new Point(10, 180);
            txtCapacity.Size = new Size(200, 20);
            txtCapacity.Name = "txtCapacity";

            Label lblSafe = new Label();
            lblSafe.Text = "���� (��/���):\n+200 ���/�����";
            lblSafe.Location = new Point(10, 210);
            lblSafe.AutoSize = true;
            lblSafe.Name = "lblSafe";

            txtSafe = new TextBox();
            txtSafe.Location = new Point(10, 240);
            txtSafe.Size = new Size(200, 20);
            txtSafe.Name = "txtSafe";

            Label lblBreakfast = new Label();
            lblBreakfast.Text = "������� (��/���):\n+300 ���/�����";
            lblBreakfast.Location = new Point(10, 270);
            lblBreakfast.AutoSize = true;
            lblBreakfast.Name = "lblBreakfast";

            txtBreakfast = new TextBox();
            txtBreakfast.Location = new Point(10, 300);
            txtBreakfast.Size = new Size(200, 20);
            txtBreakfast.Name = "txtBreakfast";

            lblSum = new Label();
            lblSum.Text = "�����: 0 ���.";
            lblSum.Location = new Point(10, 330);
            lblSum.AutoSize = true;
            lblSum.Font = new Font(lblSum.Font, FontStyle.Bold);
            lblSum.Name = "lblSum";

            btnCalculate = new Button();
            btnCalculate.Text = "����������";
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
                    MessageBox.Show("������� ���������� ���������� ���� (����� ����� ������ 0)", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCategory.Text, out int category) || category < 1 || category > 3)
                {
                    MessageBox.Show("������� ��������� ������ (1, 2 ��� 3)\n1-������, 2-��������, 3-����", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity < 1 || capacity > 3)
                {
                    MessageBox.Show("������� ����������� ������ (1, 2 ��� 3)", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string safeInput = txtSafe.Text.Trim().ToLower();
                if (safeInput != "��" && safeInput != "���")
                {
                    MessageBox.Show("������� '��' ��� '���' ��� �����", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool hasSafe = safeInput == "��";

                string breakfastInput = txtBreakfast.Text.Trim().ToLower();
                if (breakfastInput != "��" && breakfastInput != "���")
                {
                    MessageBox.Show("������� '��' ��� '���' ��� ��������", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool hasBreakfast = breakfastInput == "��";

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

                lblSum.Text = $"�����: {total:N0} ���.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������:\n{ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
