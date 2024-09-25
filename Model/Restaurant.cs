using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_7.Model
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public int MediaRestaurantId { get; set; }
        public MediaRestaurant MediaRestaurant { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<RestaurantUser> RestaurantUsers { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
