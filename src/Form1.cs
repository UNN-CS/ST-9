namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(textBoxDays.Text);
                int category = int.Parse(textBoxCategory.Text); 
                int capacity = int.Parse(textBoxCapacity.Text); 
                bool safe = textBoxSafe.Text.ToLower() == "да";
                bool breakfast = textBoxBreakfast.Text.ToLower() == "да";

                decimal costPerDay = 0;
                switch (category)
                {
                    case 1: 
                        costPerDay = 3000;
                        break;
                    case 2: 
                        costPerDay = 2000;
                        break;
                    case 3: 
                        costPerDay = 1000;
                        break;
                    default:
                        MessageBox.Show("Неверная категория номера. Введите 1, 2 или 3.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                if (capacity < 1 || capacity > 3)
                {
                    MessageBox.Show("Неверная вместимость номера. Введите 1, 2 или 3.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal totalCost = days * costPerDay * capacity;

                if (safe)
                {
                    totalCost += days * 300; 
                }

                if (breakfast)
                {
                    totalCost += days * 500 * capacity; 
                }

                textBoxSum.Text = totalCost.ToString("C"); 
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода данных. Пожалуйста, проверьте правильность введенных числовых значений.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
