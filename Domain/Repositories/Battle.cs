using Domain.Repositories.Enums;

namespace Domain.Repositories;
using Monsters;
using Heroes;

public class Battle
{
    public readonly Hero Hero;
    public Monster? ActiveMonster;
    private List<Monster> _monsters = new();
    private readonly Random _random = new();
    
    public Battle(Hero hero)
    {
        Hero = hero;
        var monsterPool = new List<Monster>();
        for (var i = 0; i < Goblin.ChanceToSpawn; i++)
        {
            monsterPool.Add(new Goblin());
        }
        for (var i = 0; i < Brute.ChanceToSpawn; i++)
        {
            monsterPool.Add(new Brute());
        }

        ActiveMonster = monsterPool[_random.Next(monsterPool.Count)];
        monsterPool.Remove(ActiveMonster);
        if (ActiveMonster is Witch)
        {
            _monsters.Add(monsterPool[_random.Next(monsterPool.Count)]);
            monsterPool.Remove(_monsters[0]);
            _monsters.Add(monsterPool[_random.Next(monsterPool.Count)]);
            monsterPool.Remove(_monsters[1]);
        }
    }

    public bool? AdvanceBattle(Attack? heroAttack, Attack? monsterAttack, bool stun)
    {
        if (heroAttack == monsterAttack)
        {
            return null;
        }
        
        if (stun)
        {
            ActiveMonster.IsStunned = true;
        }
        switch (heroAttack)
        {
            case Attack.Direct when monsterAttack != Attack.Counter:
            case Attack.Side when monsterAttack != Attack.Counter:
            case Attack.Counter when monsterAttack != Attack.Direct:
                Console.WriteLine("Pogodak!");
                ActiveMonster.TakeDamage(Hero.Damage);
                if (!ActiveMonster.IsAlive)
                    ActiveMonster = null;
                if (_monsters.Count == 0)
                {
                    Hero.AdvanceRound(true);
                    return true;
                }
                ActiveMonster = _monsters[0];
                _monsters.RemoveAt(0);
                Hero.AdvanceRound(true);
                return null;
            case null when monsterAttack != null:
                Console.WriteLine("Protivnik iskroisti priliku i pogodi heroja!");
                Hero.TakeDamage(ActiveMonster.Damage);
                Hero.AdvanceRound(false);
                if (!Hero.IsAlive)
                    return false;
                return null;
            case null when monsterAttack == null:
                Console.WriteLine("Ništa se ne dogodi!");
                Hero.AdvanceRound(false);
                return null;
            default:
                Console.WriteLine("Heroj je pogođen!");
                Hero.TakeDamage(ActiveMonster.Damage);
                Hero.AdvanceRound(true);
                if (!Hero.IsAlive)
                    return false;
                return null;
        }
    }
    
    public void Mess()
    {
        Hero.Mess();
        ActiveMonster.Mess();
        foreach (var monster in _monsters)
        {
            monster.Mess();
        }
    }
}