namespace Domain.Repositories.Monsters;
using Enums;
public abstract class Monster : Entity
{
    public bool IsStunned { get; set; }
    
    protected Monster(int healthPoints, int damage) : base(healthPoints, damage)
    {
    }
    
    public virtual Attack? MakeDecision(Battle battle)
    {
        if (IsStunned)
        {
            IsStunned = false;
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