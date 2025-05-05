using DCFApixels.DragonECS;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [field:SerializeField] public EcsEntityConnect Connect {  get; private set; }

    [SerializeField] private Animator animator;

    private EcsEntityConnect _mainHall;

    private Coroutine _attack;

    public void Damage(int damage)
    {
        Connect.Entity.World.NewEntityLong().Get<DamageComponent>()
                        = new DamageComponent()
                 {
                      value = damage,
                     connect = Connect
                 };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EcsEntityConnect hall))
        {
            if (hall.Entity.Has<MainHallComponent>()) 
            {
                _mainHall = hall;
                Debug.Log(_mainHall.name);
                animator.SetTrigger("Attack");
                _attack = StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            int damage = Connect.Entity.Get<EnemyComponent>().Damage;
            _mainHall.Entity.World.NewEntityLong().Get<MainHallAttackRequest>() = new MainHallAttackRequest()
            {
                damage = damage,
                connect = _mainHall
            };

            yield return new WaitForSeconds(5f);
        }
    }
}
