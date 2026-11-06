using UnityEngine;

public class BirdBody : MonoBehaviour
{
    private BirdController birdController;
    private Rigidbody2D birdControllerRB2D;

    private AudioSource audioSource;
    [SerializeField] private AudioClip hit_AudioClip;
    [SerializeField] private AudioClip point_AudioClip;



    private void OnEnable()
    {
        birdController = GetComponentInParent<BirdController>();
        birdControllerRB2D = birdController.GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        if (!GameData.IsPlaying)
            return;
        if (birdControllerRB2D == null)
            return;

        float velocity = birdControllerRB2D.linearVelocity.y;
        velocity = Mathf.Clamp(velocity, -birdController.flyUpForce, birdController.flyUpForce);
        float angle = velocity * 4f;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }




    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!GameData.IsPlaying)
            return;

        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Pipe"))
        {
            PlaySound(hit_AudioClip, 0.5f);
            EventsManager.InvokeLoseGame_Event();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameData.IsPlaying)
            return;

        if (other.gameObject.CompareTag("Reward"))
        {
            PlaySound(point_AudioClip, 0.5f);
            GameData.Points++;
        }
    }


    private void PlaySound(AudioClip audioClip, float volume)
    {
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
