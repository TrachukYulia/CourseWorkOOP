using System;
using System.Collections.Generic;


namespace MyRestaurant
{
    /// <summary>
    /// The Menu class.
    /// Contains list of all type of dish and method for add dish to menu.
    /// </summary>
    public class Menu
    {
        public List<Dish> hotRolls = new List<Dish>();
        public List<Dish> dessert = new List<Dish>();
        public List<Dish> rolls = new List<Dish>();
        public List<Dish> drinks = new List<Dish>();
        public List<Dish> additives = new List<Dish>();
        public List<Dish> chooseType = new List<Dish>();

        /// <exception cref="System.MenuException">
        /// Thrown when entered data doesnt exist 
        /// </exception>
        public void AddDishToMenu(Dish dish)
        {
            if (dish != null)
            {
                if (dish.TypeOfDish == "Hot-Rolls")
                    hotRolls.Add(dish);
                else
                    if (dish.TypeOfDish == "Rolls")
                    rolls.Add(dish);
                else
                    if (dish.TypeOfDish == "Drinks")
                    drinks.Add(dish);
                else
                    if (dish.TypeOfDish == "Dessert")
                    dessert.Add(dish);
                else
                     if (dish.TypeOfDish == "Additives")
                    additives.Add(dish);
                else
                    throw new MenuException("This type of dish not found");
            }
            else
                throw new MenuException("Dish not introduced");
        }
    }
}
