namespace Recycling.Presentation
{
    public partial class Users : Form
    {
        public int itemID;
        public int userID;
        public string itemType;
        public double weight;
        public string condition;
        public DateTime createdAt;
        public DateTime updatedAt;
        public string imagePath;

        public Users()
        {
            InitializeComponent();
        }

        RecycledItemsData recycled = new RecycledItemsData();

        private DataGridView dt;

        private void button1_Click(object sender, EventArgs e)
        {
            //dt = new DataGridView();

            recycled.addItem(itemID, userID, itemType, weight, condition, imagePath);

            dt.DataSource = recycled;
            recycledItemsView.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //dt = new DataGridView();

            recycled.updateItem(itemID, itemType, weight, condition);

            recycledItemsView.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //dt = new DataGridView();
            
            recycled.removeItem(itemID, userID);

            recycledItemsView.DataSource = dt;
        }

        private void Users_Load(object sender, EventArgs e)
        {
           // dt = new DataGridView();
            recycled.viewAll();

            recycledItemsView.DataSource= dt;

        }

        private void txtItemType_TextChanged(object sender, EventArgs e)
        {
            if (txtItemType.Text != "Plastic" || txtItemType.Text != "Paper" || txtItemType.Text != "Glass" || txtItemType.Text == "Metal")
            {
                MessageBox.Show("Invalid Input. Enter Paper, Plastic, Glass or Metal");

                if (txtItemType.Text == "Plastic")
                {
                    itemType = (string)txtItemType.Text;
                }

                else if (txtItemType.Text == "Metal")
                {
                    itemType = (string)txtItemType.Text;
                }

                else if (txtItemType.Text == "Paper")
                {
                    itemType = (string)txtItemType.Text;
                }

                else if (txtItemType.Text == "Glass")
                {
                    itemType = (string)txtItemType.Text;
                }
            }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            int weight = Convert.ToInt32(txtWeight.Text);

            //check if text is a number

        }

        private void txtCondition_TextChanged(object sender, EventArgs e)
        {
            if (txtCondition.Text != "Good" || txtCondition.Text != "Okay" || txtCondition.Text != "Damaged")
            {
                MessageBox.Show("Invalid Input. Enter Good, Okay or Damaged");

                if (txtCondition.Text == "Good")
                {
                    condition = (string)txtCondition.Text;
                }

                else if (txtCondition.Text == "Okay")
                {
                    condition = (string)txtCondition.Text;
                }

                else if (txtCondition.Text == "Damaged")
                {
                    condition = (string)txtCondition.Text;
                }
            }
        }

        private void bttnView_Click(object sender, EventArgs e)
        {
            dt = new DataGridView();
            recycled.viewAll();

            recycledItemsView.DataSource = dt;
        }
    }
}
