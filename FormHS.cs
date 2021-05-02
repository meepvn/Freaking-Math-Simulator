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
    public partial class FormHS : Form
    {
        private void loadLV() {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2K5RFTQ;Initial Catalog=FreakingMath;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from HighScore order by Score desc,Name", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            for (int i = 0; reader.Read(); i++)
            {
                listView1.Items.Add((i + 1).ToString());
                listView1.Items[i].SubItems.Add(reader[1].ToString());
                listView1.Items[i].SubItems.Add(reader[2].ToString());
                listView1.Items[i].SubItems.Add(Convert.ToDateTime(reader[3].ToString()).ToShortDateString());
            }
            connection.Close();
        }
        public FormHS()
        {
            InitializeComponent();
            loadLV();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }
    }
}
