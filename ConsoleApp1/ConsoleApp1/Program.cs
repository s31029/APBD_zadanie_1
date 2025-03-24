namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var statekA = new StatekKontenerowiec(25.0, 5, 50.0);
                var statekB = new StatekKontenerowiec(20.0, 3, 30.0);

                KontenerNaPlyny kontenerPaliwo = new KontenerNaPlyny(200, 1000, 300, 5000, true);
                KontenerNaPlyny kontenerMleko = new KontenerNaPlyny(200, 900, 300, 5000, false);

                KontenerNaGaz kontenerHel = new KontenerNaGaz(250, 1200, 300, 4000, 15.0);
                KontenerNaGaz kontenerAzot = new KontenerNaGaz(250, 1200, 300, 4000, 10.0);

                KontenerChlodniczy kontenerBanany = new KontenerChlodniczy(300, 1500, 400, 3000, 15);
                KontenerChlodniczy kontenerLody = new KontenerChlodniczy(300, 1500, 400, 3000, -20);

                // Ładowanie kontenerów
                Console.WriteLine("=== ŁADOWANIE KONTENERÓW ===\n");

                Console.WriteLine("Ładowanie kontenera z paliwem:");
                kontenerPaliwo.Zaladuj(2000);

                Console.WriteLine("Ładowanie kontenera z mlekiem:");
                kontenerMleko.Zaladuj(4000);

                Console.WriteLine("Ładowanie kontenera z helem:");
                kontenerHel.Zaladuj(2000);

                Console.WriteLine("Ładowanie kontenera z bananami:");
                kontenerBanany.ZaladujProdukt(1000, "banany");

                Console.WriteLine("Ładowanie kontenera z lodami:");
                try
                {
                    kontenerLody.ZaladujProdukt(500, "ice cream");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd ładowania lodów: " + ex.Message);
                }

                Console.WriteLine("\n=== ZAŁADUNEK NA STATEK A ===\n");

                statekA.ZaladujKontener(kontenerPaliwo);
                statekA.ZaladujKontener(kontenerMleko);
                statekA.ZaladujKontener(kontenerHel);
                statekA.ZaladujKontener(kontenerBanany);
                statekA.ZaladujKontener(kontenerAzot);

                statekA.WypiszInformacje();

                Console.WriteLine("Szczegóły kontenera z helem:");
                statekA.WypiszInformacjeOKontenerze(kontenerHel.NumerSeryjny);

                Console.WriteLine("=== OPRÓŻNIANIE KONTENERA Z GAZEM (HEL) ===\n");
                statekA.OproznijKontener(kontenerHel.NumerSeryjny);
                statekA.WypiszInformacjeOKontenerze(kontenerHel.NumerSeryjny);

                Console.WriteLine("=== PRZENOSZENIE KONTENERA ===\n");
                Console.WriteLine($"Przenoszenie kontenera z bananami ({kontenerBanany.NumerSeryjny}) na statek B (ID={statekB.IdStatku})");

                statekA.PrzeniesKontenerDoInnegoStatku(kontenerBanany.NumerSeryjny, statekB);

                Console.WriteLine("\nZawartość statku A:");
                statekA.WypiszInformacje();

                Console.WriteLine("Zawartość statku B:");
                statekB.WypiszInformacje();

                Console.WriteLine("=== ZASTĘPOWANIE KONTENERA ===\n");

                Console.WriteLine("Zastępowanie kontenera z mlekiem kontenerem chłodniczym (lody)");
                kontenerLody.Temperatura = -18;
                kontenerLody.ZaladujProdukt(500, "ice cream");
                statekA.ZastapKontener(kontenerMleko.NumerSeryjny, kontenerLody);

                statekA.WypiszInformacje();
            }
            catch (OverfillException oe)
            {
                Console.WriteLine("Błąd OverfillException: " + oe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inny błąd: " + ex.Message);
            }
        }
    }
}
