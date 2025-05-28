namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void calcBtn_Click(object sender, EventArgs e)
        {
            Func<string, bool> chkB = s => s.ToLower() switch
            {
                "да" => true,
                "нет" => false,
                _ => throw new Exception()
            };

            try
            {
                price.Text = (0
                    + 10000 * int.Parse(days.Text)
                    + 1000 * int.Parse(category.Text)
                    + 100 * int.Parse(capacity.Text)
                    + 10 * (chkB(safe.Text) ? 1 : 0)
                    + 1 * (chkB(breakfast.Text) ? 1 : 0)).ToString();
            } catch {
                MessageBox.Show("Некорректный ввод");
            }
        }
    }
}
