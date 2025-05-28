using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelCalculatorForm
{
    public partial class Form1 : Form
    {
        private TextBox txtDays;
        private ComboBox cmbCategory;
        private ComboBox cmbPeople;
        private CheckBox chkSafebox;
        private CheckBox chkBreakfast;
        private TextBox txtResult;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Гостиничный калькулятор";
            this.Size = new System.Drawing.Size(400, 300);

            Label lblDays = new Label { Text = "Количество дней проживания", Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(200, 20) };
            txtDays = new TextBox { Location = new System.Drawing.Point(10, 30), Size = new System.Drawing.Size(200, 20) };
            txtDays.Name = "txtDays";

            Label lblCategory = new Label { Text = "Категория номера (1,2,3)", Location = new System.Drawing.Point(10, 60), Size = new System.Drawing.Size(200, 20) };
            cmbCategory = new ComboBox { Location = new System.Drawing.Point(10, 80), Size = new System.Drawing.Size(200, 20) };
            cmbCategory.Items.AddRange(new string[] { "1", "2", "3" });
            cmbCategory.SelectedIndex = 0;
            cmbCategory.Name = "cmbCategory";

            Label lblPeople = new Label { Text = "Вместимость номера (1,2,3)", Location = new System.Drawing.Point(10, 110), Size = new System.Drawing.Size(200, 20) };
            cmbPeople = new ComboBox { Location = new System.Drawing.Point(10, 130), Size = new System.Drawing.Size(200, 20) };
            cmbPeople.Items.AddRange(new string[] { "1", "2", "3" });
            cmbPeople.SelectedIndex = 0;
            cmbPeople.Name = "cmbPeople";

            chkSafebox = new CheckBox { Text = "Сейф (да/нет)", Location = new System.Drawing.Point(10, 160), Size = new System.Drawing.Size(200, 20) };
            chkSafebox.Name = "chkSafebox";

            chkBreakfast = new CheckBox { Text = "Завтрак (да/нет)", Location = new System.Drawing.Point(10, 190), Size = new System.Drawing.Size(200, 20) };
            chkBreakfast.Name = "chkBreakfast";

            Label lblResult = new Label { Text = "Сумма (руб.)", Location = new System.Drawing.Point(10, 225), Size = new System.Drawing.Size(80, 20) };
            txtResult = new TextBox { Location = new System.Drawing.Point(90, 220), Size = new System.Drawing.Size(100, 20), ReadOnly = true };
            txtResult.Name = "txtResult";

            Button btnCalculate = new Button { Text = "Рассчитать", Location = new System.Drawing.Point(195, 220), Size = new System.Drawing.Size(100, 23) };
            btnCalculate.Name = "btnCalculate";

            this.Controls.AddRange(new Control[] { lblDays, txtDays, lblCategory, cmbCategory, lblPeople, cmbPeople, chkSafebox, chkBreakfast, lblResult, txtResult, btnCalculate });

            btnCalculate.Click += (s, e) =>
            {
                try
                {
                    int days = int.Parse(txtDays.Text);

                    if (cmbCategory.SelectedItem == null)
                    {
                        MessageBox.Show("Пожалуйста, выберите категорию номера.");
                        return;
                    }
                    int category = int.Parse(cmbCategory.SelectedItem.ToString());

                    if (cmbPeople.SelectedItem == null)
                    {
                        MessageBox.Show("Пожалуйста, выберите количество людей.");
                        return;
                    }
                    int people = int.Parse(cmbPeople.SelectedItem.ToString());

                    bool safebox = chkSafebox.Checked;
                    bool breakfast = chkBreakfast.Checked;

                    int roomCostPerDay = category == 1 ? 8500 : category == 2 ? 4900 : 2000;
                    int totalRoomCost = roomCostPerDay * people * days;

                    int safeboxCost = safebox ? 1300 * days : 0;

                    int breakfastCost = breakfast ? 600 * people * days : 0;

                    int total = totalRoomCost + safeboxCost + breakfastCost;
                    txtResult.Text = total.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Пожалуйста, введите корректные данные: " + ex.Message);
                }
            };
        }
    }
}