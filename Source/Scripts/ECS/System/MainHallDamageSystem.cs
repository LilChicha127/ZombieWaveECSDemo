using DCFApixels.DragonECS;
using UnityEngine;

public class MainHallDamageSystem : IEcsRun
{
    [DI] EcsDefaultWorld _world;

    private Inc<MainHallAttackRequest> hallRequest;
    private Inc<MainHallComponent> mainHall;

    public void Run()
    {
        foreach(var req in (_world, hallRequest))
        {
            if (req.IsAlive)
            {
                ref var request = ref req.Get<MainHallAttackRequest>();
                Debug.Log("HAVE REQUEST");

                foreach (var hall in (_world, mainHall))
                {
                    if (hall.IsAlive)
                    {
                        ref var health = ref hall.Get<MainHallComponent>().health;
                        health -= request.damage;

                        if(health <= 0)
                        {
                            _world.NewEntityLong().Get<MainHallDefeate>() = new MainHallDefeate();
                        }
                    }
                }

                _world.DelEntity(req);
            }
        }
    }
}
