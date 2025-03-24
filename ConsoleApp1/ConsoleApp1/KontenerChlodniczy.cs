namespace ConsoleApp1;

public class KontenerChlodniczy : Kontener
    {
        private static Dictionary<string, double> _minimalnaTemperaturaDlaProduktu = new Dictionary<string, double>()
        {
            { "banany", 13.3 },
            { "chocolate", 18 },
            { "fish", 2 },
            { "meat", -2 },
            { "ice cream", -18 },
            { "frozen pizza", -12 },
            { "cheese", 7.2 },
            { "sausages", 5 },
            { "butter", 20.5 },
            { "eggs", 19 }
        };

        public string RodzajProduktu { get; private set; } = null;
        public double Temperatura { get; set; }
        protected override string SymbolTypu => "C";

        public KontenerChlodniczy(double wys, double wagaWlasna, double gleb, double maksLad, double temperatura)
            : base(wys, wagaWlasna, gleb, maksLad)
        {
            Temperatura = temperatura;
        }

        public void ZaladujProdukt(double masa, string rodzajProduktu)
        {
            if (RodzajProduktu == null)
            {
                RodzajProduktu = rodzajProduktu.ToLower();
            }
            else
            {
                if (RodzajProduktu != rodzajProduktu.ToLower())
                {
                    throw new Exception($"Kontener {NumerSeryjny} przechowuje już produkt typu {RodzajProduktu}. Nie można załadować innego rodzaju ({rodzajProduktu}).");
                }
            }

            if (_minimalnaTemperaturaDlaProduktu.ContainsKey(RodzajProduktu))
            {
                double minTemp = _minimalnaTemperaturaDlaProduktu[RodzajProduktu];
                if (Temperatura < minTemp)
                {
                    throw new Exception($"Temperatura kontenera ({Temperatura}°C) jest niższa niż wymagana ({minTemp}°C) dla produktu {RodzajProduktu}.");
                }
            }

            base.Zaladuj(masa);
        }

        public override void WypiszInformacje()
        {
            base.WypiszInformacje();
            Console.WriteLine($"Rodzaj produktu: {RodzajProduktu ?? "(brak)"}");
            Console.WriteLine($"Ustawiona temperatura: {Temperatura} °C");
        }
    }