using System;
namespace MyRestaurant
{
    public class Dessert : Dish
    {
        public Dessert(string _name, double _cost) : base(_name, _cost)
        { 
             _typeOfDish = "Dessert";
            _listOfIngredients =  Name + ":\n";
        }
    }
}
