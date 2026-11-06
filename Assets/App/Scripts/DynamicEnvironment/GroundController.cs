using System;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] private GameObject[] grounds;

    private float speed = GameData.GameSpeed / 50f;

    private void FixedUpdate()
    {
        foreach (GameObject ground in grounds)
        {
            ground.transform.localPosition -= new Vector3(speed, 0f, 0f);
        }
    }

    private void LateUpdate()
    {
        if (grounds[0].transform.localPosition.x <= -65f)
        {
            grounds[0].transform.localPosition =
                grounds[grounds.Length - 1].transform.localPosition + new Vector3(66.8f, 0f, 0f);
        }

        for (int i = 1; i < grounds.Length; i++)
        {
            if (grounds[i].transform.localPosition.x <= -65f)
            {
                grounds[i].transform.localPosition =
                    grounds[i - 1].transform.localPosition + new Vector3(66.8f, 0f, 0f);
            }
        }
    }

}
