using System.Runtime.ExceptionServices;

namespace Domain.Repositories.Monsters;

public class Goblin : Monster
{
    public const int ChanceToSpawn = 7;
    public Goblin() : base(65, 25)
    {
    }
}