using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    private Animation anim;
    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    private IEnumerator Play()
    {
        yield return new WaitForSeconds(anim.clip.length);
        gameObject.SetActive(false);
    }
}
