using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts_Presentation
{
    public partial class ContactManager : Form
    {



        public ContactManager()
        {
            InitializeComponent();
        }

        private void _RefreshContactList()
        {
            dgvAllContacts.DataSource = clsContacts.GetAllContacts();
        }
        private void ContactManager_Load(object sender, EventArgs e)
        {
            _RefreshContactList();

        }

        private void editToolStripeMenuItem_Click(object sender, EventArgs e)
        {
            AddEditContact frm = new AddEditContact((int)dgvAllContacts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           if ( MessageBox.Show("Are you sure that you want to delete contact [ " + dgvAllContacts.CurrentRow.Cells[0].Value 
                + " ]" , "Confirm Delete Message " , MessageBoxButtons.OKCancel) == DialogResult.OK)

           {
               if( clsContacts.DeleteContact((int)dgvAllContacts.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully");
                    _RefreshContactList();
                }
               else
                {
                    MessageBox.Show("Contact Is Not Deleted");
                }
           }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditContact frm = new AddEditContact(-1);
            frm.ShowDialog();
            _RefreshContactList();
        }


    }
}
