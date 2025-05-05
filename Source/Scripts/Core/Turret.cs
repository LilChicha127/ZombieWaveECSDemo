using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretSO _turret;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _shootSpeed = 1f;
    [SerializeField] private MuzzleBehaviour _muzzleBehavior;

    private int _idUP;
    private List<IDamagable> _enemys = new List<IDamagable>();

    private Coroutine _coroutine;

    private void Start()
    {
        _enemys.Clear();
    }

    public void Upgrade(Player player)
    {
        if(_turret.Upgrades.Count > _idUP && player.Config.Money >= _turret.Upgrades[_idUP+1].Price)
        {
            player.SetMoney(_turret.Upgrades[_idUP + 1].Price);
            _idUP += 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDamagable enemy))
        {
            _enemys.Add(enemy);
            if(_coroutine == null)
            {
                _coroutine = StartCoroutine(Shoot());
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable enemy))
        {
            _enemys.Remove(enemy);
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_shootSpeed);
        if (_enemys.Count > 0)
        {
            _enemys[0].Damage(_turret.Upgrades[_idUP].Damage);
            _muzzleBehavior.Effect();
        }
        else
        {
            _coroutine = null;
        }
    }
}
