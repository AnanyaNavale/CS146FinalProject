using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public VideoPlayer videoPlayer;
    [Tooltip("Type the exact file name with .mp4")]
    public string textVideoName = "Setup.mp4";
    public string anomalyVideoName = "5_12_Anomaly.mp4";
    public GameObject player;

    [Header("Start Menu")]
    public GameObject startMenuCanvas;

    [Header("Music")]
    public GameObject musicManager; // Drag your MusicManager GameObject here

    private bool isPlayingSecondVideo = false;

    void Start()
    {
        if (player != null) player.GetComponent<PlayerMovement>().enabled = false;
        if (startMenuCanvas != null) startMenuCanvas.SetActive(true);
    }

    public void PlayCinematic()
    {
        if (startMenuCanvas != null) startMenuCanvas.SetActive(false);
        if (videoPlayer != null)
        {
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, textVideoName);
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (!isPlayingSecondVideo)
        {
            isPlayingSecondVideo = true;
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, anomalyVideoName);
            videoPlayer.Play();
        }
        else
        {
            // Both videos done — start music and resume game
            if (musicManager != null)
            {
                musicManager.SetActive(true);
                musicManager.GetComponent<MusicManager>().Play();
            }

            if (player != null) player.GetComponent<PlayerMovement>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}