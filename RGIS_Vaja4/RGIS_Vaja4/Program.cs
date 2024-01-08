using Microsoft.EntityFrameworkCore;
using RGIS_Vaja4;
using RGIS_Vaja4.Pages;

var builder = WebApplication.CreateBuilder(args);
Administrator admin1 = new Administrator { administratorId = 1, Ime = "Janez", Priimek = "Novak" };
Administrator admin2 = new Administrator { administratorId = 2, Ime = "Ana", Priimek = "Kovač" };
Administrator admin3 = new Administrator { administratorId = 3, Ime = "Marko", Priimek = "Lah" };

Koledar koledar1 = new Koledar { koledarId = 1, datum = DateTime.Now.AddDays(7) };
Koledar koledar2 = new Koledar { koledarId = 2, datum = DateTime.Now.AddDays(14) };
Koledar koledar3 = new Koledar { koledarId = 3, datum = DateTime.Now.AddDays(21) };

SistemPlacilo placilo1 = new SistemPlacilo { sistemPlaciloId = 1, Znesek = 50.0, DatumPlacila = DateTime.Now };
SistemPlacilo placilo2 = new SistemPlacilo { sistemPlaciloId = 2, Znesek = 75.5, DatumPlacila = DateTime.Now.AddDays(5) };
SistemPlacilo placilo3 = new SistemPlacilo { sistemPlaciloId = 3, Znesek = 120.75, DatumPlacila = DateTime.Now.AddDays(10) };

Rezervacija rezervacija1 = new Rezervacija { rezervacijaId = 1, Rezervirano = true, DatumRezervacije = DateTime.Now };
Rezervacija rezervacija2 = new Rezervacija { rezervacijaId = 2, Rezervirano = false, DatumRezervacije = DateTime.Now.AddDays(3) };
Rezervacija rezervacija3 = new Rezervacija { rezervacijaId = 3, Rezervirano = true, DatumRezervacije = DateTime.Now.AddDays(7) };

Uporabnik uporabnik1 = new Uporabnik { uporabnikId = 1, Ime = "Maja", Priimek = "Hribar", Kraj = "Ljubljana", Starost = 30 };
Uporabnik uporabnik2 = new Uporabnik { uporabnikId = 2, Ime = "Andrej", Priimek = "Zupan", Kraj = "Maribor", Starost = 25 };
Uporabnik uporabnik3 = new Uporabnik { uporabnikId = 3, Ime = "Nina", Priimek = "Golob", Kraj = "Celje", Starost = 35 };

Evidenca evidenca1 = new Evidenca { evidencaId = 1, DatumVnosa = DateTime.Now };
Evidenca evidenca2 = new Evidenca { evidencaId = 2, DatumVnosa = DateTime.Now.AddDays(1) };
Evidenca evidenca3 = new Evidenca { evidencaId = 3, DatumVnosa = DateTime.Now.AddDays(2) };
// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();



