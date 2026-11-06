using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip flyUp_AudioClip;

    private EnvironmentManager environmentManager;

    public float flyUpForce = 15f;

    private InputActions inputActions;
    private InputActions.BirdActions birdActions;

    private Rigidbody2D rb2D;


    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();

        environmentManager = GetComponentInParent<EnvironmentManager>();

        inputActions = new InputActions();
        birdActions = inputActions.Bird;
        birdActions.Enable();

        birdActions.Fly.performed += FlyUp;

        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0f;
    }

    private void OnDisable()
    {
        birdActions.Disable();
    }


    private void FlyUp(InputAction.CallbackContext context)
    {
        if (!GameData.IsPlaying)
            return;

        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, flyUpForce);

        PlaySound(flyUp_AudioClip, 0.5f);
    }

    private void PlaySound(AudioClip audioClip, float volume)
    {
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
