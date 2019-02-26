using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;


public class VideoController : MonoBehaviour
{
    public VideoPlayer video;
    public Slider slider;
    public float PlayUntil = 0.0f;
    public float timePaused = 0f;
    public float startTime;

    //properties of the video player
    bool isDone;

    public float getTimePaused
    {
        get { return timePaused; }
    }
    public bool IsPlaying
    {
        get { return video.isPlaying; }
    }

    public bool IsPrepared
    {
        get { return video.isPrepared; }
    }

    public bool IsDone
    {
        get { return isDone;  }
    }

    public float currentTime
    {
        get { return (float)video.time; }
    }
    
    public ulong Duration
    {
        get { return (ulong)(video.frameCount / video.frameRate); }
    }


    void OnEnable()
    {
        video.errorReceived += errorReceived;
        video.frameReady += frameReady;
        video.prepareCompleted += prepareCompleted;
        video.seekCompleted += seekCompleted;
        video.started += started;
        video.time = 0.1f;
    }

    void OnDisable()
    {
        video.errorReceived -= errorReceived;
        video.frameReady -= frameReady;
        video.prepareCompleted -= prepareCompleted;
        video.seekCompleted -= seekCompleted;
        video.started -= started;
    }

    void errorReceived(VideoPlayer v, string msg)
    {
        Debug.Log("video player error: " + msg);
    }

    void frameReady(VideoPlayer v, long frame)
    {

    }

    void prepareCompleted(VideoPlayer v)
    {
        Debug.Log("video player finished prepping");
        isDone = false;
    }

    void seekCompleted(VideoPlayer v)
    {
        Debug.Log("video player finished seeking");
        isDone = false;
    }

    void started(VideoPlayer v)
    {
       //  Debug.Log("video player started");
    }

    private void Start()
    {
        video.Prepare();
        Application.targetFrameRate = 20;
        video.targetCameraAlpha = 0.1f;
        video.time = 0.1f;
    }

    public void LoadVideo()
    {
        string temp = Application.dataPath + "/Video/" + "NotI.mp4";
        if (video.url == temp) return;
        video.url = temp;
        video.Prepare();
        Debug.Log("can set direct audio volume: " + video.canSetDirectAudioVolume);
        Debug.Log("can set playback speed: " + video.canSetPlaybackSpeed);
        Debug.Log("can set time: " + video.canSetTime);
        Debug.Log("can step: " + video.canStep);
    }

    public void PlayVideo()
    {
        if (!IsPrepared) return;
        if (IsPlaying) return;
        video.Play();
        timePaused += Time.time - startTime;
        startTime = 0f;

    }

    public void PauseVideo()
    {
        if (!IsPlaying) return;
        video.Pause();
        startTime = Time.time;

    }

    public void Seek(float time)
    {
        if (!video.canSetTime) return;
        if (!IsPrepared) return;
        video.time = time;
    }

    public void SetPlaybackSpeed(float speed)
    {
        if (!video.canSetPlaybackSpeed) return;
        video.playbackSpeed = speed;
    }

    public void IncrementPlaybackSpeed()
    {
        if (!video.canSetPlaybackSpeed) return;

        video.playbackSpeed += 1;
    }

    public void DecrementPlaybackSpeed()
    {
        if (!video.canSetPlaybackSpeed) return;

        video.playbackSpeed -= 1;
    }
    
}
