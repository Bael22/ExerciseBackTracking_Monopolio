using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace S13_Repaso_Monopolio_BackTracking
{
    internal class BackTracking01
    {
        public static void Imprimir(int[,]b,int dado,int dinero)
        {
            Console.WriteLine();
            for (int f = 0; f < b.GetLength(0); f++)
            {
                for (int c = 0; c < b.GetLength(1); c++)
                {
                    Console.Write(b[f,c]+",");
                    
                }
                if (f == 0 ) Console.Write("\t" + dado+" "+dinero);
                Console.WriteLine();
            }
        }
        public static bool lleno(int[,]b)
        {
            for(int f=0;f<b.GetLength(0); f++)
            {
                for(int c=0;c<b.GetLength(1); c++)
                    if(b[f,c]==2) return false;
            }
            return true;
        }
        
        public static bool movimiento1(ref int fil, ref int col, int dado, ref int dinero)
        {
            int[,] res = {
    { 20,21,22,23,24,25,26,27,28,29,30},
    { 19,90,90,90,90,90,90,90,90,90,31},
    { 18,90,90,90,90,90,90,90,90,90,32},
    { 17,90,90,90,90,90,90,90,90,90,33},
    { 16,90,90,90,90,90,90,90,90,90,34},
    { 15,90,90,90,90,90,90,90,90,90,35},
    { 14,90,90,90,90,90,90,90,90,90,36},
    { 13,90,90,90,90,90,90,90,90,90,37},
    { 12,90,90,90,90,90,90,90,90,90,38},
    { 11,90,90,90,90,90,90,90,90,90,39},
    { 10,9,8,7,6,5,4,3,2,1,0}
      };
            bool flag = false;
            int cont = dado + res[fil,col];
            if (dado < 0 && cont <= 0)
            {
                flag = true;
                //dinero -= 200;
                if (cont < 0) 
                    cont = 40 + cont;
            }
            else if ((cont - 40 > 0 || res[fil, col] == 0)&&dado>0)
            {
                flag = true;
                //if(cont - 40 > 0) dinero += 200;
                if (cont - 40 > 0) cont -= 40;
            }
            else if (cont - 40 == 0) cont = 0;
            for (int f = 0; f < res.GetLength(0); f++)
            {
                for (int c = 0; c < res.GetLength(1); c++)
                    if (res[f, c] == cont)
                    {
                        fil = f;col = c;return flag;
                    }
            }
            return flag;

        }
        public static bool movimiento2(int[,] a, int[,] b, ref int fil, ref int col, int dado, ref int dinero)
        {
           bool flag= movimiento1(ref fil, ref col, dado, ref dinero);
            
            if (b[fil, col] == 1)
            {
                movimiento1(ref fil, ref col, -dado, ref dinero);
                //if (flag) dinero += 200;
                return false;
                //return true;
            }
            else if (dinero + a[fil,col]<0) {
                movimiento1(ref fil, ref col, -dado, ref dinero);
                //dinero -= a[fil, col];
                return false; }
            else
            {
                if (flag) dinero += 200;
                dinero += a[fil, col]; b[fil, col] = 1;return true;
            }
        }
        
        public static bool SolucionMonop(int[,] a, int[,] b,int dado, ref int fil, ref int col, ref int dinero)
        {
            if (lleno(b))
                return true;
            else
            {
                Imprimir(b, dado,dinero);
                
                for(int i=12;i>=2;i--)
                {
                    //Console.WriteLine("bien"+ dinero);
                    if (movimiento2(a, b, ref fil, ref col, i, ref dinero) &&
                                            SolucionMonop(a, b,i, ref fil, ref col, ref dinero))
                        return true;                   
                }
                Console.WriteLine("Retrocede"+dinero);
                    dinero -= a[fil, col];
                    b[fil, col] = 2;
                if (movimiento1(ref fil, ref col, -dado, ref dinero))
                    dinero -= 200;
                return false;
            }
        }
    }
}
