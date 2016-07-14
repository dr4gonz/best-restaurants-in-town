using Nancy;
using System;
using System.Collections.Generic;

namespace BestRestaurantsInTown
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
              List<Cuisine> cuisineList = Cuisine.GetAll();
              return View["index.cshtml", cuisineList];
            };

            Get["/cuisine/add"] = _ => View["add_cuisine.cshtml"];

            Post["/cuisine/add"] = _ => {
              Cuisine newCuisine = new Cuisine(Request.Form["cuisine-type"]);
              newCuisine.Save();
              List<Cuisine> cuisineList = Cuisine.GetAll();
              return View["index.cshtml", cuisineList];
            };

            Get["/restaurant/add"] = _ => {
              List<Cuisine> cuisineList = Cuisine.GetAll();
              return View["add_restaurant.cshtml", cuisineList];
            };

            Post["/restaurant/add"] = _ => {
              Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["cuisines"], Request.Form["restaurant-description"], Request.Form["restaurant-address"], Request.Form["restaurant-phone"], Request.Form["restaurant-email"]);
              newRestaurant.Save();
              List<Cuisine> cuisineList = Cuisine.GetAll();
              return View["index.cshtml", cuisineList];
            };

            Get["/restaurant/all"] = _ => {
              List<Restaurant> allRestaurants = Restaurant.GetAll();
              return View["restaurant_list.cshtml", allRestaurants];
            };

            Get["/cuisine/{id}"] = parameters => {
              Cuisine cuisine = Cuisine.Find(parameters.id);
              List<Restaurant> restarantsInCuisine = cuisine.GetRestaurants();
              return View["restaurant_list.cshtml", restarantsInCuisine];
            };


        }
    }
}
