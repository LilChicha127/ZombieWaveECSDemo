using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Turret turret;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            turret.Upgrade(player);
        }
    }
}
