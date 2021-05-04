using NTier.BLL.RepositoryImplementation;
using NTier.DAL.Context;
using NTier.Model.Entity;
using NTier.Model.Enum;
using NTier.Model.Helpers;
using System.Linq;
using System.Windows.Forms;

namespace NTier_Code_First
{
    public partial class frmUrunler : Form
    {
        private readonly ProductRepository _productRepository;
        public frmUrunler()
        {
            InitializeComponent();
            _productRepository = new ProductRepository();
        }
        DataContext db = new DataContext();
        private void frmUrunler_Load(object sender, System.EventArgs e)
        {
            cmbKategoriler.DataSource = db.Categories.ToList();
            cmbKategoriler.DisplayMember = "Name";
            cmbKategoriler.ValueMember = "Id";
            ProductList();
        }

        private void ProductList()
        {
            lstUrunler.Items.Clear();
            foreach (var item in _productRepository.SearchList(x => x.Status == Status.Active || x.Status == Status.Updated))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.Id.ToString();
                lvi.SubItems.Add(item.Name);
                lvi.SubItems.Add(item.UnitInStock.ToString());
                lvi.SubItems.Add(item.Price.ToString());
                lvi.SubItems.Add(item.QuantityPerUnit);
                lvi.SubItems.Add(item.Category.Name.ToString());
                lvi.Tag = item;
                lstUrunler.Items.Add(lvi);
            }
        }

        Product guncellenecek;
        private void lstUrunler_DoubleClick(object sender, System.EventArgs e)
        {
            if (lstUrunler.Items.Count <= 0) return;
            guncellenecek = new Product();
            guncellenecek = _productRepository.SelectById(((Product)lstUrunler.SelectedItems[0].Tag).Id);

            cmbKategoriler.SelectedItem = guncellenecek.Category;
            txtUrunAdi.Text = guncellenecek.Name;
            txtBirim.Text = guncellenecek.QuantityPerUnit;
            nmFiyat.Value = guncellenecek.Price;
            nmStok.Value = guncellenecek.UnitInStock;

            ProductList();
        }

        private void btnKaydet_Click(object sender, System.EventArgs e)
        {
            Product product = new Product();
            product.Name = txtUrunAdi.Text;
            product.CategoryId = (int)cmbKategoriler.SelectedValue;
            product.QuantityPerUnit = txtBirim.Text;
            product.UnitInStock = (short)nmStok.Value;
            product.Price = nmFiyat.Value;
            product.Status = NTier.Model.Enum.Status.Active;


            ResultModel<Product> result = _productRepository.Insert(product);
            if (result.IsSuccess)
            {
                ProductList();
                Temizle();
            }
            MessageBox.Show(result.Message);
        }


        public void Temizle()
        {
            txtUrunAdi.Text = string.Empty;
            txtBirim.Text = string.Empty;
            nmStok.Value = 0;
            nmFiyat.Value = 0;
        }

        private void btnTemizle_Click(object sender, System.EventArgs e)
        {
            Temizle();
        }

        private void btnGuncelle_Click(object sender, System.EventArgs e)
        {
            if (lstUrunler.Items.Count <= 0) return;

            guncellenecek.Name = txtUrunAdi.Text;
            guncellenecek.CategoryId = (int)cmbKategoriler.SelectedValue;
            guncellenecek.QuantityPerUnit = txtBirim.Text;
            guncellenecek.UnitInStock = (short)nmStok.Value;
            guncellenecek.Price = nmFiyat.Value;
            guncellenecek.Status = NTier.Model.Enum.Status.Updated;

            _productRepository.Update(guncellenecek);
            Temizle();
            ProductList();
        }

        private void btnSil_Click(object sender, System.EventArgs e)
        {
            if (lstUrunler.SelectedItems.Count <= 0) return;
            guncellenecek = _productRepository.SelectById(((Product)lstUrunler.SelectedItems[0].Tag).Id);
            guncellenecek.Status = Status.Deleted;
            _productRepository.Update(guncellenecek); // Pasife çekme gerçek silme için ise cr.delete çağırılır.
            ProductList();
            Temizle();
        }
    }
}
