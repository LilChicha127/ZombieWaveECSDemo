using DCFApixels.DragonECS;
using UnityEngine;

public class MainHallDamageSystem : IEcsRun
{
    [DI] EcsDefaultWorld _world;

    private Inc<MainHallAttackRequest> mainHall;

    public void Run()
    {
        foreach(var req in (_world, mainHall))
        {
            ref var request = ref req.Get<MainHallAttackRequest>();
            ref var health = ref request.connect.Entity.Get<MainHallComponent>().health;
            ref var slider = ref request.connect.Entity.Get<SliderComponent>().view;

            health -= request.damage;
            slider.value -= request.damage;
            _world.DelEntity(req);
        }
    }
}
