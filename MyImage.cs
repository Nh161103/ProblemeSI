using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace PSI_V01
{
    class MyImage
    {
        private string type;
        private int taille;
        private int offset;
        private int hauteur;
        private int largeur;
        private int nombre_Octets;
        private byte[] image;
        private Pixel[,] imageRGB;
        public MyImage(string fichier)
        {
            this.image = File.ReadAllBytes(fichier);
            this.type = Convert.ToString(image[0]) + " " + Convert.ToString(image[1]);
            this.taille = Convertir_Endian_To_Int(byteCreaTab(2, 5));
            this.offset = Convertir_Endian_To_Int(byteCreaTab(14, 17));//header info 14 à 17
            this.largeur = Convertir_Endian_To_Int(byteCreaTab(18, 21)); //largeur 18 a 21
            this.hauteur = Convertir_Endian_To_Int(byteCreaTab(22, 25));//hauteur 22 25
            this.nombre_Octets = Convertir_Endian_To_Int(byteCreaTab(28, 29));//nombre octet par couleur 28 29
            this.imageRGB = CreaMatPixel();
        }
        // pixels à partir de 54 code rgb

        /// <summary>
        /// Crée un tableau de Byte à partir d'un autre entre les index début et fin
        /// </summary>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <returns> Un tableau de Byte </returns>
        public byte[] byteCreaTab(int debut, int fin)
        {
            int compteur = 0;
            byte[] tab = new byte[4];
            for (int i = debut; i <= fin; i++)
            {
                tab[compteur] = this.image[i];
                compteur++;
            }
            return tab;

        }

        /// <summary>
        /// Crée une matrice de Pixel à partir d'un fichier bitmap
        /// </summary>
        /// <returns> matrice de Pixel </returns>
        public Pixel[,] CreaMatPixel()
        {
            this.imageRGB = new Pixel[this.hauteur, this.largeur];
            int compteur = 54;
            for (int i = 0; i < this.hauteur; i++)
            {
                for (int j = 0; j < this.largeur; j++)
                {
                    this.imageRGB[i, j] = new Pixel(this.image[compteur + 2], this.image[compteur + 1], this.image[compteur]);
                    compteur = compteur + 3;
                }

            }
            return this.imageRGB;
        }

        /// <summary>
        /// prend une instance de MyImage et la transforme en fichier binaire respectant la structure du fichier.bmp
        /// </summary>
        /// <param name="fichier"></param>
        public void From_Image_To_File(string fichier)
        {
            int tailleImage = (this.largeur) * (this.hauteur) * (this.nombre_Octets );
            byte[] bytesortie = new byte[this.offset + tailleImage];
            for (int i = 0; i < this.offset; i++)
            {
                bytesortie[i] = this.image[i];
                
            }
            int compteur = this.offset+14;
            for (int x = 0; x <this.imageRGB.GetLength(0); x++)
            {
                for (int j = 0; j < this.imageRGB.GetLength(1); j++)
                {
                    bytesortie[compteur] = this.imageRGB[x, j].B;
                    bytesortie[compteur + 1] = this.imageRGB[x, j].G;
                    bytesortie[compteur + 2] = this.imageRGB[x, j].R;
                    compteur = compteur + 3;
                }
            }
            File.WriteAllBytes(fichier, bytesortie);
        }

        /// <summary>
        /// Cette methode met un a un tous les pixels de notre image en gris en faisant une moyenne des 3 couleurs 
        /// </summary>
        /// <returns>elle retourne une matrice de pixel avec des nuances de gris </returns>
        public Pixel[,] MettreenGris()
        {
            Pixel[,] gris = new Pixel[this.imageRGB.GetLength(0), this.imageRGB.GetLength(1)];

            for (int i = 0; i < gris.GetLength(0); i++)
            {
                for (int j = 0; j < gris.GetLength(1); j++)
                {
                    gris[i, j] = new Pixel(0, 0, 0);
                    gris[i, j].R = (byte)((imageRGB[i, j].R + imageRGB[i, j].G + imageRGB[i, j].B) / 3);
                    gris[i, j].G = (byte)((imageRGB[i, j].R + imageRGB[i, j].G + imageRGB[i, j].B) / 3);
                    gris[i, j].B = (byte)((imageRGB[i, j].R + imageRGB[i, j].G + imageRGB[i, j].B) / 3);
                }
            }
            return gris;
        }

        /// <summary>
        /// Convertit une séquence d’octets au format little endian en entier
        /// </summary>
        /// <param name="tab"></param>
        /// <returns> Un entier </returns>
        public int Convertir_Endian_To_Int(byte[] tab)
        {
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(tab);
            return BitConverter.ToInt32(tab, 0);

        }

        /// <summary>
        /// Convertit un entier en séquence d’octets au format little endian
        /// </summary>
        /// <param name="val"></param>
        /// <returns> un tableau de Byte</returns>
        public byte[] Convertir_Int_To_Endian(int val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        //Proprietés de tous nos attributs
        #region Attributs
        public byte[] Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        public string Type
        {
            get { return this.type; }
        }
        public int Taille
        {
            get { return this.taille; }
        }
        public int Hauteur
        {
            get { return this.hauteur; }
        }
        public int Largeur
        {
            get { return this.largeur; }
        }
        public int Nombre_Octets
        {
            get { return this.nombre_Octets; }
        }
        public int Offset
        {
            get { return this.offset; }
        }
        public Pixel[,] ImageRGB
        {
            get { return this.imageRGB; }
            set { this.imageRGB = value; }
        }
        #endregion

    }
}

