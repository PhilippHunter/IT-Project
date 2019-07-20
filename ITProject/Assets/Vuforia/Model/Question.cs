﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model
{
    public class Question
    {
        public Question(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return this.Text;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Question))
                return false;

            return this.Text == (obj as Question).Text;
        }
        public int ID { get; set; }
        public string Text { get; set; }
        public Country _Country { get; set; }
    }
}
