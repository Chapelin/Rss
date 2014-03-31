using System;
using System.Threading.Tasks;

namespace ServiceRssToDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new ScrappersManager();
            manager.InitListeScrap();
            Task.Factory.StartNew(manager.Scrap);
            Console.ReadLine();
           

        }
    }

  
}
