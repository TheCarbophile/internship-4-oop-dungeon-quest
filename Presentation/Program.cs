using Domain.Repositories;
using Domain.Repositories.Enums;
using Domain.Repositories.Heroes;
using Domain.Repositories.Monsters;
using Data;

Console.WriteLine("Dobrodosli u Dungeon Crawler!");

MakeNewHero();

var battles = 0;
while (true)
{
    Console.WriteLine(Datastore.Hero is Enchanter
        ? $"{(Datastore.Hero.GetType()).ToString().Split(".")[3]} - HP: {Datastore.Hero.HealthPoints}, DMG: {Datastore.Hero.Damage}, MP: {((Enchanter)Datastore.Hero).Mana}"
        : $"{(Datastore.Hero.GetType()).ToString().Split(".")[3]} - HP: {Datastore.Hero.HealthPoints}, DMG: {Datastore.Hero.Damage}");
    Console.WriteLine("Želite li stupiti u bitku? (1 - Da, 2 - Ne)");
    var answer = Console.ReadLine();
    if (answer == "2")
    {
        Console.WriteLine("Tu ste da se borite!");
        continue;
    }
    if (answer != "1")
    {
        Console.WriteLine("Krivi unos!");
        continue;
    }
    Console.WriteLine($"Bitka {battles + 1}!");
    var battle = new Battle(Datastore.Hero);
    Console.WriteLine("Iz grma iskoči " + (battle.ActiveMonster.GetType()).ToString().Split(".")[3]);
    while (true)
    {
        Console.WriteLine($"{(battle.ActiveMonster.GetType()).ToString().Split(".")[3]} - HP: {battle.ActiveMonster?.HealthPoints}, DMG: {battle.ActiveMonster?.Damage}");
        Console.WriteLine(Datastore.Hero is Enchanter
            ? $"{(Datastore.Hero.GetType()).ToString().Split(".")[3]} - HP: {Datastore.Hero.HealthPoints}, DMG: {Datastore.Hero.Damage}, MP: {((Enchanter)Datastore.Hero).Mana}"
            : $"{(Datastore.Hero.GetType()).ToString().Split(".")[3]} - HP: {Datastore.Hero.HealthPoints}, DMG: {Datastore.Hero.Damage}");
        Console.WriteLine("Odaberite napad (1 - Direct, 2 - Side, 3 - Counter, 4 - Preskoči potez):");
        var heroAttack = Console.ReadLine();
        if (Datastore.Hero is Gladiator)
        {
            while (true)
            {
                Console.WriteLine("Želite li iskoristiti ability (dupli damage za 15% ukupnog HP-a (30 HP)? (1 - Da, 2 - Ne)");
                var rage = Console.ReadLine();
                if (rage == "1")
                {
                    ((Gladiator)Datastore.Hero).Rage();
                    break;
                }
                if (rage == "2")
                {
                    break;
                }
                Console.WriteLine("Krivi unos!");
            }
        }
        var stun = false;
        if (Datastore.Hero is Marksman)
        {
            stun = ((Marksman)Datastore.Hero).SpecialAttack();
        }
        if (heroAttack != "1" && heroAttack != "2" && heroAttack != "3" && heroAttack != "4")
        {
            Console.WriteLine("Krivi unos!");
            continue;
        }
        if (Datastore.Hero.CanAttack == false)
        {
            heroAttack = "4";
            Console.WriteLine("Heroj nije u stanju napasti!");
        }
        var monsterAttack = battle.ActiveMonster?.MakeDecision(battle);
        
        bool? battleResult = null;
        switch (heroAttack)
        {
            case "1":
                battleResult = battle.AdvanceBattle(Attack.Direct, monsterAttack, stun);
                break;
            case "2":
                battleResult = battle.AdvanceBattle(Attack.Side, monsterAttack, stun);
                break;
            case "3":
                battleResult = battle.AdvanceBattle(Attack.Counter, monsterAttack, stun);
                break;
            case "4":
                battleResult = battle.AdvanceBattle(null, monsterAttack, stun);
                break;
        }
        if (battleResult == true)
        {
            Console.WriteLine("Pobjeda!");
            battles++;
            Datastore.Hero.Win();
            Datastore.Hero.HealthPoints += Datastore.Hero.MaxHealthPoints / 4;
            Console.WriteLine($"XP: {Datastore.Hero.Experience}/100");
            Console.WriteLine("Potrošite pola XP-a za full heal (1 - Da, 2 - Ne)");
            var heal = Console.ReadLine();
            if (heal == "1")
            {
                Datastore.Hero.HealthPoints = Datastore.Hero.MaxHealthPoints;
                Datastore.Hero.Experience /= 2;
            }
            if (heal == "2")
            {
                Datastore.Hero.HealthPoints = Datastore.Hero.MaxHealthPoints;
            }
            break;
        }
        if (battleResult == false)
        {
            Console.WriteLine("Poraz!");
            Console.WriteLine("Želite li započeti novu igru? (1 - Da, 2 - Ne)");
            var newGame = Console.ReadLine();
            if (newGame == "1")
            {
                MakeNewHero();
                battles = 0;
                break;
            }
            if (newGame == "2")
            {
                Console.WriteLine("Hvala na igri!");
                return;
            }
            break;
        }
        if (battleResult == null)
        {
            Console.WriteLine("Neriješeno! Nastavljamo borbu!");
        }

        if (battles == 10)
        {
            Console.WriteLine("Čestitamo, pobijedili ste!");
            Console.WriteLine("Želite li započeti novu igru? (1 - Da, 2 - Ne)");
            var newGame = Console.ReadLine();
            if (newGame == "1")
            {
                MakeNewHero();
                battles = 0;
                break;
            }
            if (newGame == "2")
            {
                Console.WriteLine("Hvala na igri!");
                return;
            }
        }
    }
}

void MakeNewHero()
{
    while (true)
    {
        Console.WriteLine("Izaberite heroja (1 - Gladiator, 2 - Enchanter, 3 - Marksman):");
        var hero = Console.ReadLine();
        if (hero == "1")
        {
            Datastore.Hero = new Gladiator();
            break;
        }
        if (hero == "2")
        {
            Datastore.Hero = new Enchanter();
            break;
        }
        if (hero == "3")
        {
            Datastore.Hero = new Marksman();
            break;
        }
        Console.WriteLine("Krivi unos!");
    }

}