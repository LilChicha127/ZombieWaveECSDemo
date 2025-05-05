using UnityEngine;

[CreateAssetMenu (fileName = "Wave", menuName = "New wave", order = 0)]
public class WaveData : ScriptableObject
{
    [Header("������� ������ �� �����: ")]
    [field: SerializeField] public int ZombieCount;
    [Header("����� ���� ��������� �� ���� �����")]
    [field: SerializeField] public Enemy Enemy;
}
