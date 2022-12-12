using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIVideoItem : UIBase
{
    private VideoPlayer VideoPlayer;
    private RawImage RawImage;
    public override void Init()
    {
        VideoPlayer = GetComponent<VideoPlayer>();
        this.RawImage = GetComponent<RawImage>();
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
        this.VideoPlayer.Play();
        yield return new WaitForSeconds((float)VideoPlayer.clip.length);
        VideoPlayer.clip = null;
        RawImage.texture = null;
        Destroy(ouTexture); //播放完毕后，销毁RenderTexture
        VideoPool.Instance.Release(this);
    }
}
