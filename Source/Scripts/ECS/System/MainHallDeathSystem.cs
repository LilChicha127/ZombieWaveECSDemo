using DCFApixels.DragonECS;
using UnityEngine;

public class MainHallDeathSystem : IEcsRun
{
    [DI] EcsDefaultWorld _world;

    private Inc<MainHallDefeate> defeate;

    public void Run()
    {
        foreach (var hall in (_world, defeate))
        {
            Debug.Log("YOY LOST");

            _world.DelEntity(hall);
        }
    }
}
