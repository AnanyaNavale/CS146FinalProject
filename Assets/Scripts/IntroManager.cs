using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public VideoPlayer videoPlayer;
    public VideoClip textVideo;     // Your new plot text video
    public VideoClip anomalyVideo;  // The clock tower collapse video
    public GameObject player;

    private bool isPlayingSecondVideo = false;

    void Start()
    {
        // 1. Freeze the player
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }

        // 2. Set up the video player and tell it what to do when a video ends
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;

            // Load the first "tape" and hit play!
            videoPlayer.clip = textVideo;
            videoPlayer.Play();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (!isPlayingSecondVideo)
        {
            // The text video just finished! Swap to the tower collapse.
            isPlayingSecondVideo = true;
            videoPlayer.clip = anomalyVideo;
            videoPlayer.Play();
        }
        else
        {
            // The tower collapse just finished! Start the game.
            if (player != null)
            {
                player.GetComponent<PlayerMovement>().enabled = true;
            }

            // Hide the video player screen
            gameObject.SetActive(false);
        }
    }
}