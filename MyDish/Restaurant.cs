using System.Collections.Generic;


namespace MyRestaurant
{
    /// <summary>
    /// The  Restaurant class.
    /// Contains all methods for to work with list and menu.
    /// </summary>
    public class Restaurant
    {
        private List<Order> _listOfOrders;
        private Menu _menu;
        private string _nameOfRestaurant;

        ///  <exception cref="System.RestaruantException">
        /// Thrown when the entered data is invalid
        /// </exception>
        public Restaurant(string name, Menu menu)
        {
            if (name == null || menu == null)
                throw new RestaruantException("Data entered incorrectly");
            _nameOfRestaurant = name;
            _menu = menu;
        }
        public List<Order> ListOfOrders => _listOfOrders;
        public string NameOfRestaurant => _nameOfRestaurant;
        public Menu Menu => _menu;

        /// <exception cref="System.OrderException">
        /// Thrown when an item(order) in the list with eneted index is empty
        /// </exception>
        public void OpenOrder()
        {
            Order newOrder = new Order();
            if (newOrder == null)
                throw new OrderException("Order is undefined");
            if (_listOfOrders == null)
            {
                _listOfOrders = new List<Order>
                {
                    newOrder
                };
            }
            else
            {
                _listOfOrders.Add(newOrder);
            }
        }
        /// <exception cref="System.OrderException">
        /// Thrown when an item(order) in the list with eneted index is empty
        /// </exception>
        public void PutOrder(int id, Dish dish, double weigth)
        {
            if (_listOfOrders[id] == null)
            {
                throw new OrderException("Order not found");
            }
            else
            {
                _listOfOrders[id].PutDish(dish, weigth);
            }
        }
        /// <exception cref="System.DishException">
        /// Thrown when an item(dish) in the list with entered index  is empty
        /// </exception>
        /// <exception cref="System.OrderException">
        /// Thrown when an item(order) in the list with entered index is empty
        /// </exception>
        public void Remove(int id, int index)
        {
            if (_listOfOrders[id] != null)
            {
                if (index > 0 || _listOfOrders[id].ListOfDishOrdered[index - 1] != null)
                {
                    _listOfOrders[id].Remove(_listOfOrders[id].ListOfDishOrdered[index - 1]);
                }
                else
                {
                    throw new DishException("Dish undefined");
                }
            }
            else
            {
                throw new OrderException("Order not found");
            }
        }
        /// <exception cref="System.OrderException">
        /// Thrown when an item(order or dish) in the list with entered index is empty
        /// </exception>
        public void Clear(int id)
        {
            if (_listOfOrders[id] == null)
            {
                throw new OrderException("Order not found");
            }
            else if (_listOfOrders[id].ListOfDishOrdered == null)
                throw new OrderException("Order is empty");
            else
            {
                _listOfOrders[id].ListOfDishOrdered.Clear();
            }

        }

    }
}
