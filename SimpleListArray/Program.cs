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
        //    customLists.OnAdded += Print;
            customLists.Add(20, 30);
        }
        static void Print(object sender, SimpleListAddingEventArgs<int> args) {
            CustomList<int> tempLists = sender as CustomList<int>;
            if (tempLists != null) {
                int k = 0;
                Console.WriteLine("До изменения CustomList: ");
                foreach (var tempList in tempLists) {
                    Console.WriteLine("Элемент списка " + k++);
                    foreach (var item in tempList) {
                        Console.Write("\t" + item);
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Добавляемые элементы CustomList: ");
            for (int i = 0; i < args.Value.Length; i++) {
                Console.Write("\t" + args.Value[i]);
            }
            Console.WriteLine();
        }
        static void Print(object sender, SimpleListAddedEventArgs<int> args) {
            for (int i =0; i< args.Value.Length; i++) {
                Console.Write("\t"+args.Value[i]);
            }
            Console.WriteLine();
        }
    }
}
