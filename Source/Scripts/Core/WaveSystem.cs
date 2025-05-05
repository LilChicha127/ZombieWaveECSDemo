using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSystem 
{
    public Transform[] SpawnPoints;
    public WaveContainer _waveContainer;

    [SerializeField] private List<Enemy> Enemys;
    private int waveID;

    public WaveData NextWave() 
    {
        waveID++;
        
        if(waveID > _waveContainer.Waves.Count)
        {
            return _waveContainer.Waves[_waveContainer.Waves.Count];
        }
        else
        {
            return _waveContainer.Waves[waveID];
        }
    }

    public EnemySpawn GetSpawnData()
    {
        var waveData = NextWave();

        if (waveData.Enemy != null)
        {
            Enemys.Add(waveData.Enemy);
        }

        EnemySpawn wave = new EnemySpawn()
        {
            spawnPoints = SpawnPoints,
            zombieCount = waveData.ZombieCount,
            spawnInterval = 1,
            timeUntilNextSpawn = 1,
            viewEnemys = Enemys
        };

        return wave;
    }
}
