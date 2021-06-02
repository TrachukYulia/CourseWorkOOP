using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant
{
    public interface IOrder
    {
        void PutDish(Dish _meal, double weight);
        void Remove(Dish _meal);
        List<Dish> ListOfDishOrdered { get; }
        (List<Dish> orderTheDish, double cost) GetListOfDishOrdered();
    }
}
