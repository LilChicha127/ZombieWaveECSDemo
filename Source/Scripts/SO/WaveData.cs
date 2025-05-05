using UnityEngine;

[CreateAssetMenu (fileName = "Wave", menuName = "New wave", order = 0)]
public class WaveData : ScriptableObject
{
    [Header("Сколько врагов на волне: ")]
    [field: SerializeField] public int ZombieCount;
    [Header("Какой враг откроется на этой волне")]
    [field: SerializeField] public Enemy Enemy;
}
