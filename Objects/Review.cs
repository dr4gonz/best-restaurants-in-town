using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurantsInTown
{
  public class Review
  {
    private int _id;
    private string _userName;
    private string _reviewTitle;
    private string _reviewText;
    private DateTime? _dateTime;
    private int _restaurantId;


    public Review(string userName, string reviewTitle, string reviewText, DateTime? dateTime, int restaurantId, int id=0)
    {
      _id = id;
      _userName = userName;
      _reviewTitle = reviewTitle;
      _reviewText = reviewText;
      _dateTime = dateTime;
      _restaurantId = restaurantId;
    }

    public string GetUserName()
    {
      return _userName;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetReviewTitle()
    {
      return _reviewTitle;
    }

    public string GetReviewText()
    {
      return _reviewText;
    }

    public DateTime? GetDate()
    {
      return _dateTime;
    }

    public int GetRestaurantId()
    {
      return _restaurantId;
    }

    public static void DeleteAll()
    {
        SqlConnection conn = DB.Connection();
        conn.Open();
        SqlCommand cmd = new SqlCommand("DELETE FROM reviews;", conn);
        cmd.ExecuteNonQuery();
    }

    public override bool Equals(System.Object otherReview)
    {
      if (!(otherReview is Review)) return false;
      else
      {
        Review newReview = (Review) otherReview;
        bool reviewIdEquality = (_id == newReview.GetId());
        bool reviewUserEquality = (_userName == newReview.GetUserName());
        bool reviewTitleEquality = (_reviewTitle == newReview.GetReviewTitle());
        bool reviewTextEquality = (_reviewText == newReview.GetReviewText());
        bool reviewDateEquality = (_dateTime == newReview.GetDate());
        bool reviewRestaurantIdEquality = (_restaurantId == newReview.GetRestaurantId());
        return (reviewIdEquality && reviewUserEquality && reviewTitleEquality && reviewTextEquality && reviewDateEquality && reviewRestaurantIdEquality);
      }
    }

    public static List<Review> GetAll()
    {
      List<Review> allReviews = new List<Review> {};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int newReviewId = rdr.GetInt32(0);
        string newReviewUser = rdr.GetString(1);
        string newReviewTitle = rdr.GetString(2);
        string newReviewText = rdr.GetString(3);
        DateTime? newReviewDate = rdr.GetDateTime(4);
        int newReviewRestaurantId = rdr.GetInt32(5);


        Review newReview = new Review(newReviewUser, newReviewTitle, newReviewText, newReviewDate, newReviewRestaurantId, newReviewId);
        allReviews.Add(newReview);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allReviews;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("INSERT INTO reviews (user_name, review_title, review_text, date_time, restaurant_id) OUTPUT INSERTED.id VALUES(@UserName, @ReviewTitle, @ReviewText, @DateTime, @RestaurantId);", conn);

      SqlParameter reviewUserParameter = new SqlParameter();
      reviewUserParameter.ParameterName = "@UserName";
      reviewUserParameter.Value = _userName;
      cmd.Parameters.Add(reviewUserParameter);

      SqlParameter reviewTitleParameter = new SqlParameter();
      reviewTitleParameter.ParameterName = "@ReviewTitle";
      reviewTitleParameter.Value = _reviewTitle;
      cmd.Parameters.Add(reviewTitleParameter);

      SqlParameter reviewTextParameter = new SqlParameter();
      reviewTextParameter.ParameterName = "@ReviewText";
      reviewTextParameter.Value = _reviewText;
      cmd.Parameters.Add(reviewTextParameter);

      SqlParameter reviewDateParameter = new SqlParameter();
      reviewDateParameter.ParameterName = "@DateTime";
      reviewDateParameter.Value = _dateTime;
      cmd.Parameters.Add(reviewDateParameter);

      SqlParameter reviewRestaurantIdParameter = new SqlParameter();
      reviewRestaurantIdParameter.ParameterName = "@RestaurantId";
      reviewRestaurantIdParameter.Value = _restaurantId;
      cmd.Parameters.Add(reviewRestaurantIdParameter);

      rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

    public static Review Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE id = @ReviewId;", conn);
      SqlParameter reviewIdParameter = new SqlParameter();
      reviewIdParameter.ParameterName = "@ReviewId";
      reviewIdParameter.Value = id.ToString();
      cmd.Parameters.Add(reviewIdParameter);
      rdr = cmd.ExecuteReader();

      int foundReviewId = 0;
      string foundReviewUser = null;
      string foundReviewTitle = null;
      string foundReviewText = null;
      DateTime? foundDateTime = null;
      int foundRestaurantId = 0;

      while (rdr.Read())
      {
        foundReviewId = rdr.GetInt32(0);
        foundReviewUser = rdr.GetString(1);
        foundReviewTitle = rdr.GetString(2);
        foundReviewText = rdr.GetString(3);
        foundDateTime = rdr.GetDateTime(4);
        foundRestaurantId = rdr.GetInt32(5);
      }
      Review foundReview = new Review(foundReviewUser, foundReviewTitle, foundReviewText, foundDateTime, foundRestaurantId, foundReviewId);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return foundReview;
    }

    public void Update(string newUserName, string newReviewTitle, string newReviewText, DateTime? newDateTime, int newRestaurantId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("UPDATE reviews SET user_name = @NewUserName, review_title = @NewReviewTitle, review_text = @NewReviewText, date_time = @NewDateTime, restaurant_id = @NewRestaurantId OUTPUT INSERTED.user_name, INSERTED.review_title, INSERTED.review_text, INSERTED.date_time, INSERTED.restaurant_id WHERE id = @ReviewId;", conn);

      SqlParameter userNameParameter = new SqlParameter();
      userNameParameter.ParameterName = "@NewUserName";
      userNameParameter.Value = newUserName;
      cmd.Parameters.Add(userNameParameter);

      SqlParameter reviewTitleParameter = new SqlParameter();
      reviewTitleParameter.ParameterName = "@NewReviewTitle";
      reviewTitleParameter.Value = newReviewTitle;
      cmd.Parameters.Add(reviewTitleParameter);

      SqlParameter reviewTextParametr = new SqlParameter();
      reviewTextParametr.ParameterName = "@NewReviewText";
      reviewTextParametr.Value = newReviewText;
      cmd.Parameters.Add(reviewTextParametr);

      SqlParameter dateTimeParameter = new SqlParameter();
      dateTimeParameter.ParameterName = "@NewDateTime";
      dateTimeParameter.Value = newDateTime;
      cmd.Parameters.Add(dateTimeParameter);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@NewRestaurantId";
      restaurantIdParameter.Value = newRestaurantId;
      cmd.Parameters.Add(restaurantIdParameter);

      SqlParameter reviewIdParameter = new SqlParameter();
      reviewIdParameter.ParameterName = "@ReviewId";
      reviewIdParameter.Value = this.GetId();
      cmd.Parameters.Add(reviewIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        _userName = rdr.GetString(0);
        _reviewTitle = rdr.GetString(1);
        _reviewText = rdr.GetString(2);
        _dateTime = rdr.GetDateTime(3);
        _restaurantId = rdr.GetInt32(4);
      }

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

    }

  }
}
