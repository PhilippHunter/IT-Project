using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Vuforia.Scripts.Model
{
    class Information
    {
        public Information(int id, string text)
        {
            ID = id;
            Text = text;
        }

        public override string ToString()
        {
            return this.Text;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Information))
                return false;

            return this.Text == (obj as Information).Text;
        }
        public int ID { get; set; }
        public string Text { get; set; }
        public Information _Information { get; set; }
    }
}
