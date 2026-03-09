using UnityEngine;
using UnityEngine.Video; // <-- Required to control video players!

public class IntroManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public VideoPlayer introVideo;
    public GameObject player;

    void Start()
    {
        // 1. Freeze the player so they can't walk around during the video
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }

        // 2. Tell Unity to listen for the exact moment the video finishes
        if (introVideo != null)
        {
            introVideo.loopPointReached += OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 1. Unfreeze the player
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
        }

        // 2. Turn off the entire Video Player object so the game world is revealed
        gameObject.SetActive(false);
    }
}