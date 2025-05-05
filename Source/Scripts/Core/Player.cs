using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField] public PlayerSO Config { get; set; }

    public void SetMoney(int money)
    {
        Config.Money -= money;
    }
}
