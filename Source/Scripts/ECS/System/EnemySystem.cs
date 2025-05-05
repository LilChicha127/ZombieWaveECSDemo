using UnityEngine;
using DCFApixels.DragonECS;

public class EnemySystem : IEcsRun
{
    [DI] private readonly EcsDefaultWorld _world;
    
    private EcsEntityConnect _entityConnect;

    private Vector3 _position;

    private Inc<EnemyComponent, EnemyView, EnemyAnim> enemy;

    public EnemySystem(Vector3 position)
    {
        _position = position;
    }
    public void Run()
    {
        foreach(var e in (_world, enemy))
        {
            ref var model = ref e.Get<EnemyView>();
            ref var enemy = ref e.Get<EnemyComponent>();

            var position = model.view.transform.position;
            var direction = (_position - position);
            float distance = direction.magnitude;
            float speed = Mathf.Lerp(enemy.MinSpeed, enemy.MaxSpeed, Mathf.Clamp01(distance / 5f));

            if (distance < 0.5f)
            {
                model.view.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                return;
            }

            direction.Normalize();
            model.view.transform.forward = direction;
            model.view.GetComponent<Rigidbody>().linearVelocity = direction * speed;
        }
    }
}
