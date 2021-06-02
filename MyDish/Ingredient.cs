using System;
using System.Collections.Generic;
namespace MyRestaurant
{
    /// <summary>
    /// The class of Ingredient.
    /// Contains constructor and property for create ingredients and get them
    /// </summary>
    public class Ingredient
    {
        public Ingredient(string name)
        {
            _name = name;
        }
        private string _name;
        public string Name => _name;
     }
}
