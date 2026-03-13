using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    // We added this to fix the CS0103 error!
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD or Arrow Keys
        // Notice we removed "Vector2" here so it updates the main class variable
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Always update speed so the Animator knows when to switch between Idle and Walk
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // ONLY update the direction parameters if the player is actively pressing a key
        // This makes the Idle Blend Tree remember the last faced direction
        if (movement.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
    }

    void FixedUpdate()
    {
        // .normalized ensures the player doesn't move faster diagonally
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        
        // Play walking sound when moving, stop when not
        if (movement != Vector2.zero)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    // void FixedUpdate()
    // {
    //     float horizontal = Input.GetAxis("Horizontal");
    //     float vertical = Input.GetAxis("Vertical");

    //     Vector2 movement = new Vector2(horizontal, vertical);
    //     rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);

    //     // Play walking sound when moving, stop when not
    //     if (movement != Vector2.zero)
    //     {
    //         if (!audioSource.isPlaying)
    //             audioSource.Play();
    //     }
    //     else
    //     {
    //         audioSource.Stop();
    //     }
    // }
}