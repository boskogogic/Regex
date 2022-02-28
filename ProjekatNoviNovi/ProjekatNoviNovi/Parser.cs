using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ProjekatNoviNovi
{
    public class Parser
    {
        int lineCount;
        LinkedList<string> my_list;



        public Parser()
        {
            my_list = new LinkedList<string>();
            lineCount = 0;
        }



        public int LineCounter(System.IO.StreamReader File)
        {
            int c = 0;
            while (File.ReadLine() != null)
            {
                c++;
            }
            lineCount = c;
            return c;


        }


        void BackToBegining(System.IO.StreamReader File)
        {
            File.DiscardBufferedData();
            File.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
        }


        public void AddLineByLine(System.IO.StreamReader File)
        {
            BackToBegining(File);
            for (int i = 0; i < lineCount; i++)
                my_list.AddLast(File.ReadLine());
        }

      

        public string[] GetTokens()
        {
            string[] arr = new string[lineCount+1];
           // Console.WriteLine("Lijeva strana je:\n");
            int k = 0;

            foreach (string str in my_list)
            {
                int j;
                for (j = 0; str[j] != ':'; j++) ;
                char[] ln = new char[j];

                for (int i = 0; str[i] != ':'; i++)
                    ln[i] = str[i];
             

                arr[k++] = new string(ln);
            }

            Console.WriteLine("\n");
            return arr;
        }


        public string[] GetRightSide()
        {
            
            string[] arr = new string[lineCount + 1];
            int f = 0;

            //Console.WriteLine("Desna strana je:\n");
            foreach (string str in my_list)
            {

                int j = 0;
                int r;



                while (str[j] != '=')
                    j++;


                char[] text = new char[str.Length - j - 1];


                if (str[j] == '=')
                {
                    for (int k = j + 1, g = 0; k < str.Length; k++, g++)
                    {
                        text[g] = str[k];
                    }

                    arr[f++] = new string(text);

                }

                
                j = 0;
            

            }

            return arr;
        }


        

        public string GetFirstLine()
        {
            return my_list.First();
        }

    }
}

