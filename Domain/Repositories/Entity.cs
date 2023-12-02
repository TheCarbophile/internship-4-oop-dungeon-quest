using Domain.Repositories.Enums;

namespace Domain.Repositories;

public abstract class Entity
{
    public int HealthPoints { get; set; }
    public int Damage { get; protected set; }
    public bool IsAlive { get; protected set; } = true;
    protected readonly Random Random = new();
    
    protected Entity(int healthPoints, int damage)
    {
        HealthPoints = healthPoints;
        Damage = damage;
    }
    
    public virtual void TakeDamage(int damage)
    {
        HealthPoints -= damage;
        if (HealthPoints >= 0) return;
        HealthPoints = 0;
        IsAlive = false;
    }

    public void Mess()
    {
        HealthPoints = new Random().Next(1, 141);
    }
}