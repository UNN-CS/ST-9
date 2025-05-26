using System;
using System.Globalization;
using System.Windows.Forms;

namespace WinFormsApp
{
    public class Form1 : Form
    {
        // поля ввода
        private TextBox txtDays;
        private TextBox txtCategory;
        private TextBox txtCapacity;
        private CheckBox chkSafe;
        private CheckBox chkBreakfast;
        private TextBox txtSum;
        private Button btnCalc;

        public Form1()
        {
            // свойства формы
            Text = "Гостиничный калькулятор";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 400;
            Height = 350;

            // helper для добавления метки + текстового поля
            void AddField(string labelText, int top, out TextBox txt, string name)
            {
                var lbl = new Label
                {
                    Text = labelText,
                    Left = 10,
                    Top = top + 3,
                    Width = 180
                };
                txt = new TextBox
                {
                    Left = 200,
                    Top = top,
                    Width = 150,
                    Name = name
                };
                Controls.Add(lbl);
                Controls.Add(txt);
            }

            AddField("Количество дней проживания", 20, out txtDays, "txtDays");
            AddField("Категория номера (1,2,3)", 60, out txtCategory, "txtCategory");
            AddField("Вместимость номера (1–3 чел.)", 100, out txtCapacity, "txtCapacity");

            // чекбоксы для опций
            chkSafe = new CheckBox
            {
                Text = "Сейф",
                Left = 10,
                Top = 140,
                Width = 180,
                Name = "chkSafe"
            };
            Controls.Add(chkSafe);

            chkBreakfast = new CheckBox
            {
                Text = "Завтрак",
                Left = 10,
                Top = 180,
                Width = 180,
                Name = "chkBreakfast"
            };
            Controls.Add(chkBreakfast);

            // поле для результата
            var lblSum = new Label
            {
                Text = "Сумма:",
                Left = 10,
                Top = 223,
                Width = 180
            };
            txtSum = new TextBox
            {
                Left = 200,
                Top = 220,
                Width = 150,
                ReadOnly = true,
                Name = "txtSum"
            };
            Controls.Add(lblSum);
            Controls.Add(txtSum);

            // кнопка «Рассчитать»
            btnCalc = new Button
            {
                Text = "Рассчитать",
                Left = 200,
                Top = 260,
                Width = 100,
                Name = "btnCalc"
            };
            btnCalc.Click += BtnCalc_Click;
            Controls.Add(btnCalc);
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(txtDays.Text, CultureInfo.InvariantCulture);
                int cat = int.Parse(txtCategory.Text, CultureInfo.InvariantCulture);
                int cap = int.Parse(txtCapacity.Text, CultureInfo.InvariantCulture);
                bool withSafe = chkSafe.Checked;
                bool withBreakfast = chkBreakfast.Checked;

                int basePrice = cat switch
                {
                    1 => 1000,
                    2 => 2000,
                    3 => 3000,
                    _ => throw new ArgumentException("Категория должна быть 1, 2 или 3")
                };

                int perDay = basePrice * cap
                             + (withSafe ? 100 : 0)
                             + (withBreakfast ? 200 : 0);

                int total = perDay * days;
                txtSum.Text = total.ToString("N0", CultureInfo.CurrentCulture) + " ₽";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ввода:\n" + ex.Message,
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
