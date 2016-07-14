using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurantsInTown
{
  public class ReviewTest : IDisposable
  {
    public ReviewTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Review.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Review.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_reviewEquals_returnTrue()
    {
      //Arrange, Act
      DateTime newDate = new DateTime(2016, 7, 14);
      Review firstReview = new Review("Matt", "The Best", "This was the best restaurant EVER", newDate, 1);
      Review secondReview = new Review("Matt", "The Best", "This was the best restaurant EVER", newDate, 1);
      //Assert
      Assert.Equal(firstReview, secondReview);
    }

    [Fact]
    public void Test_reviewSavesToDatabase()
    {
      //Arrange
      DateTime newDate = new DateTime(2016, 7, 14);
      Review testReview = new Review("Matt", "The Best", "This was the best restaurant EVER", newDate, 1);
      //Act
      testReview.Save();
      Review expectedReview = Review.GetAll()[0];
      //Assert
      Assert.Equal(expectedReview, testReview);
    }

    [Fact]
    public void Test_reviewAssignsId()
    {
      //Arrange
      DateTime newDate = new DateTime(2016, 7, 14);
      Review testReview = new Review("Matt", "The Best", "This was the best restaurant EVER", newDate, 1);
      testReview.Save();
      //Act
      int expectedResult = testReview.GetId();
      int testResult = Review.GetAll()[0].GetId();
      //Assert
      Assert.Equal(expectedResult, testResult);
    }

    [Fact]
    public void Test_reviewUpdates()
    {
      //Arrange
      DateTime newDate = new DateTime(2016, 7, 14);
      Review newReview = new Review("Matt", "The Best", "This was the best restaurant EVER", newDate, 1);
      newReview.Save();
      //Act
      string expectedUser = "Brad";
      string expectedTitle = "The Worst";
      string expectedText = "This was the worst restaurant EVER";
      DateTime? expectedDate = new DateTime(2016, 6, 28);
      int expectedRestaurantId = 1;

      newReview.Update("Brad", "The Worst", "This was the worst restaurant EVER", expectedDate, 1);
      string actualUser = newReview.GetUserName();
      string actualTitle = newReview.GetReviewTitle();
      string actualText = newReview.GetReviewText();
      DateTime? actualDate = newReview.GetDate();
      int actualRestaurantId = newReview.GetRestaurantId();
      //Assert
      Assert.Equal(expectedUser, actualUser);
      Assert.Equal(expectedTitle, actualTitle);
      Assert.Equal(expectedText, actualText);
      Assert.Equal(expectedDate, actualDate);
      Assert.Equal(expectedRestaurantId, actualRestaurantId);
    }
    [Fact]
    public void Test_FindReviewInDatabase()
    {
      //Arrange
      DateTime newDate = new DateTime(2016, 7, 14);
      Review newReview = new Review("Matt", "The Best", "This was the best restaurant EVER", newDate, 1);
      newReview.Save();
      //Act
      Review foundReview = Review.Find(newReview.GetId());
      //Assert
      Assert.Equal(newReview, foundReview);
    }

  }
}
