using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponManager[] playerWeapons;

	[SerializeField]
	private GameObject[] weaponBullets;

    private int weaponIndex;

	private Vector2 targetPos;

	private Vector2 direction;

	private Camera mainCam;

	private Vector2 bulletSpawnPos;

	private Quaternion bulletRot;

	private CameraShake camShake;

	[SerializeField]
	private float camShakeCoolDown = 0.2f;

	private void Awake()
	{
		weaponIndex = 0;

		playerWeapons[weaponIndex].gameObject.SetActive(true);

		mainCam = Camera.main;

		camShake = mainCam.GetComponent<CameraShake>();
	}

	private void Update()
	{
		ChangeWeapon();
	}

	private void ChangeWeapon()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			playerWeapons[weaponIndex].gameObject.SetActive(false);

			weaponIndex++;

			if (weaponIndex == playerWeapons.Length)
				weaponIndex = 0;

			playerWeapons[weaponIndex].gameObject.SetActive(true);
		}
	}

	public void ActivateGun(int gunIndex)
	{
		playerWeapons[weaponIndex].ActivateGun(gunIndex);
	}

	public void Shoot(Vector3 spawnPos)
	{
		targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

		bulletSpawnPos = new Vector2(spawnPos.x, spawnPos.y);

		direction = (targetPos - bulletSpawnPos).normalized;

		bulletRot = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

		/*
		GameObject newBullet = Instantiate(weaponBullets[weaponIndex], bulletSpawnPos, bulletRot);

		newBullet.GetComponent<Bullet>().MoveInDirection(direction);*/

		BulletPool.instance.FireBullet(weaponIndex, spawnPos, bulletRot, direction);

		camShake.ShakeCamera(camShakeCoolDown);
	}
}
