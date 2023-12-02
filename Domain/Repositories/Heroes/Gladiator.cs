namespace Domain.Repositories.Heroes;

public class Gladiator : Hero
{
    private bool _rage = false;
    public Gladiator() : base(200, 20)
    {
    }

    public override void AdvanceRound(bool attack)
    {
        if (!_rage || !attack) return;
        _rage = false;
        Damage /= 2;
    }
    
    public void Rage()
    {
        Console.WriteLine("Gladiator se naljutio i udvostručio svoj damage na sljedeci napad!");
        _rage = true;
        Damage *= 2;
        HealthPoints -= 30;
    }
}