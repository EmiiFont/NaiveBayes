using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayes
{
    public class NaiveBayes
    {
        public Dictionary<string, Probability> ProbabilityDictionary = new Dictionary<string, Probability>(StringComparer.InvariantCultureIgnoreCase);
        public Dictionary<string, string> Testdata = new Dictionary<string, string>
        {
            {"Cielo Soleado Temperatura Caliente humedad alta sin viento","No"},
            {"Cielo Soleado Temperatura Caliente humedad alta con viento","No"},
            {"Cielo Nublado Temperatura Caliente humedad alta sin viento","Si"},
            {"Cielo Lluvioso Temperatura Suave humedad alta sin viento","Si"},
            {"Cielo Lluvioso Temperatura Fria humedad Normal sin viento","Si"},
            {"Cielo Lluvioso Temperatura Fria humedad Normal con viento","No"},
            {"Cielo Nublado Temperatura Fria humedad Normal con viento","Si"},
            {"Cielo Soleado Temperatura Suave humedad alta sin viento","No"},
            {"Cielo Soleado Temperatura Suave humedad Normal sin viento","Si"},
            {"Cielo Nublado Temperatura Suave humedad Normal con viento","Si"},
            {"Cielo Soleado Temperatura Suave humedad Normal con viento","Si"},
            {"Cielo Nublado Temperatura Suave humedad alta con viento","Si"},
            {"Cielo Nublado Temperatura Caliente humedad Normal sin viento","Si"},
            {"Cielo Lluvioso Temperatura Suave humedad alta con viento","No"},
        };

        public List<string> _wordList = new List<string>
        {
            "Soleado",
            "Lluvioso",
            "Nublado",
            "Caliente",
            "Fria",
            "Suave",
            "alta",
            "sin viento",
            "con viento",
        };

        public void Test()
        {
            var sentenceToTest = "soleado, fria, alta, Temperatura, con viento, Cielo";
            
            double positiveProb = 1;
            double negativeProb = 1;
            foreach (var word in sentenceToTest.Split(','))
            {
                var word1 = word.Trim();
                double positiveVal = 1;
                double negativeVal = 1;
                if (ProbabilityDictionary.ContainsKey(word1))
                {
                  positiveVal = ProbabilityDictionary.First(v => v.Key.Equals(word1, StringComparison.InvariantCultureIgnoreCase)).Value.Positive;
                  negativeVal = ProbabilityDictionary.First(v => v.Key.Equals(word1, StringComparison.InvariantCultureIgnoreCase)).Value.Negative;
                }
                positiveProb *= positiveVal;
                negativeProb *= negativeVal;
            }

           positiveProb *=ProbabilityDictionary["Global"].Positive;
           negativeProb *= ProbabilityDictionary["Global"].Negative;
        }

        public void Trainer()
        {
            //calculate probabilities of negative
            var positive = Testdata.Count(v => v.Value == "Si");
            var negative = Testdata.Count(v => v.Value == "No");
           
            
            //add probability of positive and negative
            ProbabilityDictionary.Add("Global", new Probability()
            {
                Positive = positive/(double) Testdata.Count,
                Negative = negative/(double) Testdata.Count
            });

            foreach (var word in _wordList)
            {
                double positiveProb = Testdata.Count(v => v.Key.Contains(word) && v.Value == "Si") / (double)positive;
                double negativeProb = Testdata.Count(v => v.Key.Contains(word) && v.Value == "No") / (double)negative;
                if (!ProbabilityDictionary.ContainsKey(word))
                {
                    ProbabilityDictionary.Add(word, new Probability()
                    {
                        Negative = negativeProb,
                        Positive = positiveProb
                    });
                }
            }
        }
    }

    public class Probability
    {
        public double Negative { get; set; }
        public double Positive { get; set; }
    }
}
