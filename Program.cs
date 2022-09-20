using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
class Program
{
    static SQLiteConnection connection;
    static SQLiteCommand command;
    static public bool Connect(string fileName)
    {
        try
        {
            connection = new SQLiteConnection("Data Source=" + fileName + ";Version=3; FailIfMissing = False");
            connection.Open();
            return true;
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
            return false;
        }
    }
    static void Main(string[] args)
    {
        if (Connect("firstBase.sqlite"))
        {
            Console.WriteLine("подключено к базе данных!\n");
        }

        command = new SQLiteCommand(connection)
        {
            CommandText = "CREATE TABLE IF NOT EXISTS [exempl]([id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, [автомобиль] TEXT, [цвет] TEXT, [цена] int,[мощность] int);"
        };
        command.ExecuteNonQuery();
        Console.WriteLine("Таблица создана\n");
        command.CommandText = "INSERT INTO exempl (автомобиль, цвет, цена, мощность) VALUES " +
            "( \"porshe\", \"белый\", 50000, 350)," +
            "( \"mersedes\", \"синий\", 70000, 400)," +
            "( \"volvo\", \"черный\", 20000, 280)," +
            "( \"mazda\", \"черный\", 20000, 280)";
        command.ExecuteNonQuery();

        using (SQLiteCommand selectCMD = connection.CreateCommand())
        {

            selectCMD.CommandText = "SELECT * FROM exempl";
            selectCMD.CommandType = CommandType.Text;
            SQLiteDataReader myReader = selectCMD.ExecuteReader();
            while (myReader.Read())
            {
                Console.WriteLine(myReader["автомобиль"] + " " + myReader["цвет"] + " " + myReader["цена"] + " " + myReader["мощность"]);
            }
            Console.ReadLine();
        }
        connection.Close();

    }
}