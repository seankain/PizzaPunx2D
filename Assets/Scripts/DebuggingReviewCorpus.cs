using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class DebuggingReviewCorpus
    {
        public static string GenerateReview()
        {
            var random = new Random();
            var starCount = random.Next(0, 4);
            return $"{starCount+1} stars. The food is {Adjectives.ForFood(starCount)}. The staff is {Adjectives.ForStaff(starCount)}";
        }
    }

    public class Adjectives
    {
        private readonly static string[][] Food = new string[][] {
            new string[] { "disgusting", "toxic", "borderline poisonous" },
            new string[] { "gross", "bad", "unacceptable" },
            new string[] { "bland", "passable", "nothing special", "okay" },
            new string[] { "good","enjoyable","tasty","worth revisiting" },
            new string[] { "great", "exceptional", "delicious","one of a kind", "the best","supreme" }
    };
        private readonly static string[][] Staff = new string[][] {
            new string[] {"pathetic","human garbage","the absolute worst","abysmal" },
            new string[] {"incompetent","bumbling","unhelpful","alienating"},
            new string[] {"somewhat committed","not a hindrance","good enough"},
            new string[] {"nice","cool","decent","helpful"},
            new string[] {"awesome","dedicated","phenominal"}
        };

        public static string ForFood(int starCount)
        {
            return Get(Food, starCount);
        }
        public static string ForStaff(int starCount) {
            return Get(Staff, starCount);
        }

        private static string Get(string[][] values, int starCount) {
            var random = new Random();
            var valueLength = values[starCount].Length;
            return values[starCount][random.Next(0, valueLength - 1)];
        }
        
    }