using System;
class Feladvany
{
    public int MegoldasSorszama;
    private int OszlopokSzama;
    private int SorokSzama;
    public int[,] Tabla;
    public Feladvany(int sorokSzama, int oszlopokSzama)
    {
        SorokSzama = sorokSzama;
        OszlopokSzama = oszlopokSzama;
        Tabla = new int[SorokSzama, OszlopokSzama];
        MegoldasSorszama = 0;
    }
    public void MegoldasokKeresese(int kiralynoSora)
    {
        // Ezt a metódust nem kell módosítania!
        if (kiralynoSora == SorokSzama)
        {
            MegoldasSorszama++;
            TablaKiir();
        }
        else
        {
            for (int aktOszlop = 0; aktOszlop < OszlopokSzama; aktOszlop++)
            {
                if (EzJoMezo(kiralynoSora, aktOszlop))
                {
                    Tabla[kiralynoSora, aktOszlop] = 1;
                    MegoldasokKeresese(kiralynoSora + 1);
                    Tabla[kiralynoSora, aktOszlop] = 0;
                }
            }
        }
    }
    public void TablaKiir()
    {
        Console.WriteLine($"Megoldás {MegoldasSorszama}.:");
        for (int i = 0; i < SorokSzama; i++)
        {
            for (int j = 0; j < OszlopokSzama; j++)
            {
                Console.Write(Tabla[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public bool EzJoMezo(int sor, int oszlop)
    {
        for (int i = 0; i < sor; i++)
        {
            if (Tabla[i, oszlop] == 1)
                return false;
        }

        int balra = sor - 1;
        int felulre = oszlop - 1;
        while (balra >= 0 && felulre >= 0)
        {
            if (Tabla[balra, felulre] == 1)
                return false;
            balra--;
            felulre--;
        }

        int jobbra = sor + 1;
        felulre = oszlop - 1;
        while (jobbra < SorokSzama && felulre >= 0)
        {
            if (Tabla[jobbra, felulre] == 1)
            {
                return false;
            }
            jobbra++;
            felulre--;
        }

        return true;
    }
}