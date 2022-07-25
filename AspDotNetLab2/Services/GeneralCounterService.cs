namespace Services
{
    public class GeneralCounterServices
    {
        private int general = 0;

        public int PlusGeneral() {
            general ++;
            return general;
        }

        public int GetGeneral() => general;
    }
}