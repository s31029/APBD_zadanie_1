namespace ConsoleApp1;

public class KontenerNaGaz : Kontener, IHazardNotifier
{
    public double Cisnienie { get; set; }
    protected override string SymbolTypu => "G";

    public KontenerNaGaz(double wys, double wagaWlasna, double gleb, double maksLad, double cisnienie)
        : base(wys, wagaWlasna, gleb, maksLad)
    {
        Cisnienie = cisnienie;
    }

    public override void Zaladuj(double masa)
    {
        double nowaMasa = MasaLadunku + masa;
        if (nowaMasa > MaksymalnaLadownosc)
        {
            PowiadomONiebezpiecznejOperacji($"Próba załadowania zbyt dużej ilości gazu do kontenera {NumerSeryjny}.");
            throw new OverfillException($"Przekroczono ładowność kontenera gazowego {NumerSeryjny}: {nowaMasa} kg > {MaksymalnaLadownosc} kg.");
        }
        MasaLadunku = nowaMasa;
    }

    public override void Oproznij()
    {
        MasaLadunku = MasaLadunku * 0.05;
    }

    public void PowiadomONiebezpiecznejOperacji(string komunikat)
    {
        Console.WriteLine($"[ALERT - KONTENER {NumerSeryjny}] {komunikat}");
    }

    public override void WypiszInformacje()
    {
        base.WypiszInformacje();
        Console.WriteLine($"Ciśnienie wewnątrz kontenera: {Cisnienie} atm");
    }
}