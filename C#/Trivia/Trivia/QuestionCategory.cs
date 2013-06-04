using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class QuestionCategory
    {
        private IList<string> questions;
        private string name;

        public QuestionCategory(string category)
        {
            this.name = category;
        }

        public void CreateQuestions()
        {
            questions = new List<string>();

            for (int i = 0; i < 50; i++)
            {
                questions.Add(CreateQuestion(i));
            }
        }

        private string CreateQuestion(int index)
        {
            return string.Format("{0} Question {1}", name, index);
        }

        public string GetNextQuestion()
        {
            var question = questions.First();
            questions.RemoveAt(0);

            return question;
        }
    }
}
