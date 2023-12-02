namespace Domain.Repositories.Monsters;
using Enums;

public class Witch : Monster
{
    public const int ChanceToSpawn = 1;
    public Witch() : base(70, 40)
    {
    }
    
    public override Attack? MakeDecision(Battle battle)
    {
        if (IsStunned)
        {
            IsStunned = false;
            return null;
        }
        if (Random.Next(1, 11) == 10)
        {
            battle.Mess();
            Console.WriteLine("Witch je iskoristila svoj ability i svima namjestila nasumicni HP!");
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