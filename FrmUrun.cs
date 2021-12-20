using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgresUrunProje
{
    public partial class FrmUrun : Form
    {
        public FrmUrun()
        {
            InitializeComponent();
        }

        private NpgsqlConnection start = new NpgsqlConnection("server=localhost;" +
                " port=7757;" +
                "DataBase=dburunler;" +
                "user ID=postgres;" +
                "password=775705161");

        // show the database on the screen
        private void BtnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from urunler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, start);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        // start form the begining
        private void FrmUrun_Load(object sender, EventArgs e)
        {
            start.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from kategoriler", start);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "kategoriad";
            comboBox1.ValueMember = "kategoriid";
            comboBox1.DataSource = dt;
            start.Close();
        }

        // this is the function where we can add a new items
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            start.Open();
            NpgsqlCommand prosses1 = new NpgsqlCommand("insert into urunler " +
                "(urunid,urunad,stok,alisfiyat,satisfiyat,gorsel,kategori) " +
                "values(@p1,@p2, @p3, @p4, @p5,@p6, @p7)", start);

            prosses1.Parameters.AddWithValue("@p1", int.Parse(TxtID.Text));
            prosses1.Parameters.AddWithValue("@p2", TxtAd.Text);
            prosses1.Parameters.AddWithValue("@p3", int.Parse(numericUpDown1.Value.ToString()));
            prosses1.Parameters.AddWithValue("@p4", double.Parse(TxtAlisFiyat.Text));
            prosses1.Parameters.AddWithValue("@p5", double.Parse(TxtSatisFiyat.Text));
            prosses1.Parameters.AddWithValue("@p6", TxtGorsel.Text);
            prosses1.Parameters.AddWithValue("@p7", int.Parse(comboBox1.SelectedValue.ToString()));

            prosses1.ExecuteNonQuery();
            start.Close();
            MessageBox.Show("kayid basarli oldu :) ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // We can update any informartion by using this function
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            start.Open();
            NpgsqlCommand prosses2 = new NpgsqlCommand("update urunler set urunad=@p1,stok=@p2," +
                "alisfiyat=@p3 where urunid=@p4", start);
            prosses2.Parameters.AddWithValue("@p1", TxtAd.Text);
            prosses2.Parameters.AddWithValue("@p2", int.Parse(numericUpDown1.Value.ToString()));
            prosses2.Parameters.AddWithValue("@p3", double.Parse(TxtAlisFiyat.Text));
            prosses2.Parameters.AddWithValue("@p4", int.Parse(TxtID.Text));
            prosses2.ExecuteNonQuery();
            MessageBox.Show("Basrili Gucelleme :) ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            start.Close();
            start.Close();
            start.Close();
            start.Close();
            start.Close();
            start.Close();
        }

        // if we want to show the database on the screen we can use this function
        private void BtnView_Click(object sender, EventArgs e)
        {
            start.Open();
            NpgsqlCommand prosses3 = new NpgsqlCommand("Select * from urunlistesi", start);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(prosses3);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            start.Close();
        }

        // of course we must have delete function to delete any information
        private void BtnSil_Click(object sender, EventArgs e)
        {
            start.Open();
            NpgsqlCommand prosses3 = new NpgsqlCommand("DElete From urunler where urunid=@p1",
                start);
            prosses3.Parameters.AddWithValue("@p1", int.Parse(TxtID.Text));
            prosses3.ExecuteNonQuery();
            start.Close();
            MessageBox.Show("Silme basirli oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        // if we have a lot of information we need the function search,to find any data fastly.
        private void BtnAra_Click(object sender, EventArgs e)
        {
            start.Open();
            string sql = "Select * from kategoriler where kategoriid=" + TxtID.Text;
            NpgsqlCommand cmd = new NpgsqlCommand(sql, start);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            start.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}