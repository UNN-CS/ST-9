using Microsoft.VisualBasic.Logging;

namespace Гостиничный_калькулятор
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnCalculate_Click(object sender, EventArgs e)
		{
			try
			{
				int days = int.Parse(Days.Text);
				int category = int.Parse(Category.Text);
				int capacity = int.Parse(Capacity.Text);

				bool hasSafe = ParseYesNo(Safe.Text);
				bool hasBreakfast = ParseYesNo(Breakfast.Text);

				decimal total = CalculateTotal(days, category, capacity, hasSafe, hasBreakfast);

				Sum.Text = total.ToString("0");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка ввода данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private bool ParseYesNo(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				return false;

			return input.Trim().ToLower() == "да";
		}

		private decimal CalculateTotal(int days, int category, int capacity, bool hasSafe, bool hasBreakfast)
		{
			decimal basePrice = 0;

			switch (category)
			{
				case 1:
					basePrice = 1000;
					break;
				case 2:
					basePrice = 2000;
					break;
				case 3:
					basePrice = 5000;
					break;
				default:
					throw new ArgumentException("Категория номера должна быть 1, 2 или 3");
			}

			decimal capacityFactor = 1.0m;
			if (capacity == 2)
				capacityFactor = 1.5m;
			else if (capacity == 3)
				capacityFactor = 1.8m;

			decimal safeCost = hasSafe ? 200 * days : 0;
			decimal breakfastCost = hasBreakfast ? 300 * capacity * days : 0;

			decimal total = (basePrice * capacityFactor * days) + safeCost + breakfastCost;

			return total;
		}
	}
}