using System.Data;
using System.Data.SqlClient;

public class Class1
{
    private SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-U19AD88\SQLEXPRESS;Initial Catalog=КурсачC#;Integrated Security=True");

    public SqlConnection GetConnection()
    {
        return connection;
    }

    public void openConnection()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
    }

    public void closeConnection()
    {
        if (connection.State == ConnectionState.Open)
        {
            connection.Close();
        }
    }
}