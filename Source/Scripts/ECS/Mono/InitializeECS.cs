using UnityEngine;
using DCFApixels.DragonECS;
using System.Collections.Generic;

public class InitializeECS : MonoBehaviour
{
    [SerializeField] private List<EcsEntityConnect> entityConnects;
    [SerializeField] private Transform _position;
    [SerializeField] private WaveSystem _waveSystem;

    private EcsPipeline _pipeline;
    private EcsDefaultWorld _world;

    private void Awake()
    {
        _world = new EcsDefaultWorld();
        var eventWorld = new EcsEventWorld();
        _pipeline = EcsPipeline.New()
            .Inject(_world, eventWorld)
            .Inject(_waveSystem, eventWorld)
            .Add(new EnemyCountSystem())
            .Add(new EnemySpawnSystem())
            .Add(new EnemySystem(_position.position))
            .Add(new EnemyDamageSystem())
            .Add(new EnemyDeathSystem())
            .Add(new MainHallDamageSystem())
            .Add(new MainHallDeathSystem())
            .AddUnityDebug(_world, eventWorld)
            .AutoInject(true)
            .BuildAndInit();

    }

    private void Start()
    {
        if (_pipeline != null)
        {
            for(int i = 0;  i < entityConnects.Count; i++)
            {
                int index = i;
                entityConnects[index].Connect(_world.NewEntityLong(), true);
            }
        }
    }

    private void Update()
    {
        _pipeline.Run();
    }
}