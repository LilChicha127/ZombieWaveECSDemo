using DCFApixels.DragonECS;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    private EcsDefaultWorld _world;

    public Transform[] SpawnPoints;
    public WaveContainer _waveContainer;

    [SerializeField] private List<Enemy> enemys;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI newEnemyText;
    [SerializeField] private int timerWave;
    
    private int _timer;
    private int _waveID;

    private Coroutine _waveCoroutine;

    public WaveData NextWave() 
    {
        _waveID++;

        Enemy enemy = _waveContainer.Waves[_waveID].Enemy;

        if (enemy != null)
        {
            NewEnemy(enemy);
        }
        if(_waveID > _waveContainer.Waves.Count)
        {
            return _waveContainer.Waves[_waveContainer.Waves.Count];
        }
        else
        {
            return _waveContainer.Waves[_waveID];
        }
    }

    public async void NewEnemy(Enemy enemy)
    {
        enemys.Add(enemy);
        newEnemyText.gameObject.SetActive(true);
        await Task.Delay(2500);
        newEnemyText.gameObject.SetActive(false);
    }

    public void StartWave(EcsDefaultWorld world)
    {
        _world = world;
        _timer = timerWave;
        _waveCoroutine = StartCoroutine(WaveTimer());
    }

    private IEnumerator WaveTimer()
    {
        while (true)
        {
            if(_timer > 0)
            {
                yield return new WaitForSeconds(1f);
                _timer--;
                timerText.text = $"До волны: {_timer} секунд";
            }
            else
            {
                timerText.text = " ";
                var waveData = NextWave();
                _world.NewEntityLong().Get<EnemySpawn>() = new EnemySpawn()
                {
                    spawnPoints = SpawnPoints,
                    zombieCount = waveData.ZombieCount,
                    spawnInterval = 1,
                    timeUntilNextSpawn = 1,
                    viewEnemys = enemys
                };

                _waveCoroutine = null;
                break;
            }
        }
    }
}
