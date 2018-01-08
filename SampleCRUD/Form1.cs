using SampleCRUD.Class;
using SampleCRUD.Forms;
using System;
using System.Windows.Forms;

namespace SampleCRUD
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeGridView();
        }


        private void InitializeGridView()
        {
            SQLCustomer.Instance.Initialize();

            dgvCustomer.DataSource = SQLCustomer.Instance.GetAllCustomerData();
            dgvCustomer.Columns["id"].Visible = false;
        }

        private void dgvCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvCustomer.CurrentRow.Index <= dgvCustomer.Rows.Count)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Customer customerDialog = new Customer(true, 0);
            customerDialog.ShowDialog();

            InitializeGridView();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Customer customerDialog = new Customer(false, Convert.ToInt32(dgvCustomer.Rows[dgvCustomer.CurrentRow.Index].Cells[0].Value.ToString()));
            customerDialog.ShowDialog();

            InitializeGridView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dResult = MessageBox.Show($"Are you sure you want to delete {dgvCustomer.Rows[dgvCustomer.CurrentRow.Index].Cells[1].Value.ToString()}?", "Delete Account ~~", MessageBoxButtons.YesNo);

            if(dResult == DialogResult.Yes)
            {
                if (SQLCustomer.Instance.DeleteCustomerData(Convert.ToInt32(dgvCustomer.Rows[dgvCustomer.CurrentRow.Index].Cells[0].Value.ToString())))
                {
                    MessageBox.Show("Successfully deleted. ");
                    InitializeGridView();
                }
                else
                {
                    MessageBox.Show("Something went wrong. ");
                }
            }
        }
    }
}
