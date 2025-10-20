namespace Recycling.Presentation
{
    public partial class Register : Form
    {
        public string username;
        public string passwordHash;
        public string email;
        public DateTime createdAt;
        public string role;

        public Register()
        {
            InitializeComponent();
        }

        private void bttnRegister_Click(object sender, EventArgs e)
        {
            LoginData loginData = new LoginData();

            loginData.RegisterUser(username, passwordHash, email, role, createdAt);

            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
