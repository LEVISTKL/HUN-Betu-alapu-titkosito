using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace titkosítás
{
    internal class Program
    {

        static void Main(string[] args)
        {
            
            Console.WriteLine("adja meg a titkosítást (szám)");
            string titkosítás = Console.ReadLine();
            Console.WriteLine("adja meg a titkosítandó szöveget");
            string szoveg = Console.ReadLine()+" ";
            int szamlalo = 0;
            for (int i = 0; i < (szoveg.Length-1); i++)//anyiszor amenyi betű van
            {
                    if (szamlalo%titkosítás.Length == 0)
                    {
                        szamlalo = 0;
                    }
                    string aa = szamitas(Convert.ToInt32(titkosítás[szamlalo].ToString()), szoveg[i], szoveg[i + 1]);
                    szamlalo++;
                    if (aa.Length > 2)
                    {
                    Console.Write(Convert.ToString(aa[0]) + Convert.ToString(aa[1]));

                    }
                    else if(aa.Length == 2)
                    {
                        Console.Write(Convert.ToString(aa[0]));

                    }
                    if (aa.IndexOf('+') != -1)
                    {
                        i++;
                    }
                    

            }
            Console.ReadLine();
        }
        static string szamitas(int szam,char betu, char betu2)
        {//4 6 11 18  21 30 32 42
          
            string abc = "aábc-d-eéfg-hiíjkl-mn-oóöőpqrs-t-uúüűvwxyz-aábc-d-eéfg-hiíjkl-mn-oóöőpqrs-t-uúüűvwxyz-";
            string[] abc_dubla = new string[8];
            abc_dubla[0] = "cs";
            abc_dubla[1] = "dz";
            abc_dubla[2] = "gy";
            abc_dubla[3] = "ly";
            abc_dubla[4] = "ny";
            abc_dubla[5] = "sz";
            abc_dubla[6] = "ty";
            abc_dubla[7] = "zs";
            string x = "-";
            int a_szam = Array.IndexOf(abc_dubla, Convert.ToString(betu) + Convert.ToString(betu2));//duplabetu indexszáma
            int b_szam = abc.IndexOf(betu);//singlebetu indexszáma
            if (a_szam != -1)//bemenet dupla
            {
                if (abc[b_szam+szam+1] != '-')//kimenet single
                {
                    x = Convert.ToString(abc[b_szam + szam+1]) +"+";
                }
                else if(abc[b_szam + szam+1] == '-')//kimenet dupla pr
                {
                    for (int i = 0; i < abc_dubla.Length; i++)
                    {
                        if (abc_dubla[i][0] == abc[b_szam+szam])
                        {
                            x = abc_dubla[i] + "+";
                        }
                    }
                }
            }
            else//bemenet single
            {
                if (abc[b_szam+szam] == '-')//kimenet dupla
                {
                    for (int i = 0; i < abc_dubla.Length; i++)
                    {
                        if (abc_dubla[i][0] == abc[b_szam + szam - 1])
                        {
                            x = abc_dubla[i]+"-";
                        }
                    }
                }
                else
                {
                    x = Convert.ToString(abc[b_szam + szam ]) + "-";
                }
 
            
            }
            
            return x;
        }

    }
}
