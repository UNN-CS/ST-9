using System.Windows.Forms;

namespace ST9_WF {
public partial class Form1 : Form
{
    public Form() { InitializeComponent(); }

    private void resultButton_Click(object sender, EventArgs e)
    {
        try {
            int daysOfStay = int.Parse(daysOfStayTextBox.Text);
            if (daysOfStay <= 0) {
                throw new ArgumentException();
            }

            int roomCategory = int.Parse(roomCategoryTextBox.Text);
            if (roomCategory < 1 || roomCategory > 3) {
                throw new ArgumentException();
            }

            int roomCapacity = int.Parse(roomCapacityTextBox.Text);
            if (roomCategory < 1 || roomCapacity > 3) {
                throw new ArgumentException();
            }

            string safeOption = safeOptionTextBox.Text.ToLower();
            if (safeOption != "да" && safeOption != "нет") {
                throw new ArgumentException();
            }

            string breakfastOption = breakfastOptionTextBox.Text.ToLower();
            if (breakfastOption != "да" && breakfastOption != "нет") {
                throw new ArgumentException();
            }

            int result = 250 * daysOfStay * roomCategory * roomCapacity;
            if (safeOption == "да") {
                result += 500;
            }
            if (breakfastOption == "да") {
                result += 1000;
            }

            resultTextBox.Text = result.ToString();
        } catch {
            resultTextBox.Text = "Ошибка ввода";
        }
    }
}
}
