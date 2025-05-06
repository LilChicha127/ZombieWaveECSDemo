using DCFApixels.DragonECS;
using UnityEngine;

public class EnemyCountSystem : IEcsRun
{
    [DI] EcsDefaultWorld _world;
    [DI] WaveSystem waveSystem;

    private Inc<CountZombie> count;
    private Inc<CountRequest> countRequest;

    private int _counter;

    public void Run()
    {
        foreach(var counter in (_world, count))
        {
            int value = counter.Get<CountZombie>().value;
            _counter = value;
            _world.DelEntity(counter);
            Debug.Log(_counter);
        }

        foreach(var request in (_world, countRequest))
        {
            if(_counter <= 1)
            {
                waveSystem.StartWave(_world);
            }

            _counter -= 1;
            Debug.Log(_counter);
            _world.DelEntity(request);
        }
    }
}
