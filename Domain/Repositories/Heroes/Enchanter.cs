namespace Domain.Repositories.Heroes;

public class Enchanter : Hero
{
    public int Mana { get; private set; } = 50;
    public bool SecondChance { get; private set; } = true;
    
    public Enchanter() : base(65, 65)
    {
    }
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (HealthPoints > 0) return;
        if (!SecondChance) return;
        SecondChance = false;
        HealthPoints = MaxHealthPoints;
        Console.WriteLine("Prije nego što se Enchanter onesvijestio je iskoristio svoj special ability i vratio se na full HP!");
        IsAlive = true;
    }
    
    protected override void LevelUp()
    {
        base.LevelUp();
        Mana += 10;
    }

    public override void AdvanceRound(bool attack)
    {
        if (attack)
        {
            Mana -= 15;
            if (Mana < 15)
            {
                CanAttack = false;
            }
            return;
        }
        Mana += 15;
        CanAttack = true;
    }
}