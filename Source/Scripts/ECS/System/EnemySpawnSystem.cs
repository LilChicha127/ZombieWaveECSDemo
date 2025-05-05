using DCFApixels.DragonECS;
using UnityEngine;

public class EnemySpawnSystem : IEcsRun
{
    [DI] private readonly EcsDefaultWorld _world;

    private readonly Inc<EnemySpawn> spawner;

    public void Run()
    {
        foreach (var spawn in (_world, spawner))
        {
            ref var main = ref spawn.Get<EnemySpawn>();
            ref var spawnPoints = ref main.spawnPoints;
            ref var entity = ref main.viewEnemys;
            ref var spawnInterval = ref main.spawnInterval;
            ref var zombieCount = ref main.zombieCount;


            if (spawnPoints != null)
            {
                ref var CountZomb = ref _world.NewEntityLong().Get<CountZombie>();
                CountZomb = new CountZombie()
                { 
                   value = zombieCount
                };

                for(int i = 0; i < zombieCount; i++)
                {
                    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    var enemy = EcsEntityConnect.Instantiate(entity[Random.Range(0, entity.Count)], spawnPoint.position, Quaternion.identity);
                    enemy.Connect(_world.NewEntityLong(), true);
                }

                _world.DelEntity(spawn);
            }
        }
    }
}
