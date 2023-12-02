namespace Domain.Repositories.Heroes;

public abstract class Hero : Entity
{
    public int Experience { get; set; }
    public int MaxHealthPoints { get; private set; }
    public bool CanAttack { get; protected set; } = true;
    
    protected Hero(int healthPoints, int damage) : base(healthPoints, damage)
    {
        MaxHealthPoints = healthPoints;
    }
    
    public void Win()
    {
        Experience += 30;
        if (Experience >= 100)
        {
            LevelUp();
        }

        HealthPoints = HealthPoints + MaxHealthPoints / 4;
    }
    
    protected virtual void LevelUp()
    {
        if (Experience >= 100) return;
        HealthPoints += 15;
        MaxHealthPoints += 15;
        Damage += 5;
        Experience -= 100;
        Console.WriteLine($"Hero je level upao! XP: {Experience}, HP: {HealthPoints}, Max HP: {MaxHealthPoints}, Damage: {Damage}");
    }
    
    public virtual void AdvanceRound(bool attack)
    {
    }
}