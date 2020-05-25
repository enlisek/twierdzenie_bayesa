using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayes
{
    class Program
    {
        static string Temp(string temp)
        {
            if (Convert.ToInt32(temp) > 20)
            {
                return "gorąco";
            }
            else if (Convert.ToInt32(temp) < 16)
            {
               return "chłodno";
            }
            else
            {
                return "ciepło";
            }
        }

        static string Dec(string[][] dane, string[] X)
        {
            double pTak;
            double pNie;
            int counterT = 0;
            int[] counter = new int[] { 0, 0, 0 };
            int[] counterN = new int[] { 0, 0, 0 };



            for (int i = 0; i < dane.Length; i++)
            {
                if (dane[i][3]=="tak")
                {
                    counterT++;

                    for (int j = 0; j < counter.Length; j++)
                    {
                        if (dane[i][j]==X[j])
                        {
                            counter[j]++;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < counterN.Length; j++)
                    {
                        if (dane[i][j] == X[j])
                        {
                            counterN[j]++;
                        }
                    }
                }

                
            }

            double[] p = new double[3];
            double[] pN = new double[3];
            int proba = 0;

            List<string> typ = new List<string>();

            for (int j = 0; j < p.Length; j++)
            {
                if (counter[j] == 0)
                {
                    for (int i = 0; i < dane.Length; i++)
                    {
                        if (!typ.Contains(dane[i][j]))
                        {
                            typ.Add(dane[i][j]);
                        }

                    }
                    proba = typ.Count();
                    p[j] = 1.0 / (counterT + typ.Count());
                    typ.Clear();
                }
                else
                {
                    p[j] = ((double)counter[j] / counterT);
                }
                if (counterN[j] == 0)
                {
                    
                    for (int i = 0; i < dane.Length; i++)
                    {
                        if (dane[i][3] == "nie" && !typ.Contains(dane[i][j]))
                        {
                            typ.Add(dane[i][j]);
                        }

                    }
                    pN[j] = 1.0 / ((dane.Length - counterT) + typ.Count());
                    typ.Clear();
                }
                else
                {
                    pN[j] = ((double)counterN[j] / (dane.Length-counterT));
                }
            }

            
            pTak = ((double)counterT / dane.Length) * p[0] * p[1] * p[2];
            pNie = ((dane.Length - counterT) / (double)dane.Length) * pN[0] * pN[1] * pN[2];
            if (pTak>pNie)
            {
                return "tak";
            }
            else
            {
                return "nie" ; 
            }




        }

        static void Main(string[] args)
        {
            string[][] dane = new string[10][];
            dane[0] = new string[4] { "słonecznie", "23", "umiarkowany","tak"};
            dane[1] = new string[4] { "deszczowo", "15", "mocny","nie"};
            dane[2] = new string[4] { "pochmurno", "17", "słaby","tak"};
            dane[3] = new string[4] { "pochmurno", "21", "umiarkowany","nie"};
            dane[4] = new string[4] { "słonecznie", "20", "mocny","tak"};
            dane[5] = new string[4] { "słonecznie", "25", "słaby","tak"};
            dane[6] = new string[4] { "deszczowo", "22", "słaby","tak"};
            dane[7] = new string[4] { "słonecznie", "14", "mocny","nie"};
            dane[8] = new string[4] { "pochmurno", "19", "mocny","nie"};
            dane[9] = new string[4] { "deszczowo", "16", "słaby","nie"};

            for (int i = 0; i < dane.Length; i++)
            {
                dane[i][1]=Temp(dane[i][1]);
            }

            string[] X = new string[] { "pochmurno", "14", "mocny"};
            X[1]=Temp(X[1]);

            Console.WriteLine(Dec(dane,X));
            Console.ReadKey();

            string[] X1 = new string[] { "deszczowo","21","słaby"};
            X1[1]=Temp(X1[1]);

            Console.WriteLine(Dec(dane,X1));
            Console.ReadKey();

            string[] X2 = new string[] { "słonecznie", "10", "słaby" };
            X2[1] = Temp(X2[1]);

            Console.WriteLine(Dec(dane, X2));





            Console.ReadKey();

            

        }
    }
}
