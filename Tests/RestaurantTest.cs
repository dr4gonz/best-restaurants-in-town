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
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
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
      Restaurant firstRestaurant = new Restaurant("Yama", 1, "unforgettable japanese dining experience", "926 NW 10th Avenue, Portland, OR 97209", "503.841.5463", "none@none.com");
      Restaurant secondRestaurant = new Restaurant("Yama", 1, "unforgettable japanese dining experience", "926 NW 10th Avenue, Portland, OR 97209", "503.841.5463", "none@none.com");
      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Restaurant_SavesToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Yama", 1, "unforgettable japanese dining experience", "926 NW 10th Avenue, Portland, OR 97209", "503.841.5463", "none@none.com");
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
      Restaurant testRestaurant = new Restaurant("Yama", 1, "unforgettable japanese dining experience", "926 NW 10th Avenue, Portland, OR 97209", "503.841.5463", "none@none.com");
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
      Restaurant newRestaurant = new Restaurant("Yama", 1, "unforgettable japanese dining experience", "926 NW 10th Avenue, Portland, OR 97209", "503.841.5463", "none@none.com");
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
      Restaurant newRestaurant = new Restaurant("Pok Pok", 2, "We serve food found at pubs, restaurants, homes and the streets of Southeast Asia", "3226 SE Division Street, Portland, OR", "503.232.1387", "none@none.com");
      string expectedName = "Yama";
      int expectedCuisineId = 1;
      string expectedDescription = "unforgettable japanese dining experience";
      string expectedAddress = "926 NW 10th Avenue, Portland, OR 97209";
      string expectedPhone = "503.841.5463";
      string expectedEmail = "none@none.com";
      newRestaurant.Save();
      //Act
      newRestaurant.Update("Yama", 1, "unforgettable japanese dining experience", "926 NW 10th Avenue, Portland, OR 97209", "503.841.5463", "none@none.com");
      string actualName = newRestaurant.GetName();
      int actualCuisineId = newRestaurant.GetCuisineId();
      string actualDescription = newRestaurant.GetDescription();
      string actualAddress = newRestaurant.GetAddress();
      string actualPhone = newRestaurant.GetPhone();
      string actualEmail = newRestaurant.GetEmail();
      //Assert
      Assert.Equal(expectedName, actualName);
      Assert.Equal(expectedCuisineId, actualCuisineId);
      Assert.Equal(expectedDescription, actualDescription);
      Assert.Equal(expectedAddress, actualAddress);
      Assert.Equal(expectedPhone, actualPhone);
      Assert.Equal(expectedEmail, actualEmail);
    }
  }
}
