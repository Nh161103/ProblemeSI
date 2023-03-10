using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_V01
{
    class Pixel
    {
        private byte red;
        private byte green;
        private byte blue;
        public Pixel(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
        public byte R
        {
            get { return this.red; }
            set { this.red = value; }
        }
        public byte G
        {
            get { return this.green; }
            set { this.green = value; }
        }
        public byte B
        {
            get { return this.blue; }
            set { this.blue = value; }
        }

        public string toString()
        {
            return $"{this.red} {this.green} {this.blue} ";
        }
    }
}
