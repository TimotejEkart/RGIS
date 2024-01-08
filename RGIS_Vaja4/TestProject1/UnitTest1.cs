using RGIS_Vaja4.Pages;

namespace TestProject1
{
    [TestClass]
    public class HotelTesti
    {
        [TestMethod]
        public void Test_VeljavnostHotela()
        {
            Hotel hotel = new Hotel
            {
                HoteliId = 1
            };

            bool rezultat = hotel.VeljavnostHotela();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_VrniHotel()
        {
            Hotel hotel = new Hotel
            {
                Ime = "Grand Hotel",
                Kraj = "Ljubljana"
            };

            string rezultat = hotel.VrniHotel();

            Assert.AreEqual("Grand Hotel v kraju Ljubljana", rezultat);
        }

        [TestMethod]
        public void Test_VrniCenoZEnoto()
        {
            Hotel hotel = new Hotel
            {
                Cena = 100
            };

            string rezultat = hotel.VrniCenoZEnoto();

            Assert.AreEqual("Cena: 100 na enoto", rezultat);
        }
    }
    [TestClass]
    public class PotovanjeTests
    {
        [TestMethod]
        public void Test_VeljavnostPotovanja_Rezervirano()
        {
            Potovanje potovanje = new Potovanje
            {
                Rezervirano = true,
                Datum = DateTime.Now.AddDays(5)
            };

            bool rezultat = potovanje.VeljavnostPotovanja();

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void Test_PridobiPodrobneInformacije()
        {
            Potovanje potovanje = new Potovanje
            {
                PotovanjeId = 1,
                Kraj = "Pariz",
                Cena = 500,
                Trajanje = 7,
                Datum = DateTime.Now.AddDays(10),
                Rezervirano = false,
                Ocene = 4.5
            };

            string rezultat = potovanje.PridobiPodrobneInformacije();

            Assert.AreEqual("Potovanje 1: Pariz, Cena: 500, Trajanje: 7 dni, Datum: " + potovanje.Datum.ToString("dd.MM.yyyy") + ", Status: na voljo, Ocene: 4.5", rezultat);
        }

        [TestMethod]
        public void Test_DodajOceno_Valid()
        {
            Potovanje potovanje = new Potovanje
            {
                Ocene = 3.0
            };

            potovanje.DodajOceno(4.0);

            Assert.AreEqual(3.5, potovanje.Ocene, 0.001);
        }
    }
    [TestClass]
    public class PonudnikTesti
    {
        [TestMethod]
        public void Test_VerodostojnostPonudnika()
        {
            Ponudnik ponudnik = new Ponudnik
            {
                Verodostojnost = true,
                Starost = 25
            };

            bool rezultat = ponudnik.VerodostojnostPonudnika();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_VrniPolnoImeInKraj()
        {
            Ponudnik ponudnik = new Ponudnik
            {
                Ime = "Janez",
                Priimek = "Novak",
                Kraj = "Ljubljana"
            };

            string rezultat = ponudnik.VrniPolnoImeInKraj();

            Assert.AreEqual("Janez Novak iz kraja Ljubljana", rezultat);
        }

        [TestMethod]
        public void Test_IzracunajStarost()
        {
            Ponudnik ponudnik = new Ponudnik
            {
                LetoRojstva = 1990
            };

            int rezultat = ponudnik.IzracunajStarost();

            Assert.AreEqual(34, rezultat);
        }
    }
    [TestClass]
    public class OdgovoriTests
    {
        [TestMethod]
        public void Test_VeljavnostOdgovorov()
        {
            Odgovori odgovori = new Odgovori
            {
                odgovorId = 1
            };

            bool rezultat = odgovori.VeljavnostOdgovorov();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_SteviloNepraznihOdgovorov()
        {
            Odgovori odgovori = new Odgovori
            {
                Odgovor1 = "Prvi odgovor",
                Odgovor2 = "",
                Odgovor3 = "Tretji odgovor"
            };

            int rezultat = odgovori.SteviloNepraznihOdgovorov();

            Assert.AreEqual(2, rezultat);
        }

        [TestMethod]
        public void Test_AliOdgovorObstaja()
        {
            Odgovori odgovori = new Odgovori
            {
                Odgovor1 = "Prvi odgovor",
                Odgovor2 = "Drugi odgovor",
                Odgovor3 = "Tretji odgovor"
            };

            bool rezultat = odgovori.AliOdgovorObstaja("Drugi odgovor");

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_AliOdgovorNeObstaja()
        {
            Odgovori odgovori = new Odgovori
            {
                Odgovor1 = "Prvi odgovor",
                Odgovor2 = "Drugi odgovor",
                Odgovor3 = "Tretji odgovor"
            };

            bool rezultat = odgovori.AliOdgovorObstaja("Cetrti odgovor");

            Assert.IsFalse(rezultat);
        }
    }
    [TestClass]
    public class AdministratorTesti
    {
        [TestMethod]
        public void Test_VeljavnostAdministratorja()
        {
            Administrator administrator = new Administrator
            {
                administratorId = 1
            };

            bool rezultat = administrator.VeljavnostAdministratorja();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_VrniAdministratorja()
        {
            Administrator administrator = new Administrator
            {
                Ime = "Janez",
                Priimek = "Novak"
            };

            string rezultat = administrator.VrniAdministratorja();

            Assert.AreEqual("Janez Novak", rezultat);
        }

        [TestMethod]
        public void Test_PovecajSteviloOdstranjenih()
        {
            Administrator administrator = new Administrator
            {
                SteviloOdstranjenih = 5
            };

            administrator.PovecajSteviloOdstranjenih();

            Assert.AreEqual(6, administrator.SteviloOdstranjenih);
        }
    }
    [TestClass]
    public class KoledarTesti
    {
        [TestMethod]
        public void Test_VeljavnostID()
        {
            Koledar koledar = new Koledar();
            koledar.koledarId = 1;

            bool rezultat = koledar.VeljavnostID();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_VeljavnostKoncaDatuma()
        {
            Koledar koledar = new Koledar();
            koledar.datum = new DateTime(2024, 1, 1);
            koledar.datumKonec = new DateTime(2024, 1, 10);

            bool rezultat = koledar.VeljavnostKoncaDatuma();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_VeljavnostDatuma()
        {
            Koledar koledar = new Koledar();
            koledar.datum = DateTime.Now.AddDays(1);

            bool rezultat = koledar.VeljavnostDatuma();

            Assert.IsTrue(rezultat);
        }
    }
    [TestClass]
    public class SistemPlaciloTesti
    {
        [TestMethod]
        public void Test_VeljavnostPlacila()
        {
            SistemPlacilo sistemPlacilo = new SistemPlacilo
            {
                sistemPlaciloId = 1
            };

            bool rezultat = sistemPlacilo.VeljavnostPlacila();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_ZnesekPozitiven()
        {
            SistemPlacilo sistemPlacilo = new SistemPlacilo
            {
                Znesek = 50.0
            };

            bool rezultat = sistemPlacilo.ZnesekPozitiven();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_VeljavnostDatumPlacila()
        {
            SistemPlacilo sistemPlacilo = new SistemPlacilo
            {
                DatumPlacila = DateTime.Now
            };

            bool rezultat = sistemPlacilo.VeljavnostDatumPlacila();

            Assert.IsTrue(rezultat);
        }
    }
    [TestClass]
    public class RezervacijaTesti
    {
        [TestMethod]
        public void Test_VeljavnostRezervacije()
        {
            Rezervacija rezervacija = new Rezervacija
            {
                rezervacijaId = 1
            };

            bool rezultat = rezervacija.VeljavnostRezervacije();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_AliJeRezervirano()
        {
            Rezervacija rezervacija = new Rezervacija
            {
                Rezervirano = true
            };

            bool rezultat = rezervacija.AliJeRezervirano();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_VeljavnostDatumaRezervacije()
        {
            Rezervacija rezervacija = new Rezervacija
            {
                DatumRezervacije = DateTime.Now
            };

            bool rezultat = rezervacija.VeljavnostDatumaRezervacije();

            Assert.IsTrue(rezultat);
        }
    }
    [TestClass]
    public class UporabnikTesti
    {
        [TestMethod]
        public void Test_VrniPolnoIme()
        {
            Uporabnik uporabnik = new Uporabnik
            {
                Ime = "Janez",
                Priimek = "Novak"
            };

            string rezultat = uporabnik.VrniPolnoIme();

            Assert.AreEqual("Janez Novak", rezultat);
        }

        [TestMethod]
        public void Test_JePolnoleten()
        {
            Uporabnik uporabnik = new Uporabnik
            {
                Starost = 20
            };

            bool rezultat = uporabnik.JePolnoleten();

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void Test_PosodobiStarost()
        {
            Uporabnik uporabnik = new Uporabnik
            {
                Starost = 25
            };

            uporabnik.PosodobiStarost(30);

            Assert.AreEqual(30, uporabnik.Starost);
        }
    }
    [TestClass]
    public class EvidencaTesti
    {
        [TestMethod]
        public void Test_VrniOblikovanDatum()
        {
            Evidenca evidenca = new Evidenca
            {
                DatumVnosa = new DateTime(2022, 1, 15)
            };

            string rezultat = evidenca.VrniOblikovanDatum();

            Assert.AreEqual("15.01.2022", rezultat);
        }

        [TestMethod]
        public void Test_PovecajEvidencaId()
        {
            Evidenca evidenca = new Evidenca
            {
                evidencaId = 5
            };

            evidenca.PovecajEvidencaId();

            Assert.AreEqual(6, evidenca.evidencaId);
        }

        [TestMethod]
        public void Test_DobiLetoVnosa()
        {
            Evidenca evidenca = new Evidenca
            {
                DatumVnosa = new DateTime(2022, 1, 15)
            };

            int rezultat = evidenca.DobiLetoVnosa();

            Assert.AreEqual(2022, rezultat);
        }
    }
}