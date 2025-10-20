namespace Recycling.Data
{
    internal class RecycledItemsData
    {
        private DataConn db = new DataConn();

        public void addItem(int itemID, int userID, string itemType, double weighht, string condition, string imagePath)
        {
            using (SqlConnection conn = db.getConn())
            {
                conn.Open();
                string query = "INSERT INTO RecyclableItems (UserID, ItemType, Weight, Condition, ImagePath) " +
                               "VALUES (@UserID, @ItemType, @Weight, @Condition, @ImagePath)" +
                               "DECLARE @NewItemID INT = SCOPE_IDENTITY();\r\n\r\n" +
                               "INSERT INTO InventoryActions (UserID, ItemID, ActionType)\r\n    " +
                               "VALUES (@UserID, @NewItemID, 'Create')";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID );
                cmd.Parameters.AddWithValue("@ItemType", itemType);
                cmd.Parameters.AddWithValue("@Weight", weighht);
                cmd.Parameters.AddWithValue("@Condition", condition);
                cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@NewItemID", itemID);
                cmd.Parameters.AddWithValue("", "Create");
                cmd.ExecuteNonQuery();
            }
        }

        public void removeItem(int itemID, int userID)
        {
            using (SqlConnection conn = db.getConn())
            {
                conn.Open();
                string query = "SELECT @UserID = UserID\r\n    " +
                               "FROM RecyclableItems\r\n    " +
                               "WHERE ItemID = @ItemID;\r\n\r\n   " +
                               "DELETE FROM RecyclableItems\r\n    " +
                               "WHERE ItemID = @ItemID;\r\n\r\n    " +
                               "INSERT INTO InventoryActions (UserID, ItemID, ActionType)\r\n    " +
                               "VALUES (@UserID, @ItemID, 'Delete');";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemID", itemID);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("", "Delete");
                cmd.ExecuteNonQuery();
            }
        }

        public void updateItem(int itemID, string itemType, double weighht, string condition)
        {
            using (SqlConnection conn = db.getConn())
            {
                conn.Open();
                string query = "UPDATE RecyclableItems\r\n    " +
                               "SET \r\n        " +
                               "ItemType = @ItemType,\r\n        " +
                               "Weight = @Weight,\r\n       " +
                               "Condition = @Condition,\r\n        " +
                               "ImagePath = @ImagePath,\r\n        " +
                               "UpdatedAt = GETDATE()\r\n   " +
                               "WHERE ItemID = @ItemID;\r\n\r\n    " +
                               "INSERT INTO InventoryActions (UserID, ItemID, ActionType)\r\n    " +
                               "SELECT UserID, @ItemID, 'Update'\r\n    " +
                               "FROM RecyclableItems\r\n    " +
                               "WHERE ItemID = @ItemID;\r\n";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemID", itemID);
                cmd.Parameters.AddWithValue("@ItemType", itemType);
                cmd.Parameters.AddWithValue("@Weight", weighht);
                cmd.Parameters.AddWithValue("@Condition", condition);
                cmd.Parameters.AddWithValue("", "Update");
                cmd.ExecuteNonQuery();
            }
        }

        public void viewAll()
        {
            using (SqlConnection conn = db.getConn())
            {
                conn.Open();
                string query = "CREATE VIEW vw_UserInventory AS " +
                               "SELECT \\r\\n    " +
                               "u.Username,\\r\\n    " +
                               "ri.ItemID,\\r\\n   " +
                               " ri.ItemType,\\r\\n   " +
                               " ri.Weight,\\r\\n    " +
                               "ri.Condition,\\r\\n   " +
                               " ri.CreatedAt,\\r\\n   " +
                               " ri.UpdatedAt\\r\\n " +
                               "FROM RecyclableItems ri\\r\\n" +
                               "INNER JOIN Users u ON ri.UserID = u.UserID;\"";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void load()
        {
            using (SqlConnection conn = db.getConn())
            {
                conn.Open();
                string query = "SELECT * FROM RecylableItems";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
