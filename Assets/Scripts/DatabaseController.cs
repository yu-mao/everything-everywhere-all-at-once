using System.Collections.Generic;
using UnityEngine;
using SQLite;

public class TaskCompletionStatus
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public List<List<string>> CompletedObjectLabels { get; set; }
    public int NumOfAllowedMistakes { get; set; }
}

public class DatabaseController : MonoBehaviour
{
    private SQLiteConnection database;
    
    private void Awake()
    {
        string dbPath = System.IO.Path.Combine(Application.persistentDataPath, "database.db");
        database = new SQLiteConnection(dbPath);

        database.CreateTable<TaskCompletionStatus>();
    }

    private void OnDestroy()
    {
        if (database != null) database.Close();
    }
}
