using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportBarang009
{
    public partial class MasterBarang_018 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source =LAPTOP-5F5TNO0N\SQLEXPRESS; Initial Catalog = QuizMahasiswa;Integrated Security = True;");

        public MasterBarang_018()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MasterBarang_018_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select isnull(max (cast (ID as int)),0) +1 from tbl_barang", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtID.Text = dt.Rows[0][0].ToString();
                }
    }
}
