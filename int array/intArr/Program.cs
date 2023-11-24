Console.WriteLine("Введите количество элементов массива:");
int n = int.Parse(Console.ReadLine());
Lib.Class1 myArray = new Lib.Class1(n);
MenuInputArr(ref myArray);
bool end = false;
while (!end)
{
    Console.WriteLine("Выберете операцию над массивом\n" +
        "1) Print\n" +
        "2) FindValue\n" +
        "3) DeleteValue\n" +
        "4) FindMax\n" +
        "5) Add\n" +
        "6) Sort\n" +
        "0) Выход");
    string method = Console.ReadLine();
    switch (method)
    {
        case "1":
            int startInd, endInd;
            Console.Write("Индекс первого элемента: ");
            startInd = int.Parse(Console.ReadLine());
            Console.Write("Индекс последнего элемента: ");
            endInd = int.Parse(Console.ReadLine());
            myArray.Print(startInd, endInd);
            break;
        case "2":
            Console.Write("Укажите элемент для поиска: ");
            int valueToFind = int.Parse(Console.ReadLine());
            var res = myArray.findValue(valueToFind);
            Console.Write($"Элемент {valueToFind} содержится под индексами: ");
            Console.WriteLine(string.Join(", ", res));
            break;
        case "3":
            Console.WriteLine("Укажите элемент для удаления: ");
            int valueToDelete = int.Parse(Console.ReadLine());
            myArray.delValue(valueToDelete);
            Console.WriteLine(myArray.n);
            break;
        case "4":
            var max = myArray.findMax();
            Console.WriteLine($"Максимальный элемент в массиве: {max}");
            break;
        case "5":
            Console.WriteLine("Создайте второй массив");
            Lib.Class1 secondArray = new Lib.Class1(myArray.n);
            MenuInputArr(ref secondArray);
            secondArray.Print(0, myArray.n-1);
            myArray.Add(ref myArray.arr, secondArray.arr);
            Console.Write("Результат сложения двух массивов: ");
            myArray.Print(0, myArray.n-1);
            break;
        case "6":
            myArray.Sort(ref myArray.arr);
            Console.Write("Результат сортировки массива: ");
            myArray.Print(0, myArray.n-1);
            break;
        case "0":
            end = true;
            break;
        default: 
            Console.WriteLine("Введите корректный пункт меню");
            break;
    }
}

void MenuInputArr(ref Lib.Class1 array)
{
    bool ArrIsEmpty = true;
    while (ArrIsEmpty)
    {
        Console.WriteLine("Выберете способ задания элементов массива:\n" +
        "1) Пользовательские\n" +
        "2) Случайные");
        string method = Console.ReadLine();
        switch (method)
        {
            case "1":
                Console.WriteLine("Введите значения массива через пробел: ");
                int[] input = Console.ReadLine().Split(' ').Select(it => Convert.ToInt32(it)).ToArray();
                array.Input(ref array.arr, input);
                ArrIsEmpty = false;
                break;
            case "2":
                array.RandomInput(ref array.arr);
                ArrIsEmpty = false;
                break;
            default:
                Console.WriteLine("Выберете корректный пункт меню");
                break;
        }
    }
}