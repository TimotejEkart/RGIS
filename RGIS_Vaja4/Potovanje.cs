namespace RGIS_Vaja4
{
    public class Potovanje
    {
        private string kraj;
        private int cena;
        private int trajanje;
        private DateTime datum;
        private string opis;
        private double ocene;

        public Potovanje SetPodatki(ref string kraj, ref DateTime datum, ref int trajanje, ref int cena)
        {
            throw new System.NotImplementedException("Not implemented");
        }
        public bool Shrani()
        {
            throw new System.NotImplementedException("Not implemented");
        }
        public Potovanje GetPotovanje(ref Potovanje potovanje)
        {
            throw new System.NotImplementedException("Not implemented");
        }
        public bool IzbrisPotovanja(ref Potovanje potovanje)
        {
            throw new System.NotImplementedException("Not implemented");
        }
        public Evidenca PosljiOpisPotovanja(ref Potovanje potovanje)
        {
            throw new System.NotImplementedException("Not implemented");
        }
        public Potovanje SetOcena(ref int id_potovanje, ref int ocena)
        {
            throw new System.NotImplementedException("Not implemented");
        }
        public double GetOcena()
        {
            throw new System.NotImplementedException("Not implemented");
        }

        private Rezervacija[] rezervacije;

        private Ponudnik ponudnik;
        private Uporabnik uporabnik;
    }
}
