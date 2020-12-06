using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    public class GroupResponse
    {
        private readonly char[] _formQuestions = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public readonly Dictionary<char, int> Responses;
        public int GroupSize { get; private set; }

        public GroupResponse()
        {
            GroupSize = 0;
            // Initializes response counts to 0 for all questions
            Responses = new Dictionary<char, int>();
            foreach (char formQuestion in _formQuestions)
            {
                Responses[formQuestion] = 0;
            }
        }

        public void AddIndividualAnswers(char[] questionsAnswered)
        {
            GroupSize++;
            foreach (var questionAnswered in questionsAnswered)
            {
                Responses[questionAnswered] = Responses[questionAnswered] + 1;
            }
        }

        public IEnumerable<char> GetQuestionsWhereAnyoneAnswered()
        {
            return Responses.Where(x => x.Value != 0).Select(x => x.Key);
        }

        public IEnumerable<char> GetQuestionsWhereEveryoneAnswered()
        {
            return Responses.Where(x => x.Value == GroupSize).Select(x => x.Key);
        }
    }
}