using System;

namespace MyRestaurant
{
  public class HotRolls : Dish
    {
        public HotRolls(string _name, double _cost) : base(_name, _cost)
        { 
            _typeOfDish = "Hot-Rolls"; 
            _listOfIngredients = Name + ":\n";
        }
    
    }
}
