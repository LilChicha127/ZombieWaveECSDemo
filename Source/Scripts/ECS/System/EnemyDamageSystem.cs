using DCFApixels.DragonECS;

public class EnemyDamageSystem : IEcsRun
{
    [DI] EcsDefaultWorld _world;

    private Inc<DamageComponent, HealthComponent> aspect;

    public void Run()
    {
        foreach (var entity in (_world, aspect))
        {
            ref var _damage = ref entity.Get<DamageComponent>().value;
            ref var _health = ref entity.Get<HealthComponent>().Health;

            if (_damage > 0)
            {
                _health -= _damage;
                _damage = 0;
            }
        }
    }
}
