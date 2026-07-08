using System;
using System.Collections.Generic;
using System.Text;

public class PokemonCardData
{
    public string id { get; set; }
    public string name { get; set; }
    public string hp { get; set; }
    public string number { get; set; }
    public string artist { get; set; }
    public string supertype { get; set; }
    public string level { get; set; }

    public string rarity { get; set; }
    public List<string> types { get; set; }

    public List<string> subtypes { get; set; }
    public string evolvesFrom { get; set; }
}
