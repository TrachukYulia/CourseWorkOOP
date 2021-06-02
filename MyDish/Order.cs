using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant
{
    /// <summary>
    /// The Order class.
    /// Contains all methods for to work with order.
    /// </summary>
    /// /// <remarks>
    /// This class can get list of ordered dish, remove and  put dish to the order.
    /// </remarks>
    public class Order: IOrder
    {
        private List<Dish> _listOfDishOrdered;
        private static int idCounter = 0;
        private double _sum;
        public Order()
        {
            _sum = 0;
            _listOfDishOrdered = new List<Dish>();
            ++idCounter;
        }
        public List<Dish> ListOfDishOrdered => _listOfDishOrdered;
        public static int GetId => idCounter;
       
        ///  <exception cref="System.OrderException">
        /// Thrown when an item(order) in the list with eneted index is empty
        /// </exception>
        public (List<Dish> orderTheDish, double cost) GetListOfDishOrdered()
        {
            if (_listOfDishOrdered != null)
            {
                if (_listOfDishOrdered.Count == 0)
                {
                    throw new OrderException("Order is empty");
                }
                else
                {
                    return (_listOfDishOrdered, _sum);
                }
            }
            else
            {
                throw new OrderException("Order is empty");
            }
        }
        /// <exception cref="System.OrderException">
        /// Thrown when entered parameter is null
        /// </exception>
        public void PutDish(Dish dish, double weigth)
        {
            if (dish == null)
            {
                throw new OrderException("You cant put an empty dish");
            }
            if (weigth < 0)
                throw new DishException("Invalid weigth");
             dish.Weigth = weigth;
            _listOfDishOrdered.Add(dish);
            _sum += dish.PriseCalculation();
        }
        /// <exception cref="System.OrderException">
        /// Thrown when entered parameter is null
        /// </exception>
        public void Remove(Dish dish)
        {
            if (dish == null)
            {
                throw new OrderException("You cant delete an empty dish");
            }
            _sum -= dish.PriseCalculation();
            _listOfDishOrdered.Remove(dish);
        }
    }
}
