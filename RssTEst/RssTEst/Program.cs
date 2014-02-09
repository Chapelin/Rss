using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Data.SQLite;
using Readers;
using RssEntity;

namespace RssTEst
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var ctx = new RssContext())
            {
                try
                {
                    Categorie ct = new Categorie() { ID = 1, Description = "Test" };

                    ctx.Categories.Add(ct);
                    ctx.SaveChanges();
                    Console.WriteLine("Test");
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.GetType());
                    Console.WriteLine(e.InnerException);
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    Console.Read();
                }
               

            }

        }
    }
}