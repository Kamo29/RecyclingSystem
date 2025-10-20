namespace Recycling.Data
{
    internal class LoginData
    {
        string conn = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=RecyclingManagement;Integrated Security=True;Encrypt=False";
        public void RegisterUser(string username, string passwordHash, string email, string role, DateTime createdAt)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string insertUserQuery = @"INSERT INTO Users (Username, PasswordHash, Email, Role)
                                         VALUES (@Username, @PasswordHash, @Email, @Role)";

                using (SqlCommand command = new SqlCommand(insertUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", HashPassword(passwordHash));
                    command.Parameters.AddWithValue("@Email", email);

                    if(role == "User")
                    {
                        command.Parameters.AddWithValue("@Role", "User");
                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Role", "Admin");
                    }
    

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("User Registered Successfully");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        public bool LoginUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string selectUserQuery = @"
                SELECT COUNT(*) 
                FROM Users 
                WHERE Username = @username AND PasswordHash = @PasswordHash;";

                using (SqlCommand command = new SqlCommand(selectUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", HashPassword(password));

                    try
                    {
                        int userCount = (int)command.ExecuteScalar();
                        return userCount > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }


    }
}
