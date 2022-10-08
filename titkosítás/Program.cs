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
            string titkosítás = Console.ReadLine();//titkosítás bekérése
            Console.WriteLine("adja meg a titkosítandó szöveget");
            string szoveg = Console.ReadLine()+" ";//titkosítandó szöveg bekérése + egy space ,hogy a program utolsó folyamatakor eltudjon elemzésre küldeni valalamit és ne írja ki hogy out of index
            int szamlalo = 0;//ez számolja hogy hányszor futott le a program egyhuzamban
            for (int i = 0; i < (szoveg.Length-1); i++)//anyiszor fut le amenyi a szöveg hossza és 
            {
                    if (szamlalo/titkosítás.Length == 1)//ha a folyamat egyszer végig ment a titkosításon akkor ujra kezdi 
                    {
                        szamlalo = 0;//szamláló reset
                    }
                    string aa = szamitas(Convert.ToInt32(titkosítás[szamlalo].ToString()), szoveg[i], szoveg[i + 1]);//kiértékelés
                    szamlalo++;//a folyamat megtörténésének száma
                    if (aa.IndexOf('+') != -1)//ha a viszatérő érték vízjele dupla betűből való elemzést mutat akkor a szöveg következendő elemét átlépi
                    {
                        Console.Write(aa.Remove(aa.Length-1,1));//letörli az utolsó elemet az az letörli a vízjelet 
                        i++;//a léptetés
                    }
                    else//sima kiírás
                    {
                        Console.Write(aa.Remove(aa.Length-1,1));//letörli az utolsó elemet az az letörli a vízjelet 
                }
                    

            }
            Console.ReadLine();//végső stop
        }
        static string szamitas(int szam,char betu, char betu2)//3 érték megy be, a kódólás száma,a szöveg egyik karaktere,a szöveg következő karaktere
        {//4 6 11 18  21 30 32 42
          
            string abc = "aábc-d-eéfg-hiíjkl-mn-oóöőpqrs-t-uúüűvwxyz-aábc-d-eéfg-hiíjkl-mn-oóöőpqrs-t-uúüűvwxyz-";//az abc. a - jelek a dupla betüket jelölik
            string[] abc_dubla = new string[8];//8 dupla betu 
            abc_dubla[0] = "cs";
            abc_dubla[1] = "dz";
            abc_dubla[2] = "gy";
            abc_dubla[3] = "ly";
            abc_dubla[4] = "ny";
            abc_dubla[5] = "sz";
            abc_dubla[6] = "ty";
            abc_dubla[7] = "zs";
            string x = betu.ToString();//ha space akkor az x nem változik ,ha nem space akkor pedig változik
            int a_szam = Array.IndexOf(abc_dubla, Convert.ToString(betu) + Convert.ToString(betu2));//ez a szám -1 lesz ha a szöveg e kettő karaktere nem egyezik egyik dupla betűvel sem
            int b_szam = abc.IndexOf(betu);//a szöveg karakterének helyének száma az abc-ben
            if (x != " ")//ha a bemenete nem egy space
            {
                if (a_szam != -1)//ha az a_szam számítása közben talált dupla betüt akkor ennek értéke nem lesz -1, így bisztosan egy dupla betűt elemez a rendszer
                {
                    if (abc[b_szam + szam + 1] != '-')/*ha ennek a dupla betűnek elemezzük az első elemét akkor metaláljuk az abcben a helyét -1. ekkor 
                                               * a kódolási eltolódást hozzáadjuk és adunk hozzá +1. ekkor elemezzűk ,hogy 
                                               * a kódolt betű egy dupla vagy egy single betű lesz. De a jelenlegi feltétel 
                                               * miatt itt single betű lesz a kódolt*/
                    {
                        x = Convert.ToString(abc[b_szam + szam + 1]) + "+";//mivel single betűről van szó,nem kell kikeresni az ide tartozó dupla betűt ezért megvan a visszatérő érték + egy vízjel ami arra szolgár,hogy tudassuk a programmal,hogy ez egy dupla betű elemzésével történt.
                    }
                    else if (abc[b_szam + szam + 1] == '-')//ha a dupla betű eslő elemének helye megvan akkor megvan a dupla betű helye -1.ekkor eltoljuk és adunk hozzá +1,ekkor itt kiderűl ,hogy a kimenet dupla betű lesz ,ezzel bedig kielemezzű melyik is.
                    {
                        for (int i = 0; i < abc_dubla.Length; i++)//górcsó alá vesszük a dupla betűket 
                        {
                            if (abc_dubla[i][0] == abc[b_szam + szam])//ha az elemzett dupla betű első eleme megegyezik az eltolt dupla betű helyének -1 helyén álló single betűnek akkor megtaláltuk a ide való dupla betűt
                            {
                                x = abc_dubla[i] + "+";//a visszatérű érték + a dupla betűből való elemzést jelző vízjel
                            }
                        }
                    }
                }
                else// ha az a_szam értéke -1 akkor nem talált dupla betűt ,így a bemeneti betű single
                {
                    if (abc[b_szam + szam] == '-')//ellenőrizzük ,hogy az eltolt betű egy single vagy egy dupla ,jelen feltétel szerint, ha igaz, ez dupla betű lesz
                    {
                        for (int i = 0; i < abc_dubla.Length; i++)//elemezzük, hogy melyik dupla betű
                        {
                            if (abc_dubla[i][0] == abc[b_szam + szam - 1])//ha az eltolt dupla betű helyén -1 álló single betű megegyezik az éppen elemzett dupla betű első elemével akkor megvan a szükséges dupla betű
                            {
                                x = abc_dubla[i] + "-";//a visszatérű érték + a single betűből való elemzést jelző vízjel
                            }
                        }
                    }
                    else//ha singel betűből az eltolással single betű lesz akkor egyszerűen kiiratjuk az eltolt betűt
                    {
                        x = Convert.ToString(abc[b_szam + szam]) + "-";
                    }


                }
            }
            else
            {
                x = betu.ToString()+" ";// space kiiratásra küldése + a vízjel
            }
            
            
            return x;//viszatérő érték
        }

    }
}
