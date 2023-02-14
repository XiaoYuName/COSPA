using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Audio;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AudioSource))]
public class UGUIVideoPlay : UIBase
{
    private VideoPlayer videoPlayer;
    private RawImage rawImage;
    private AudioSource AudioSource;
    public override void Init()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        rawImage = GetComponent<RawImage>();
        AudioSource = GetComponent<AudioSource>();
    }
    
    public void  Play(VideoClip clip,Action action = null)
    {
        videoPlayer.clip = clip;
        videoPlayer.source = VideoSource.VideoClip;
        StartCoroutine(PlayVideo(null));
    }
    
    public void StarPlay(VideoClip clip)
    {
        videoPlayer.clip = clip;
        StartCoroutine(PlayVideo(null));
    }
    
    public IEnumerator PlayVideo(Action action)
    {
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0,AudioSource);
        AudioManager.Instance.SetSnapshot(AudioSnapshotsType.Video,1);
        videoPlayer.Play();
        action?.Invoke();
        yield return new WaitForSeconds((float)videoPlayer.clip.length);
        AudioManager.Instance.SetSnapshot(AudioSnapshotsType.Normal,2);
    }

    public VideoPlayer GetVideoPlayer()
    {
        return videoPlayer;
    }

    public override void Close()
    {
        base.Close();
        videoPlayer.clip = null;
    }
}
