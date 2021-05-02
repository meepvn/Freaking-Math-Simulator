namespace FreakingMath
{
    public partial class Form1 : Form
    {
        public bool isPlaying = false;
        public Form1()
        {
            InitializeComponent();
        }
        public changingForm(){
            isPlaying = true;
            this.Close();
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            FormGame fGame = new FormGame();
            fGame.Show();
            changingForm();
        }

        private void btnHighScore_Click(object sender, EventArgs e)
        {
            FormHS fHS = new FormHS();
            fHS.Show();
            changingForm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult DR = MessageBox.Show("Do you really want to quit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DR == DialogResult.No) return;
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isPlaying) Application.Exit();
        }
    }
}
