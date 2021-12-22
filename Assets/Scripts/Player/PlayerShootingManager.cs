using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField]
    private float shootingTimerLimite = 0.2f;

    [SerializeField]
    private Transform bulletSpawnPos;

    private Animator muzzleAnimator;

    private PlayerWeaponManager playerWeaponManager;

    private float shootingTimer;

	private void Awake()
	{
        playerWeaponManager = GetComponent<PlayerWeaponManager>();

        muzzleAnimator = bulletSpawnPos.GetComponent<Animator>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
	{
        if  (Input.GetMouseButtonDown(0))
		{
            if (Time.time > shootingTimer)
			{
                shootingTimer = Time.time + shootingTimerLimite;

                muzzleAnimator.SetTrigger(TagManager.SHOOT_ANIMATION_PARAMETER);

                CreateBullet();
			}
		}
	}

    private void CreateBullet()
	{
        playerWeaponManager.Shoot(bulletSpawnPos.position);
	}
}
