using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Models
{
  public class Task
  {
    private int _id;
    private string _description;
    private int _categoryId;
    private string _dueDate;

    public Task(string description, int categoryId, string dueDate, int Id = 0)
    {
      _id = Id;
      _categoryId = categoryId;
      _description = description;
      _dueDate = dueDate;
    }

    public override bool Equals(System.Object otherTask)
    {
    if (!(otherTask is Task))
      {
        return false;
      }
      else
      {
        Task newTask = (Task) otherTask;
        bool idEquality = (this.GetId() == newTask.GetId());
        bool descriptionEquality = (this.GetDescription() == newTask.GetDescription());
        bool categoryEquality = this.GetCategoryId() == newTask.GetCategoryId();
        bool dueDateEquality = this.GetDueDate() == newTask.GetDueDate();
        return (idEquality && descriptionEquality && categoryEquality && dueDateEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetDescription().GetHashCode();
    }

    public string GetDescription()
    {
      return _description;
    }

    public void UpdateDescription(string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE tasks SET description = @newDescription WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _description = newDescription;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void DeleteTask()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tasks WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public int GetId()
    {
      return _id;
    }

    public int GetCategoryId()
    {
      return _categoryId;
    }

    public string GetDueDate()
    {
      return _dueDate;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO tasks (description, category_id, due_date) VALUES (@description, @category_id, @due_date);";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      MySqlParameter categoryId = new MySqlParameter();
      categoryId.ParameterName = "@category_id";
      categoryId.Value = this._categoryId;
      cmd.Parameters.Add(categoryId);

      MySqlParameter dueDate = new MySqlParameter();
      dueDate.ParameterName = "@due_date";
      dueDate.Value = this._dueDate;
      cmd.Parameters.Add(dueDate);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Task> GetAll()
    {
      List<Task> allTasks = new List<Task> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tasks ORDER BY due_date DESC;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        int taskCategoryId = rdr.GetInt32(2);
        string taskDueDate = rdr.GetString(3);
        Task newTask = new Task(taskDescription, taskCategoryId, taskDueDate, taskId);
        allTasks.Add(newTask);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allTasks;
    }

    public static Task Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `tasks` WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int taskId = 0;
      string taskName = "";
      int taskCategoryId = 0;
      string taskDueDate = "";

      while (rdr.Read())
      {
        taskId = rdr.GetInt32(0);
        taskName = rdr.GetString(1);
        taskCategoryId = rdr.GetInt32(2);
        taskDueDate = rdr.GetString(3);
      }

      Task newTask= new Task(taskName, taskCategoryId, taskDueDate, taskId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newTask;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tasks;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
