using NTier.BLL.RepositoryImplementation;
using NTier.Model.Entity;
using NTier.Model.Enum;
using NTier.Model.Helpers;
using System;
using System.Windows.Forms;

namespace NTier_Code_First
{
    public partial class frmKategoriler : Form
    {
        private readonly CategoryRepository _categoryRepository;
        public frmKategoriler()
        {
            InitializeComponent();
            _categoryRepository = new CategoryRepository();
        }

        private void frmKategoriler_Load(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void CategoryList()
        {
            lstKategoriler.Items.Clear();
            foreach (var item in _categoryRepository.SearchList(x => x.Status == Status.Active || x.Status == Status.Updated))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.Id.ToString();
                lvi.SubItems.Add(item.Name);
                lvi.SubItems.Add(item.Description);
                lvi.Tag = item;
                lstKategoriler.Items.Add(lvi);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Name = txtKategoriAdi.Text;
            category.Description = txtAciklama.Text;
            category.Status = NTier.Model.Enum.Status.Active;

            ResultModel<Category> result = _categoryRepository.Insert(category);
            if (result.IsSuccess)
            {
                txtAciklama.Text = string.Empty;
                txtAciklama.Text = string.Empty;
                CategoryList();
            }
            MessageBox.Show(result.Message);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (lstKategoriler.Items.Count <= 0) return;
            guncellenecek.Name = txtKategoriAdi.Text;
            guncellenecek.Description = txtAciklama.Text;
            guncellenecek.Status = Status.Updated;

            _categoryRepository.Update(guncellenecek);
            txtAciklama.Text = string.Empty;
            txtKategoriAdi.Text = string.Empty;
            CategoryList();
        }

        Category guncellenecek;
     
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAciklama.Text = string.Empty;
            txtKategoriAdi.Text = string.Empty;
        }

        private void btnUrunler_Click(object sender, EventArgs e)
        {
            frmUrunler frm = new frmUrunler();
            frm.Show();
            this.Hide();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lstKategoriler.SelectedItems.Count <=0) return;
            guncellenecek = new Category();
            guncellenecek = _categoryRepository.SelectById(((Category)lstKategoriler.SelectedItems[0].Tag).Id);
            //_categoryRepository.Delete(((Category)lstKategoriler.SelectedItems[0].Tag).Id);
            guncellenecek.Status = Status.Deleted;
            _categoryRepository.Update(guncellenecek);
            CategoryList();

            txtAciklama.Text = string.Empty;
            txtKategoriAdi.Text = string.Empty;
        }

        private void lstKategoriler_DoubleClick_1(object sender, EventArgs e)
        {
            if (lstKategoriler.SelectedItems.Count <= 0) return;
            guncellenecek = new Category();
            guncellenecek = _categoryRepository.SelectById(((Category)lstKategoriler.SelectedItems[0].Tag).Id);
            txtAciklama.Text = guncellenecek.Description;
            txtKategoriAdi.Text = guncellenecek.Name;
            CategoryList();
        }
    }
}
