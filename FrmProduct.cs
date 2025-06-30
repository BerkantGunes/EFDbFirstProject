using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFDbFirstProduct2
{
    public partial class FrmProduct: Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        Db2Project20Entities1 db = new Db2Project20Entities1();

        void ProductList()
        {
            DGVProduct.DataSource = db.TblProduct.ToList();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            ProductList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TblProduct tblProduct = new TblProduct();
            tblProduct.ProductPrice = decimal.Parse(txtProductPrice.Text);
            tblProduct.ProductStock = int.Parse(txtProductStock.Text);
            tblProduct.ProductName = txtProductName.Text;
            tblProduct.CategoryId = int.Parse(cmbCat.SelectedValue.ToString());
            db.TblProduct.Add(tblProduct);
            db.SaveChanges();
            ProductList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var value = db.TblProduct.Find(int.Parse(txtProductId.Text));
            db.TblProduct.Remove(value);
            db.SaveChanges();
            ProductList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var value = db.TblProduct.Find(int.Parse(txtProductId.Text));
            value.ProductPrice = decimal.Parse(txtProductPrice.Text);
            value.ProductName = txtProductName.Text;
            value.ProductStock = int.Parse(txtProductStock.Text);
            value.CategoryId = int.Parse(cmbCat.SelectedValue.ToString());
            db.SaveChanges();
            ProductList();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            var values = db.TblCategory.ToList();
            cmbCat.DisplayMember = "CategoryName"; //cmbox da gozukecek kisim
            cmbCat.ValueMember = "CategoryId"; // categoryleri listelerken id ye gore listeleyecek
            cmbCat.DataSource = values;
            
        }

        private void btnListCat_Click(object sender, EventArgs e)
        {
            var values = db.TblProduct.Join(db.TblCategory,
                product => product.CategoryId,
                category => category.CategoryId,
                (product, category) => new
                { // product ve category tablolarini birlestirdik. Her iki tablodaki categoryId ile yaptık bunu.
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductStock = product.ProductStock,
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                }) // TblCategory Yerine CategoryName geldi!
                .ToList();
            DGVProduct.DataSource = values;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values = db.TblProduct.Where(x => x.ProductName == txtProductName.Text).ToList();
            DGVProduct.DataSource = values;
        }
    } 
}
