using System;
using System.Linq;
using System.Collections.Generic;
namespace Homework
{
    class node
    {
        enum NodeType
        {
            STRING,
            INTEGER,
            LIST,
            DICTIONARY
        }

        int x;
        string y;
        List<node> list;
        Dictionary<string, node> dict;

        NodeType choice = NodeType.INTEGER;
        static List<Window> windows = new List<Window>();


        class Window
        {
            public int x;
            public int y;
            public int z;
            public List<string> text;
            public Window parent;

        }


        

        public node()
        {
        }

        public node(int x)
        {
            this.x = x;
            choice = NodeType.INTEGER;
        }
        public node(string y)
        {
            this.y = y;
            choice = NodeType.STRING;
        }
        public node(List<node> list)
        {
            this.list = list;
            choice = NodeType.LIST;
        }
        public node(Dictionary<string, node> dict)
        {
            this.dict = dict;
            choice = NodeType.DICTIONARY;
        }
        public string pretty_string(int spacePlaces = 0)
        {
            string spaces = new String(' ', spacePlaces + 4);
            string s = "";

            switch (choice)
            {
                case NodeType.INTEGER:
                    s = spaces +  x.ToString() + "\n";
                    break;
                case NodeType.STRING:
                    s = spaces + y + "\n";
                    break;
                case NodeType.LIST:
                    if (list.Count == 0)
                        s = "[]";
                    else
                    {
                        s = "[\n";
                        foreach (var item in list)
                        {
                            s += spaces + item.pretty_string(spacePlaces + 4);
                        }
                        s += "\n" + spaces + "],\n";
                    }
                    break;
                case NodeType.DICTIONARY:
                    if (dict.Count == 0)
                        s = "{}\n";
                    else
                    {
                        s = "{\n";
                        foreach (KeyValuePair<string, node> item in dict)
                        {
                            s += spaces + item.Key  + ":" + item.Value.pretty_string(spacePlaces + 4) ;
                        }

                        s += "\n"  + spaces + "},\n";
                        
                        
                    }
                    break;
                default:
                    s += "";
                    break;

            }
            return s;
        }


        private List<string> draw_window(int w, int h,string text= "")
        {
            int text_width = w - 2;
            if (text_width < 0)
                text_width = 0;
            string top = '/' + new String('-', text_width) + '\\' ;
            string bottom = '\\' + new String('-', text_width) + '/';

            List<string> t= new List<string>();

            t.Add(top);

            if (text.Length > 0)
            {
                IEnumerable<string> s = Enumerable.Range(0, text.Length / text_width).Select(i => '|' + text.Substring(i * text_width, text_width) + '|');

                foreach (var item in s)
                {
                    t.Add(item);
                }
            }
            else
            {
                for(int i=0; i<h-2; i++)
                {
                    t.Add('|' + new String(' ', text_width) + '|');
                }
            }
            

            t.Add(bottom);

            return t;


        }

        private void recursion(int iter = 0, Window parent = null)
        {
            if (choice == NodeType.DICTIONARY)
            {
                int x = 0;
                if (this.dict.ContainsKey("x"))
                    x = this.dict["x"].x;
                int y = 0;
                if (this.dict.ContainsKey("y"))
                     y = this.dict["y"].x;
                int w = 0;
                if (this.dict.ContainsKey("w"))
                    w = this.dict["w"].x;
                int h = 0;
                if (this.dict.ContainsKey("h"))
                    h = this.dict["h"].x;

                if (parent != null)
                {
                    x += parent.x;
                    y += parent.y;
                }

                //int z = item.dict["z"].x;
                Window win = new Window();
                win.x = x+1;
                win.y = y+1;

                if (this.dict["children"].choice == NodeType.STRING)
                {
                    string text = this.dict["children"].y;

                    List<string> window = this.draw_window(w, h, text);
                    win.text = window;
                    node.windows.Add(win);

                }
                else
                {
                    List<string> window = this.draw_window(w, h);
                    win.text = window;
                    node.windows.Add(win);

                    this.dict["children"].recursion(iter + 1, win);
                }



                return;

            }

            else if (choice == NodeType.LIST)
            {
                foreach (var item in this.list)
                {
                    item.recursion(iter + 1,node.windows[node.windows.Count-1]);
                    
                }
                return;
            }

        }

        public override string ToString()
        {
            recursion();

            List<string> final = new List<string>();
            var d = draw_window(100, 40, "");


                for (int i=0;i< 20; i++)
            {
                final.Add(d[i]);
                foreach (var window in node.windows)
                {
                    if (window.y <= i && i < window.y + window.text.Count)
                    {
                        
                        final[final.Count - 1] = final[final.Count - 1].Remove(window.x, window.text[i - window.y].Length).Insert(window.x, window.text[i - window.y]);


                    }
                    //Console.WriteLine(window.text);

                }
            }


            string f = "";
            foreach (var item in final)
            {
                f += item + "\n";

            }
            return f;
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {


            node x = new node(
                new Dictionary<string, node> {
                    {
                        "children",
                        new node(
                            new List<node> {
                                new node(
                                    new Dictionary<string, node> {
                                        {"x", new node(2)},
                                        {"y", new node(4)},
                                        {"w", new node(20)},
                                        {"h", new node(4)},
                                        {"z", new node(2)},
                                        {"children", new node("Neki dugacak tekst   neki dugacak tekst")},
                                    }
                                ),
                                new node(
                                    new Dictionary<string, node> {
                                        {"x", new node(18)},
                                        {"y", new node(1)},
                                        {"w", new node(20)},
                                        {"h", new node(5)},
                                        {"z", new node(1)},
                                        {"children", new node("Neki jos dulji tekst neki jos dulji tekst neki jos dulji tekst")},
                                    }
                                ),
                                new node(
                                    new Dictionary<string, node> {
                                        {"x", new node(42)},
                                        {"y", new node(1)},
                                        {"w", new node(32)},
                                        {"h", new node(7)},
                                        {
                                            "children",
                                            new node(
                                                new List<node> {
                                                    new node(
                                                            new Dictionary<string, node> {
                                                            {"w", new node(6)},
                                                            {"h", new node(3)},
                                                            {"children", new node("")},
                                                        }
                                                    ),
                                                    new node(
                                                        new Dictionary<string, node> {
                                                            {"x", new node(24)},
                                                            {"y", new node(2)},
                                                            {"children", new node(new List<node>() {})},
                                                        }
                                                    ),
                                                }
                                            )
                                        },
                                    }
                                ),
                            }
                        )
                    }
                }
            );

            //Console.WriteLine(x.pretty_string());
            Console.WriteLine(x);
        }
    }
}

