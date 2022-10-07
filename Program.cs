using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;


public class Type
{
    [JsonProperty("name")]
    public string Name { get; set; }
}
public class Types
{
    [JsonProperty("type")]
    public Type Type;
}
public class Ability
{
    [JsonProperty("name")]
    public string Name { get; set; }
}
public class Abilities
{
    [JsonProperty("ability")]
    public Ability Ability;
}
public class Pokemon
{
    [JsonProperty("name")]
    public String Name { get; set; }

    [JsonProperty("id")]
    public String Id { get; set; }

    public List<Types> Types { get; set; }
    public List<Abilities> Abilities { get; set; }
}
class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        await ProcessRepositories();
    }

    private static async Task ProcessRepositories()
    {
        try
        {
            while (true)
            {
                Console.WriteLine("\nEnter Pokemon name. Leave empty to exit");
                var name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    break;
                }

                var result = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/" + name.ToLower());
                var resultRead = await result.Content.ReadAsStringAsync();

                var pokemon = JsonConvert.DeserializeObject<Pokemon>(resultRead);

                Console.WriteLine("---");
                Console.WriteLine(pokemon.Name + ", Dex Number " + pokemon.Id);
                Console.WriteLine("Type(s):");
                pokemon.Types.ForEach(t=>Console.Write(" " + t.Type.Name));
                Console.WriteLine("\nPossible Abilities:");
                pokemon.Abilities.ForEach(a => Console.Write(" " + a.Ability.Name));
            }
        } catch (Exception)
        {
            Console.WriteLine("An error has occured. Please make sure to enter a U.S. zip code.");
        }

    }
}