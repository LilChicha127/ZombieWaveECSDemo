using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Container", menuName = "New wave Container", order = 1)]
public class WaveContainer : ScriptableObject
{
    [field: SerializeField] public List<WaveData> Waves {  get; set; }
    
}
