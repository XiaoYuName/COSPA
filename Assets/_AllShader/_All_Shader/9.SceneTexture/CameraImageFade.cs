using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraImageFade : MonoBehaviour
{
    public Shader FadeOut;
    public Vector2 Offset;
    [Range(0,1)]
    public float _Valua;
    public Texture TransitionTexture;


    private Material _material;

    private void Awake()
    {
        _Valua = -0.1f;
        DOTween.To(() => _Valua, x => _Valua = x, 1.1f, 2)
            .SetLoops(-1, LoopType.Yoyo);
        _material = new Material(FadeOut);
    }

    private void OnRenderImage(RenderTexture Source, RenderTexture destination)
    {
        if (FadeOut != null)
        {
            _material.SetTexture("_TransitionTex",TransitionTexture);
            _material.SetVector("_Offset",Offset);
            _material.SetFloat("_Value",_Valua);
            Graphics.Blit(Source,destination,_material);
        }
        else
        {
            Graphics.Blit(Source,destination);
        }
    }
}
