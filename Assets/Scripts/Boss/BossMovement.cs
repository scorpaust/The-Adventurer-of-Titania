using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField]
    private float normalSpeed = 0.5f, playerDetectedSpeed = 1f;

    [SerializeField]
    private Transform[] movementPos;

	[SerializeField]
	private float damageAmount = 20f;

	[SerializeField]
	private float shootTimeDelay = 2f;

	[SerializeField]
	private GameObject door;

	private float shootTimer;

	private EnemyShootController enemyShootController;

	private CharacterHealth bossHealth;

    private float moveSpeed;

    private Vector3 targetPos;

    private Vector3 myScale;

	private bool playerDetected;

	private Transform playerTarget;

	private bool chasePlayer;

	private void Start()
	{
		playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;

		enemyShootController = GetComponent<EnemyShootController>();

		bossHealth = GetComponent<CharacterHealth>();

		moveSpeed = normalSpeed;

		GetRandomMovementPosition();
	}

	private void Update()
	{
		if (!playerTarget) return;

		if (!bossHealth.IsAlive()) return;

		HandleMovement();

		HandleFacingDirection();

		HandleShooting();
	}

	private void OnDisable()
	{
		if (!bossHealth.IsAlive())
			door.SetActive(false);
	}

	private void GetRandomMovementPosition()
	{
		int randomIndex = Random.Range(0, movementPos.Length);

		while (targetPos == movementPos[randomIndex].position)
		{
			randomIndex = Random.Range(0, movementPos.Length);
		}

		targetPos = movementPos[randomIndex].position;
	}

	private void HandleMovement()
	{
		transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

		if (Vector3.Distance(transform.position, targetPos) < 0.1f)
		{
			if (playerDetected)
			{
				if (Random.Range(0, 10) > 7)
				{
					targetPos = playerTarget.position;

					chasePlayer = true;
				}
			}
			else
			{
				if (!chasePlayer)
				{
					GetRandomMovementPosition();
				}
			}
		}
	}

	private void HandleFacingDirection()
	{
		myScale = transform.localScale;

		if (targetPos.x > transform.position.x)
		{
			myScale.x = Mathf.Abs(myScale.x);
		}
		else if (targetPos.x < transform.position.x)
		{
			myScale.x = -Mathf.Abs(myScale.x);
		}

		transform.localScale = myScale;
	}

	private void PlayerDetectedChangeMovementSpeed(bool detected)
	{
		if (playerDetected)
		{
			moveSpeed = playerDetectedSpeed;
		}
		else
		{
			moveSpeed = normalSpeed;
		}
	}

	private void HandleShooting()
	{
		if (playerDetected)
		{
			if (Time.time > shootTimer)
			{
				shootTimer = Time.time + shootTimeDelay;

				Vector2 direction = (playerTarget.position - transform.position).normalized;

				enemyShootController.ShootOnRandom(direction, transform.position);
			}
		}
	}

	public void PlayerDetectedInfo(bool detected)
	{
		playerDetected = detected;

		PlayerDetectedChangeMovementSpeed(detected);

		if (!playerDetected)
		{
			chasePlayer = false;

			GetRandomMovementPosition();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagManager.PLAYER_TAG))
		{
			chasePlayer = false;

			GetRandomMovementPosition();

			collision.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
		}
	}
}
