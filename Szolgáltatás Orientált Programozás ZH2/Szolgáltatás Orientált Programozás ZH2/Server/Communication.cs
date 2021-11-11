using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.IO;
using System.Net.Sockets;

namespace Server
{
    class Communication
    {
        // User
        bool logged = false; 
        string user = string.Empty;
        bool admin = false;
        // Stream
        StreamReader SR = null;
        StreamWriter SW = null;
        // TCP
        TcpClient incoming = null;
        // LIST
        public static List<Pizza> pizzas = new List<Pizza>();

        public Communication(TcpClient client) 
        {
            incoming = client;
            SR = new StreamReader(incoming.GetStream(), Encoding.UTF8);
            SW = new StreamWriter(incoming.GetStream(), Encoding.UTF8);
        }

        public void StartCommunication()
        {
            Console.WriteLine("Client Connected!");
            bool end = false;
            string find = string.Empty;
            string[] findparameter;
            SW.WriteLine("Welcome!");
            SW.Flush();
            while (!end)
            {
                try
                {
                    find = SR.ReadLine();
                    findparameter = find.Split('|');
                    switch (findparameter[0])
                    {
                        case "HELP":
                            SW.WriteLine("Ok*");
                            SW.Flush();
                            Help();
                            SW.WriteLine("Ok!");
                            break;

                        case "LOGIN":
                            LogIn(findparameter[1], findparameter[2]);
                            SW.WriteLine("Welcome, {0}!", findparameter[1]);
                            break;

                        case "LOGOUT":
                            SW.WriteLine("Goodbye, {0}!", user);
                            user = null;
                            logged = false;
                            break;

                        case "LIST":
                            if (logged)
                            {
                                SW.WriteLine("Ok*");
                                SW.Flush();
                                List();
                                SW.WriteLine("Ok!");
                            }
                            else
                            {
                                SW.WriteLine("Log in please.");
                            }
                            break;

                        case "ADD":
                            if (logged)
                            {
                                if(admin)
                                {
                                    AddPizza(int.Parse(findparameter[1]), int.Parse(findparameter[2]), int.Parse(findparameter[3]), bool.Parse(findparameter[4]), findparameter[5]);
                                }
                            }
                            else
                            {
                                SW.WriteLine("Log in please.");
                            }
                            break;

                        case "ORDER":
                            Order(int.Parse(findparameter[1]));
                            break;

                        case "RETURN":
                            Return(int.Parse(findparameter[1]));
                            break;

                        case "EDIT":
                            Edit(int.Parse(findparameter[1]), int.Parse(findparameter[2]), int.Parse(findparameter[3]), bool.Parse(findparameter[4]), findparameter[5]);
                            break;

                        case "DC":
                            end = true;
                            SW.WriteLine("dc");
                            break;

                        default:
                            SW.WriteLine("Invalid Command. Try 'HELP'");
                            break;
                    }
                    SW.Flush();
                }
                catch (Exception e)
                {
                    if (incoming.Connected)
                    {
                        SW.WriteLine("ERR|{0}", e.Message);
                        SW.Flush();
                    }
                    else
                        end = true;
                }
            }
            Console.WriteLine("A Client has disconnected.");
        }

        public void Help()
        {
            SW.WriteLine("Disconnect: DC");
            SW.WriteLine("Login: LOGIN|USERNAME|PASSWORD");
            SW.WriteLine("Logout: LOGOUT");
            SW.WriteLine("List pizzas: LIST");
            SW.WriteLine("Add pizza: ADD|TYPE|PRICE|SIZE|VEGETARIAN|COMPANY ");
            SW.WriteLine("Order pizza: ORDER|PIZZAID");
            SW.WriteLine("Return pizza: RETURN|PIZZAID ");
            SW.WriteLine("Edit pizza: EDIT|PIZZAID|NEWTYPE|NEWPRICE|NEWSIZE|NEWVEGETARIAN|NEWCOMPANY ");
            SW.WriteLine("Pizza types:");
            SW.WriteLine("NEPOLIAN : 0");
            SW.WriteLine("CHICAGO : 1");
            SW.WriteLine("SICILIAN : 2");
            SW.WriteLine("GREEK : 3");
            SW.WriteLine("CALIFORNIA : 4");
            SW.WriteLine("DETROIT : 5");
            SW.Flush();
        }

        public void LogIn(string username, string password)
        {
            user = username;
            if(user == "ADMIN")
            {
                if(password == "ADMIN")
                {
                    admin = true;
                }
            }
            logged = true;
        }

        public void List()
        {
            foreach (Pizza p in pizzas)
                SW.WriteLine(p);
            SW.Flush();
        }

        public void AddPizza(int type, int price, int size, bool vegetarian, string company)
        {
            PizzaType pt = (PizzaType)type;
            Pizza pizza = new Pizza(price, pt, size, vegetarian, company, delivery:false, orderer:null);
            pizzas.Add(pizza);
            SW.WriteLine("Pizza added!");
        }

        public void Order(int id)
        {
            foreach (Pizza p in pizzas)
            {
                if (p.Id == id)
                {
                    if (p.Delivery != true)
                    {
                        p.Delivery = true;
                        p.Orderer = user;
                        SW.WriteLine("Pizza successfully ordered");
                        return;
                    }
                }
            }
        }

        public void Return(int id)
        {
            foreach (Pizza p in pizzas)
            {
                if (p.Id == id)
                {
                    if (p.Delivery == true && user == p.Orderer)
                    {
                        p.Delivery = false;
                        SW.WriteLine("Pizza returned");
                        return;
                    }
                }
            }
        }

        public void Edit(int id, int type, int price, int size, bool vegetarian, string company)
        {
            foreach (Pizza p in pizzas)
            {
                if (p.Id == id)
                {
                    if (p.Delivery != true)
                    {
                        p.Type = (PizzaType)type;
                        p.Price = price;
                        p.Size = size;
                        p.Vegetarian = vegetarian;
                        p.Company = company;
                        SW.WriteLine("Pizza edited");
                    }
                    else
                    {
                        SW.WriteLine("You can't edit.");
                    }
                    return;
                }
            }
        }
    }
}

