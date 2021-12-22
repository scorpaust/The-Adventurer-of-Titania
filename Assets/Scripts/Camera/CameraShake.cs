using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeAmount = 0.007f;

    private Vector3 camPos;

    private float camShakingOffsetX, camShakingOffsetY;

    private void StartCameraShaking()
	{
		if (shakeAmount > 0)
		{
			camPos = transform.position;

			camShakingOffsetX = Random.value * shakeAmount * 2 - shakeAmount;

			camShakingOffsetY = Random.value * shakeAmount * 2 - shakeAmount;

			camPos.x += camShakingOffsetX;

			camPos.y += camShakingOffsetY;

			transform.position = camPos;
		}
	}

    private void StopCameraShaking()
	{
		CancelInvoke("StartCameraShaking");

		transform.localPosition = Vector3.zero;
	}

    public void ShakeCamera(float shakeTime)
	{
		InvokeRepeating("StartCameraShaking", 0f, 0.01f);

		Invoke("StopCameraShaking", shakeTime);
	}
}
