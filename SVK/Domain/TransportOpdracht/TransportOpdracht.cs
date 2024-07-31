using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Gebruikers;
using Domain.Fotos;
using Domain.Producten;
using Domain.Bestanden;

namespace Domain.TransportOpdracht;

public class TransportOpdracht
{
    public Guid Id { get; set; }
    public DateTime Datum { get; set; }
    public string Routenummer { get; set; }
    public List<string> Laadbonnummers { get; set; }
    public Gebruiker Lader { get; set; }
    public Foto Foto {  get; set; }
    public List<Bestand> Bestanden {  get; set; }
    public string Transporteur { get; set; }
    public string Nummerplaat { get; set; }
    public List<Product> Producten {  get; set; }

}
