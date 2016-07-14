using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurantsInTown
{
    public class CuisineTest : IDisposable
    {
        public CuisineTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
        }
        public void Dispose()
        {
            Cuisine.DeleteAll();
        }
        [Fact]
        public void Test_DatbaseEmptyToStart()
        {
            //Arrange, Act
            int result = Cuisine.GetAll().Count;
            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Cuisine_AreEqual()
        {
          //Arrange, Act
          Cuisine firstCuisine = new Cuisine("Japanese");
          Cuisine secondCuisine = new Cuisine("Japanese");
          //Assert
          Assert.Equal(firstCuisine, secondCuisine);
        }

        [Fact]
        public void Cuisine_SavesToDatabase()
        {
          //Arrange
          Cuisine testCuisine = new Cuisine("Japanese");
          //Act
          testCuisine.Save();
          Cuisine savedCuisine = Cuisine.GetAll()[0];
          //Assert
          Assert.Equal(testCuisine, savedCuisine);
        }

        [Fact]
        public void Cuisine_SavesWithId()
        {
          //Arrange
          Cuisine testCuisine = new Cuisine("Japanese");
          //Act
          testCuisine.Save();
          int result = Cuisine.GetAll()[0].GetId();
          int expectedResult = testCuisine.GetId();
          //Assert
          Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Cuisine_FindsReturnsCorrectCuisine()
        {
          //Arrange
          Cuisine testCuisine = new Cuisine("Japanese");
          //Act
          testCuisine.Save();
          Cuisine foundCuisine = Cuisine.Find(1);
          //Assert
          Assert.Equal(testCuisine, foundCuisine);
        }

        [Fact]
        public void Cuisine_UpdateCuisineName()
        {
          //Arrange
          Cuisine testCuisine = new Cuisine("Japanese");
          string expectedResult = "Mexican";
          testCuisine.Save();
          //Act
          testCuisine.Update("Mexican");
          string result = testCuisine.GetCuisineType();
          //Assert
          Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Cuisine_GetsAllRestaurantsInCuisine()
        {
          Cuisine testCuisine = new Cuisine("Japanese");
          testCuisine.Save();

          Restaurant firstRestaurant = new Restaurant("Yama", testCuisine.GetId(), "unforgettable japanese dining experience", "926 NW 10th Avenue, Portland, OR 97209", "503.841.5463", "none@none.com");
          firstRestaurant.Save();

          Restaurant secondRestaurant = new Restaurant("Marinepolis Sushi Land", testCuisine.GetId(), "conveyer-belt sushi restaurant", "138 NW 10th Ave, Portland, OR 97209", "503.546.9933", "none@none.com");
          secondRestaurant.Save();

          Restaurant thirdRestaurant = new Restaurant("Saburo", testCuisine.GetId(), "no-frills japanese restaurant", "1667 SE Bybee Blvd, Portland, OR 97202", "503.236.4237", "none@none.com");
          thirdRestaurant.Save();

          List<Restaurant> expectedList = new List<Restaurant> {firstRestaurant, secondRestaurant, thirdRestaurant};
          Console.WriteLine("restaurant 1: " + expectedList[0].GetName() + " " + expectedList[0].GetId());
          Console.WriteLine("restaurant 2: " + expectedList[1].GetName() + " " + expectedList[1].GetId());
          Console.WriteLine("restaurant 3: " + expectedList[2].GetName() + " " + expectedList[2].GetId());
          List<Restaurant> testList = testCuisine.GetRestaurants();
          Console.WriteLine("restaurant 1: " + testList[0].GetName() + " " + testList[0].GetId());
          Console.WriteLine("restaurant 2: " + testList[1].GetName() + " " + testList[1].GetId());
          Console.WriteLine("restaurant 3: " + testList[2].GetName() + " " + testList[2].GetId());

          Assert.Equal(expectedList, testList);
        }
    }
}
