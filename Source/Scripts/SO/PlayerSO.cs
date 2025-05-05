using UnityEngine;

[CreateAssetMenu(fileName ="Player", menuName ="Create player config")]
public class PlayerSO : ScriptableObject
{
    [field:SerializeField] public int Money { get; set; }
}
