using System.Collections.Generic;

namespace ArcPrime.WPF.Model
{

    public struct Result
    {
        public decimal Turn { get; set; }
        public string ShouldIRestartExperimentAndCry { get; set; }
        public bool IsTerminated { get; set; }
        public decimal EventScore { get; set; }
        public decimal ExperimentScore { get; set; }
        public decimal TotalScore { get; set; }
        public IList<string> NehoRunes { get; set; }
        public decimal FoodQuantity { get; set; }
        public decimal Waste { get; set; }
        public decimal SocialCapital { get; set; }
        public decimal Production { get; set; }
        public decimal FoodCapacity { get; set; }
        public decimal ArcologyIntegrity { get; set; }
        public decimal Population { get; set; }
        public decimal PopulationCapacity { get; set; }
        public IList<string> Events { get; set; }

    }
}