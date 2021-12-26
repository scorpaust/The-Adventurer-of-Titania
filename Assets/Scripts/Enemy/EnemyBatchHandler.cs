using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatchHandler : MonoBehaviour
{
	[SerializeField]
	private bool hasShooterEnemies;

    [SerializeField]
    private List<CharacterMovement> enemies;

	[SerializeField]
	private Transform shooterEnemiesHolder;

	[SerializeField]
	private List<EnemyShooter> shooterEnemies;

	[SerializeField]
	private GameObject batchDoor;

	private void Start()
	{
		if (hasShooterEnemies)
		{
			foreach (Transform tr in shooterEnemiesHolder.GetComponentInChildren<Transform>())
			{
				if (tr != this && tr.gameObject.activeInHierarchy)
				{
					shooterEnemies.Add(tr.GetComponent<EnemyShooter>());
				}
			}
		}
		else
		{
			foreach (Transform tr in GetComponentInChildren<Transform>())
			{
				if (tr != this && tr.gameObject.activeInHierarchy)
				{
					enemies.Add(tr.GetComponent<CharacterMovement>());
				}
			}
		}
	}

	private void CheckToUnlockDoor()
	{
		if (hasShooterEnemies)
		{
			if (enemies.Count == 0 && shooterEnemies.Count == 0)
			{
				batchDoor.SetActive(false);
			}
		}
		else
		{
			if (enemies.Count == 0)
			{
				batchDoor.SetActive(false);
			}
		}
	}

	public void EnablePlayerTarget()
	{
		foreach (CharacterMovement charMovement in enemies)
		{
			charMovement.HasPlayerTarget = true;
		}
	}

	public void DisablePlayerTarget()
	{
		foreach (CharacterMovement charMovement in enemies)
		{
			charMovement.HasPlayerTarget = false;
		}
	}

	public void RemoveEnemy(CharacterMovement enemy)
	{
		enemies.Remove(enemy);

		CheckToUnlockDoor();
	}

	public void RemoveShooterEnemy(EnemyShooter shooterEnemy)
	{
		if (shooterEnemies != null)
		{
			shooterEnemies.Remove(shooterEnemy);
		}

		CheckToUnlockDoor();
	}
}
