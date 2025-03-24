namespace ConsoleApp1
{
    public class StatekKontenerowiec
    {
        private static int _licznikId = 0;

        public int IdStatku { get; }

        public double MaksymalnaPredkoscWezly { get; set; }
        public int MaksymalnaLiczbaKontenerow { get; set; }
        public double MaksymalnaWagaWszystkichKontenerowWTonach { get; set; }

        private List<Kontener> _kontenery = new List<Kontener>();

        public StatekKontenerowiec(double maxPredkosc, int maxKontenery, double maxWagaTon)
        {
            IdStatku = ++_licznikId;
            MaksymalnaPredkoscWezly = maxPredkosc;
            MaksymalnaLiczbaKontenerow = maxKontenery;
            MaksymalnaWagaWszystkichKontenerowWTonach = maxWagaTon;
        }

        public void ZaladujKontener(Kontener kontener)
        {
            if (_kontenery.Count >= MaksymalnaLiczbaKontenerow)
            {
                throw new Exception($"Statek {IdStatku} nie może zabrać więcej kontenerów (limit: {MaksymalnaLiczbaKontenerow}).");
            }

            double aktualnaWagaTon = _kontenery.Sum(k => k.AktualnaWaga()) / 1000.0;
            double nowaWagaTon = aktualnaWagaTon + (kontener.AktualnaWaga() / 1000.0);

            if (nowaWagaTon > MaksymalnaWagaWszystkichKontenerowWTonach)
            {
                throw new Exception($"Statek {IdStatku} przekroczy dopuszczalną łączną masę kontenerów (limit: {MaksymalnaWagaWszystkichKontenerowWTonach} ton).");
            }

            _kontenery.Add(kontener);
        }

        public void ZaladujKontenery(IEnumerable<Kontener> kontenery)
        {
            foreach (var k in kontenery)
            {
                ZaladujKontener(k);
            }
        }

        public void UsunKontener(string numerSeryjny)
        {
            var kontener = _kontenery.FirstOrDefault(k => k.NumerSeryjny == numerSeryjny);
            if (kontener != null)
            {
                _kontenery.Remove(kontener);
            }
            else
            {
                throw new Exception($"Nie znaleziono kontenera o numerze {numerSeryjny} na statku {IdStatku}.");
            }
        }

        public void OproznijKontener(string numerSeryjny)
        {
            var kontener = _kontenery.FirstOrDefault(k => k.NumerSeryjny == numerSeryjny);
            if (kontener != null)
            {
                kontener.Oproznij();
            }
            else
            {
                throw new Exception($"Nie znaleziono kontenera o numerze {numerSeryjny} na statku {IdStatku}.");
            }
        }

        public void ZastapKontener(string numerSeryjny, Kontener nowyKontener)
        {
            UsunKontener(numerSeryjny);
            ZaladujKontener(nowyKontener);
        }

        public void PrzeniesKontenerDoInnegoStatku(string numerSeryjny, StatekKontenerowiec statekDocelowy)
        {
            var kontener = _kontenery.FirstOrDefault(k => k.NumerSeryjny == numerSeryjny);
            if (kontener == null)
            {
                throw new Exception($"Nie znaleziono kontenera {numerSeryjny} na statku {IdStatku}.");
            }

            statekDocelowy.ZaladujKontener(kontener);
            _kontenery.Remove(kontener);
        }

        public void WypiszInformacje()
        {
            Console.WriteLine($"Informacje o statku ID: {IdStatku}");
            Console.WriteLine($"Maksymalna prędkość: {MaksymalnaPredkoscWezly} węzłów");
            Console.WriteLine($"Liczba kontenerów: {_kontenery.Count}/{MaksymalnaLiczbaKontenerow}");
            double aktualnaWagaTon = _kontenery.Sum(k => k.AktualnaWaga()) / 1000.0;
            Console.WriteLine($"Aktualna masa kontenerów: {aktualnaWagaTon} ton (limit: {MaksymalnaWagaWszystkichKontenerowWTonach} ton)");
            Console.WriteLine("Lista kontenerów:");
            foreach (var k in _kontenery)
            {
                Console.WriteLine($" - {k.NumerSeryjny} (Aktualna waga: {k.AktualnaWaga()} kg)");
            }
            Console.WriteLine();
        }

        public void WypiszInformacjeOKontenerze(string numerSeryjny)
        {
            var kontener = _kontenery.FirstOrDefault(k => k.NumerSeryjny == numerSeryjny);
            if (kontener == null)
            {
                Console.WriteLine($"Kontener {numerSeryjny} nie jest załadowany na statek {IdStatku}.\n");
                return;
            }
            Console.WriteLine($"Informacje o kontenerze {numerSeryjny} na statku {IdStatku}:");
            kontener.WypiszInformacje();
            Console.WriteLine();
        }
    }
}
