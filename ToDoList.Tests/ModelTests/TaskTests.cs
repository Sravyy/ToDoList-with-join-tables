using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{

  [TestClass]
  // public class TaskTests : IDisposable
  public class TaskTests : IDisposable
  {
    public TaskTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
    }

    public void Dispose()
    {
      Task.DeleteAll();
      Category.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Task.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfDescriptionsAreTheSame_Task()
    {
      // Arrange, Act
      Task firstTask = new Task("Mow the lawn","2017-01-01");
      Task secondTask = new Task("Mow the lawn","2017-01-01");

      // Assert
      Assert.AreEqual(firstTask, secondTask);
    }

    [TestMethod]
    public void Save_SavesToDatabase_TaskList()
    {
      //Arrange
      Task testTask = new Task("Mow the lawn","2017-01-01");

      //Act
      testTask.Save();
      List<Task> result = Task.GetAll();
      List<Task> testList = new List<Task>{testTask};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Task testTask = new Task("Mow the lawn","2017-01-01");

      //Act
      testTask.Save();
      Task savedTask = Task.GetAll()[0];

      int result = savedTask.GetId();
      int testId = testTask.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsTaskInDatabase_Task()
    {
      //Arrange
      Task testTask = new Task("Mow the lawn","2017-01-01");
      testTask.Save();

      //Act
      Task foundTask = Task.Find(testTask.GetId());

      //Assert
      Assert.AreEqual(testTask, foundTask);
    }

    [TestMethod]
    public void AddCategory_AddsCategoryToTask_CategoryList()
    {
      //Arrange
      Task testTask = new Task("Mow the lawn","2017-01-01");
      testTask.Save();

      Category testCategory = new Category("Home stuff");
      testCategory.Save();

      //Act
      testTask.AddCategory(testCategory);

      List<Category> result = testTask.GetCategories();
      List<Category> testList = new List<Category>{testCategory};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetCategories_ReturnsAllTaskCategories_CategoryList()
    {
      //Arrange
      Task testTask = new Task("Mow the lawn","2017-01-01");
      testTask.Save();

      Category testCategory1 = new Category("Home stuff");
      testCategory1.Save();

      Category testCategory2 = new Category("Work stuff");
      testCategory2.Save();

      //Act
      testTask.AddCategory(testCategory1);
      List<Category> result = testTask.GetCategories();
      List<Category> testList = new List<Category> {testCategory1};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    // [TestMethod]
    // public void Update_UpdatesTaskInDatabase_String()
    // {
    //   //Arrange
    //   string description = "Walk the Dog";
    //   Task testTask = new Task(description, 1, "2017-01-01");
    //   testTask.Save();
    //   string newDescription = "Mow the lawn";
    //
    //   //Act
    //   testTask.UpdateDescription(newDescription);
    //
    //   string result = Task.Find(testTask.GetId()).GetDescription();
    //
    //   //Assert
    //   Assert.AreEqual(newDescription, result);
    // }
    //
    // [TestMethod]
    // public void Delete_DeleteTaskInDatabase_Null()
    // {
    //   //Arrange
    //   string description = "Feed the Fish";
    //   Task testTask = new Task(description, "2017-01-01");
    //   testTask.Save();
    //   // string deletedTask = "";
    //
    //   //Act
    //   testTask.DeleteTask();
    //   int result = Task.GetAll().Count;
    //
    //   //Assert
    //   Assert.AreEqual(0, result);

    [TestMethod]
    public void Delete_DeletesTaskAssociationsFromDatabase_TaskList()
    {
      //Arrange
      Category testCategory = new Category("Home stuff");
      testCategory.Save();

      string testDescription = "Mow the lawn";
      Task testTask = new Task(testDescription,"2017-01-01");
      testTask.Save();

      //Act
      testTask.AddCategory(testCategory);
      testTask.Delete();

      List<Task> resultCategoryTasks = testCategory.GetTasks();
      List<Task> testCategoryTasks = new List<Task> {};

      //Assert
      CollectionAssert.AreEqual(testCategoryTasks, resultCategoryTasks);
    }


  }
}
