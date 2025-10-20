namespace RecyclingSENEXAM.Data
{
    internal class DataConn
    {
        private readonly string connection = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=RecyclingManagement;Integrated Security=True;Encrypt=False";

        public SqlConnection getConn()
        {
            return new SqlConnection(connection);
        }
    }
}
