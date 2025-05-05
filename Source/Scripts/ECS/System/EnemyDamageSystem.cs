using DCFApixels.DragonECS;

public class EnemyDamageSystem : IEcsRun
{
    [DI] EcsDefaultWorld _world;

    private Inc<DamageComponent> aspect;

    public void Run()
    {
        foreach (var entity in (_world, aspect))
        {
            ref var _damage = ref entity.Get<DamageComponent>();
            ref var _health = ref _damage.connect.Entity.Get<HealthComponent>().Health;
            ref var _slider = ref _damage.connect.Entity.Get<SliderComponent>().view;

            if (_damage.value > 0)
            {
                _health -= _damage.value;
                _slider.value -= _damage.value;

                _world.DelEntity(entity);
            }
        }
    }
}
