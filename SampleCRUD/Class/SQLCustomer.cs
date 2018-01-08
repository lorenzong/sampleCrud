using SampleCRUD.DataModel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SampleCRUD.Class
{
    public class SQLCustomer
    {
        #region Private fields

        private string connectionString = ConfigurationManager.ConnectionStrings["localDb"].ConnectionString;
        private string selectAllCustomer = "SELECT * FROM tbl_customer";
        private string selectCustomerWithId = "SELECT * FROM tbl_customer where id = @val1";
        private string deleteCustomerWithId = "DELETE FROM tbl_customer where id = @val1";
        private string insertNewCustomer = "INSERT INTO tbl_customer (first_name, last_name, address, contact_number) VALUES (@val1, @val2, @val3, @val4)";
        private string updateCustomerDetail = "UPDATE tbl_customer SET first_name=@val1, last_name=@val2, address=@val3, contact_number=@val4 where id=@val5";
        private static SQLCustomer instance;

        #endregion


        #region Public properties

        public SqlConnection SqlConn;

        public static SQLCustomer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SQLCustomer();
                }

                return instance;
            }
        }

        #endregion

        public SQLCustomer()
        {

        }

        #region Methods

        public void Initialize()
        {
            SqlConn = new SqlConnection(connectionString);
        }

        public DataTable GetAllCustomerData()
        {
            DataTable dataTable = new DataTable();

            using (SqlConn)
            {
                SqlConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(selectAllCustomer, SqlConn))
                {
                    try
                    {
                        dataTable.Load(sqlCommand.ExecuteReader());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error message: {ex.Message}");
                    }
                }
            }

            return dataTable;
        }

        public bool CreateNewCustomer(CustomerDataModel customer)
        {
            bool isSuccess = false;

            Initialize();

            using (SqlConn)
            {
                SqlConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = SqlConn;
                    sqlCommand.CommandText = insertNewCustomer;

                    sqlCommand.Parameters.AddWithValue("@val1", customer.FirstName);
                    sqlCommand.Parameters.AddWithValue("@val2", customer.LastName);
                    sqlCommand.Parameters.AddWithValue("@val3", customer.Address);
                    sqlCommand.Parameters.AddWithValue("@val4", customer.ContactNumber);

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                    }
                }
            }

            return isSuccess;
        }

        public bool UpdateCustomerDetail(CustomerDataModel customer)
        {
            bool isSuccess = false;

            Initialize();

            using (SqlConn)
            {
                SqlConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = SqlConn;
                    sqlCommand.CommandText = updateCustomerDetail;

                    sqlCommand.Parameters.AddWithValue("@val1", customer.FirstName);
                    sqlCommand.Parameters.AddWithValue("@val2", customer.LastName);
                    sqlCommand.Parameters.AddWithValue("@val3", customer.Address);
                    sqlCommand.Parameters.AddWithValue("@val4", customer.ContactNumber);
                    sqlCommand.Parameters.AddWithValue("@val5", customer.id);

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                    }
                }
            }

            return isSuccess;
        }

        public CustomerDataModel GetCustomerData(int id)
        {
            DataTable dataTable = new DataTable();
            CustomerDataModel customer = new CustomerDataModel();

            Initialize();

            using (SqlConn)
            {
                SqlConn.Open();
                
                using (SqlCommand sqlCommand = new SqlCommand(selectCustomerWithId, SqlConn))
                {
                    try
                    {
                        sqlCommand.Parameters.AddWithValue("@val1", id);
                        dataTable.Load(sqlCommand.ExecuteReader());

                        foreach(DataRow row in dataTable.Rows)
                        {
                            customer.FirstName = row["first_name"].ToString();
                            customer.LastName = row["last_name"].ToString();
                            customer.Address = row["address"].ToString();
                            customer.ContactNumber = row["contact_number"].ToString();
                            customer.id = id;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error message: {ex.Message}");
                    }
                }
            }

            return customer;
        }

        public bool DeleteCustomerData(int id)
        {
            bool isSuccess = false;

            Initialize();

            using (SqlConn)
            {
                SqlConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = SqlConn;
                    sqlCommand.CommandText = deleteCustomerWithId;

                    sqlCommand.Parameters.AddWithValue("@val1", id);

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                    }
                }
            }

            return isSuccess;
        }

        #endregion
    }
}
