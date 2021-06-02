using System;
using System.Collections.Generic;

namespace MyRestaurant
{
    /// <summary>
    /// The abcstract Dish class.
    /// Contains all methods for create dish.
    /// </summary>
    public abstract class Dish: IDish
    {
        protected string _listOfIngredients;
        protected string _typeOfDish;
        protected double _cost;
        protected string _name;
        protected double _weigth;
        List<Ingredient> ingredients = new List<Ingredient>();

        /// <exception cref="System.DishException">
        /// Thrown when the entered data is invalid
        /// </exception>
        /// /// <summary>
        public Dish(string name, double cost)
        { if (cost <= 0)
                throw new DishException("Cost cant be not positive", nameof(cost));
            _cost = cost;
            _name = name;
        }
        public List<Ingredient>  Ingredients => ingredients;
        public string TypeOfDish => _typeOfDish;
        /// <exception cref="System.DishException">
        /// Thrown when the entered data is invalid
        /// </exception>
        public double Weigth
        {
            set
            {
                if (value > 0)
                    _weigth = value;
                else
                    throw new DishException("Invalid weigth");
            }
            get
            {
                if(_weigth > 0)
                    return _weigth;
                else
                    throw new DishException("Invalid weigth");
            }
        }
        public string Name => _name;
        /// <exception cref="System.DishException">
        /// Thrown when the entered data is null
        /// </exception>
        public string GetListOfIngredients
        {
            get
            {
                if (_listOfIngredients == null)
                {
                    throw new DishException("Empty dish");
                }
                _listOfIngredients = _listOfIngredients.TrimEnd(' ');
                _listOfIngredients = _listOfIngredients.TrimEnd(',');
                return _listOfIngredients;
            }
        }
        /// <exception cref="System.DishException">
        /// Thrown when the entered data is invalid
        /// </exception>
        public double Cost => _cost;    
        public void AddIngredientToTheDish(Ingredient i)
        {
            if (i != null)
            {
                ingredients.Add(i);
                _listOfIngredients += i.Name + ", ";
            }
            else
                throw new DishException("Ingredient didnt found");
        }
        public double PriseCalculation()
        {
            double sum = Weigth * Cost;
            return sum;
        }
        public double PriseCalculation(double weigth)
        {
            double sum = weigth * Cost;
            return sum;
        }
    }
    
}
