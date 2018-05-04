using System;

namespace SimpleListArray
{
    class Program
    {
        static void Main(string[] args)
        {
           
            CustomList<int> customLists = new CustomList<int>();
            customLists.Add();
            customLists.Add(10);
            customLists.Add(20,30);
            customLists.Add(new int[] {40,50,60});
            try {
                customLists.RemoveAt(1);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return;
            }
            //Console.WriteLine("Кол-во элементов: " + customLists.Count);
            //int k = 0;
            //foreach (var customList in customLists) {
            //    Console.WriteLine("Элемент списка " + k++);
            //    foreach (var item in customList) {
            //        Console.Write("\t"+ item);
            //    }             
            //    Console.WriteLine();
            //}
            customLists.OnAdding += Print;
            customLists.OnAdded += Print;
            customLists.Add(70, 80);
            customLists.Insert(20, new int[] { 90 });
            // Добавим метод для обработки удаления по индексу, используем лямбда-выражение
            customLists.onRemovingAt = x => Console.WriteLine("Удаляем значение с индексом: "+x);
            // Создаем локальный стандратный делегат и добавляем в него метод
            Action<int> operation = customLists.RemoveAt;
            // Вызываем событие
            operation(2);
            // Регистрируем события на удаление
            customLists.OnRemoving += Print;
            customLists.OnRemoved += Print;
            // Ссылки удаляет, а массивы нет надо реализовать IEqutable<T[]>
            var A = new int[] { 70, 80 };
            customLists.Add(A);
            Console.WriteLine( customLists.Remove(A));
            Console.WriteLine(customLists.Remove(new int[] { 70, 80 }));
            customLists.Clear();
            customLists.OnUpdated += Print;
            customLists.Add(A);
        }
        static void Print(object sender, SimpleListChangingEventArgs<int> args) {
            Console.WriteLine("До изменения CustomList: ");
            CustomList<int> tempLists = sender as CustomList<int>;
            if (tempLists != null) {
                int k = 0;           
                foreach (var tempList in tempLists) {
                    Console.Write("Элементы списка " + k++ + ": ");
                    foreach (var item in tempList) {
                        Console.Write("\t" + item);
                    }
                    Console.WriteLine();
                }
            }
            if (args.Value != null) {
                Console.WriteLine("Передаваемые аргументы CustomList: ");        
                for (int i = 0; i < args.Value.Length; i++) {
                    Console.Write("\t" + args.Value[i]);
                    if (args.Value[i] > 80) {
                        args.Cancel = true;
                    }
                }           
                Console.WriteLine();
                Console.WriteLine();
            }

        }
        static void Print(object sender, SimpleListChangedEventArgs<int> args) {
            Console.WriteLine("После изменения CustomList: ");
            CustomList<int> tempLists = sender as CustomList<int>;
            if (tempLists != null) {
                int k = 0;
                foreach (var tempList in tempLists) {
                    Console.Write("Элементы списка " + k++ + ": ");
                    foreach (var item in tempList) {
                        Console.Write("\t" + item);
                    }
                    Console.WriteLine();
                }
            }
            if (args.Value != null) {
                Console.WriteLine("Передаваемые аргументы CustomList: ");
                for (int i = 0; i < args.Value.Length; i++) {
                    Console.Write("\t" + args.Value[i]);
                }            
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
