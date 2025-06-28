using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Kursach
{
    public partial class Form1 : Form
    {
        Class1 database = new Class1();
        private bool darkMode = false;

        public Form1()
        {
            InitializeComponent();
            ApplyTheme();
            SetupControls();
        }

        private void SetupControls()
        {
            // Настройка плейсхолдеров
            txtUsername.Enter += (s, e) => { if (txtUsername.Text == "Логин") txtUsername.Text = ""; };
            txtUsername.Leave += (s, e) => { if (string.IsNullOrEmpty(txtUsername.Text)) txtUsername.Text = "Логин"; };

            txtPassword.Enter += (s, e) => { if (txtPassword.Text == "Пароль") { txtPassword.Text = ""; txtPassword.UseSystemPasswordChar = true; } };
            txtPassword.Leave += (s, e) => { if (string.IsNullOrEmpty(txtPassword.Text)) { txtPassword.Text = "Пароль"; txtPassword.UseSystemPasswordChar = false; } };
 }

        private void ApplyTheme()
        {
            if (darkMode)
            {
                this.BackColor = Color.FromArgb(32, 32, 32);
                this.ForeColor = Color.White;
                panel1.BackColor = Color.FromArgb(48, 48, 48);
                btnTheme.Text = "🌞";

                foreach (Control ctrl in panel1.Controls)
                {
                    if (ctrl is TextBox txt)
                    {
                        txt.BackColor = Color.FromArgb(64, 64, 64);
                        txt.ForeColor = Color.White;
                        txt.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (ctrl is Button btn && btn != btnTheme)
                    {
                        btn.BackColor = Color.FromArgb(0, 120, 215);
                        btn.ForeColor = Color.White;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(177, 70, 194);
                        btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(125, 49, 137);
                    }
                }
            }
            else
            {
                // Светлая тема
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
                panel1.BackColor = SystemColors.ControlLight;
                btnTheme.Text = "🌙";

                foreach (Control ctrl in panel1.Controls)
                {
                    if (ctrl is TextBox txt)
                    {
                        txt.BackColor = SystemColors.Window;
                        txt.ForeColor = SystemColors.WindowText;
                        txt.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (ctrl is Button btn && btn != btnTheme)
                    {
                        btn.BackColor = SystemColors.ControlLight;
                        btn.ForeColor = SystemColors.ControlText;
                        btn.FlatStyle = FlatStyle.Standard;
                        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 157, 240);
                        btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(125, 49, 137);
                    }
                }
            }
        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            darkMode = !darkMode;
            ApplyTheme();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateRegistration())
            {
                try
                {
                    database.openConnection();
                    string query = "INSERT INTO Авторизация (Логин, Пароль, Роль) VALUES (@login, @password, @role)";
                    SqlCommand cmd = new SqlCommand(query, database.GetConnection());
                    cmd.Parameters.AddWithValue("@login", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@role", "Клиент"); // По умолчанию регистрируем как клиента

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show($"Аккаунт успешно создан! Ваша роль: \"Клиент\"", "Успех!");
                        ClearRegistrationFields();

                        // После регистрации сразу входим
                        Form3 mainForm = new Form3(txtUsername.Text, "Клиент");
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Аккаунт не создан!", "Ошибка");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                }
                finally
                {
                    database.closeConnection();
                }
            }
        }


        private bool ValidateRegistration()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text == "Логин")
            {
                MessageBox.Show("Введите логин!", "Ошибка");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text == "Пароль")
            {
                MessageBox.Show("Введите пароль!", "Ошибка");
                return false;
            }

            if (UserExists())
            {
                return false;
            }

            return true;
        }

        private bool UserExists()
        {
            try
            {
                database.openConnection();
                string query = "SELECT COUNT(*) FROM Авторизация WHERE Логин=@login";
                SqlCommand cmd = new SqlCommand(query, database.GetConnection());
                cmd.Parameters.AddWithValue("@login",   txtUsername.Text);

                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    MessageBox.Show("Пользователь уже существует!", "Ошибка");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка проверки: {ex.Message}", "Ошибка");
                return true;
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void ClearRegistrationFields()
        {
            txtUsername.Text = "Логин";
            txtPassword.Text = "Пароль";
            txtPassword.UseSystemPasswordChar = false;
        }

        private void btnSwitchToLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 loginForm = new Form1();
            loginForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Инициализация плейсхолдеров
            txtUsername.Text = "Логин";
            txtPassword.Text = "Пароль";
            txtPassword.UseSystemPasswordChar = false;
        }

        private void btnGoToLogin_Click(object sender, EventArgs e)
        {
            Form2 loginForm = new Form2();
            loginForm.Show();
            this.Hide();
        }
    }
}