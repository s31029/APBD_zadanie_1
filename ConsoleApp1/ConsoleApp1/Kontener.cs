namespace ConsoleApp1
{
    public abstract class Kontener
    {
        private static int _globalnyLicznik = 0;

        public int IdKontenera { get; }
        public string NumerSeryjny { get; private set; }
        public double MasaLadunku { get; protected set; }
        public double Wysokosc { get; protected set; }
        public double WagaWlasna { get; protected set; }
        public double Glebokosc { get; protected set; }
        public double MaksymalnaLadownosc { get; protected set; }

        protected abstract string SymbolTypu { get; }

        protected Kontener(double wys, double wagaWlasna, double gleb, double maksLad)
        {
            IdKontenera = ++_globalnyLicznik;

            Wysokosc = wys;
            WagaWlasna = wagaWlasna;
            Glebokosc = gleb;
            MaksymalnaLadownosc = maksLad;

            NumerSeryjny = $"KON-{SymbolTypu}-{IdKontenera}";
        }

        public virtual void Zaladuj(double masa)
        {
            double nowaMasa = MasaLadunku + masa;
            if (nowaMasa > MaksymalnaLadownosc)
            {
                throw new OverfillException($"Przekroczono ładowność kontenera {NumerSeryjny}. Próbowano załadować: {nowaMasa} kg, maks: {MaksymalnaLadownosc} kg.");
            }
            MasaLadunku = nowaMasa;
        }

        public virtual void Oproznij()
        {
            MasaLadunku = 0;
        }

        public double AktualnaWaga()
        {
            return WagaWlasna + MasaLadunku;
        }

        public virtual void WypiszInformacje()
        {
            Console.WriteLine($"Numer kontenera: {NumerSeryjny}");
            Console.WriteLine($"Typ: {SymbolTypu}");
            Console.WriteLine($"Waga własna: {WagaWlasna} kg");
            Console.WriteLine($"Masa ładunku: {MasaLadunku} kg");
            Console.WriteLine($"Maksymalna ładowność: {MaksymalnaLadownosc} kg");
            Console.WriteLine($"Wysokość: {Wysokosc} cm, Głębokość: {Glebokosc} cm");
        }

    }
}