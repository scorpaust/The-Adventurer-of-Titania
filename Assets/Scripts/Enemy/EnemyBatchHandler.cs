using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatchHandler : MonoBehaviour
{
    [SerializeField]
    private List<CharacterMovement> enemies;

	private void Start()
	{
		foreach (Transform tr in GetComponentInChildren<Transform>())
		{
			if (tr != this)
			{
				enemies.Add(tr.GetComponent<CharacterMovement>());
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
}
