using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model
{
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public Answer(string text, bool isCorrectAnswer)
        {
            this.Text = text;
            this.IsCorrectAnswer = isCorrectAnswer;
        }
    }
}
