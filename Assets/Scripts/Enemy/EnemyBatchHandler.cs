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

	private void Start()
	{
		foreach (Transform tr in GetComponentInChildren<Transform>())
		{
			if (tr != this)
			{
				enemies.Add(tr.GetComponent<CharacterMovement>());
			}
		}

		if (hasShooterEnemies)
		{
			foreach (Transform tr in shooterEnemiesHolder.GetComponentInChildren<Transform>())
			{
				if (tr != this)
				{
					shooterEnemies.Add(tr.GetComponent<EnemyShooter>());
				}
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
	}

	public void RemoveShooterEnemy(EnemyShooter shooterEnemy)
	{
		if (shooterEnemies != null)
		{
			shooterEnemies.Remove(shooterEnemy);
		}
	}
}
