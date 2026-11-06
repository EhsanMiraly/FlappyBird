using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds;

    private float speed = GameData.GameSpeed / 500f;

    private void FixedUpdate()
    {
        foreach (GameObject background in backgrounds)
        {
            background.transform.localPosition -= new Vector3(speed, 0f, 0f);
        }
    }

    private void LateUpdate()
    {
        if (backgrounds[0].transform.localPosition.x <= -65f)
        {
            backgrounds[0].transform.localPosition =
                backgrounds[backgrounds.Length - 1].transform.localPosition + new Vector3(71.9f, 0f, 0f);
        }

        for (int i = 1; i < backgrounds.Length; i++)
        {
            if (backgrounds[i].transform.localPosition.x <= -65f)
            {
                backgrounds[i].transform.localPosition =
                    backgrounds[i - 1].transform.localPosition + new Vector3(71.9f, 0f, 0f);
            }
        }
    }

}
