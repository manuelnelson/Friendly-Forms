using Models.Contract;

namespace Models
{
    public class Bcso : IEntity
    {
        public long Id { get; set; }
        public int GrossIncome { get; set; }
        public double OneChildAmount { get; set; }
        public double TwoChildAmount { get; set; }
        public double ThreeChildAmount { get; set; }
        public double FourChildAmount { get; set; }
        public double FiveChildAmount { get; set; }
        public double SixChildAmount { get; set; }
    }
}
