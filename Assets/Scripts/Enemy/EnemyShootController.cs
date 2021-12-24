using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBulletType
{
    Normal,
    Spread,
    SlowSpread
}

public class EnemyShootController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyBullet;

    [SerializeField]
    private int numberOfBullets = 3;

    [SerializeField]
    private EnemyBulletType bulletType = EnemyBulletType.Spread;

    [SerializeField]
    private float bulletSpeed = 1f;

	private void SpreadShoot (Vector3 direction, Vector3 origin, bool isBulletSlow)
	{
        float offset = 0.5f;

        for (int i = 0; i < numberOfBullets; i++)
		{
            Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            GameObject bullet = Instantiate(enemyBullet, origin, rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed / 5f;

            bullet.GetComponent<EnemyBullet>().SetIsSlow(isBulletSlow);

            direction.x += Random.Range(-offset, offset);

            direction.y += Random.Range(-offset, offset);
		}
	}

    private void NormalShoot (Vector3 direction, Vector3 origin)
	{
        float offset = 0.5f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            GameObject bullet = Instantiate(enemyBullet, origin, rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            direction.x += Random.Range(-offset, offset);

            direction.y += Random.Range(-offset, offset);
        }
    }

    public void ShootOnRandom(Vector3 direction, Vector3 origin)
    {
        int randShot = Random.Range(0, 3);

        if (randShot == 0)
            SpreadShoot(direction, origin, false);

        if (randShot == 1)
            SpreadShoot(direction, origin, true);

        if (randShot == 2)
            NormalShoot(direction, origin);
    }

    public void Shoot(Vector3 direction, Vector3 origin)
	{
        if (bulletType == EnemyBulletType.Spread)
            SpreadShoot(direction, origin, false);

        if (bulletType == EnemyBulletType.SlowSpread)
            SpreadShoot(direction, origin, true);

        if (bulletType == EnemyBulletType.Normal)
            NormalShoot(direction, origin);
    }
}
