using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    AudioManager audioManager;

    public void RotateBell()
    {
        transform.Rotate(Vector3.forward, -90);

        Debug.Log(Vector2.Dot(Vector2.up, Vector2.left));
    }

    public void InitializeBell(float angle)
    {
        transform.Rotate(Vector3.forward, angle);
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public Vector2 GetFacingDirection()
    {
        float rotatedAngle = transform.rotation.eulerAngles.z;

        if (rotatedAngle == 0)
        {
            return Vector2.down;
        }
        else if (rotatedAngle == 270)
        {
            return Vector2.left ;
        }
        else if(rotatedAngle == 180)
        {
            return Vector2.up;
        }
        else
        {
            return Vector2.right;
        }
    }

    public void PerformMove()
    {
        if (audioManager)
            audioManager.PlayBellSound();
        Debug.Log("Bell move");
    }


}
