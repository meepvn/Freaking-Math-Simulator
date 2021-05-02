using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace FreakingMath
{
    public partial class FormAddHs : Form
    {
        public FormAddHs()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txbName.Text == "") {
                MessageBox.Show("Name can not be blank");
                return;
            }
            if (txbName.TextLength > 10) {
                MessageBox.Show("Maxium 10 letters");
                return;
            }
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2K5RFTQ;Initial Catalog=FreakingMath;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("update HighScore set Name=@name,Score=@score,Time= CURRENT_TIMESTAMP where ID=(select top 1 ID from HighScore order by Score,Time)", connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@name", txbName.Text);
            cmd.Parameters.AddWithValue("@score", lbHS.Text);
            cmd.ExecuteNonQuery();
            connection.Close();
            
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        private void FormAddHs_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.ShowBalloonTip(3000);
            Form1 f = new Form1();
            f.Show();
        }
    }
}
