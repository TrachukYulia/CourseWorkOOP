using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant
{ 
    public class Drinks : Dish
    {
        public Drinks(string _nameOfDish, double _cost) : base(_nameOfDish, _cost)
        { 
            _typeOfDish = "Drinks"; 
            _listOfIngredients =  Name + ":\n "; 
        }
     
    }
}
