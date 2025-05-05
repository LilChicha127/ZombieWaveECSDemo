using DCFApixels.DragonECS;
using System.Collections;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Legacy
{
	public class Projectile : MonoBehaviour
	{
        [SerializeField] private BulletPool _pool;
		[SerializeField] private int _damage;

		public void SetProjectile(BulletPool pool, int damage)
		{
			_pool = pool;
			_damage = damage;
		}

        private void OnEnable()
        {
			StartCoroutine(ProjectileDeath());
        }

        private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.TryGetComponent(out IDamagable enemy))
			{
				Debug.Log("hit");
                enemy.Damage(_damage);
                _pool.ReturnBullet(this.gameObject);
            }

			_pool.ReturnBullet(this.gameObject);
		}

		private IEnumerator ProjectileDeath()
		{
			yield return new WaitForSeconds(1f);
			if (this.gameObject.activeSelf)
			{
				_pool.ReturnBullet(this.gameObject);
			}
		}
	}
}