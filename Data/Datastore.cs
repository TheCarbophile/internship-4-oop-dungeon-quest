namespace Data;
using Domain.Repositories.Heroes;

public class Datastore
{
    public static Hero Hero { get; set; }
    
    public Datastore(Hero hero)
    {
        Hero = hero;
    }
}