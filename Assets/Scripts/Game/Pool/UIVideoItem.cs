using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Audio;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIVideoItem : UIBase
{
    private VideoPlayer VideoPlayer;
    private RawImage RawImage;
    private AudioSource Source;
    public override void Init()
    {
        VideoPlayer = GetComponent<VideoPlayer>();
        this.RawImage = GetComponent<RawImage>();
        Source = GetComponent<AudioSource>();
    }

    public void StarPlay(VideoClip clip)
    {
        this.VideoPlayer.source = VideoSource.VideoClip;
        this.VideoPlayer.clip = clip;
        RenderTexture ouTexture = new RenderTexture((int)clip.width, (int)clip.height, 16, RenderTextureFormat.ARGB32);
        this.VideoPlayer.targetTexture = ouTexture;
        RawImage.texture = ouTexture;
        StartCoroutine(PlayVideo(ouTexture));
    }

    public void StarPlay(string url)
    {
        this.VideoPlayer.source = VideoSource.Url;
        this.VideoPlayer.url = url;
        RenderTexture ouTexture = new RenderTexture((int)VideoPlayer.width, (int)VideoPlayer.height, 16, RenderTextureFormat.ARGB32);
        this.VideoPlayer.targetTexture = ouTexture;
        RawImage.texture = ouTexture;
        StartCoroutine(PlayVideo(ouTexture));
    }

    IEnumerator PlayVideo(RenderTexture ouTexture)
    {
        VideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        VideoPlayer.SetTargetAudioSource(0,Source);
        AudioManager.Instance.SetSnapshot(AudioSnapshotsType.Video,1);
        this.VideoPlayer.Play();
        yield return new WaitForSeconds((float)VideoPlayer.clip.length);
        VideoPlayer.clip = null;
        RawImage.texture = null;
        AudioManager.Instance.SetSnapshot(AudioSnapshotsType.Normal,2);
        Destroy(ouTexture); //播放完毕后，销毁RenderTexture
        VideoPool.Instance.Release(this);
    }
}
