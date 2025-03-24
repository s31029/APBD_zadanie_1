namespace ConsoleApp1;

public class KontenerNaPlyny : Kontener, IHazardNotifier
{
    public bool LadunekNiebezpieczny { get; private set; }
    protected override string SymbolTypu => "L";

    public KontenerNaPlyny(double wys, double wagaWlasna, double gleb, double maksLad, bool ladunekNiebezpieczny)
        : base(wys, wagaWlasna, gleb, maksLad)
    {
        LadunekNiebezpieczny = ladunekNiebezpieczny;
    }

    public override void Zaladuj(double masa)
    {
        double limit = LadunekNiebezpieczny ? MaksymalnaLadownosc * 0.5 : MaksymalnaLadownosc * 0.9;
        double nowaMasa = MasaLadunku + masa;
        if (nowaMasa > limit)
        {
            PowiadomONiebezpiecznejOperacji($"Próba przekroczenia dopuszczalnego limitu dla kontenera {NumerSeryjny} (ładunek {(LadunekNiebezpieczny ? "niebezpieczny" : "zwykły")}).");
            throw new OverfillException($"Limit {(LadunekNiebezpieczny ? "50%" : "90%")} przekroczony: {nowaMasa} kg > {limit} kg.");
        }
        if (nowaMasa > MaksymalnaLadownosc)
        {
            throw new OverfillException($"Przekroczono 100% ładowności kontenera {NumerSeryjny}: {nowaMasa} kg > {MaksymalnaLadownosc} kg.");
        }
        MasaLadunku = nowaMasa;
    }

    public void PowiadomONiebezpiecznejOperacji(string komunikat)
    {
        Console.WriteLine($"[ALERT - KONTENER {NumerSeryjny}] {komunikat}");
    }

    public override void WypiszInformacje()
    {
        base.WypiszInformacje();
        Console.WriteLine($"Czy ładunek niebezpieczny: {LadunekNiebezpieczny}");
    }
}
