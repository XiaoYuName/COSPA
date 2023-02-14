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
    private RenderTexture ouTexture;
    public override void Init()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        rawImage = GetComponent<RawImage>();
        AudioSource = GetComponent<AudioSource>();
    }
    
    public void  Play(VideoClip clip,bool isLoop,Action action = null)
    {
        videoPlayer.clip = clip;
        videoPlayer.isLooping = isLoop;
        videoPlayer.source = VideoSource.VideoClip;
        StartCoroutine(PlayVideo(null));
    }
    
    public void StarPlay(VideoClip clip,bool isCreatRawImage)
    {
        videoPlayer.clip = clip;
        if (isCreatRawImage && ouTexture == null)
        {
            ouTexture = new RenderTexture(1920, 1080, 16, RenderTextureFormat.ARGB32);
            videoPlayer.targetTexture = ouTexture;
            rawImage.texture = ouTexture;
        }
        StartCoroutine(PlayVideo(null));
    }
    
    public IEnumerator PlayVideo(Action action)
    {
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0,AudioSource);
        videoPlayer.Play();
        action?.Invoke();
        yield return new WaitForSeconds((float)videoPlayer.clip.length);
    }
    

    public override void Close()
    {
        base.Close();
        videoPlayer.Stop();
        if (ouTexture != null)
        {
            Destroy(ouTexture);
        }
        videoPlayer.clip = null;
    }
}
