namespace Domain.Repositories.Heroes;

public class Marksman : Hero
{
    private int _criticalChance = 10;
    private int _stunChance = 10;
    private bool _critical = false;

    public Marksman() : base(100, 40)
    {
    }
    
    public bool SpecialAttack()
    {
        var random = new Random();
        var chance = random.Next(1, 101);
        if (chance <= _criticalChance)
        {
            Console.WriteLine("Marksman pokušava napraviti kritični udarac!");
            Damage *= 2;
            _critical = true;
        }
        chance = random.Next(1, 101);
        if (chance <= _stunChance)
        {
            Console.WriteLine("Marksman je uspio stunati protivnika!");
            return true;
        }
        return false;
    }
}