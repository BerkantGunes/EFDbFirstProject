﻿using System;
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
    public partial class FrmCategory: Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        Db2Project20Entities1 db = new Db2Project20Entities1();

        void CategoryList()
        {
            DGVCategory.DataSource = db.TblCategory.ToList();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            TblCategory tblCategory = new TblCategory();
            tblCategory.CategoryName = txtCategoryName.Text;
            db.TblCategory.Add(tblCategory);
            db.SaveChanges();
            CategoryList();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryId.Text);
            var value = db.TblCategory.Find(id);
            db.TblCategory.Remove(value);
            db.SaveChanges();
            CategoryList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryId.Text);
            var value = db.TblCategory.Find(id);
            value.CategoryName = txtCategoryName.Text;
            db.SaveChanges();
            CategoryList();
        }
    }
}
