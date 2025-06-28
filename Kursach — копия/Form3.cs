using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Kursach
{
    public partial class Form3 : Form
    {
        private bool darkMode = false;
        Class1 database = new Class1();
        int selectedRow;
        private string currentTable = ""; // Текущая выбранная таблица
        private string currentUserRole;
        private string currentUserLogin;

        public Form3(string login, string role)
        {
            InitializeComponent();
            currentUserLogin = login;
            currentUserRole = role;

            InitializeControls();
            ApplyTheme();
            UpdateUserInterface();
           }
        private void UpdateUserInterface()
        {
            // Пример: скрываем элементы управления для не-администраторов
            if (currentUserRole != "Администратор")
            {
                label10.Visible = false; // Скрываем кнопку "Аккаунты"
                pictureBox11.Visible = false;
            }
        }

        private void ApplyTheme()
        {
            if (darkMode)
            {
                // Тёмная тема
                this.BackColor = Color.FromArgb(102, 0, 102);
                this.ForeColor = Color.White;
                btnTheme.Text = "🌞";

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is System.Windows.Forms.TextBox txt)
                    {
                        txt.BackColor = Color.FromArgb(64, 64, 64);
                        txt.ForeColor = Color.White;
                        txt.BorderStyle = BorderStyle.FixedSingle;

                    }
                    
                    else if (ctrl is System.Windows.Forms.ComboBox box)
                    {
                        box.BackColor = Color.FromArgb(64, 64, 64);
                        box.ForeColor = Color.White;
                        box.FlatStyle = FlatStyle.Flat;
                    }
                   
                    else if (ctrl is System.Windows.Forms.Button btn && btn != btnTheme)
                    {
                        btn.BackColor = Color.FromArgb(64, 64, 64);
                        btn.ForeColor = Color.White;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 1;
                        btn.FlatAppearance.BorderColor = Color.White;
                        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(177, 70, 194);
                        btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(125, 49, 137);
                    }

                    else if (ctrl is System.Windows.Forms.Label lbl)
                    {
                        lbl.ForeColor = Color.White;
                    }
                    numericUpDown1.BackColor = Color.FromArgb(64,64,64);
                    numericUpDown1.ForeColor = Color.White;
                    numericUpDown2.BackColor = Color.FromArgb(64, 64, 64);
                    numericUpDown2.ForeColor = Color.White;
                    dataGridView1.ForeColor = Color.White;
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(181, 113, 192);
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0,0,102);
                    dataGridView1.BackgroundColor = Color.FromArgb(64, 64, 64);
                    dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(64,64,64);
                }
                pictureBox1.Image = Image.FromFile(@"C:\Users\Misterios\Downloads\icons8-поиск-32 (2).png");
                pictureBox2.Image = Image.FromFile(@"C:\Users\Misterios\Downloads\icons8-добавить-32 (5).png");
                pictureBox3.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-редактировать-32.png");
                pictureBox4.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-мусор-32.png");
                pictureBox5.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-идентифицированный-пользователь-мужчина-32.png");
                pictureBox6.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-товар-32 (1).png");
                pictureBox7.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-чек-32.png");
                pictureBox8.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-категория-32 (1).png");
                pictureBox9.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-склад-32 (1).png");
                pictureBox10.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-администратор-32.png");
                pictureBox11.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-веб-аккаунт-32.png");
                pictureBox12.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-microsoft-word-32 (1).png");
                pictureBox13.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-microsoft-excel-32 (1).png");
            }
            else
            {
                // Светлая тема
                this.BackColor = Color.FromArgb(71,252,77);
                this.ForeColor = Color.Black;
                btnTheme.Text = "🌙";

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is System.Windows.Forms.TextBox txt)
                    {
                        txt.BackColor = SystemColors.Window;
                        txt.ForeColor = SystemColors.WindowText;
                        txt.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (ctrl is System.Windows.Forms.ComboBox box)
                    {
                        box.BackColor = SystemColors.Window;
                        box.ForeColor = SystemColors.WindowText;
                        box.FlatStyle = FlatStyle.Standard;
                    }
                   
                    else if (ctrl is System.Windows.Forms.Button btn && btn != btnTheme)
                    {
                        btn.BackColor = SystemColors.ControlLight;
                        btn.ForeColor = SystemColors.ControlText;
                        btn.FlatStyle = FlatStyle.Standard;
                        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 157, 240);
                        btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(125, 49, 137);
                    }
                    else if (ctrl is System.Windows.Forms.Label lbl)
                    {
                        lbl.ForeColor = SystemColors.ControlText;
                    }
                    numericUpDown1.BackColor = SystemColors.Window;
                    numericUpDown1.ForeColor = Color.Black;
                    numericUpDown2.BackColor = Color.White;
                    numericUpDown2.ForeColor = Color.Black;
                    dataGridView1.ForeColor = Color.Black;
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(181, 113, 192);
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 102);
                    dataGridView1.BackgroundColor = Color.White;
                    dataGridView1.DefaultCellStyle.BackColor = Color.White;
                }
                pictureBox1.Image = Image.FromFile(@"C:\Users\Misterios\Downloads\icons8-поиск-32 (1).png");
                pictureBox2.Image = Image.FromFile(@"C:\Users\Misterios\Downloads\icons8-добавить-32.png");
                pictureBox3.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-edit-pencil-32.png");
                pictureBox4.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-мусор-32 (1).png");
                pictureBox5.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-collaborator-male-32.png");
                pictureBox6.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-товар-32.png");
                pictureBox7.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-cheque-32.png");
                pictureBox8.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-категория-32.png");
                pictureBox9.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-склад-32.png");
                pictureBox10.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-administrator-male-32.png");
                pictureBox11.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-web-account-32.png");
                pictureBox12.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-microsoft-word-32.png");
                pictureBox13.Image = Image.FromFile(@"C:\Users\Misterios\\Downloads\\icons8-microsoft-excel-32.png");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                selectedRow = e.RowIndex;
                FillFormFromSelectedRow();
            }
        }
        private void FillFormFromSelectedRow()
        {
            if (selectedRow < 0) return;

            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            ClearFormFields();

            switch (currentTable)
            {
                case "Клиент":
                    textBox2.Text = row.Cells["Фамилия"].Value?.ToString() ?? "";
                    textBox3.Text = row.Cells["Имя"].Value?.ToString() ?? "";
                    textBox4.Text = row.Cells["Отчество"].Value?.ToString() ?? "";
                    break;

                case "Товары":
                    textBox2.Text = row.Cells["Название"].Value?.ToString() ?? "";
                    textBox3.Text = row.Cells["Фирма"].Value?.ToString() ?? "";
                    comboBox2.SelectedItem = row.Cells["Категория"].Value?.ToString();
                    textBox5.Text = row.Cells["Цена"].Value?.ToString() ?? "";
                    textBox6.Text = row.Cells["Кол-во на складе"].Value?.ToString() ?? "";
                    comboBox3.SelectedItem = row.Cells["Склад"].Value?.ToString();
                    textBox7.Text = row.Cells["Описание"].Value?.ToString() ?? "";
                    break;

                case "Чек":
                    comboBox4.SelectedItem = row.Cells["Клиент"].Value?.ToString();
                    comboBox5.SelectedItem = row.Cells["Товар"].Value?.ToString();
                    comboBox6.SelectedItem = row.Cells["Сотрудник"].Value?.ToString();
                    if (row.Cells["Дата"].Value != null)
                        dateTimePicker1.Value = Convert.ToDateTime(row.Cells["Дата"].Value);
                    textBox6.Text = row.Cells["Количество"].Value?.ToString() ?? "";
                    textBox7.Text = row.Cells["Стоимость"].Value?.ToString() ?? "";
                    break;

                case "Категории":
                    textBox2.Text = row.Cells["Название"].Value?.ToString() ?? "";
                    break;

                case "Склад":
                    textBox2.Text = row.Cells["Название"].Value?.ToString() ?? "";
                    textBox3.Text = row.Cells["Адрес"].Value?.ToString() ?? "";
                    break;

                case "Сотрудник":
                    textBox2.Text = row.Cells["Фамилия"].Value?.ToString() ?? "";
                    textBox3.Text = row.Cells["Имя"].Value?.ToString() ?? "";
                    textBox4.Text = row.Cells["Отчество"].Value?.ToString() ?? "";
                    break;

                case "Аккаунты":
                    
                        textBox2.Text = row.Cells["Логин"].Value?.ToString() ?? "";
                        textBox3.Text = row.Cells["Пароль"].Value?.ToString() ?? "";
                        textBox4.Text = row.Cells["Роль"].Value?.ToString() ?? "";
                        break;

                    }
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            if (currentTable == "") {
                MessageBox.Show("Выберите таблицу для выполнения действий");
                return;
            }
            if (selectedRow < 0)
            {
                MessageBox.Show("Выберите запись для удаления");
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    database.openConnection();
                    int id = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["id"].Value);
                    string table = currentTable;

                    // Проверка связанных записей
                    if (table == "Товары" && HasRelatedRecords(id, "Чек", "id_товара"))
                    {
                        MessageBox.Show("Невозможно удалить товар, так как он связан с записями в таблице Чек");
                        return;
                    }
                    // Добавьте другие проверки для разных таблиц...

                    string query = $"DELETE FROM {table} WHERE id = @id";
                    SqlCommand command = new SqlCommand(query, database.GetConnection());
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Запись успешно удалена");
                        RefreshDataGrid(dataGridView1);
                        // Сбрасываем выбранную строку после удаления
                        selectedRow = -1;
                        ClearFormFields();
                    }
                    else
                    {
                        MessageBox.Show("Запись не найдена");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
                finally
                {
                    database.closeConnection();
                }
            }
        }

        private void ClearFormFields()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
        }

        private void InitializeControls()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.CellClick += dataGridView1_CellClick;

            // Установка начальных дат для фильтрации
            dateTimePicker2.Value = DateTime.Now.AddYears(-2);
            dateTimePicker1.Value = DateTime.Now;
        }

       
        private void UpdateFormControls()
        {
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            dateTimePicker3.Visible = false;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            comboBox5.Visible = false;
            comboBox6.Visible = false;
            comboBox1.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            comboBox10.Visible = false;
            button1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            switch (currentTable)
            {
                case "Товары":
                    textBox2.Visible = true; // Название
                    textBox3.Visible = true; // Фирма
                    comboBox2.Visible = true; // Категория
                    textBox5.Visible = true; // Цена
                    textBox6.Visible = true; // Количество
                    textBox7.Visible = true; // Описание
                    comboBox3.Visible = true; // Склад
                    numericUpDown1.Visible = true;
                    numericUpDown2.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    comboBox8.Visible = true;
                    comboBox9.Visible = true;
                    button1.Visible = true;
                    LoadCategoriesToComboBox(comboBox8);
                    LoadWarehousesToComboBox(comboBox9);
                    LoadCategoriesToComboBox(comboBox2);
                    LoadWarehousesToComboBox(comboBox3);
                    break;

                case "Клиент":
                    textBox2.Visible = true; // Фамилия
                    textBox3.Visible = true; // Имя
                    textBox4.Visible = true; // Отчество
                    label1.Visible = true;
                    break;

                case "Чек":
                    comboBox4.Visible = true; // Администратор
                    comboBox5.Visible = true; // Товар
                    comboBox6.Visible = true; // Администратор
                    textBox6.Visible = true; // Количество
                    dateTimePicker1.Visible = true; // Дата
                    dateTimePicker2.Visible = true; // Дата
                    dateTimePicker3.Visible = true; // Дата
                    numericUpDown1.Visible = true;
                    numericUpDown2.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    comboBox10.Visible = true;
                    button1.Visible = true;
                    LoadAdminsToComboBox(comboBox10);
                    LoadClientsToComboBox(comboBox4);
                    LoadProductsToComboBox(comboBox5);
                    LoadAdminsToComboBox(comboBox6);
                    break;

                case "Категории":
                    textBox2.Visible = true;
                    label1.Visible = true;// Название категории
                    break;

                case "Склад":
                    textBox2.Visible = true; // Название склада
                    textBox3.Visible = true; // Адрес
                    label1.Visible = true;
                    break;

                case "Сотрудник":
                    textBox2.Visible = true; // Фамилия
                    textBox3.Visible = true; // Имя
                    textBox4.Visible = true; // Отчество
                    label1.Visible = true;
                    break;

                case "Аккаунты":
                    textBox2.Visible = true; // Фамилия
                    textBox3.Visible = true; // Имя
                    label1.Visible = true;
                    label2.Visible = true;
                    comboBox1.Visible = true;
                    comboBox7.Visible = true;
                    button1.Visible = true;
                    break;
            }
        }


        private void RefreshDataGrid(DataGridView dgv)
        {

            SqlConnection connection = null;
            try
            {
                database.openConnection();

                string queryString = "";

                switch (currentTable)
                {
                    case "Чек":
                        queryString = @"SELECT 
                            ч.id, 
                            к.Фамилия + ' ' + к.Имя + ISNULL(' ' + к.Отчество, '') as Клиент,
                            т.Название as Товар,
                            с.Фамилия + ' ' + с.Имя + ISNULL(' ' + с.Отчество, '') as Сотрудник,
                            ч.Дата, 
                            ч.Количество, 
                            ч.Стоимость
                        FROM Чек ч
                        JOIN Клиент к ON ч.id_клиента = к.id
                        JOIN Товары т ON ч.id_товара = т.id
                        JOIN Сотрудник с ON ч.id_админа = с.id";
                        break;

                    case "Товары":
                        queryString = @"SELECT 
                            т.id, 
                            т.Название, 
                            т.Фирма, 
                            к.Название as Категория,
                            т.Цена,
                            т.Количество as 'Кол-во на складе',
                            с.Название as Склад,
                            т.Описание
                        FROM Товары т
                        JOIN Категории к ON т.id_категории = к.id
                        JOIN Склад с ON т.id_склада = с.id_склада";
                        break;

                    case "Сотрудник":
                        queryString = @"SELECT 
                            id,
                            Фамилия,
                            Имя,
                            Отчество
                        FROM Сотрудник";
                        break;

                    case "Аккаунты":
                        queryString = "SELECT id, Логин, Пароль, Роль FROM Авторизация";
                        break;

                    default:
                        queryString = $"SELECT * FROM {currentTable}";
                        break;
                }

                SqlCommand command = new SqlCommand(queryString, database.GetConnection());
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                dgv.DataSource = dt;

                if (dgv.Columns.Contains("id"))
                    dgv.Columns["id"].Visible = false;
                if (dgv.Columns.Contains("id_склада"))
                    dgv.Columns["id_склада"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void LoadAdminsToComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            SqlConnection connection = null;
            try
            {
                connection = database.GetConnection();
                connection.Open();

                string query = @"SELECT 
                        id,
                        Фамилия + ' ' + Имя + ISNULL(' ' + Отчество, '') as ФИО 
                    FROM Сотрудник";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                comboBox.Items.Clear();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader["ФИО"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки администраторов: {ex.Message}");
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            EditRecord();
        }

        private void EditRecord()
        {
            if (currentTable == "")
            {
                MessageBox.Show("Выберите таблицу для выполнения действий");
                return;
            }
            if (selectedRow < 0)
            {
                MessageBox.Show("Выберите запись для редактирования");
                return;
            }

            // Общая проверка заполненности полей
            if (!ValidateFormFields())
                return;

            try
            {
                database.openConnection();
                string table = currentTable;
                string query = "";
                SqlCommand command;
                int id = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["id"].Value);

                switch (table)
                {
                    case "Товары":
                        // Проверки для товаров
                        if (comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
                        {
                            MessageBox.Show("Выберите категорию и склад");
                            return;
                        }

                        if (!decimal.TryParse(textBox5.Text, out decimal price) || price <= 0)
                        {
                            MessageBox.Show("Введите корректную цену (положительное число)");
                            return;
                        }

                        if (!int.TryParse(textBox6.Text, out int quantity) || quantity < 0)
                        {
                            MessageBox.Show("Введите корректное количество (неотрицательное целое число)");
                            return;
                        }

                        int categoryId = GetCategoryId(comboBox2.SelectedItem.ToString());
                        int warehouseId = GetWarehouseId(comboBox3.SelectedItem.ToString());

                        if (categoryId == -1 || warehouseId == -1)
                        {
                            MessageBox.Show("Не удалось определить ID категории или склада");
                            return;
                        }

                        query = "UPDATE Товары SET Название = @name, Фирма = @firm, id_категории = @categoryId, " +
                                "Цена = @price, Количество = @quantity, id_склада = @warehouseId, Описание = @description WHERE id = @id";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@name", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@firm", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@categoryId", categoryId);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@warehouseId", warehouseId);
                        command.Parameters.AddWithValue("@description", textBox7.Text.Trim());
                        command.Parameters.AddWithValue("@id", id);
                        break;

                    case "Клиент":
                        // Проверка ФИО
                        if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                        {
                            MessageBox.Show("Фамилия и имя обязательны для заполнения");
                            return;
                        }

                        query = "UPDATE Клиент SET Фамилия = @lastname, Имя = @firstname, " +
                                "Отчество = @middlename WHERE id = @id";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@lastname", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@firstname", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@middlename", textBox4.Text.Trim());
                        command.Parameters.AddWithValue("@id", id);
                        break;

                    case "Чек":
                        // Проверка выбранных значений в комбобоксах
                        if (comboBox4.SelectedIndex == -1 || comboBox5.SelectedIndex == -1 || comboBox6.SelectedIndex == -1)
                        {
                            MessageBox.Show("Заполните все обязательные поля: Клиент, Товар, Администратор");
                            return;
                        }

                        if (!int.TryParse(textBox6.Text, out int chekQuantity) || chekQuantity <= 0)
                        {
                            MessageBox.Show("Введите корректное количество (положительное целое число)");
                            return;
                        }

                        decimal productPrice = GetProductPrice(comboBox5.SelectedItem.ToString());
                        if (productPrice == 0)
                        {
                            MessageBox.Show("Не удалось получить цену товара");
                            return;
                        }

                        decimal total = productPrice * chekQuantity;
                        int clientId = GetClientId(comboBox4.SelectedItem.ToString());
                        int adminId = GetAdminId(comboBox6.SelectedItem.ToString());
                        int productId = GetProductId(comboBox5.SelectedItem.ToString());

                        if (clientId == -1 || adminId == -1 || productId == -1)
                        {
                            MessageBox.Show("Не удалось определить ID клиента, администратора или товара");
                            return;
                        }

                        query = "UPDATE Чек SET id_клиента = @clientId, id_админа = @adminId, " +
                                "id_товара = @productId, Дата = @date, Количество = @quantity, Стоимость = @total WHERE id = @id";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@clientId", clientId);
                        command.Parameters.AddWithValue("@adminId", adminId);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@quantity", chekQuantity);
                        command.Parameters.AddWithValue("@total", total);
                        command.Parameters.AddWithValue("@id", id);
                        textBox7.Text = total.ToString("F2");
                        break;

                    case "Аккаунты":
                        if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedIndex == -1)
                        {
                            MessageBox.Show("Заполните все поля: Логин, Пароль и Роль");
                            return;
                        }
                        query = "UPDATE Авторизация SET Логин = @login, Пароль = @password, Роль = @role WHERE id = @id";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@login", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@password", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@role", comboBox1.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@id", id);
                        break;

                    case "Склад":
                        if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                        {
                            MessageBox.Show("Заполните все поля: Название, Адрес");
                            return;
                        }
                        query = "UPDATE Склад SET Название = @login, Адрес = @password WHERE id = @id";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@login", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@password", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@id", id);
                        break;

                    case "Категории":
                        if (string.IsNullOrWhiteSpace(textBox2.Text))
                        {
                            MessageBox.Show("Заполните название категории");
                            return;
                        }
                        query = "UPDATE Категории SET Название = @login WHERE id = @id";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@login", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@id", id);
                        break;

                    default:
                        MessageBox.Show("Редактирование для этой таблицы не реализовано");
                        database.closeConnection();
                        return;
                }

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Запись успешно обновлена");
                    RefreshDataGrid(dataGridView1);
                }
                else
                {
                    MessageBox.Show("Запись не найдена");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании: {ex.Message}");
            }
            finally
            {
                database.closeConnection();
            }
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (selectedRow < 0)
            {
                MessageBox.Show("Выберите запись для удаления");
                return;
            }
            if (currentTable == "")
            {
                MessageBox.Show("Выберите таблицу для выполнения действий");
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlConnection connection = null;
                try
                {
                    // Открываем соединение один раз
                    connection = database.GetConnection();
                    connection.Open();

                    int id = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["id"].Value);
                    string table = currentTable;

                    // Проверка связанных записей (передаем открытое соединение)
                    if (table == "Товары" && HasRelatedRecords(connection, id, "Чек", "id_товара"))
                    {
                        MessageBox.Show("Невозможно удалить товар, так как он связан с записями в таблице Чек");
                        return;
                    }
                    else if (table == "Клиент" && HasRelatedRecords(connection, id, "Чек", "id_клиента"))
                    {
                        MessageBox.Show("Невозможно удалить клиента, так как он связан с записями в таблице Чек");
                        return;
                    }
                    else if (table == "Категории" && HasRelatedRecords(connection, id, "Товары", "id_категории"))
                    {
                        MessageBox.Show("Невозможно удалить категорию, так как она связана с товарами");
                        return;
                    }
                    else if (table == "Склад" && HasRelatedRecords(connection, id, "Товары", "id_склада"))
                    {
                        MessageBox.Show("Невозможно удалить склад, так как он связан с товарами");
                        return;
                    }
                    else if (table == "Аккаунты") {
                        table = "Авторизация";
                    }
                        string query = $"DELETE FROM {table} WHERE id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Запись успешно удалена");
                        RefreshDataGrid(dataGridView1);
                    }
                    else
                    {
                        MessageBox.Show("Запись не найдена");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        // Измененный метод HasRelatedRecords (не закрывает соединение)
        private bool HasRelatedRecords(SqlConnection connection, int id, string relatedTable, string relatedField)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM {relatedTable} WHERE {relatedField} = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
            catch
            {
                return false;
            }
            // Не закрываем соединение здесь!
        }

        private bool HasRelatedRecords(int id, string relatedTable, string relatedField)
        {
            try
            {
                database.openConnection();
                string query = $"SELECT COUNT(*) FROM {relatedTable} WHERE {relatedField} = @id";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@id", id);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e) // Добавление записи
        {
            try
            {
                if (currentTable == "")
                {
                    MessageBox.Show("Выберите таблицу для выполнения действий");
                    return;
                }

                // Общая проверка заполненности полей
                if (!ValidateFormFields())
                    return;

                database.openConnection();
                string table = currentTable;
                string query = "";
                SqlCommand command;

                switch (table)
                {
                    case "Товары":
                        // Дополнительные проверки для товаров
                        if (comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
                        {
                            MessageBox.Show("Выберите категорию и склад");
                            return;
                        }

                        if (!decimal.TryParse(textBox5.Text, out decimal price) || price <= 0)
                        {
                            MessageBox.Show("Введите корректную цену (положительное число)");
                            return;
                        }

                        if (!int.TryParse(textBox6.Text, out int quantity) || quantity < 0)
                        {
                            MessageBox.Show("Введите корректное количество (неотрицательное целое число)");
                            return;
                        }

                        int categoryId = GetCategoryId(comboBox2.SelectedItem?.ToString());
                        int warehouseId = GetWarehouseId(comboBox3.SelectedItem?.ToString());

                        if (categoryId == -1 || warehouseId == -1)
                        {
                            MessageBox.Show("Не удалось получить ID категории или склада");
                            return;
                        }

                        query = "INSERT INTO Товары (Название, Фирма, id_категории, Цена, Количество, id_склада, Описание) " +
                                "VALUES (@name, @firm, @categoryId, @price, @quantity, @warehouseId, @description)";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@name", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@firm", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@categoryId", categoryId);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@warehouseId", warehouseId);
                        command.Parameters.AddWithValue("@description", textBox7.Text.Trim());
                        break;

                    case "Клиент":
                        // Проверка ФИО
                        if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                        {
                            MessageBox.Show("Фамилия и имя обязательны для заполнения");
                            return;
                        }

                        query = "INSERT INTO Клиент (Фамилия, Имя, Отчество) " +
                                "VALUES (@lastname, @firstname, @middlename)";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@lastname", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@firstname", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@middlename", textBox4.Text.Trim());
                        break;

                    case "Чек":
                        // Проверка выбранных значений в комбобоксах
                        if (comboBox4.SelectedIndex == -1 || comboBox5.SelectedIndex == -1 || comboBox6.SelectedIndex == -1)
                        {
                            MessageBox.Show("Заполните все обязательные поля: Клиент, Товар, Администратор");
                            return;
                        }

                        if (!int.TryParse(textBox6.Text, out int chekQuantity) || chekQuantity <= 0)
                        {
                            MessageBox.Show("Введите корректное количество (положительное целое число)");
                            return;
                        }

                        decimal productPrice = GetProductPrice(comboBox5.SelectedItem.ToString());
                        if (productPrice == 0)
                        {
                            MessageBox.Show("Не удалось получить цену товара");
                            return;
                        }

                        decimal total = productPrice * chekQuantity;
                        int clientId = GetClientId(comboBox4.SelectedItem.ToString());
                        int adminId = GetAdminId(comboBox6.SelectedItem.ToString());
                        int productId = GetProductId(comboBox5.SelectedItem.ToString());

                        if (clientId == -1 || adminId == -1 || productId == -1)
                        {
                            MessageBox.Show("Не удалось определить ID клиента, администратора или товара");
                            return;
                        }

                        query = "INSERT INTO Чек (id_клиента, id_админа, id_товара, Дата, Количество, Стоимость) " +
                                "VALUES (@clientId, @adminId, @productId, @date, @quantity, @total)";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@clientId", clientId);
                        command.Parameters.AddWithValue("@adminId", adminId);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@quantity", chekQuantity);
                        command.Parameters.AddWithValue("@total", total);
                        textBox7.Text = total.ToString("F2");
                        break;

                    // Аналогичные проверки для других таблиц...
                    case "Категории":
                        if (string.IsNullOrWhiteSpace(textBox2.Text))
                        {
                            MessageBox.Show("Введите название категории");
                            return;
                        }
                        query = "INSERT INTO Категории (Название) VALUES (@name)";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@name", textBox2.Text.Trim());
                        break;

                    case "Склад":
                        if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                        {
                            MessageBox.Show("Заполните название и адрес склада");
                            return;
                        }
                        query = "INSERT INTO Склад (Название, Адрес) VALUES (@name, @address)";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@name", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@address", textBox3.Text.Trim());
                        break;

                    case "Сотрудник":
                        if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                        {
                            MessageBox.Show("Фамилия и имя обязательны для заполнения");
                            return;
                        }
                        query = "INSERT INTO Сотрудник (Фамилия, Имя, Отчество) VALUES (@lastname, @firstname, @middlename)";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@lastname", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@firstname", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@middlename", textBox4.Text.Trim());
                        break;

                    case "Аккаунты":
                        if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                        {
                            MessageBox.Show("Заполните все поля: Логин, Пароль и Роль");
                            return;
                        }
                        query = "INSERT INTO Авторизация (Логин, Пароль, Роль) VALUES (@login, @password, @role)";
                        command = new SqlCommand(query, database.GetConnection());
                        command.Parameters.AddWithValue("@login", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@password", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@role", comboBox1.SelectedItem.ToString());
                        break;

                    default:
                        MessageBox.Show("Добавление для этой таблицы не реализовано");
                        database.closeConnection();
                        return;
                }

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Запись успешно добавлена");
                    RefreshDataGrid(dataGridView1);
                    ClearFormFields();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить запись");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }
            finally
            {
                database.closeConnection();
            }
        }

        private bool ValidateFormFields()
        {
            switch (currentTable)
            {
                case "Товары":
                    if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                        string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text))
                    {
                        MessageBox.Show("Заполните все обязательные поля: Название, Фирма, Цена, Количество");
                        return false;
                    }
                    if (comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
                    {
                        MessageBox.Show("Выберите категорию и склад");
                        return false;
                    }
                    break;

                case "Клиент":
                    if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        MessageBox.Show("Фамилия и имя обязательны для заполнения");
                        return false;
                    }
                    break;

                case "Чек":
                    if (comboBox4.SelectedIndex == -1 || comboBox5.SelectedIndex == -1 || comboBox6.SelectedIndex == -1)
                    {
                        MessageBox.Show("Выберите клиента, товар и администратора");
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(textBox6.Text))
                    {
                        MessageBox.Show("Введите количество");
                        return false;
                    }
                    break;

                case "Категории":
                case "Склад":
                    if (string.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        MessageBox.Show("Введите название");
                        return false;
                    }
                    break;

                case "Сотрудник":
                    if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        MessageBox.Show("Фамилия и имя обязательны для заполнения");
                        return false;
                    }
                    break;

                case "Аккаунты":
                    if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Заполните все поля: Логин, Пароль и Роль");
                        return false;
                    }
                    break;
            }

            return true;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox8.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                RefreshDataGrid(dataGridView1);
                return;
            }
                dataGridView1.ClearSelection();
                if (string.IsNullOrEmpty(textBox8.Text))
                    return;
                var values = textBox8.Text.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    foreach (string value in values)
                    {
                        var row = dataGridView1.Rows[i];
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            if (row.Cells[j].Value.ToString().ToLower().Contains(value))
                            {
                                row.Selected = true;
                            }
                        }
                    }
                };
            }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (currentTable == "Чек") FilterByDateRange();
        }

        private void DateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (currentTable == "Чек") FilterByDateRange();
        }

        private void FilterByDateRange()
        {
            try
            {
                database.openConnection();
                DateTime fromDate = dateTimePicker1.Value.Date;
                DateTime toDate = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

                string query = @"
                    SELECT 
                        ч.id, 
                        к.Фамилия + ' ' + к.Имя + ' ' + к.Отчество as Клиент,
                        т.Название as Товар,
                        а.Фамилия + ' ' + а.Имя as Администратор,
                        ч.Дата, 
                        ч.Количество, 
                        ч.Стоимость
                    FROM Чек ч
                    JOIN Клиент к ON ч.id_клиента = к.id
                    JOIN Товары т ON ч.id_товара = т.id
                    JOIN Авторизация а ON ч.id_админа = а.id
                    WHERE ч.Дата BETWEEN @FromDate AND @ToDate";

                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@FromDate", fromDate);
                command.Parameters.AddWithValue("@ToDate", toDate);

                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации: {ex.Message}");
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label9.Text = "";
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            comboBox6.Visible = false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            dateTimePicker3.Visible = false;
            numericUpDown1.Visible= false;
            numericUpDown2.Visible=false;
            label2.Visible=false;

            database.openConnection();
            SqlCommand command = new SqlCommand("SELECT Роль FROM Авторизация WHERE Логин = @login", database.GetConnection());
            command.Parameters.AddWithValue("@login", Environment.UserName); // или другой способ идентификации
            currentUserRole = command.ExecuteScalar()?.ToString();
            database.closeConnection();

            UpdateFormControls();
        }

        // Вспомогательные методы для работы с Combobox
        private void LoadCategoriesToComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            try
            {
                database.openConnection();
                string query = "SELECT Название FROM Категории";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                SqlDataReader reader = command.ExecuteReader();

                comboBox.Items.Clear();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader["Название"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}");
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void LoadWarehousesToComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            try
            {
                database.openConnection();
                string query = "SELECT Название FROM Склад";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                SqlDataReader reader = command.ExecuteReader();

                comboBox.Items.Clear();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader["Название"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки складов: {ex.Message}");
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void LoadClientsToComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            try
            {
                database.openConnection();
                string query = "SELECT Фамилия + ' ' + Имя + ' ' + Отчество as ФИО FROM Клиент";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                SqlDataReader reader = command.ExecuteReader();

                comboBox.Items.Clear();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader["ФИО"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}");
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void LoadProductsToComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            try
            {
                database.openConnection();
                string query = "SELECT Название FROM Товары";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                SqlDataReader reader = command.ExecuteReader();

                comboBox.Items.Clear();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader["Название"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}");
            }
            finally
            {
                database.closeConnection();
            }
        }

        

        // Вспомогательные методы для получения ID
        private int GetClientId(string clientName)
        {
            try
            {
                string[] names = clientName.Split(' ');
                string query = "SELECT id FROM Клиент WHERE Фамилия = @lastName AND Имя = @firstName AND Отчество = @middleName";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@lastName", names[0]);
                command.Parameters.AddWithValue("@firstName", names[1]);
                command.Parameters.AddWithValue("@middleName", names[2]);
                return (int)command.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
        }
        private int GetCategoryId(string categoryName)
        {
            try
            {
                // Используем уже открытое соединение из database
                string query = "SELECT id FROM Категории WHERE Название = @name";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@name", categoryName);

                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении ID категории: {ex.Message}");
                return -1;
            }
        }
        private int GetProductId(string productName)
        {
            try
            {
                database.openConnection();
                string query = "SELECT id FROM Товары WHERE Название = @name";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@name", productName);
                return (int)command.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
        }

        private int GetAdminId(string adminName)
        {
            try
            {
                database.openConnection();
                string[] names = adminName.Split(' ');
                string query = "SELECT id FROM Сотрудник WHERE Фамилия = @lastName AND Имя = @firstName";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@lastName", names[0]);
                command.Parameters.AddWithValue("@firstName", names[1]);
                return (int)command.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
        }

        private int GetWarehouseId(string warehouseName)
        {
            try
            {
                database.openConnection();
                string query = "SELECT id_склада FROM Склад WHERE Название = @name";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@name", warehouseName);
                return (int)command.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
        }

        private decimal GetProductPrice(string productName)
        {
            try
            {
                database.openConnection();
                string query = "SELECT Цена FROM Товары WHERE Название = @name";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@name", productName);
                return (decimal)command.ExecuteScalar();
            }
            catch
            {
                return 0;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentTable == "Чек" && comboBox5.SelectedIndex != -1 && !string.IsNullOrEmpty(textBox5.Text))
            {
                try
                {
                    decimal price = GetProductPrice(comboBox5.SelectedItem.ToString());
                    int quantity = int.Parse(textBox6.Text);
                    decimal total = price * quantity;
                    textBox6.Text = total.ToString();
                }
                catch
                {
                    // Игнорируем ошибки
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (currentTable == "Чек" && comboBox5.SelectedIndex != -1 && !string.IsNullOrEmpty(textBox5.Text))
            {
                try
                {
                    decimal price = GetProductPrice(comboBox3.SelectedItem.ToString());
                    int quantity = int.Parse(textBox6.Text);
                    decimal total = price * quantity;
                    textBox6.Text = total.ToString();
                }
                catch
                {
                }
            }
        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            darkMode = !darkMode;
            ApplyTheme();
        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            currentTable = "Клиент";
            RefreshDataGrid(dataGridView1);
            UpdateFormControls();
            HighlightSelectedLabel(label3);
            label9.Text = currentTable;
        }
        private void HighlightSelectedLabel(System.Windows.Forms.Label selectedLabel)
        {
            if (darkMode == false) {
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
                label8.ForeColor = Color.Black;
                label10.ForeColor = Color.Black;
                selectedLabel.ForeColor = Color.FromArgb(11, 125, 19);
                selectedLabel.Font = new System.Drawing.Font(selectedLabel.Font, FontStyle.Bold);
            }
            if (darkMode == true)
            {
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                label7.ForeColor = Color.White;
                label8.ForeColor = Color.White;
                label10.ForeColor = Color.White;
                selectedLabel.ForeColor = Color.FromArgb(229, 157, 240);
                selectedLabel.Font = new System.Drawing.Font(selectedLabel.Font, FontStyle.Bold);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            currentTable = "Товары";
            RefreshDataGrid(dataGridView1);
            UpdateFormControls();
            HighlightSelectedLabel(label4);
            ClearFormFields();
            label9.Text = currentTable;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            currentTable = "Чек";
            RefreshDataGrid(dataGridView1);
            UpdateFormControls();
            HighlightSelectedLabel(label5);
            ClearFormFields();
            label9.Text = currentTable;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            currentTable = "Категории";
            RefreshDataGrid(dataGridView1);
            UpdateFormControls();
            HighlightSelectedLabel(label6);
            ClearFormFields();
            label9.Text = currentTable;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            currentTable = "Склад";
            RefreshDataGrid(dataGridView1);
            UpdateFormControls();
            HighlightSelectedLabel(label7);
            ClearFormFields();
            label9.Text = currentTable;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            currentTable = "Сотрудник";
            RefreshDataGrid(dataGridView1);
            UpdateFormControls();
            HighlightSelectedLabel(label8);
            ClearFormFields();
            label9.Text= currentTable;
        }

        private void label10_Click(object sender, EventArgs e)
        {
                currentTable = "Аккаунты";
                RefreshDataGrid(dataGridView1);
                UpdateFormControls();
                HighlightSelectedLabel(label10);
                ClearFormFields();
                label9.Text = currentTable;
        }
        private bool HasPermission(string requiredRole)
        {
            return currentUserRole == requiredRole || currentUserRole == "Администратор";
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            if (currentTable != "Чек")
            {
                MessageBox.Show("Выберите таблицу 'Чек' для экспорта");
                return;
            }

            if (selectedRow < 0)
            {
                MessageBox.Show("Выберите чек для экспорта");
                return;
            }

            int checkId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["id"].Value);
            ExportCheckToWord(checkId);
        }

        private void ExportCheckToWord(int checkId)
        {
            try
            {
                // Получаем данные чека из базы данных
                database.openConnection();
                string query = @"SELECT 
                ч.id, 
                к.Фамилия + ' ' + к.Имя + ISNULL(' ' + к.Отчество, '') as Клиент,
                т.Название as Товар,
                т.Цена as Цена_ед,
                с.Фамилия + ' ' + с.Имя + ISNULL(' ' + с.Отчество, '') as Сотрудник,
                ч.Дата, 
                ч.Количество, 
                ч.Стоимость
            FROM Чек ч
            JOIN Клиент к ON ч.id_клиента = к.id
            JOIN Товары т ON ч.id_товара = т.id
            JOIN Сотрудник с ON ч.id_админа = с.id
            WHERE ч.id = @checkId";

                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@checkId", checkId);

                SqlDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    MessageBox.Show("Чек не найден");
                    return;
                }

                // Создаем новый документ Word
                Application wordApp = new Application();
                Document doc = wordApp.Documents.Add();

                // Настройка документа - узкий как настоящий чек
                wordApp.Visible = true;
                doc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
                doc.PageSetup.PaperSize = WdPaperSize.wdPaperA4;
                doc.PageSetup.TopMargin = wordApp.CentimetersToPoints(0.5f);
                doc.PageSetup.BottomMargin = wordApp.CentimetersToPoints(0.5f);
                doc.PageSetup.LeftMargin = wordApp.CentimetersToPoints(1f);
                doc.PageSetup.RightMargin = wordApp.CentimetersToPoints(1f);
                doc.PageSetup.TextColumns.SetCount(2); // Две колонки как в настоящем чеке
                doc.PageSetup.TextColumns[1].Width = wordApp.CentimetersToPoints(4f);

                // Узкий шрифт как в кассовых чеках
                string receiptFont = "Lucida Console"; // Моноширинный шрифт
                int smallFontSize = 8;
                int mediumFontSize = 10;
                int largeFontSize = 12;

                // Шапка чека
                Paragraph header = doc.Paragraphs.Add();
                header.Range.Text = "ООО 'Торгаш'";
                header.Range.Font.Name = receiptFont;
                header.Range.Font.Size = largeFontSize;
                header.Range.Font.Bold = 1;
                header.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                header.Range.InsertParagraphAfter();

                // Адрес и контакты
                Paragraph address = doc.Paragraphs.Add();
                address.Range.Font.Name = receiptFont;
                address.Range.Font.Size = smallFontSize;
                address.Range.Text = "г. Гомель, ул. Пролетарская 39, \nТел.: \n+375 (29) 556-99-33\nИНН 1234567890";
                address.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                address.Range.InsertParagraphAfter();

                // Разделительная линия
                AddDoubleSeparator(doc, receiptFont);

                // Номер чека и дата
                Paragraph checkInfo = doc.Paragraphs.Add();
                checkInfo.Range.Text = $"ЧЕК №{reader["id"]}\n" +
                                      $"КАССИР: {reader["Сотрудник"]}\n" +
                                      $"{Convert.ToDateTime(reader["Дата"]).ToString("dd.MM.yyyy")}";
                checkInfo.Range.Font.Name = receiptFont;
                checkInfo.Range.Font.Size = mediumFontSize;
                checkInfo.Range.InsertParagraphAfter();

                AddSingleSeparator(doc, receiptFont);

                // Заголовок таблицы товаров
                Paragraph tableHeader = doc.Paragraphs.Add();
                tableHeader.Range.Text = "НАИМЕНОВАНИЕ          ЦЕНА  КОЛ-ВО   СУММА";
                tableHeader.Range.Font.Name = receiptFont;
                tableHeader.Range.Font.Size = mediumFontSize;
                tableHeader.Range.Font.Bold = 1;
                tableHeader.Range.InsertParagraphAfter();

                AddSingleSeparator(doc, receiptFont);

                // Данные о товаре
                string productName = reader["Товар"].ToString();
                if (productName.Length > 20) productName = productName.Substring(0, 17) + "...";

                decimal price = Convert.ToDecimal(reader["Цена_ед"]);
                int quantity = Convert.ToInt32(reader["Количество"]);
                decimal sum = Convert.ToDecimal(reader["Стоимость"]);
                string priceStr = string.Format("{0:0.00} BYN", price);
                string sumStr = string.Format("{0:0.00} BYN", sum);

                Paragraph productRow = doc.Paragraphs.Add();
                productRow.Range.Text = $"{productName.PadRight(20)} {priceStr.PadLeft(7)} {quantity.ToString().PadLeft(5)} {sumStr.PadLeft(8)}";
                productRow.Range.Font.Name = receiptFont;
                productRow.Range.Font.Size = mediumFontSize;
                productRow.Range.InsertParagraphAfter();

                AddDoubleSeparator(doc, receiptFont);

                // Итоговая сумма
                Paragraph total = doc.Paragraphs.Add();
                total.Range.Text = $"ИТОГО: {sumStr.PadLeft(34)}";
                total.Range.Font.Name = receiptFont;
                total.Range.Font.Size = mediumFontSize;
                total.Range.Font.Bold = 1;
                total.Range.InsertParagraphAfter();

                AddDoubleSeparator(doc, receiptFont);

                // Клиент (если есть)
                if (!string.IsNullOrEmpty(reader["Клиент"].ToString()))
                {
                    Paragraph client = doc.Paragraphs.Add();
                    client.Range.Text = $"КЛИЕНТ: {reader["Клиент"]}";
                    client.Range.Font.Name = receiptFont;
                    client.Range.Font.Size = mediumFontSize;
                    client.Range.InsertParagraphAfter();
                    AddSingleSeparator(doc, receiptFont);
                }

                // Футер
                Paragraph footer = doc.Paragraphs.Add();
                footer.Range.Text = "СПАСИБО ЗА ПОКУПКУ!\n" +
                                   "Возврат товара в течение 14 дней\n" +
                                   "с сохранением товарного вида\n" +
                                   "Кассовый чек является документом\n" +
                                   $"ФН №12345678901234  ККМ №987654321\n";
                footer.Range.Font.Name = receiptFont;
                footer.Range.Font.Size = smallFontSize;
                footer.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                footer.Range.InsertParagraphAfter();

                // Сохранение документа
                string fileName = $"Кассовый_чек_{reader["id"]}.docx";
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, fileName);

                doc.SaveAs2(filePath);

                // Автоматическое открытие Word
                doc.Activate();

                // Если нужно сразу печатать:
                // doc.PrintOut();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании чека:\n{ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        // Метод для добавления одинарной разделительной линии
        private void AddSingleSeparator(Document doc, string fontName)
        {
            Paragraph separator = doc.Paragraphs.Add();
            separator.Range.Text = new string('-', 42);
            separator.Range.Font.Name = fontName;
            separator.Range.Font.Size = 8;
            separator.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            separator.Range.InsertParagraphAfter();
        }

        // Метод для добавления двойной разделительной линии
        private void AddDoubleSeparator(Document doc, string fontName)
        {
            Paragraph separator = doc.Paragraphs.Add();
            separator.Range.Text = new string('=', 42);
            separator.Range.Font.Name = fontName;
            separator.Range.Font.Size = 8;
            separator.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            separator.Range.InsertParagraphAfter();
        }
        private void ExportToExcel()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта");
                return;
            }

            try
            {
                // Создаем приложение Excel
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excelApp.Workbooks.Add();
                Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

                // Настройки документа
                excelApp.Visible = true;
                worksheet.Name = currentTable;

                // Заголовки столбцов
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i].Visible)
                    {
                        worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                        ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1]).Font.Bold = true;
                        ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1]).Interior.Color = Color.LightGray;
                    }
                }

                // Данные
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Columns[j].Visible)
                        {
                            object cellValue = dataGridView1.Rows[i].Cells[j].Value;
                            string header = dataGridView1.Columns[j].HeaderText;

                            // Корректировка ценовых значений
                            if ((header == "Цена" || header == "Стоимость" || header == "Цена_ед") && cellValue != null)
                            {
                                if (decimal.TryParse(cellValue.ToString(), out decimal priceValue))
                                {
                                    decimal correctedValue = priceValue;
                                    worksheet.Cells[i + 2, j + 1] = correctedValue;
                                }
                                else
                                {
                                    worksheet.Cells[i + 2, j + 1] = cellValue;
                                }
                            }
                            else
                            {
                                worksheet.Cells[i + 2, j + 1] = cellValue;
                            }
                        }
                    }
                }

                // Автоподбор ширины столбцов
                worksheet.Columns.AutoFit();

                // Форматирование для денежных значений
                if (currentTable == "Чек" || currentTable == "Товары")
                {
                    int lastRow = dataGridView1.Rows.Count + 1;

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        string header = dataGridView1.Columns[j].HeaderText;
                        if (header == "Цена" || header == "Стоимость" || header == "Цена_ед")
                        {
                            Microsoft.Office.Interop.Excel.Range priceRange = worksheet.Range[worksheet.Cells[2, j + 1], worksheet.Cells[lastRow, j + 1]];
                            priceRange.NumberFormat = "#,##0.00\" BYN\"";
                        }
                    }
                }

                // Добавляем итоговую строку для таблицы Чек
                if (currentTable == "Чек")
                {
                    int lastRow = dataGridView1.Rows.Count + 2;
                    worksheet.Cells[lastRow, 1] = "ИТОГО:";

                    int sumColumn = 0;
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Columns[j].HeaderText == "Стоимость")
                        {
                            sumColumn = j + 1;
                            break;
                        }
                    }

                    if (sumColumn > 0)
                    {
                        worksheet.Cells[lastRow, sumColumn].Formula = $"=SUM({GetExcelColumnName(sumColumn)}2:{GetExcelColumnName(sumColumn)}{lastRow - 1})";
                        ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[lastRow, sumColumn]).NumberFormat = "#,##0.00\" BYN\"";
                        ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[lastRow, 1]).Font.Bold = true;
                        ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[lastRow, sumColumn]).Font.Bold = true;
                    }
                }

                // Сохраняем файл
                string fileName = $"Экспорт_{currentTable}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, fileName);

                workbook.SaveAs(filePath);
                MessageBox.Show($"Данные успешно экспортированы в файл:\n{filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте в Excel:\n{ex.Message}");
            }
        }

        // Вспомогательный метод для получения имени столбца Excel
        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = string.Empty;

            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        // Добавьте этот метод в обработчик клика по кнопке экспорта в Excel
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentTable))
            {
                MessageBox.Show("Сначала выберите таблицу для экспорта");
                return;
            }

            ExportToExcel();
        }

        // Общие поля для отслеживания состояния фильтров
        private DateTime? lastStartDate = null;
        private DateTime? lastEndDate = null;
        private decimal lastMinPrice = 0;
        private decimal lastMaxPrice = 0;
        private string lastEmployeeFilter = null;
        private string lastCategoryFilter = null;
        private string lastWarehouseFilter = null;
        private string lastRoleFilter = null;

        private void ApplyFilters()
        {
            if (string.IsNullOrEmpty(currentTable))
            {
                return;
            }

            try
            {
                switch (currentTable)
                {
                    case "Товары":
                        if (ShouldRefreshProductsFilter())
                        {
                            FilterProducts();
                        }
                        break;
                    case "Чек":
                        if (ShouldRefreshChecksFilter())
                        {
                            FilterChecks();
                        }
                        break;
                    case "Аккаунты":
                        if (ShouldRefreshAccountsFilter())
                        {
                            FilterAccounts();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при применении фильтров: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Проверка необходимости обновления фильтров для товаров
        private bool ShouldRefreshProductsFilter()
        {
            bool priceChanged = numericUpDown1.Value != lastMinPrice ||
                              numericUpDown2.Value != lastMaxPrice;
            bool categoryChanged = comboBox8.SelectedItem?.ToString() != lastCategoryFilter;
            bool warehouseChanged = comboBox9.SelectedItem?.ToString() != lastWarehouseFilter;

            if (priceChanged || categoryChanged || warehouseChanged)
            {
                lastMinPrice = numericUpDown1.Value;
                lastMaxPrice = numericUpDown2.Value;
                lastCategoryFilter = comboBox8.SelectedItem?.ToString();
                lastWarehouseFilter = comboBox9.SelectedItem?.ToString();
                return true;
            }
            return false;
        }

        // Проверка необходимости обновления фильтров для чеков
        private bool ShouldRefreshChecksFilter()
        {
            bool dateChanged = dateTimePicker2.Value != lastStartDate ||
                              dateTimePicker3.Value != lastEndDate;
            bool priceChanged = numericUpDown1.Value != lastMinPrice ||
                               numericUpDown2.Value != lastMaxPrice;
            bool employeeChanged = comboBox10.SelectedItem?.ToString() != lastEmployeeFilter;

            if (dateChanged || priceChanged || employeeChanged)
            {
                lastStartDate = dateTimePicker2.Value;
                lastEndDate = dateTimePicker3.Value;
                lastMinPrice = numericUpDown1.Value;
                lastMaxPrice = numericUpDown2.Value;
                lastEmployeeFilter = comboBox10.SelectedItem?.ToString();
                return true;
            }
            return false;
        }

        // Проверка необходимости обновления фильтров для аккаунтов
        private bool ShouldRefreshAccountsFilter()
        {
            bool roleChanged = comboBox7.SelectedItem?.ToString() != lastRoleFilter;

            if (roleChanged)
            {
                lastRoleFilter = comboBox7.SelectedItem?.ToString();
                return true;
            }
            return false;
        }

        private void FilterProducts()
        {
            try
            {
                database.openConnection();

                string query = @"SELECT 
            т.id, 
            т.Название, 
            т.Фирма, 
            к.Название as Категория,
            т.Цена,
            т.Количество as 'Кол-во на складе',
            с.Название as Склад,
            т.Описание
        FROM Товары т
        JOIN Категории к ON т.id_категории = к.id
        JOIN Склад с ON т.id_склада = с.id_склада
        WHERE 1=1";

                SqlCommand command = new SqlCommand(query, database.GetConnection());

                // Фильтрация по цене с параметрами
                if (numericUpDown1.Value > 0)
                {
                    query += " AND т.Цена >= @MinPrice";
                    command.Parameters.AddWithValue("@MinPrice", numericUpDown1.Value);
                }
                if (numericUpDown2.Value > 0 && numericUpDown2.Value > numericUpDown1.Value)
                {
                    query += " AND т.Цена <= @MaxPrice";
                    command.Parameters.AddWithValue("@MaxPrice", numericUpDown2.Value);
                }

                // Фильтрация по категории с параметром
                if (comboBox8.SelectedIndex != -1)
                {
                    query += " AND к.Название = @Category";
                    command.Parameters.AddWithValue("@Category", comboBox8.SelectedItem.ToString());
                }

                // Фильтрация по складу с параметром
                if (comboBox9.SelectedIndex != -1)
                {
                    query += " AND с.Название = @Warehouse";
                    command.Parameters.AddWithValue("@Warehouse", comboBox9.SelectedItem.ToString());
                }

                command.CommandText = query;
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
                UpdateFilterStatus("Товары");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации товаров: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void FilterChecks()
        {
            try
            {
                database.openConnection();

                string query = @"SELECT 
            ч.id, 
            к.Фамилия + ' ' + к.Имя + ISNULL(' ' + к.Отчество, '') as Клиент,
            т.Название as Товар,
            с.Фамилия + ' ' + с.Имя + ISNULL(' ' + с.Отчество, '') as Сотрудник,
            ч.Дата, 
            ч.Количество, 
            ч.Стоимость
        FROM Чек ч
        JOIN Клиент к ON ч.id_клиента = к.id
        JOIN Товары т ON ч.id_товара = т.id
        JOIN Сотрудник с ON ч.id_админа = с.id
        WHERE 1=1";

                SqlCommand command = new SqlCommand(query, database.GetConnection());

                // Фильтрация по сумме
                if (numericUpDown1.Value > 0)
                {
                    query += " AND ч.Стоимость >= @MinCost";
                    command.Parameters.AddWithValue("@MinCost", numericUpDown1.Value);
                }
                if (numericUpDown2.Value > 0 && numericUpDown2.Value > numericUpDown1.Value)
                {
                    query += " AND ч.Стоимость <= @MaxCost";
                    command.Parameters.AddWithValue("@MaxCost", numericUpDown2.Value);
                }

                // Фильтрация по дате
                if (dateTimePicker2.Value != DateTimePicker.MinimumDateTime)
                {
                    query += " AND ч.Дата >= @StartDate";
                    command.Parameters.AddWithValue("@StartDate", dateTimePicker2.Value.Date);
                }
                if (dateTimePicker3.Value != DateTimePicker.MinimumDateTime)
                {
                    query += " AND ч.Дата <= @EndDate";
                    command.Parameters.AddWithValue("@EndDate", dateTimePicker3.Value.Date.AddDays(1).AddSeconds(-1));
                }

                // Фильтрация по сотруднику
                if (comboBox10.SelectedItem != null)
                {
                    query += " AND (с.Фамилия + ' ' + с.Имя + ISNULL(' ' + с.Отчество, '')) = @Employee";
                    command.Parameters.AddWithValue("@Employee", comboBox10.SelectedItem.ToString());
                }

                command.CommandText = query;
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
                UpdateFilterStatus("Чек");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации чеков: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void UpdateFilterStatus(string tableName)
        {
            string status = $"Фильтры ({tableName}): ";

            if (tableName == "Товары")
            {
                if (numericUpDown1.Value > 0 || numericUpDown2.Value > 0)
                    status += $"Цена [{numericUpDown1.Value}-{numericUpDown2.Value}] ";

                if (comboBox8.SelectedItem != null)
                    status += $"Категория: {comboBox8.SelectedItem} ";

                if (comboBox9.SelectedItem != null)
                    status += $"Склад: {comboBox9.SelectedItem}";
            }
            else if (tableName == "Чек")
            {
                if (dateTimePicker2.Value != DateTimePicker.MinimumDateTime ||
                    dateTimePicker3.Value != DateTimePicker.MinimumDateTime)
                    status += $"Дата [{dateTimePicker2.Value:dd.MM.yyyy}-{dateTimePicker3.Value:dd.MM.yyyy}] ";

                if (numericUpDown1.Value > 0 || numericUpDown2.Value > 0)
                    status += $"Сумма [{numericUpDown1.Value}-{numericUpDown2.Value}] ";

                if (comboBox10.SelectedItem != null)
                    status += $"Сотрудник: {comboBox10.SelectedItem}";
            }
            else if (tableName == "Аккаунты")
            {
                if (comboBox7.SelectedItem != null)
                    status += $"Роль: {comboBox7.SelectedItem}";
            }

        }

        // Обработчики событий
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (currentTable == "Чек") ApplyFilters();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (currentTable == "Чек") ApplyFilters();
        }
        private void FilterAccounts()
        {
            try
            {
                database.openConnection();

                string query = "SELECT id, Логин, Пароль, Роль FROM Авторизация WHERE 1=1";

                // Фильтрация по роли
                if (comboBox7.SelectedIndex != -1)
                {
                    query += $" AND Роль = '{comboBox7.SelectedItem.ToString()}'";
                }

                SqlCommand command = new SqlCommand(query, database.GetConnection());
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации аккаунтов: {ex.Message}");
            }
            finally
            {
                database.closeConnection();
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            comboBox9.SelectedIndex = -1;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;

            // Обновление данных
            RefreshDataGrid(dataGridView1);
        }
    }
}