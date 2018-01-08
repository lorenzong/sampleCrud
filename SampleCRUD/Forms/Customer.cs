using SampleCRUD.Class;
using SampleCRUD.DataModel;
using System.Windows.Forms;

namespace SampleCRUD.Forms
{
    public partial class Customer : Form
    {
        #region Private fields

        private CustomerDataModel customer = new CustomerDataModel();
        private bool isAdd = false;

        #endregion

        public Customer(bool isAdd, int customerId)
        {
            InitializeComponent();
            this.isAdd = isAdd;

            if (!isAdd)
            {
                Text = "Edit Customer ~~";
                customer = SQLCustomer.Instance.GetCustomerData(customerId);

                if(customer != null)
                {
                    txtFirstName.Text = customer.FirstName;
                    txtLastName.Text = customer.LastName;
                    txtContactNumber.Text = customer.ContactNumber;
                    txtAddress.Text = customer.Address;
                }
            }
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (!ValidateFields())
            {
                if (isAdd)
                {
                    if (SQLCustomer.Instance.CreateNewCustomer(GetCustomer()))
                    {
                        MessageBox.Show("Successfully added. ");
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong. ");
                    }
                }
                else
                {
                    if (SQLCustomer.Instance.UpdateCustomerDetail(GetUpdatedCustomerDetail()))
                    {
                        MessageBox.Show("Successfully updated. ");
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong. ");
                    }
                }
            }
        }

        private bool ValidateFields()
        {
            bool isError = false;
            string error = string.Empty;

            if (txtFirstName.Text == null || txtFirstName.Text == string.Empty)
            {
                isError = true;
                error += "First name should not be empty. ";
            }

            if(txtLastName.Text == null || txtLastName.Text == string.Empty)
            {
                isError = true;
                error += "Last name should not be empty. ";
            }

            if (txtAddress.Text == null || txtAddress.Text == string.Empty)
            {
                isError = true;
                error += "Address should not be empty. ";
            }

            if (txtContactNumber.Text == null || txtContactNumber.Text == string.Empty)
            {
                isError = true;
                error += "Contact number should not be empty. ";
            }

            if (error != string.Empty)
            {
                MessageBox.Show(error);
            }

            return isError;
        }

        private CustomerDataModel GetCustomer()
        {
            customer = new CustomerDataModel()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Address = txtAddress.Text,
                ContactNumber = txtContactNumber.Text
            };

            return customer;
        }

        private CustomerDataModel GetUpdatedCustomerDetail()
        {
            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastName.Text;
            customer.Address = txtAddress.Text;
            customer.ContactNumber = txtContactNumber.Text;

            return customer;
        }
    }
}
