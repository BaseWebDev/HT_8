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
            customLists.RemoveAt(1);
            Console.WriteLine("Кол-во элементов: " + customLists.Count);
            int k = 0;
            foreach (var customList in customLists) {
                Console.WriteLine("Элемент списка " + k++);
                foreach (var item in customList) {
                    Console.Write("\t"+ item);
                }             
                Console.WriteLine();
            }
           
        }
    }
}
