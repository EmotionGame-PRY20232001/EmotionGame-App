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
}
