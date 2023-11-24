Random rnd = new Random();
List<Lib.Baker> bakers = new List<Lib.Baker>() { new Lib.Baker(0, 3), new Lib.Baker(1, 6), new Lib.Baker(2, 6)};

List<Lib.Courier> couriers = new List<Lib.Courier>() { new Lib.Courier(0,1,1), new Lib.Courier(1,2,2)  };

List<Lib.Order> orders = new List<Lib.Order>() { new Lib.Order(0, TimeSpan.FromMinutes(120)), new Lib.Order(1, TimeSpan.FromMinutes(120)),
new Lib.Order(2, TimeSpan.FromMinutes(120)), new Lib.Order(3, TimeSpan.FromMinutes(120)), new Lib.Order(4, TimeSpan.FromMinutes(120)), 
new Lib.Order(5, TimeSpan.FromMinutes(120)), new Lib.Order(6, TimeSpan.FromMinutes(120)), new Lib.Order(7, TimeSpan.FromMinutes(120))};
            
Lib.Pizzeria pizzeria = new Lib.Pizzeria(orders, bakers, couriers, 5);

pizzeria.ProcessingOrders();
pizzeria.GiveReport();