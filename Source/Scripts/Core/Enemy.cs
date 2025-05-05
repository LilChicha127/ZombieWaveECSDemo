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
        var request = Connect.Entity;
        request.Get<DamageComponent>().value = damage;
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
            _mainHall.World.NewEntityLong().Get<MainHallAttackRequest>() = new MainHallAttackRequest()
            {
                damage = damage
            };

            yield return new WaitForSeconds(5f);
        }
    }
}
