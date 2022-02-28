using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

//POLOZEN 26.09.2019 !!!
/// <MAILADRESS> ::=mejl_adresa


    
/*for email regex :  ^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$*/

namespace ProjekatNoviNovi
{
    partial class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C: \Users\bgogi\Desktop\Disauskas\FAKULTET !!!\Formalne metode\Projektni zadatak\Projektni Novi Novi\ProjekatNoviNovi\ProjekatNoviNovi\Datoteke\Provjera.config.bnf.txt");
            //string file2 = @"C:\User\bgogi\Desktop\NewFile.xml";
            Parser boljiZivot = new Parser();
            int numLines = boljiZivot.LineCounter(file);

            string[] tokens = new string[numLines + 1];
            string[] right = new string[numLines + 1];

            string[] standard_expression_left = new string[5];
            string[] standard_expression_right = new string[5];

            /*XmlTextWriter xmlWriter = new XmlTextWriter(file2, System.Text.Encoding.UTF8);//primjer sa file, ovdje bi trebao biti izlazni
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();*/

            standard_expression_left[0] = "broj_telefona";
            standard_expression_left[1] = "mejl_adresa";
            standard_expression_left[2] = "web_link";
            standard_expression_left[3] = "brojevna_konstantna";
            standard_expression_left[4] = "veliki_grad";


            standard_expression_right[0] = @"(00387|0)(65|66)(/)(1|2|3|4|5|6|7|8|9|0)(1|2|3|4|5|6|7|8|9|0)(1|2|3|4|5|6|7|8|9|0)(-)(1|2|3|4|5|6|7|8|9|0)(1|2|3|4|5|6|7|8|9|0)(1|2|3|4|5|6|7|8|9|0)";
            standard_expression_right[1] = @"([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})";
            standard_expression_right[2] = @"http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?";
            standard_expression_right[3] = @"\d*\.?\d*";

            standard_expression_right[4] = "";


            boljiZivot.AddLineByLine(file);
            boljiZivot.GetTokens().CopyTo(tokens, 0);

          //  for (int i = 0; i < numLines; i++)
            //    Console.WriteLine(tokens[i]);


            boljiZivot.GetRightSide().CopyTo(right, 0);

            //provjera za gradove
            string cities;
            var fileStream1 = new FileStream(@"C: \Users\bgogi\Desktop\Disauskas\FAKULTET !!!\Formalne metode\Projektni zadatak\Projektni Novi Novi\ProjekatNoviNovi\ProjekatNoviNovi\Datoteke\Cities.txt", FileMode.Open, FileAccess.Read);

            //citanje imena gradova iz page source
            using (var streamReader1 = new StreamReader(fileStream1, Encoding.UTF8))
            {
                cities = streamReader1.ReadToEnd();
            }
            int number = 0;
            string pattern1 = "face=" + '"' + "Arial,Helvetica,Geneva,Swiss,SunSans-Regular" + '"' + @"><b>(\w+\s?\w+?)( \(\w+\))?<\/b>"; //regex izraz za pronalazak grada
            Console.WriteLine(pattern1);
            Regex regexCities = new Regex(pattern1, RegexOptions.IgnoreCase);
            Match matchCitites = regexCities.Match(cities);
            while (matchCitites.Success)
            {
                number++;
                string something = matchCitites.Groups[1].Value; //group[1] jer trazimo gradove a oni spadaju u grupu -> 1
                if (number > 4)
                    standard_expression_right[4] = standard_expression_right[4] + something + '|';
                matchCitites = matchCitites.NextMatch();
            }
            standard_expression_right[4] = standard_expression_right[4].Remove(standard_expression_right[4].Length - 1);

            for (int i = 0; i < numLines; i++)
            {
                tokens[i] = tokens[i].Replace(" ", "");
                right[i] = right[i].Trim();
                right[i] = right[i].Replace("\"", "");
            }

            for (int i = 0; i < numLines; i++)
                right[0] = right[0].Replace(tokens[i], "(" + right[i] + ")");


            for (int i = 0; i < numLines; i++)
                for(int j=0; j<numLines; j++)
                if (right[0].Contains(tokens[j]))
                    right[0] = right[0].Replace(tokens[j], "(" + right[j] + ")");

            

            for (int i = 0; i < 5; i++)
                if (right[0].Contains(standard_expression_left[i]))
                    right[0] = right[0].Replace(standard_expression_left[i], "(" +standard_expression_right[i]+")");

            for (int i = 0; i < numLines; i++)
            {
                if (right[i].Contains(tokens[i]))
                {
                    right[i] = '(' + right[i] + ')' ;

                } //REKURZIJA 
            }
           
            //provjera za standardne izraze;
            string text;
            var fileStream = new FileStream(@"C:\Users\bgogi\Desktop\Disauskas\FAKULTET !!!\Formalne metode\Projektni zadatak\Projektni Novi Novi\ProjekatNoviNovi\ProjekatNoviNovi\Datoteke\Provjera.txt", FileMode.Open, FileAccess.Read);



            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                Console.WriteLine("Sadrzaj iz Datoteke je: ");
                // Read the stream to a string, and write the string to the console.
                text = streamReader.ReadToEnd();
                Console.WriteLine(text);
            }

           
           
            string pattern = @right[0];
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(text);
            //Console.WriteLine("Evo me ovde!");
            int num = 0;
            while(match.Success)
            { 
                num++;
                Console.WriteLine("\nIspravna linija iz datoteke:");
               // xmlWriter.WriteStartElement(tokens[num]);
                //Console.WriteLine("Usao sam u while petlju!");
                string temp = match.Groups[0].Value;
                Console.WriteLine("\n" + temp + "\n");

                //xmlWriter.WriteElementString("EHEJ!",Console.ReadLine());
                //TODO do something with the phone number
                match = match.NextMatch();
                //xmlWriter.WriteEndElement();
            }

           // Console.WriteLine("Niz sa desne strane jeste: " + right[0]);
            System.Console.ReadLine();
            /*xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.Flush();
            xmlWriter.Close();*/

        }
    }
}
