namespace Hotel_Calculator
{
    public partial class Form1 : Form
    {
        private Dictionary<string, int> roomTypesCost = new Dictionary<string, int> { { "1", 2500 }, { "2", 4000 }, { "3", 7300 } };
        private Dictionary<string, int> roomSizeCost = new Dictionary<string, int> { { "1", 0 }, { "2", 1000 }, { "3", 2000 } };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            int sum = 0;
            int stayingDays = Int32.Parse(StayingDaysTextBox.Text);
            string guestsAmount = PersonsAmountTextBox.Text;
            string roomType = RoomTypeTextBox.Text;
            if (stayingDays > 0 && Int32.Parse(guestsAmount) < 4 && Int32.Parse(roomType) < 4)
            {
                sum += stayingDays * roomTypesCost[roomType] + roomSizeCost[guestsAmount];
            }
            else
            {
                return;
            }
            if (VaultTextBox.Text.Equals("y"))
            {
                sum += 500;
            }
            if (BreakfastTextBox.Text.Equals("y"))
            {
                sum += Int32.Parse(PersonsAmountTextBox.Text) * 400;
            }
            SumTextBox.Text = sum.ToString();
        }
    }
}
