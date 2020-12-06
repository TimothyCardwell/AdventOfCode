using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var groupAnswers = ParseInput();

            Part1(groupAnswers);
            Part2(groupAnswers);

            Console.ReadLine();
        }

        private static void Part1(IEnumerable<GroupResponse> groupResponses)
        {
            var sumQuestionsAnswered = 0;
            foreach (var groupResponse in groupResponses)
            {
                sumQuestionsAnswered += groupResponse.GetQuestionsWhereAnyoneAnswered().Count();
            }

            Console.WriteLine(sumQuestionsAnswered);
        }

        private static void Part2(IEnumerable<GroupResponse> groupResponses)
        {
            var sumQuestionsAnswered = 0;
            foreach (var groupResponse in groupResponses)
            {
                sumQuestionsAnswered += groupResponse.GetQuestionsWhereEveryoneAnswered().Count();
            }

            Console.WriteLine(sumQuestionsAnswered);
        }

        private static IEnumerable<GroupResponse> ParseInput()
        {
            var groupResponses = new List<GroupResponse>();

            string line;
            using (var file = new StreamReader("./input.txt"))
            {
                var groupResponse = new GroupResponse();

                while ((line = file.ReadLine()) != null)
                {
                    // Represents a new group response
                    if (string.IsNullOrEmpty(line))
                    {
                        groupResponses.Add(groupResponse);
                        groupResponse = new GroupResponse();
                    }

                    // Represents a new individual answer in the current group
                    else
                    {
                        var individualAnswers = line.ToCharArray();
                        groupResponse.AddIndividualAnswers(individualAnswers);
                    }
                }

                groupResponses.Add(groupResponse);
            }

            return groupResponses;
        }
    }
}
