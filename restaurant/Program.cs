using System;
using System.Collections.Generic;
using MyRestaurant;
namespace Restaurant_Menu
{
    class Program
    {
        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            try
            {
                Menu menu = CreateTheMenu();
                Restaurant restaurant = new Restaurant ("Sushinori", menu);
                Console.WriteLine(" Welcome to Japan restaurant {0}!", restaurant.NameOfRestaurant);
                Console.WriteLine(" Please, choose category: ");
                Console.WriteLine();
                bool flag = true;
                while (flag)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" 1. Show menu\t   2. Create order\t     3. Add to order\t  4. Remove from order\n 5. Show order\t   6. Complete the order     7. Show All order    8. Exit\t ");
                    Console.WriteLine();
                    try
                    {
                        int pickNumber = Convert.ToInt32(Console.ReadLine());
                        if (pickNumber <= 0 || pickNumber > 8)
                            throw new Exception("Invalid pick number of category. Please, try again");
                        else
                            try
                            {
                                switch (pickNumber)
                                {
                                    case 1:
                                        ShowMenu(restaurant);
                                        break;
                                    case 2:
                                        CreateOrder(restaurant);
                                        break;
                                    case 3:
                                        AddToOrder(restaurant);
                                        break;
                                    case 4:
                                        RemoveInOrder(restaurant);
                                        break;
                                    case 5:
                                        ShowOrder(restaurant);
                                        break;
                                    case 6:
                                        CompleteOrder(restaurant);
                                        break;
                                    case 7:
                                        ShowAllOrders(restaurant);
                                        break;
                                    case 8:
                                        Exit();
                                        break;
                                }
                            }
                            catch (ArgumentException)
                            {
                                ConsoleColor color = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid pick number of category");
                                Console.ForegroundColor = color;
                            }
                    }
                    catch (Exception ex)
                    {
                        ConsoleColor color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = color;
                    }
                }
            }
            catch(RestaruantException ex)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = color;
            }
            finally
            {
                Console.ReadKey();
            }
        }
        static void CreateOrder(Restaurant restaurant)
        {
            Console.WriteLine("");
            restaurant.OpenOrder();
            Console.WriteLine("You create order №{0}", Order.GetId);
            AddToOrder(restaurant, Order.GetId);
        }
        static void ShowOrder(Restaurant restaurant, int id)
        {

            int k = 1;
            if (Order.GetId == 0)
            {
                throw new OrderException("No orders have been created yet. Please create the order.");
            }
            double cost;
            List<Dish> orderTheDish;
            (orderTheDish, cost) = restaurant.ListOfOrders[id].GetListOfDishOrdered();
            Console.WriteLine("Your order №{0}", id + 1);
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Name                    Portion          Cost");
            foreach (Dish i in orderTheDish)
            {
                Console.WriteLine("{0} {1}\t {2}               {3}$", k++, i.Name,  i.Weigth, i.PriseCalculation(i.Weigth));
            }
           
            Console.WriteLine();
            Console.WriteLine("---------------------");
            Console.WriteLine(" Current cost : {0}$ ", cost);
            Console.WriteLine("---------------------");
            Console.ForegroundColor = color;
            Console.WriteLine();
        }
        static void ShowOrder(Restaurant restaurant)
        {
            if (Order.GetId == 0)
            {
                throw new OrderException("No orders have been created yet. Please create the order.");
            }
            Console.WriteLine("Enter id of order:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (id > Order.GetId || id <= 0)
                throw new OrderException("Order is undefined.Please enter a valid number.");
            ShowOrder(restaurant, id-1);
        }
        static void Exit()
        {
            Environment.Exit(0);
        }
        static void RemoveInOrder(Restaurant restaurant)
        {
            if (Order.GetId == 0)
            {
                throw new OrderException("No orders have been created yet. Please create the order.");
            }
            Console.WriteLine("Enter ID order:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (Order.GetId < id || id <= 0)
            {
                throw new OrderException("Order is undefined.Please enter a valid number.");
            }
            ShowOrder(restaurant, id - 1);
            Console.WriteLine("Choose number of dish that you have to remove");
            int index = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You removed \"{0}\" from order №{1}",restaurant.ListOfOrders[id-1].ListOfDishOrdered[index-1].Name, id);
            restaurant.Remove(id - 1, index);
            ShowOrder(restaurant, id - 1);
        }
        static void ShowAllOrders(Restaurant restaurant)
        {
            if (Order.GetId == 0)
            {
                throw new OrderException("No orders have been created yet. Please create the order.");
            }
            int id = Order.GetId;
            Console.WriteLine("All orders:");
            for (int i = 0; i < id; i++)
            {
                try
                {   
                    ShowOrder(restaurant, i);
                    Console.WriteLine();
                }
                catch(OrderException ex)
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.WriteLine("Your order №{0}", i+1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        static void CompleteOrder(Restaurant restaurant)
        {
            if (Order.GetId == 0)
            {
                throw new OrderException("No orders have been created yet. Please create the order.");
            }
            Console.WriteLine("Enter ID order:");
            int id = Convert.ToInt32(Console.ReadLine());
             if (id > Order.GetId || id <= 0) 
             {
                throw new OrderException("Order is undefined.Please enter a valid number.");
             }
            Console.WriteLine();
            GetCheck(restaurant, id - 1);
            restaurant.Clear(id - 1);
            Console.WriteLine();
            Console.WriteLine("Your order is being prepared. Bon Appetit");
        }

        static void GetCheck(Restaurant restaurant, int id)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("                Check");
            Console.WriteLine("******************************************");
            Console.WriteLine("       Restaurant \"{0}\"", restaurant.NameOfRestaurant);
            Console.WriteLine("          {0}", DateTime.Now);
            ShowOrder(restaurant, id);
            Console.WriteLine("******************************************");
            Console.ForegroundColor = color;
        }
        static void AddToOrder(Restaurant restaurant)
        {
            if (Order.GetId == 0)
            {
                throw new OrderException("No orders have been created yet. Please create the order.");
            }
            Console.WriteLine("Enter ID order: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (id > Order.GetId || id<=0)
                throw new OrderException("Order is undefined.Please enter a valid number or create the order.");
            AddToOrder(restaurant, id);
        }
        static int AddDishToOrder(Restaurant restaurant, List<Dish> chooseType)
        {
            string typeOfWeigth;
            if (restaurant.Menu.hotRolls.Equals(chooseType))
            {
                restaurant.Menu.chooseType = restaurant.Menu.hotRolls;
                typeOfWeigth = "pcs";
            }
            else if (restaurant.Menu.rolls.Equals(chooseType))
            {
                restaurant.Menu.chooseType = restaurant.Menu.rolls;
                typeOfWeigth = "pcs";
            }
            else if (restaurant.Menu.dessert.Equals(chooseType))
            {
                restaurant.Menu.chooseType = restaurant.Menu.dessert;
                typeOfWeigth = "kg";
            }
            else if (restaurant.Menu.additives.Equals(chooseType))
            {
                restaurant.Menu.chooseType = restaurant.Menu.additives;
                typeOfWeigth = "pcs";
            }
            else if (restaurant.Menu.drinks.Equals(chooseType))
            {
                restaurant.Menu.chooseType = restaurant.Menu.drinks;
                typeOfWeigth = "l";
            }
            else
                throw new OrderException("Dish undefined");
            int i = 1;
            foreach (Dish meal in restaurant.Menu.chooseType)
            {
                Console.WriteLine("{0} {1} {2}$/{3}", i++, meal.Name, meal.Cost, typeOfWeigth);
            }
            int stop = 0;
            Console.WriteLine("{0}. Cansel", stop);
            Console.WriteLine("Choose the dish : ");
            int  numberOfCategory = Convert.ToInt32(Console.ReadLine());
            if (numberOfCategory == stop)
                return numberOfCategory;
            else if (numberOfCategory < 0 || numberOfCategory > restaurant.Menu.chooseType.Count)
                throw new OrderException("Category undefined");
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("You pick : ");
            Console.WriteLine(restaurant.Menu.chooseType[numberOfCategory - 1].GetListOfIngredients);
            Console.ForegroundColor = color;
            return numberOfCategory;
        }
        static int ChoosePortionOfRolls(Restaurant restaurant, List<Dish> chooseType, int numberOfCategory, int id)
        {
            if (restaurant.Menu.hotRolls.Equals(chooseType))
            {
                restaurant.Menu.chooseType = restaurant.Menu.hotRolls;
            }
            else if (restaurant.Menu.rolls.Equals(chooseType))
            {
                restaurant.Menu.chooseType = restaurant.Menu.rolls;
            }
            Console.WriteLine("Choose portion : ");
            for (int k = 0; k < 2; k++)
            {
                Console.WriteLine($"{k + 1}. {pieces[k]} pcs");
            }
            Console.WriteLine("3. Choose yourself");
            Console.WriteLine("0. Cancle");
            int choosePortion = Convert.ToInt32(Console.ReadLine());
            if (choosePortion > 0 && choosePortion < 3)
            {
                double weight = pieces[choosePortion - 1];
                restaurant.PutOrder(id, restaurant.Menu.chooseType[numberOfCategory - 1], weight);
                Console.WriteLine("Now your order is :");
                ShowOrder(restaurant, id);

            }
            else if (choosePortion == 3)
            {
                Console.WriteLine("Enter the desired number of weigth (pcs)");
                int chooseWeigth = Convert.ToInt32(Console.ReadLine());
                if (chooseWeigth > 0 && chooseWeigth < 50)
                {
                    restaurant.PutOrder(id, restaurant.Menu.chooseType[numberOfCategory - 1], chooseWeigth);
                    Console.WriteLine("Now your order is :");
                    ShowOrder(restaurant, id);
                }
                else
                    throw new OrderException("We can`t cook the weigth You entered.");
            }
            else if (choosePortion>3 || choosePortion < 0)
            {
                throw new OrderException("Portion didnt found");

            }
            return choosePortion;
        }
        static void AddToOrder(Restaurant restaurant, int id)
        {
            id--;
            if (Order.GetId == 0)
            {
                throw new OrderException("No orders have been created yet. Please create the order.");
            }
            Console.WriteLine("Choose menu category :");
            Console.WriteLine("1. Hot-Rolls\t 2. Rolls\t3. Desserts\t 4. Drinks\t 5. Additives\t 6. Canсel");
            int numberOfCategoryOfMenu = Convert.ToInt32(Console.ReadLine());
            if (numberOfCategoryOfMenu <= 0 || numberOfCategoryOfMenu > 6)
            {
                throw new IndexOutOfRangeException("This category does not exist. Please enter a valid number of category");
            }
            switch (numberOfCategoryOfMenu)
            {
                case 1:
                    int numberOfCategory = AddDishToOrder(restaurant, restaurant.Menu.hotRolls);
                    if (numberOfCategory == 0)
                    { break; }
                    int choosePortion = ChoosePortionOfRolls(restaurant, restaurant.Menu.hotRolls, numberOfCategory, id);
                    if (choosePortion == 0)
                    { break; }
                    break;
                case 2:
                    numberOfCategory = AddDishToOrder(restaurant, restaurant.Menu.rolls);
                    if (numberOfCategory == 0)
                    { break; }
                    choosePortion = ChoosePortionOfRolls(restaurant, restaurant.Menu.rolls, numberOfCategory, id);
                    if (choosePortion == 0)
                    { break; }
                    break;
                case 3:
                    numberOfCategory = AddDishToOrder(restaurant, restaurant.Menu.dessert);
                    if (numberOfCategory == 0)
                    { break; }
                    Console.WriteLine("Choose portion : ");
                    for (int k = 0; k < grams.Length; k++)
                    {
                        Console.WriteLine($"{k + 1}. {grams[k] * 1000} g");
                    }
                    Console.WriteLine("3. Choose yourself");
                    Console.WriteLine("0. Cancle");
                    choosePortion = Convert.ToInt32(Console.ReadLine());
                    if (choosePortion > 0 && choosePortion < 3)
                    {
                        double weight = grams[choosePortion - 1];
                        restaurant.PutOrder(id, restaurant.Menu.dessert[numberOfCategory - 1], weight);
                        Console.WriteLine("Now your order is :");
                        ShowOrder(restaurant, id);
                    }
                    else if (choosePortion == 3)
                    {
                        Console.WriteLine("Enter the desired number of weigth (kg)");
                        double chooseWeigth = Convert.ToDouble(Console.ReadLine());
                        if (chooseWeigth > 0 && chooseWeigth < 4)
                        {
                            restaurant.PutOrder(id, restaurant.Menu.dessert[numberOfCategory - 1], chooseWeigth);
                            Console.WriteLine("Now your order is :");
                            ShowOrder(restaurant, id);
                        }
                        else
                            throw new OrderException("We can`t cook the weigth You entered.");
                    }
                    else if (choosePortion == 0)
                    {
                        break;
                    }
                    else
                    {
                        throw new OrderException("Portion didnt found");
                    }
                    break;
                case 4:
                    numberOfCategory = AddDishToOrder(restaurant, restaurant.Menu.drinks);
                    if (numberOfCategory == 0)
                    { break; }
                    Console.WriteLine("Choose portion : ");
                    for (int k = 0; k < litre.Length; k++)
                    {
                        Console.WriteLine($"{k + 1}. {litre[k]} l");
                    }
                    Console.WriteLine("0. Cancle");
                    choosePortion = Convert.ToInt32(Console.ReadLine());
                    if (choosePortion > 0 && choosePortion < 3)
                    {
                        double weight = litre[choosePortion - 1];
                        restaurant.PutOrder(id, restaurant.Menu.drinks[numberOfCategory - 1], weight);
                        Console.WriteLine("Now your order is :");
                        ShowOrder(restaurant, id);
                    }
                    else if (choosePortion == 0)
                    {
                        break;
                    }
                    else
                    {
                        throw new OrderException("Portion didnt found");
                    }
                    break;
                case 5:
                    numberOfCategory = AddDishToOrder(restaurant, restaurant.Menu.additives);
                    if (numberOfCategory == 0)
                    { break; }
                    Console.WriteLine("Choose portion : ");
                    Console.WriteLine("1. 1 pcs ");
                    Console.WriteLine("2. Choose yourself");
                    Console.WriteLine("0. Cancle");
                    choosePortion = Convert.ToInt32(Console.ReadLine());
                    if (choosePortion > 0 && choosePortion < 2)
                    {
                        double weight = pieces[choosePortion - 1];
                        restaurant.PutOrder(id, restaurant.Menu.additives[numberOfCategory - 1], weight);
                        Console.WriteLine("Now your order is :");
                        ShowOrder(restaurant, id);
                    }
                    else if (choosePortion == 2)
                    {
                        Console.WriteLine("Enter the desired number of weigth (pcs)");
                        choosePortion = Convert.ToInt32(Console.ReadLine());
                        if (choosePortion > 0 && choosePortion < 50)
                        {
                            restaurant.PutOrder(id, restaurant.Menu.additives[numberOfCategory - 1], choosePortion);
                            Console.WriteLine("Now your order is :");
                            ShowOrder(restaurant, id);
                        }
                        else
                            throw new OrderException("We can`t cook the weigth You entered.");
                    }
                    else if (choosePortion == 0)
                    {
                        break;
                    }
                    else
                    {
                        throw new OrderException("Portion didnt found");
                    }
                    break;
                case 6:
                    {
                        break;
                    }
            }            
        }

        readonly static double[] litre = new double[2]
        { 0.5, 1};
        readonly static double[] pieces = new double[2]
        { 4.0, 8.0};
        readonly static double[] grams = new double[2]
        { 0.2, 0.45};
        static void ShowMenu(Restaurant restaurant)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("________                                     ..........MENU..........                           ____________________________");
            Console.WriteLine("\nName                                                                                               weight           cost  ");
            Console.WriteLine("____________________________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                                   ~ Rolls ~                                                ");
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 1; i < 4; i++)
            {

                if (i == 2)
                {
                    Console.WriteLine("{0}\t                                 ................................. {1}|{2} pcs\t {3}|{4} $ ", restaurant.Menu.rolls[2 - 1].GetListOfIngredients, pieces[0], pieces[1], restaurant.Menu.rolls[2 - 1].PriseCalculation(pieces[0]), restaurant.Menu.rolls[2 - 1].PriseCalculation(pieces[1]));
                }
                else
                    Console.WriteLine("{0}\t ................................. {1}|{2} pcs\t {3}|{4} $ ", restaurant.Menu.rolls[i - 1].GetListOfIngredients, pieces[0], pieces[1], restaurant.Menu.rolls[i - 1].PriseCalculation(pieces[0]), restaurant.Menu.rolls[i - 1].PriseCalculation(pieces[1]));

            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("____________________________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                                  ~ Hot-Rolls ~                                                   ");
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < 4; i++)
            {
                if (i == 2)
                {
                    Console.WriteLine("{0}\t                 ................................. {1}|{2} pcs\t {3}|{4} $ ", restaurant.Menu.hotRolls[2 - 1].GetListOfIngredients, pieces[0], pieces[1], restaurant.Menu.hotRolls[2 - 1].PriseCalculation(pieces[0]), restaurant.Menu.hotRolls[2 - 1].PriseCalculation(pieces[1]));
                }
                else
                    Console.WriteLine("{0}\t         ................................. {1}|{2} pcs\t {3}|{4} $", restaurant.Menu.hotRolls[i - 1].GetListOfIngredients, pieces[0], pieces[1], restaurant.Menu.hotRolls[i - 1].PriseCalculation(pieces[0]), restaurant.Menu.hotRolls[i - 1].PriseCalculation(pieces[1]));

            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("____________________________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                                  ~ Additives ~");
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < 4; i++)
            {
                if (i == 1)
                {
                    Console.WriteLine("{0}\t                                                 ................................. {1} pcs\t {2} $", restaurant.Menu.additives[1 - 1].GetListOfIngredients, 1, restaurant.Menu.additives[1 - 1].PriseCalculation(1));
                }
                else
                    Console.WriteLine("{0}\t                                                         ................................. {1} pcs\t {2} $ ", restaurant.Menu.additives[i - 1].GetListOfIngredients, 1, restaurant.Menu.additives[i - 1].PriseCalculation(1));

            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("____________________________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                                  ~ Desserts ~");
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < 3; i++)
            {
                if (i == 2)
                {
                    Console.WriteLine("{0}\t                                         ................................. {1}|{2} g\t {3}|{4} $ ", restaurant.Menu.dessert[2 - 1].GetListOfIngredients, grams[0] * 1000, grams[1] * 1000, restaurant.Menu.dessert[2 - 1].PriseCalculation(grams[0]), restaurant.Menu.dessert[2 - 1].PriseCalculation(grams[1]));
                }
                else
                    Console.WriteLine("{0}\t         ................................. {1}|{2} g\t {3}|{4} $ ", restaurant.Menu.dessert[i - 1].GetListOfIngredients, grams[0] * 1000, grams[1] * 1000, restaurant.Menu.dessert[i - 1].PriseCalculation(grams[0]), restaurant.Menu.dessert[i - 1].PriseCalculation(grams[1]));

            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("____________________________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                                   ~ Drinks ~");
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < 5; i++)
            {
                if (i == 4)
                {
                    Console.WriteLine("{0}\t                                                 ................................. {1}|{2} l\t {3}|{4} $", restaurant.Menu.drinks[4 - 1].GetListOfIngredients, litre[0], litre[1], restaurant.Menu.drinks[3].PriseCalculation(litre[0]), restaurant.Menu.drinks[3].PriseCalculation(litre[1]));
                }
                else if (i == 3)
                    Console.WriteLine("{0}\t                                                         ................................. {1}|{2} l\t {3}|{4} $", restaurant.Menu.drinks[3 - 1].GetListOfIngredients, litre[0], litre[1], restaurant.Menu.drinks[1].PriseCalculation(litre[0]), restaurant.Menu.drinks[1].PriseCalculation(litre[1]));
                else if (i == 1)
                    Console.WriteLine("{0}\t                 ................................. {1}|{2} l\t {3}|{4} $", restaurant.Menu.drinks[0].GetListOfIngredients, litre[0], litre[1], restaurant.Menu.drinks[0].PriseCalculation(litre[0]), restaurant.Menu.drinks[0].PriseCalculation(litre[1]));
                else
                    Console.WriteLine("{0}\t         ................................. {1}|{2} l\t {3}|{4} $ ", restaurant.Menu.drinks[i - 1].GetListOfIngredients, litre[0], litre[1], restaurant.Menu.drinks[i - 1].PriseCalculation(litre[0]), restaurant.Menu.drinks[i - 1].PriseCalculation(litre[1]));


            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("____________________________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
        }

        static Menu CreateTheMenu()
        {
            Menu menu = new Menu();
            try
            {
                Ingredient cucumber = new Ingredient("cucumber");
                Ingredient salmon = new Ingredient("salmon");
                Ingredient tomato = new Ingredient("tomato");
                Ingredient fig = new Ingredient("fig");
                Ingredient nori = new Ingredient("nori");
                Ingredient hiyashi = new Ingredient("hiyashi");
                Ingredient avocado = new Ingredient("avocado");
                Ingredient sesame = new Ingredient("sesame");
                Ingredient eel = new Ingredient("eel");
                Ingredient tempura = new Ingredient("tempura");
                Ingredient creamCheese = new Ingredient("cream-cheese");
                Ingredient strawberry = new Ingredient("strawberry");
                Ingredient unagiSauce = new Ingredient("unagi sauce");
                Ingredient risePaper = new Ingredient("rise paper");
                Ingredient banana = new Ingredient("banana");
                Ingredient chocolate = new Ingredient("chocolate");
                Ingredient iceCream = new Ingredient("ice-cream");
                Ingredient ginger = new Ingredient("ginger");
                Ingredient wasabi = new Ingredient("wasabi");
                Ingredient soysauce = new Ingredient("soy sauce");
                Ingredient waterBottle = new Ingredient("water");
                Ingredient whiteWineBottle = new Ingredient("white wine Cavalli Neri Bianco Italiano");
                Ingredient redWineBottle = new Ingredient("red wine Chateau D'Estoublon Les Baux de Provence");
                Ingredient lemon = new Ingredient("lemon");
                Ingredient shrimp = new Ingredient("shrimp");
                Ingredient tuna = new Ingredient("tuna");

                Drinks water = new Drinks("Water Morshincka", 1.3);
                water.AddIngredientToTheDish(waterBottle);

                Drinks waterWithLemon = new Drinks("Water with lemon", 1.5);
                waterWithLemon.AddIngredientToTheDish(waterBottle);
                waterWithLemon.AddIngredientToTheDish(lemon);

                Drinks redWine = new Drinks("Red wine", 5);
                redWine.AddIngredientToTheDish(redWineBottle);

                Drinks whiteWine = new Drinks("White wine", 7);
                whiteWine.AddIngredientToTheDish(whiteWineBottle);

                HotRolls hotRoll = new HotRolls("Tempura", 2.5);
                hotRoll.AddIngredientToTheDish(nori);
                hotRoll.AddIngredientToTheDish(fig);
                hotRoll.AddIngredientToTheDish(shrimp);
                hotRoll.AddIngredientToTheDish(avocado);
                hotRoll.AddIngredientToTheDish(creamCheese);
                hotRoll.AddIngredientToTheDish(tempura);

                HotRolls hotRollWithEel = new HotRolls("Hot-Roll with eel ", 3);
                hotRollWithEel.AddIngredientToTheDish(nori);
                hotRollWithEel.AddIngredientToTheDish(fig);
                hotRollWithEel.AddIngredientToTheDish(avocado);
                hotRollWithEel.AddIngredientToTheDish(creamCheese);
                hotRollWithEel.AddIngredientToTheDish(tempura);
                hotRollWithEel.AddIngredientToTheDish(eel);

                HotRolls tempuraEbbi = new HotRolls("Tempura Ebbi", 3.5);
                tempuraEbbi.AddIngredientToTheDish(nori);
                tempuraEbbi.AddIngredientToTheDish(fig);
                tempuraEbbi.AddIngredientToTheDish(tomato);
                tempuraEbbi.AddIngredientToTheDish(cucumber);
                tempuraEbbi.AddIngredientToTheDish(creamCheese);
                tempuraEbbi.AddIngredientToTheDish(tempura);

                Rolls nessiRoll = new Rolls("Nessi", 2);
                nessiRoll.AddIngredientToTheDish(nori);
                nessiRoll.AddIngredientToTheDish(fig);
                nessiRoll.AddIngredientToTheDish(shrimp);
                nessiRoll.AddIngredientToTheDish(avocado);

                Rolls hiyashiRoll = new Rolls("Hiyashi Roll", 2.5);
                hiyashiRoll.AddIngredientToTheDish(nori);
                hiyashiRoll.AddIngredientToTheDish(fig);
                hiyashiRoll.AddIngredientToTheDish(salmon);
                hiyashiRoll.AddIngredientToTheDish(hiyashi);
                hiyashiRoll.AddIngredientToTheDish(creamCheese);
                hiyashiRoll.AddIngredientToTheDish(cucumber);
                hiyashiRoll.AddIngredientToTheDish(sesame);

                Rolls toriRoll = new Rolls("Tori Roll", 4);
                toriRoll.AddIngredientToTheDish(nori);
                toriRoll.AddIngredientToTheDish(fig);
                toriRoll.AddIngredientToTheDish(cucumber);
                toriRoll.AddIngredientToTheDish(unagiSauce);
                toriRoll.AddIngredientToTheDish(tomato);

                Rolls deLuxWithTuna = new Rolls("de luxure with Tuna ", 3);
                deLuxWithTuna.AddIngredientToTheDish(nori);
                deLuxWithTuna.AddIngredientToTheDish(fig);
                deLuxWithTuna.AddIngredientToTheDish(tuna);
                deLuxWithTuna.AddIngredientToTheDish(avocado);
                deLuxWithTuna.AddIngredientToTheDish(creamCheese);
                deLuxWithTuna.AddIngredientToTheDish(sesame);
                deLuxWithTuna.AddIngredientToTheDish(unagiSauce);

                Dessert rollWIthFruits = new Dessert("Sweet roll with fruits", 55);
                rollWIthFruits.AddIngredientToTheDish(risePaper);
                rollWIthFruits.AddIngredientToTheDish(banana);
                rollWIthFruits.AddIngredientToTheDish(strawberry);
                rollWIthFruits.AddIngredientToTheDish(chocolate);
                rollWIthFruits.AddIngredientToTheDish(creamCheese);

                Dessert iceCreamWithStrawberry = new Dessert("Ice-Cream with strawberry", 33);
                iceCreamWithStrawberry.AddIngredientToTheDish(iceCream);
                iceCreamWithStrawberry.AddIngredientToTheDish(strawberry);

                Additives soySauce = new Additives("Soy sauce", 0.5);
                soySauce.AddIngredientToTheDish(soysauce);
                Additives marinatedGinger = new Additives("Marinated ginger", 0.5);
                marinatedGinger.AddIngredientToTheDish(ginger);
                Additives wasabiSauce = new Additives("Wasabi", 0.5);
                wasabiSauce.AddIngredientToTheDish(wasabi);
                try
                {
                    menu.AddDishToMenu(deLuxWithTuna);
                    menu.AddDishToMenu(hotRoll);
                    menu.AddDishToMenu(hotRollWithEel);
                    menu.AddDishToMenu(whiteWine);
                    menu.AddDishToMenu(redWine);
                    menu.AddDishToMenu(water);
                    menu.AddDishToMenu(waterWithLemon);
                    menu.AddDishToMenu(rollWIthFruits);
                    menu.AddDishToMenu(iceCreamWithStrawberry);
                    menu.AddDishToMenu(tempuraEbbi);
                    menu.AddDishToMenu(nessiRoll);
                    menu.AddDishToMenu(hiyashiRoll);
                    menu.AddDishToMenu(soySauce);
                    menu.AddDishToMenu(wasabiSauce);
                    menu.AddDishToMenu(marinatedGinger);
                }
                catch (MenuException ex)
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = color;
            }
             return menu;
        }
    }
}