namespace DllForSigning
{
    public class APlusB
    {
        public int A { get; set; }
        public int B { get; set; }

        public APlusB()
        {
            A = 10;
            B = 20;
        }

        public int GetSum()
        {
            return A + B;
        }
    }
}