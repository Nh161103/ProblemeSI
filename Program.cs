using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;

namespace PSI_V01
{
    class Program
    {
        static void Main(string[] args)
        {
            MyImage imagetest = new MyImage("coco.bmp");
            Pixel[,] mat = imagetest.ImageRGB;
            imagetest.ImageRGB = imagetest.MettreenGris();
            imagetest.From_Image_To_File("Testbis4.bmp");
        }
        /// <summary>
        /// Methode pour afficher les piexels de notre images 
        /// </summary>
        /// <param name="mat">la matrice de pixels</param>
        static void AfficheMatPixel(Pixel[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write(mat[i, j].toString());
                }
                Console.WriteLine();
            }
        }
    }
}
