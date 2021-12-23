using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyShooterType
{
    Horizontal,
    Vertical,
    Stationary
}

public class EnemyShooter : MonoBehaviour
{
    [SerializeField]
    private EnemyShooterType enemyType = EnemyShooterType.Horizontal;

    [SerializeField]
    private float changingPosDelay = 2f;

    [SerializeField]
    private float moveSpeed = 0.75f;

    private float minXY_Pos, maxXY_Pos;

    private Vector3 minPos, maxPos;

    private float changingPosTimer;

    private Vector3 startingPos;

    private Vector3 targetPos;

    private bool changedPos;

    private Vector3 myScale;

	private void Awake()
	{
        startingPos = transform.position;

        if (enemyType == EnemyShooterType.Horizontal)
		{
            minXY_Pos = transform.GetChild(0).localPosition.x;

            maxXY_Pos = transform.GetChild(1).localPosition.x;
		}

        else if (enemyType == EnemyShooterType.Vertical)
        {
            minXY_Pos = transform.GetChild(0).localPosition.y;

            maxXY_Pos = transform.GetChild(1).localPosition.y;
        }
        else
		{
            minPos = transform.GetChild(0).transform.position;
            maxPos = transform.GetChild(1).transform.position;
            targetPos = maxPos;
        }

        changingPosTimer = Time.time + changingPosDelay;
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        EnemyMovement();
	}

    private void HandleFacingDirection()
	{
        myScale = transform.localScale;

        if (targetPos.x > transform.position.x)
		{
            myScale.x = -Mathf.Abs(myScale.x);
		}
        else if (targetPos.x < transform.position.x)
        {
            myScale.x = Mathf.Abs(myScale.x);
        }

        transform.localScale = myScale;
	}

	private void EnemyMovement()
	{
        if (enemyType == EnemyShooterType.Horizontal)
		{
            if (!changedPos)
			{
                float xPos = Random.Range(minXY_Pos, maxXY_Pos);

                targetPos = startingPos + Vector3.right * xPos;

                changedPos = true;
			}
		}
        else if (enemyType == EnemyShooterType.Vertical)
		{
            if (!changedPos)
			{
                float yPos = Random.Range(minXY_Pos, maxXY_Pos);

                targetPos = startingPos + Vector3.up * yPos;

                changedPos = true;
            }
        }
        else
		{
            if (!changedPos)
			{
                targetPos = maxPos == targetPos ? minPos : maxPos;

                changedPos = true;
			}
		}

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
		{
            if (Time.time > changingPosTimer)
			{
                changedPos = false;

                changingPosTimer = Time.time + changingPosDelay;
			}
		}

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        HandleFacingDirection();
    }
}
