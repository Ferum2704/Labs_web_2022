using System;
using DictionaryLib;
using LinkedListLibrary;
using System.Collections;

namespace lab1_asp
{
    class Program
    {
        static void Main(string[] args)
        {
            OwnDictionary<int?, string> MyDictionary = new OwnDictionary<int?, string>(5);
            MyDictionary.Added += Handlers.AddItemHandler;
            MyDictionary.Removed += Handlers.RemoveItemHandler;
            MyDictionary.Cleared += Handlers.ClearDictionaryHandler;
            try
            {
                MyDictionary.Add(1, "один");
                MyDictionary.Add(2, "два");
                MyDictionary.Add(3, "три");
                MyDictionary.Add(4, "чотири");
                MyDictionary.Add(11, "ОДИНАДЦЯТЬ");
                MyDictionary.Add(12, "ДВАНАДЦЯТЬ");
                DictionaryOutput(MyDictionary);
                MyDictionary.Remove(12);
                if (!MyDictionary.ContainsValue("три"))
                {
                    Console.WriteLine("Item with value 'три' is not found");
                }
                MyDictionary[3] = "пять";
                DictionaryOutput(MyDictionary);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            MyDictionary.Clear();
        }
        static void DictionaryOutput(IEnumerable dictionary)
        {
            foreach (var dictItem in dictionary)
            {
                Console.WriteLine(dictItem);
            }
        }
    }
}
