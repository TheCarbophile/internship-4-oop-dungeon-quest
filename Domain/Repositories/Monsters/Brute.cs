using Domain.Repositories.Enums;

namespace Domain.Repositories.Monsters;

public class Brute : Monster
{
    public const int ChanceToSpawn = 2;
    public Brute() : base(140, 35)
    {
    }
    
    public override Attack? MakeDecision(Battle battle)
    {
        if (IsStunned)
        {
            IsStunned = false;
            return null;
        }
        if (Random.Next(1, 5) == 4)
        {
            battle.Hero.TakeDamage(battle.Hero.HealthPoints / 4);
            Console.WriteLine("Brute je iskoristio svoj ability i oduzeo 25% HP-a!");
            return null;
        }
        return Random.Next(1, 4) switch
        {
            1 => Attack.Direct,
            2 => Attack.Side,
            3 => Attack.Counter
        };
    }
}