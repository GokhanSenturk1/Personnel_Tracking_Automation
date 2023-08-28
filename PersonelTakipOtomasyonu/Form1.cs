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
using System.Linq.Expressions;

namespace PersonelTakipOtomasyonu
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti= new SqlConnection("Data Source=DESKTOP-GF5TQSU\\SQLEXPRESS;Initial Catalog=Deneme;Integrated Security=True");

        SqlCommand komut;
        DataTable tablo=new DataTable();    
      

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.personelTakipTablosuTableAdapter.Fill(this.denemeDataSet1.PersonelTakipTablosu);
            labelRadioButton.Visible = false;
        }
        private void buttonListele_Click(object sender, EventArgs e)
        {
            this.personelTakipTablosuTableAdapter.Fill(this.denemeDataSet1.PersonelTakipTablosu);

        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            komut = new SqlCommand("insert into PersonelTakipTablosu(PerAd, PerSoyad, PerSehir, PerMaas, PerMeslek) values (@ad, @soyad, @sehir, @maas, @meslek)",baglanti);
            SqlDataAdapter adap= new SqlDataAdapter(komut);

            komut.Parameters.AddWithValue("@ad",textBoxAD.Text);
            komut.Parameters.AddWithValue("@soyad",textBoxSOYAD.Text);
            komut.Parameters.AddWithValue("@sehir",comboBoxSehir.Text);
            komut.Parameters.AddWithValue("@maas",maskedTextBoxMaas.Text);
            komut.Parameters.AddWithValue("@meslek",textBoxMeslek.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Ekleme İşlemi Başarıyla Güncellenmiştir");
        }
        private void dgw_PersonelList_CellEnter(object sender, DataGridViewCellEventArgs e)  
        {
            textBoxID.Text = dgw_PersonelList.CurrentRow.Cells[0].Value.ToString();
            textBoxAD.Text = dgw_PersonelList.CurrentRow.Cells[1].Value.ToString();
            textBoxSOYAD.Text = dgw_PersonelList.CurrentRow.Cells[2].Value.ToString();
            comboBoxSehir.Text = dgw_PersonelList.CurrentRow.Cells[3].Value.ToString();
            maskedTextBoxMaas.Text = dgw_PersonelList.CurrentRow.Cells[4].Value.ToString();
            textBoxMeslek.Text = dgw_PersonelList.CurrentRow.Cells[6].Value.ToString();
            labelRadioButton.Text = dgw_PersonelList.CurrentRow.Cells[5].Value.ToString();
        }

        private void labelRadioButton_TextChanged(object sender, EventArgs e)
        {
            if (labelRadioButton.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (labelRadioButton.Text == "False")
            {
                radioButton2.Checked = false;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                labelRadioButton.Text = "True";
            }

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                labelRadioButton.Text = "false";
            }
        }
        private void buttonSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            komut = new SqlCommand("DELETE From PersonelTakipTablosu WHERE PerAd=@ad",baglanti);
            komut.Parameters.AddWithValue("@ad",textBoxAD.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kayıt Başarıyla Silinmiştir");
        }
        private void buttonGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            komut = new SqlCommand("UPDATE PersonelTakipTablosu SET PerAd=@ad, PerSoyad=@soyad, PerSehir=@sehir, PerMaas=@maas, PerMeslek=@meslek WHERE Personelid=@perid",baglanti); //Burda perid deki bilgilerin parametrelerini güncelle diyoruz Where komutunu yazmasak hepsini günceller dikkat et 
           
            komut.Parameters.AddWithValue("@ad",textBoxAD.Text);
            komut.Parameters.AddWithValue("@soyad",textBoxSOYAD.Text);
            komut.Parameters.AddWithValue("@sehir",comboBoxSehir.Text);
            komut.Parameters.AddWithValue("@maas",maskedTextBoxMaas.Text);
            komut.Parameters.AddWithValue("@meslek",textBoxMeslek.Text);
            komut.Parameters.AddWithValue("@perid", textBoxID.Text);

            komut .ExecuteNonQuery();   
            baglanti.Close();
            MessageBox.Show("Personel Güncellemesi Başarıyla Gerçekleştir");
        }
         void temizle()
        {
            textBoxAD.Text = "";
            textBoxSOYAD.Text = "";
            textBoxID.Text = "";
            textBoxMeslek.Text = "";
            maskedTextBoxMaas.Text = "";
            comboBoxSehir.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBoxAD.Focus();
        }
        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}