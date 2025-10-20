namespace Recycling.Presentation
{
    public partial class Login : Form
    {
        public string username;
        public string passwordHash;
        public string email;
        public DateTime createdAt;

        public Login()
        {
            InitializeComponent();
        }

        private void bttnLogin_Click(object sender, EventArgs e)
        {
            LoginData loginData = new LoginData();
            loginData.LoginUser(username, passwordHash);

            Users users = new Users();
            users.Show();
            this.Hide();

            if(radioAdmin.Checked)
            {
                
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
