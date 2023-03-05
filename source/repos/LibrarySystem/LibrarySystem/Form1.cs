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
using System.Runtime.Remoting.Contexts;


namespace LibrarySystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        DbKutuphaneSyetemEntities1 db = new DbKutuphaneSyetemEntities1();


        private void BtnKitapAra_Click(object sender, EventArgs e)
        {

            SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DbKutuphaneSyetem;Integrated Security=True");

            dataGridView1.DataSource = db.kitap.Where(x => x.kitapadi == txtkitapad.Text).ToList();


        }

        private void BtnYazarAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.yazar.Where(x => x.yazarad == txtyazarad.Text).ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)       // 2) "Töre Sivrioğlu" nun herhangi bir kitabını alan öğrencileri bulunuz. 
            {
                var ogrenciler = db.ogrenci
                    .Where(o => o.islem.Any(i => i.kitap.kitapadi == "Töre Silvioğlu")).ToList();

                dataGridView1.DataSource = ogrenciler;
            }



            if (radioButton2.Checked == true)      // 5) Erkeklerin ve kızların kitap alma sayıları nelerdir
            {
                var islemstudents = db.ogrenci .Where(o => o.islem.Any()).GroupBy(o => o.cinsiyet)
                    .Select  (g => new  {
                        Cinsiyet = g.Key,
                        Sayi = g.Count()
                    }).ToList();

                dataGridView1.DataSource = islemstudents;

            }
          


            if (radioButton3.Checked == true)    //1) En az bir kitap almış öğrencileri bulunuz
            {
                var kitapalan = db.ogrenci.Where(x => x.islem.Any()).ToList();
                dataGridView1.DataSource = kitapalan;

            }



            if (radioButton4.Checked == true)    //4) 400 ve üzeri sayfa sayısına sahip kitapları 2023 yılına kadar alınma sayısı kaçtır
            {
                var teslim = db.kitap.Where(x => x.sayfasayisi >= 400).Select(a => new
                {
                    Kitapadı = a.kitapadi,

                    AlmaSayısı = a.islem.Count(y=>y.atarih.Value.Year <= 2023)

                }).ToList();

                dataGridView1.DataSource = teslim;
            }



            if(radioButton5.Checked == true)  // 3) Bilim Kurgu türünde kitap alıp teslim etmeyen öğrencileri bulunuz.
            {

                var teslimolmayan = db.ogrenci.Where(x => x.islem.Any(i => i.kitap.tur.turadi == "Bilim Kurgu" && i.vtarih == null)).ToList();
                                    

                dataGridView1.DataSource = teslimolmayan;  
            }



        }

       

    }
}
