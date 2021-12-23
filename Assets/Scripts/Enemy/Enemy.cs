using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterMovement
{
    // For testing. Delete later.
    // public bool CHASE_PLAYER;

    [SerializeField]
    private float damageCoolDownThreshold = 1f;

    [SerializeField]
    private float damageAmount = 10f;

    [SerializeField]
    private float chaseSpeed = 0.8f;

    [SerializeField]
    private float turningDelayRate = 1f;

    private Transform playerTarget;

    private Vector3 playerLastTrackPos;

    private Vector3 startingPos;

    private Vector3 enemyMovementMotion;

    private bool dealtDamageToPlayer;

    private float damageCoolDownTimer;

    private float lastFollowTime;

    private float turningTimeDelay = 1f;

    private Vector3 myScale;

    private CharacterHealth enemyHealth;

	protected override void Awake()
	{
		base.Awake();
	}

	private void Start()
	{
        playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;

        enemyHealth = GetComponent<CharacterHealth>();

        playerLastTrackPos = playerTarget.position;

        startingPos = transform.position;

        lastFollowTime = Time.time;

        turningTimeDelay = ((float)1f - (float)xSpeed);

        turningTimeDelay += 1f * turningDelayRate;
	}

	private void Update()
	{
        if (!playerTarget) return;

        if (!enemyHealth.IsAlive()) return;

        HandleFacingDirection();
	}

	private void FixedUpdate()
	{
        if (!playerTarget) return;

        if (!enemyHealth.IsAlive()) return;

        HandleChasingPlayer();
	}

	private void HandleChasingPlayer()
	{
        if (HasPlayerTarget)
		{
            if (!dealtDamageToPlayer)
			{
                ChasePlayer();
			} else
			{
                if (Time.time < damageCoolDownTimer)
				{
                    enemyMovementMotion = startingPos - transform.position;
				}
                else
				{
                    dealtDamageToPlayer = false;
				}
			}
		}
        else
		{
            enemyMovementMotion = startingPos - transform.position;

            if (Vector3.Distance(transform.position, startingPos) < 0.1f)
			{
                enemyMovementMotion = Vector3.zero;
			}
		}

        HandleMovement(enemyMovementMotion.x, enemyMovementMotion.y);
	}

    private void ChasePlayer()
	{
        if (Time.time - lastFollowTime > turningTimeDelay)
		{
            playerLastTrackPos = playerTarget.position;

            lastFollowTime = Time.time;
		}

        if (Vector3.Distance(transform.position, playerLastTrackPos) > 0.016f)
		{
            enemyMovementMotion = (playerLastTrackPos - transform.position).normalized * chaseSpeed;
		}
        else
		{
            enemyMovementMotion = Vector3.zero;
		}
	}

    private void HandleFacingDirection()
	{
        myScale = transform.localScale;

        if (HasPlayerTarget)
		{
            if (playerTarget.position.x > transform.position.x)
			{
                myScale.x = Mathf.Abs(myScale.x);
			}
            else if (playerTarget.position.x < transform.position.x)
            {
                myScale.x = -Mathf.Abs(myScale.x);
            }
		}
        else
		{
            if (startingPos.x > transform.position.x)
            {
                myScale.x = Mathf.Abs(myScale.x);
            }
            else if (startingPos.x < transform.position.x)
            {
                myScale.x = -Mathf.Abs(myScale.x);
            }
        }

        transform.localScale = myScale;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagManager.PLAYER_TAG))
		{
            damageCoolDownTimer = Time.time + damageCoolDownThreshold;

            dealtDamageToPlayer = true;

            collision.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
		}
	}
}
