using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurantsInTown
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=BestRestaurants_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

    [Fact]
    public void Restaurant_DatabaseEmptyToStart()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Restaurant_AreEqual()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Yama", 1);
      Restaurant secondRestaurant = new Restaurant("Yama", 1);
      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Restaurant_SavesToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Yama", 1);
      //Act
      testRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];
      //Assert
      Assert.Equal(testRestaurant, savedRestaurant);
    }

    [Fact]
    public void Restaurant_SavesWithId()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Yama", 1);
      //Act
      testRestaurant.Save();
      int result = Restaurant.GetAll()[0].GetId();
      int expectedResult = testRestaurant.GetId();
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Restaurant_FindReturnsCorrectRestaurant()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("Sakura", 1);
      newRestaurant.Save();
      //Act
      Restaurant foundRestaurant = Restaurant.Find(newRestaurant.GetId());
      //Assert
      Assert.Equal(newRestaurant, foundRestaurant);
    }

    [Fact]
    public void Restaurant_UpdatesRestaurantName()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("Pok Pok", 2);
      string expectedName = "Sakura";
      int expectedCuisineId = 1;
      newRestaurant.Save();
      //Act
      newRestaurant.Update("Sakura", 1);
      string actualName = newRestaurant.GetName();
      int actualCuisineId = newRestaurant.GetCuisineId();
      //Assert
      Assert.Equal(expectedName, actualName);
      Assert.Equal(expectedCuisineId, actualCuisineId);
    }
  }
}
