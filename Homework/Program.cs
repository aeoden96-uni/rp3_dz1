using System;
using System.Collections.Generic;

namespace Homework
{
    class node
    {
        int x;
        string y;
        List<node> list;
        Dictionary<string, node> dict;

        int choice = 0;

        public node()
        {
        }

        public node(int x)
        {
            this.x = x;
            choice = 0;
        }
        public node(string y)
        {
            this.y = y;
            choice = 1;
        }
        public node(List<node> list)
        {
            this.list = list;
            choice = 2;
        }
        public node(Dictionary<string, node> dict)
        {
            this.dict = dict;
            choice = 3;
        }
        public string pretty_string()
        {
            string spaces = "    ";


                    string s = "{\n";
            switch (choice)
            {
                case 0:
                    s =  x.ToString() + "\n";
                    break;
                case 1:
                    s = y + "\n";
                    break;
                case 2:
                    if (list.Count == 0)
                        s = "[]";
                    else
                    {
                        string t = "[\n";
                        foreach (var item in list)
                        {
                            t +=  item.pretty_string();
                        }
                        s =  t + "]";
                    }
                    break;
                case 3:
                    if (dict.Count == 0)
                        s = "{}\n";
                    else
                    {
                        string t = "";
                        foreach (KeyValuePair<string, node> item in dict)
                        {
                            t +=item.Key  + ":" + item.Value.pretty_string() ;
                        }
                        s += t ;
                    }
                    break;
                default:
                    s += "";
                    break;

            }
            return s;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            

            node n = new node(
                new Dictionary<string, node> {
                    {
                        "x",
                        new node(5)
                    },
                    {
                        "y",
                        new node(
                            new List<node> {
                                new node(6),
                                new node(new Dictionary<string, node> {}),
                                new node(
                                new Dictionary<string, node> {
                                    {
                                        "z",
                                        new node(new List<node> {})
                                    }
                                }
                                ),
                                new node("sedam")
                            }
                        )
                    }
                }
            );
            Console.WriteLine(n.pretty_string());
        }
    }
}

