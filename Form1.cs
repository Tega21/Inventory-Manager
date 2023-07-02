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
        /*Commenting out this line to start the change from using the DataTable to a list
         *DataTable inventory = new DataTable();
         */
        BindingList<InventoryItem> inventory = new BindingList<InventoryItem>();

        private LoginForm loginForm;
        public Form1()
        {
            InitializeComponent();
            loginForm = new LoginForm(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshInventoryGridView();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            // Saving all the text values and making them variables
            string id = idTxtBox.Text;
            string name = nameTxtBox.Text;
            string price = priceTxtBox.Text;
            string quantity = quantityTxtBox.Text;
            string category = (string)categoryBox.SelectedItem;



            /* Commenting this out and adding the code for the list so it can add to the
             * inventory list.
             * //Adding to datatable
            inventory.Rows.Add(id, name, price, quantity, category);
            */

            if (!decimal.TryParse(price, out var parsedPrice))
            {
                MessageBox.Show("Invalid price value. Please enter a valid decimal number.");
                return;
            }

            if (!int.TryParse(quantity, out var parsedQuantity))
            {
                MessageBox.Show("Invalid quantity value. Please enter a valid integer number.");
                return;
            }

            InventoryItem newItem = new InventoryItem(id, name, parsedPrice, parsedQuantity, category);
            inventory.Add(newItem);

            clearBtn_Click(sender, e);
            RefreshInventoryGridView();
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
                if (inventoryGridView.CurrentCell != null)
                {
                    int selectedRowIndex = inventoryGridView.CurrentCell.RowIndex;

                    // Check if a valid row is selected
                    if (selectedRowIndex >= 0 && selectedRowIndex < inventory.Count)
                    {
                        inventory.RemoveAt(selectedRowIndex);
                        RefreshInventoryGridView();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err);
            }

        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            // Hide the inventory manager when you logout
            this.Hide();

            // Make sure the login form fields are clear
            if (loginForm != null)
            {
                loginForm.ClearCredentials();

                //Show the login form again
                loginForm.Show();
            }
        }

        private void inventoryGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /* Changing this code so that way the event handler is adapted to the change from DataTable to List
                 * InventoryItem selectedItem = inventory[inv]
                 *idTxtBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[0].ToString();
                 *nameTxtBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[1].ToString();
                 *priceTxtBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[2].ToString();
                 *quantityTxtBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[3].ToString();
                 *String itemToLookFor = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[4].ToString();
                 *categoryBox.SelectedIndex = categoryBox.Items.IndexOf(itemToLookFor);
                 */
            try
            {                          
                   

                int selectedRowIndex = e.RowIndex;

                // Check if a row is selected
                if (selectedRowIndex >= 0 && selectedRowIndex < inventory.Count)
                {                    
                    DataGridViewRow selectedRow = inventoryGridView.Rows[selectedRowIndex];

                    // Retrieve cell values from the selected row

                    idTxtBox.Text = selectedRow.Cells[0].Value.ToString();
                    nameTxtBox.Text = selectedRow.Cells[1].Value.ToString();
                    priceTxtBox.Text = selectedRow.Cells[2].Value.ToString();
                    quantityTxtBox.Text = selectedRow.Cells[3].Value.ToString();
                    categoryBox.SelectedItem = selectedRow.Cells[4].Value.ToString();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occurred: " + err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {                    
            string searchItems = searchTxtBox.Text.Trim();
             if (string.IsNullOrEmpty(searchItems))
            {
                RefreshInventoryGridView(); // Reset grid view to display all items if search bar is empty
                return;
            }

            /* This is getting commented out so I can update it so it will search through the list instead
             * of it searching through the datatable
             * 
            // List to store matching rows
            List<DataTable> matchingRows = new List<DataTable>();

            // Loop through the inventory in DataTable to find the match
            foreach (DataRow row in inventory.Rows)
            {
                string productName = row["Name"].ToString();
                string id = row["ID"].ToString();                

                if (productName.IndexOf(searchItems, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    id.IndexOf(searchItems, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    matchingRows.Add(row);
                }

                if (matchingRows.Count > 0)
                {
                    // Populate text fields and category with matching values
                    idTxtBox.Text = id;
                    nameTxtBox.Text = productName;
                    priceTxtBox.Text = row["Price"].ToString();
                    quantityTxtBox.Text = row["Quantity"].ToString();

                    string categoryLook = row["Category"].ToString();
                    categoryBox.SelectedIndex = categoryBox.Items.IndexOf(categoryLook);

                    return; // Exits method if it has found a match
                }
            } */

            List<InventoryItem> matchingItems = new List<InventoryItem>();
            foreach (InventoryItem item in inventory)
            {
                if (item.ID.ToLower().Contains(searchItems.ToLower()) || item.Name.ToLower().Contains(searchItems.ToLower()))
                {
                    matchingItems.Add(item);
                }
            }
            UpdateInventoryGridView(matchingItems);
        }

        private void RefreshInventoryGridView()
        {
            inventoryGridView.DataSource = null;
            inventoryGridView.DataSource = inventory;
        }

        private void UpdateInventoryGridView(List<InventoryItem> source)
        {
            inventoryGridView.SuspendLayout();
            inventoryGridView.DataSource = null;
            inventoryGridView.DataSource = source;
            inventoryGridView.ClearSelection();
            inventoryGridView.ResumeLayout();
        }
    }

   
}

