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
            // Добавим метод для обработки удаления по индексу, используем лямбда-выражение
            customLists.onRemovingAt = x => Console.WriteLine("Удаляем значение с индексом: " + x);
            // Создаем локальный стандратный делегат и добавляем в него метод
            Action<int> operation = customLists.RemoveAt;
            
            customLists.Add();
            customLists.Add(10);
            customLists.Add(new int[] {20,30});
            customLists.Add(new int[] { 40, 50 });
            // Вызываем событие
            operation(2);
            try {
                customLists.RemoveAt(1);
                customLists[0] = 15;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return;
            }
            customLists.Insert(2, 50);
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
