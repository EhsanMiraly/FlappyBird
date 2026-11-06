using UnityEngine;

public class Pipe : MonoBehaviour
{
    private Rigidbody2D rb2D;


    private void FixedUpdate()
    {
        if (rb2D == null)
            return;

        rb2D.linearVelocity = new Vector2(-GameData.GameSpeed, 0f);
    }


    public void InitializePipe()
    {
        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(30f, Random.Range(-5f, 5f), 0f);
        rb2D = GetComponent<Rigidbody2D>();
    }

}
