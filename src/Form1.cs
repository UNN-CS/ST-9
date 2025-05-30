namespace HotelForm
{
    public partial class Form1 : Form
    {
        private int baseCount = 500;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string countDayStr = countDays.Text;
            string catRoomStr = catRoom.Text;
            string volRoomStr = volRoom.Text;
            string isSafeStr = isSafe.Text;
            string isBreakfastStr = isBreakfast.Text;
            int countDaysInt = checkCountDay(countDayStr);
            if (countDaysInt < 0)
            {
                setTextOnItog("error", true);
                return;
            }
            int catRoomInt = checkCatRoomStr(catRoomStr);
            if (catRoomInt < 0)
            {
                setTextOnItog("error", true);
                return;
            }
            int volRoomInt = checkCatRoomStr(volRoomStr);
            if (volRoomInt < 0)
            {
                setTextOnItog("error", true);
                return;
            }
            bool isSafeBool = checkTextOnYesOrNo(isSafeStr);
            bool isBreakfastBool = checkTextOnYesOrNo(isBreakfastStr);
            int res = countDaysInt * (baseCount + (catRoomInt - 1) * 200 + (volRoomInt - 1) * 400 + (isSafeBool ? 200 : 0) + (isBreakfastBool ? 350 : 0));
            setTextOnItog(res.ToString(), false);
        }
        private int checkCountDay(string countDayStr)
        {
            if (string.IsNullOrEmpty(countDayStr) || countDayStr.Length < 1 || !int.TryParse(countDayStr, out int number) || number < 0)
            {
                return -1;
            }
            return number;
        }
        private int checkCatRoomStr(string catRoomStr)
        {
            if (string.IsNullOrEmpty(catRoomStr) || !int.TryParse(catRoomStr, out int number) || !(number == 1 || number == 2 || number == 3))
            {
                return -1;
            }
            return number;
        }
        private bool checkTextOnYesOrNo(string text)
        {
            if (text == "да" || text == "Да" || text == "Yes" || text == "yes")
                return true;
            return false;
        }
        private void setTextOnItog(String text, bool isError)
        {
            if (isError)
                result.ForeColor = Color.Red;
            else
                result.ForeColor = Color.Green;
            result.Text = text;
        }

        private void isBreakfast_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
