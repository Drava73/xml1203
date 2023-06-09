﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace xml1203
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var order = new SortedDictionary<string, int>();

            for (bool unend_order = true; unend_order;)
            {
                Console.Clear();
                Console.WriteLine("\n\n\tХотите добавить что-то к заказу?");
                Console.WriteLine("\t(Y - Да; N - нет;)");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y: 
                        {
                            Console.Clear();
                            Console.Write("\n\n\tВведи название предмета для покупки: ");
                            string think_name = Console.ReadLine();
                            Console.Write("\tВведи количество: ");
                            int think_count = Convert.ToInt32(Console.ReadLine());

                            try
                            {
                                order[think_name] += think_count;
                            }
                            catch (Exception e)
                            {
                                if (e is KeyNotFoundException)
                                {
                                    order.Add(think_name, think_count);
                                }
                                else throw e;
                            }

                            break;
                        }

                    case ConsoleKey.N: 
                        {
                            unend_order = false;
                            break;
                        }
                }
            }
            Console.Clear();
            var xDoc = new XmlDocument();

            {
                var xRoot = xDoc.CreateElement("Order");
                xDoc.AppendChild(xRoot);
                foreach (var item in order)
                {
                    var userNode = xDoc.CreateElement(item.Key.Replace(" ", "_"));
                    var attribute = xDoc.CreateAttribute("count");
                    attribute.Value = item.Value.ToString();
                    userNode.Attributes.Append(attribute);
                    xRoot.AppendChild(userNode);
                }
                xDoc.Save("Order.xml");
            }
            {
                xDoc.Load("Order.xml");
                XmlElement xRoot = xDoc.DocumentElement;
                Console.WriteLine("\n\tOrder: ");
                foreach (XmlElement node in xRoot)
                {
                    Console.WriteLine($"\t  {node.Name} - {node.Attributes.GetNamedItem("count").Value}");
                }
            }

            Console.ReadKey();
        }
    }
}
