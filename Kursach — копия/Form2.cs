using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace Kursach
{
    public partial class Form2 : Form
    {
        Class1 database = new Class1();
        private bool darkMode = false;

        public Form2()
        {
            InitializeComponent();
            ApplyTheme();
            SetupPlaceholders();
        }

        private void SetupPlaceholders()
        {
            // Настройка плейсхолдеров
            txtUsername.Enter += (s, e) => { if (txtUsername.Text == "Логин") txtUsername.Text = ""; };
            txtUsername.Leave += (s, e) => { if (string.IsNullOrEmpty(txtUsername.Text)) txtUsername.Text = "Логин"; };
            txtPassword.Enter += (s, e) => { if (txtPassword.Text == "Пароль") { txtPassword.Text = ""; txtPassword.UseSystemPasswordChar = true; } };
            txtPassword.Leave += (s, e) => { if (string.IsNullOrEmpty(txtPassword.Text)) { txtPassword.Text = "Пароль"; txtPassword.UseSystemPasswordChar = false; } };
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || txtUsername.Text == "Логин" ||
                string.IsNullOrEmpty(txtPassword.Text) || txtPassword.Text == "Пароль")
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                database.openConnection();
                string query = "SELECT Роль FROM Авторизация WHERE Логин=@login AND Пароль=@password";
                SqlCommand cmd = new SqlCommand(query, database.GetConnection());
                cmd.Parameters.AddWithValue("@login", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string role = result.ToString();
                    MessageBox.Show($"Вход выполнен! Ваша роль: {role}", "Успех",
                        MessageBoxButtons.OK,
                        darkMode ? MessageBoxIcon.Information : MessageBoxIcon.None);

                    // Передаем логин и роль в основную форму
                    Form3 mainForm = new Form3(txtUsername.Text, role);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Form1 registerForm = new Form1();
            registerForm.Show();
            this.Hide();
        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            darkMode = !darkMode;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (darkMode)
            {
                // Тёмная тема
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

        private void Form2_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "Логин";
            txtPassword.Text = "Пароль";
            txtPassword.UseSystemPasswordChar = false;
        }

        private void btnGoToLogin_Click(object sender, EventArgs e)
        {
            Form1 registerForm = new Form1();
            registerForm.Show();
            this.Hide();
        }
    }
}