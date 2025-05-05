using DCFApixels.DragonECS;
using UnityEngine;

public class EnemyDeathSystem : IEcsRun
{
    [DI] private EcsDefaultWorld _world;

    private Inc<EnemyView, HealthComponent> enemy;


    public void Run()
    {
        foreach (var e in (_world, enemy))
        {
            ref var model = ref e.Get<EnemyView>().view;
            ref var health = ref e.Get<HealthComponent>().Health;

            if (health <= 0)
            {
                var Die = _world.NewEntityLong().Get<CountRequest>();
                Die = new CountRequest();

                Debug.Log(Die);
                GameObject.Destroy(model.gameObject);
                _world.DelEntity(e);
            }
        }
    }
}
