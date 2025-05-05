using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 50;
    [SerializeField] private Transform spawnPoint;

    private GameObject projectile;


    private Queue<GameObject> pool = new Queue<GameObject>();

    public void Init(GameObject bullet)
    {
        ClearPool();
        projectile = bullet;

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(projectile);
            bullet.SetActive(false);
            bullet.transform.parent = spawnPoint;
            bullet.transform.position = spawnPoint.position;
            pool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet() 
    {
        if (pool.Count > 0)
        {
            GameObject bullet = pool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(projectile);
            bullet.SetActive(true);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.transform.parent = spawnPoint;
        bullet.transform.position = spawnPoint.position;
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }

    private void ClearPool()
    {
        foreach (var obj in pool)
        {
            Destroy(obj);
        }
        pool.Clear();
    }
}
