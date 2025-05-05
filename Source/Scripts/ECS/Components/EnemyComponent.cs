using UnityEngine;
using DCFApixels.DragonECS;
using System;
using System.Collections.Generic;

[Serializable]
public struct EnemyComponent : IEcsComponent
{
    public int Damage;
    public int MinSpeed;
    public int MaxSpeed;
}

[Serializable]
public struct EnemyView : IEcsComponent
{
    public Enemy enemy;
    public GameObject view;
}

[Serializable]
public struct EnemySpawn : IEcsComponent 
{
    public Transform[] spawnPoints;
    public int zombieCount;
    public float spawnInterval;
    public float timeUntilNextSpawn;
    public List<Enemy> viewEnemys;
}

[Serializable]
public struct EnemyAnim : IEcsComponent
{
    public Animator animator;
}

[Serializable]
public struct MainHall : IEcsComponent
{
    public GameObject view;
}

[Serializable]
public struct DamageComponent : IEcsComponent
{
    public int value;
    public EcsEntityConnect connect;
}

[Serializable]
public struct HealthComponent : IEcsComponent
{
    public int Health;
    public int MaxHealth;
}

public struct CountZombie : IEcsComponent
{
    public int value;
}

public class EnemyComponentT : ComponentTemplate<EnemyComponent>{ }
public class EnemyAnimT : ComponentTemplate<EnemyAnim>{ }
public class EnemyViewT : ComponentTemplate<EnemyView>{ }
public class EnemySpawnT : ComponentTemplate<EnemySpawn>{ }
public class MainHallTagT : ComponentTemplate<MainHall> { }
public class DamageT : ComponentTemplate<DamageComponent> { }
public class HealthT : ComponentTemplate<HealthComponent> { }

