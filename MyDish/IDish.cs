using MyRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant
{
    public interface IDish
    {
        void AddIngredientToTheDish(Ingredient i);
        double PriseCalculation();
        double PriseCalculation(double weigth);
        string GetListOfIngredients { get; }
        string TypeOfDish {get;}
        double Weigth { get; set; }
        string Name {get; }
        double Cost { get; }
    }
}
