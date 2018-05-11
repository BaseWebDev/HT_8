using System;

namespace SimpleListArray
{
    class Program
    {
        static void Main(string[] args)
        {
           
            CustomList<int> customLists = new CustomList<int>();
            customLists.OnAdding += Print;
            customLists.OnAdded += Print;
            // Регистрируем события на удаление
            customLists.OnRemoving += Print;
            // Регистрируем события на обновление
            customLists.OnUpdated += Print;
            // Создаем локальный стандратный делегат и добавляем в него метод
            Action<int> operation = customLists.RemoveAt;
            Console.WriteLine("***Add*** ");
            customLists.Add();
            customLists.Add(10);
            customLists.Add(new int[] {20,30, 40, 50});
            
           
            
            try {
                Console.WriteLine("***RemoveAt***");
                // Вызываем событие
                operation(2);
                Console.WriteLine("***Set by index***");
                customLists[0] = 15;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("***Insert***");
            customLists.Insert(2, 50);
            Console.WriteLine("***Clear***");
            customLists.Clear();
            
        }
        static void Print(object sender, SimpleListChangingEventArgs<int> args) {
            Console.WriteLine("До изменения CustomList: ");
            CustomList<int> tempLists = sender as CustomList<int>;
            if (tempLists != null) {
                foreach (var tempList in tempLists) {
                    Console.Write("\t" + tempList);                     
                }
            }
            Console.WriteLine();
            Console.WriteLine("Передаваемые аргументы CustomList: ");        
            Console.Write("\t" + args.Value);
            if (args.Value > 80) {
                args.Cancel = true;
            }
            Console.WriteLine();
        }
        static void Print(object sender, SimpleListChangedEventArgs<int> args) {
            Console.WriteLine("После изменения CustomList: ");
            CustomList<int> tempLists = sender as CustomList<int>;
            if (tempLists != null) {
                foreach (var tempList in tempLists) {
                    Console.Write("\t" + tempList);                      
                }
            }
            Console.WriteLine();
            Console.WriteLine("Передаваемые аргументы CustomList: ");
                Console.Write("\t" + args.Value);
                Console.WriteLine();
        }
    }
}
