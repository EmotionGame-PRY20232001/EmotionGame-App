using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    private static DBManager _instance;
    public static DBManager Instance { get { return _instance; } }

    private string dbname = "EmotionGame.db";
    private string dbPath;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        InitializeDB();
    }

    private void InitializeDB()
    {
        dbPath = Application.persistentDataPath + "/" + dbname;
        var db = new SQLiteConnection(dbPath);
        db.CreateTable<Player>();
    }

    public void AddPlayerToDb(Player player)
    {
        var db = new SQLiteConnection(dbPath);
        db.Insert(player);
    }

    public void UpdatePlayerToDb(Player player)
    {
        var db = new SQLiteConnection(dbPath);
        db.Update(player);
    }

    public void DeletePlayerFromDb(Player player)
    {
        var db = new SQLiteConnection(dbPath);
        db.Delete(player);
    }

    public List<Player> GetPlayersFromDb()
    {
        var db = new SQLiteConnection(dbPath);
        var playerList = db.Query<Player>($"select * from Player");
        return playerList;
    }

    // // https://stackoverflow.com/questions/18920136/check-if-a-column-exists-in-sqlite
    // public bool DoFieldExist(string table, string column)
    // {
    //     SQLiteConnection connection = GetConnection();
    //     DataTable ColsTable = connection.GetSchema("Columns");

    //     var data = ColsTable.Select(string.Format("COLUMN_NAME='{1}' AND TABLE_NAME='{0}1'", table, column));
    //     return data.Length == 1;
    // }

    // https://stackoverflow.com/questions/1569685/c-sharp-dynamically-add-columns-to-table-in-database
    public void AddColumn(string table, string column, string columnType)
    {
        // if (DoFieldExist(table, column)) return;
        SQLiteConnection connection = new SQLiteConnection(dbPath);

        SQLiteCommand cmd = new SQLiteCommand(connection);
        cmd.CommandText = "ALTER TABLE " + table + " ADD COLUMN " + column + " " + columnType + ";";
        cmd.ExecuteNonQuery();
    }
}
