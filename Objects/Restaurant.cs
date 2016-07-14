using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurantsInTown
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private int _cuisineId;
    private string _description;
    private string _address;
    private string _phoneNumber;
    private string _email;


    public Restaurant(string name, int cuisineId, string description, string address, string phoneNumber, string email, int Id = 0)
    {
      _id = Id;
      _name = name;
      _cuisineId = cuisineId;
      _description = description;
      _address = address;
      _phoneNumber = phoneNumber;
      _email = email;

    }

    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public string GetDescription()
    {
      return _description;
    }
    public string GetAddress()
    {
      return _address;
    }
    public string GetPhone()
    {
      return _phoneNumber;
    }
    public string GetEmail()
    {
      return _email;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }

    public override bool Equals(System.Object otherRestaurant)
    {
        if(!(otherRestaurant is Restaurant))
        {
            return false;
        }
        else
        {
            Restaurant newRestaurant = (Restaurant) otherRestaurant;
            bool nameEquality = (_name == newRestaurant.GetName());
            bool idEquality = (_id == newRestaurant.GetId());
            bool cuisineEquality = (_cuisineId == newRestaurant.GetCuisineId());
            bool descriptionEquality = (_description == newRestaurant.GetDescription());
            bool addressEquality = (_address == newRestaurant.GetAddress());
            bool phoneNumberEquality = (_phoneNumber == newRestaurant.GetPhone());
            bool emailEquality = (_email == newRestaurant.GetEmail());
            return (nameEquality && idEquality && cuisineEquality && descriptionEquality && addressEquality && phoneNumberEquality && emailEquality);
        }
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int newId = rdr.GetInt32(0);
        string newName = rdr.GetString(1);
        int newCuisineId = rdr.GetInt32(2);
        string newDescription = rdr.GetString(3);
        string newAddress = rdr.GetString(4);
        string newPhone = rdr.GetString(5);
        string newEmail = rdr.GetString(6);
        Restaurant newRestaurant = new Restaurant(newName, newCuisineId, newDescription, newAddress, newPhone, newEmail, newId);
        allRestaurants.Add(newRestaurant);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return allRestaurants;
    }


    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, cuisine_id, description, address, phone, email) OUTPUT INSERTED.id VALUES(@NewName, @NewCuisineId, @NewDescription, @NewAddress, @NewPhone, @NewEmail);", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = _name;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter newCuisineIdParameter = new SqlParameter();
      newCuisineIdParameter.ParameterName = "@NewCuisineId";
      newCuisineIdParameter.Value = _cuisineId;
      cmd.Parameters.Add(newCuisineIdParameter);

      SqlParameter newDescriptionParameter = new SqlParameter();
      newDescriptionParameter.ParameterName = "@NewDescription";
      newDescriptionParameter.Value = _description;
      cmd.Parameters.Add(newDescriptionParameter);

      SqlParameter newAddressParameter = new SqlParameter();
      newAddressParameter.ParameterName = "@NewAddress";
      newAddressParameter.Value = _address;
      cmd.Parameters.Add(newAddressParameter);

      SqlParameter newPhoneParameter = new SqlParameter();
      newPhoneParameter.ParameterName = "@NewPhone";
      newPhoneParameter.Value = _phoneNumber;
      cmd.Parameters.Add(newPhoneParameter);

      SqlParameter newEmailParameter = new SqlParameter();
      newEmailParameter.ParameterName = "@NewEmail";
      newEmailParameter.Value = _email;
      cmd.Parameters.Add(newEmailParameter);

      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);

      SqlParameter newIdParameter = new SqlParameter();
      newIdParameter.ParameterName = "@RestaurantId";
      newIdParameter.Value = id;
      cmd.Parameters.Add(newIdParameter);

      int foundId = 0;
      string foundName = null;
      int foundCuisineId= 0;
      string foundDescription = null;
      string foundAddress = null;
      string foundPhone = null;
      string foundEmail = null;

      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundCuisineId = rdr.GetInt32(2);
        foundDescription = rdr.GetString(3);
        foundAddress = rdr.GetString(4);
        foundPhone = rdr.GetString(5);
        foundEmail = rdr.GetString(6);
      }
      Restaurant foundRestaurant = new Restaurant(foundName, foundCuisineId, foundDescription, foundAddress, foundPhone, foundEmail, foundId);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return foundRestaurant;
    }

    public void Update(string newName, int newCuisineId, string newDescription, string newAddress, string newPhone, string newEmail)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET name = @NewName, cuisine_id = @NewCuisineId, description = @NewDescription, address = @NewAddress, phone = @NewPhone, email = @NewEmail OUTPUT INSERTED.name, INSERTED.cuisine_id, INSERTED.description, INSERTED.address, INSERTED.phone, INSERTED.email WHERE id = @RestaurantId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter newCuisineIdParameter = new SqlParameter();
      newCuisineIdParameter.ParameterName = "@NewCuisineId";
      newCuisineIdParameter.Value = newCuisineId;
      cmd.Parameters.Add(newCuisineIdParameter);

      SqlParameter newDescriptionParameter = new SqlParameter();
      newDescriptionParameter.ParameterName = "@NewDescription";
      newDescriptionParameter.Value = newDescription;
      cmd.Parameters.Add(newDescriptionParameter);

      SqlParameter newAddressParameter = new SqlParameter();
      newAddressParameter.ParameterName = "@NewAddress";
      newAddressParameter.Value = newAddress;
      cmd.Parameters.Add(newAddressParameter);

      SqlParameter newPhoneParamater = new SqlParameter();
      newPhoneParamater.ParameterName = "@NewPhone";
      newPhoneParamater.Value = newPhone;
      cmd.Parameters.Add(newPhoneParamater);

      SqlParameter newEmailParameter = new SqlParameter();
      newEmailParameter.ParameterName = "@NewEmail";
      newEmailParameter.Value = newEmail;
      cmd.Parameters.Add(newEmailParameter);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@RestaurantId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        _name = rdr.GetString(0);
        _cuisineId = rdr.GetInt32(1);
        _description = rdr.GetString(2);
        _address = rdr.GetString(3);
        _phoneNumber = rdr.GetString(4);
        _email = rdr.GetString(5);
      }

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

    public List<Review> GetAllReviews()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE restaurant_id = @RestaurantId ORDER BY date_time;", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = _id;
      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();
      List<Review> allReviews = new List<Review>{};
      while(rdr.Read())
      {
        int reviewId = rdr.GetInt32(0);
        string userName = rdr.GetString(1);
        string reviewTitle = rdr.GetString(2);
        string reviewText = rdr.GetString(3);
        DateTime? reviewDate = rdr.GetDateTime(4);
        int reviewRestaurantId = rdr.GetInt32(5);
        Review newReview = new Review(userName, reviewTitle, reviewText, reviewDate, reviewRestaurantId, reviewId);
        allReviews.Add(newReview);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return allReviews;
    }
  }
}
