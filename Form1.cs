using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Manager
{
    public partial class Form1 : Form
    {
        DataTable inventory = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inventory.Columns.Add("ID");
            inventory.Columns.Add("Name");
            inventory.Columns.Add("Price");
            inventory.Columns.Add("Quantity");
            inventory.Columns.Add("Category");

            inventoryGridView.DataSource = inventory;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            // Saving all the text values and making them variables
            String id = idTxtBox.Text;
            String name = nameTxtBox.Text;
            String price = priceTxtBox.Text;
            String quantity = quantityTxtBox.Text;
            String category = (string)categoryBox.SelectedItem;

            //Adding to datatable
            inventory.Rows.Add(id, name, price, quantity, category);

            // clearing fields after save
            clearBtn_Click(sender, e);
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            idTxtBox.Text = "";
            nameTxtBox.Text = "";
            priceTxtBox.Text = "";
            quantityTxtBox.Text = "";
            categoryBox.SelectedIndex = -1;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                inventory.Rows[inventoryGridView.CurrentCell.RowIndex].Delete();
            }
            catch (Exception err)
            {
                Console.WriteLine("Error: " + err);
            }

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inventoryGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                idTxtBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[0].ToString();
                nameTxtBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[1].ToString();
                priceTxtBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[2].ToString();
                quantityTxtBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[3].ToString();

                String itemToLookFor = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[4].ToString();
                categoryBox.SelectedIndex = categoryBox.Items.IndexOf(itemToLookFor);
            }
            catch (Exception err)
            {
                Console.WriteLine("There has been an error: " + err);
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchItems = searchTxtBox.Text;

            // Clear text fields
            clearBtn_Click(sender, e);

            // Loop through the inventory in DataTable to find the match
            foreach (DataRow row in inventory.Rows)
            {
                string productName = row["Name"].ToString();
                string id = row["Id"].ToString();

                if (productName.IndexOf(searchItems, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    id.IndexOf(searchItems, StringComparison.OrdinalIgnoreCase) >= 0) ;
                {
                    // Populate text fields and category with matching values
                    idTxtBox.Text = row["ID"].ToString();
                    nameTxtBox.Text = row["Name"].ToString();
                    priceTxtBox.Text = row["Price"].ToString();
                    quantityTxtBox.Text = row["Quantity"].ToString();

                    string categoryLook = row["Category"].ToString();
                    categoryBox.SelectedIndex = categoryBox.Items.IndexOf(categoryLook);

                    return; // Exits method if it has found a match
                }
            }

            MessageBox.Show("No Match found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
