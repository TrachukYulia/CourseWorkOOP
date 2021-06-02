using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant
{
    public class Rolls : Dish
    {
        public Rolls(string _name, double _cost) : base(_name, _cost)
        {

            _typeOfDish = "Rolls";
            _listOfIngredients = Name + ":\n ";
        }
    }     
}
