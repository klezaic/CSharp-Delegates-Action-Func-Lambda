using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegati
{
    
    public class Objekt1
    {
        public delegate int IncrementDelegate(int i);

        private IncrementDelegate IncrementFunction;

        private int broj;
        
        public Objekt1(IncrementDelegate function)
        {
            IncrementFunction = function;
        }

        public int getBroj()
        {
            return broj;
        }

        public void setBroj(int x)
        {
            broj = IncrementFunction(x);
        }

    }

    public class Objekt2
    {
        private Action<Objekt2> IncrementAction;

        private int broj;

        public Objekt2(Action<Objekt2> IncrementAction)
        {
            this.IncrementAction = IncrementAction;
        }

        public int getBroj()
        {
            return broj;
        }
        public void printBroj()
        {
            IncrementAction(this);
        }
        public void setBroj(int x)
        {
            broj = x;
        }
    }

    public class Objekt3
    {
        private Func<int, int> IncrementFunc;

        private int broj;

        public Objekt3(Func<int, int> foo)
        {
            IncrementFunc = foo;
        }

        public int getBroj()
        {
            return broj;
        }

        public void setBroj(int x)
        {
            broj = IncrementFunc(x);
        }
    }

    public static class HelperMetode
    {
        //Func<int, int>
        public static int IncrementFunction(int a)
        {
            return a + 1;
        }

        public static string ToCapsLock(string x)
        {
            return x.ToUpper();
        }

        public static bool Comparator(string original, string pattern)
        {
            return original.Contains(pattern);
        }

        //Mutate Func za promjene na listi (ForEach ne smije mijenjat listu)
        public static void MutateEach<T>(this List<T> list, Func<T, T> mutatorFunction)
        {
            int count = list.Count;
            for (int n = 0; n < count; n++)
                list[n] = mutatorFunction(list[n]);
        }

        //Mutate Func za promjenu samo odabranih clanova po filter funkciji i patternu
        public static void MutateWithFilter(this List<string> list, Func<string, string, bool> filter, string pattern, Func<string, string> mutatorFunction)
        {
            int count = list.Count;
            for (int n = 0; n < count; n++)
                if(filter(list[n], pattern))
                    list[n] = mutatorFunction(list[n]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Delegate
            Console.WriteLine("Uvecavanje varijable putem delegate int: ");
            Objekt1 o1 = new Objekt1((int x) => { return x + 1; });
            o1.setBroj(10);
            Console.WriteLine(o1.getBroj());

            //Action
            Console.WriteLine("Ispis varijable putem Action<int>: ");
            Objekt2 o2 = new Objekt2((Objekt2 o) => { Console.WriteLine(o.getBroj() + 1); });
            o2.setBroj(20);
            o2.printBroj();

            //Func
            Console.WriteLine("Uvecavanje varijable putem Func<int, int>: ");
            Objekt3 o3 = new Objekt3(HelperMetode.IncrementFunction);
            o3.setBroj(30);
            Console.WriteLine(o3.getBroj());

            //Lambda
            Console.WriteLine("Ispis mutirane liste Int-ova: ");
            List<int> lista = new List<int>();
            for (int i = 0; i < 5; i++)
                lista.Add(i*100);
            HelperMetode.MutateEach<int>(lista, HelperMetode.IncrementFunction);
            foreach (var broj in lista)
                Console.WriteLine(broj);

            //Lambda2
            Console.WriteLine("Ispis mutirane liste String-ova: ");
            List<string> lista2 = new List<string>();
            for (int i = 0; i < 5; i++)
                lista2.Add("Ovo Je String !!!");
            HelperMetode.MutateEach<string>(lista2, HelperMetode.ToCapsLock);
            foreach (var niz in lista2)
                Console.WriteLine(niz);

            //Lambda3
            Console.WriteLine("Ispis mutirane liste odabranih String-ova: ");
            List<string> lista3 = new List<string>();
            lista3.Add("Pero");
            lista3.Add("Ivan");
            lista3.Add("Marko");
            lista3.Add("Iva");
            lista3.Add("Petra");

            HelperMetode.MutateWithFilter(lista3, HelperMetode.Comparator, "Iva", HelperMetode.ToCapsLock);
            foreach (var niz in lista3)
                Console.WriteLine(niz);
        }
    }



}
