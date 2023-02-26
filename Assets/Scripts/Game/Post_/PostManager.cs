using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ARPG
{
    public class PostManager : MonoSingleton<PostManager>
    {
        public VolumeProfile profile;

        [Button]
        public void StarTween()
        {
            ColorAdjustments component = profile.components[0] as ColorAdjustments;
            StartCoroutine(WaitTween(component));
        }

        public IEnumerator WaitTween(ColorAdjustments component)
        {
            component.postExposure.value = 0f;
            float temp = 0;
            while (temp <= 5)
            {
                temp += 0.1f;
                component.postExposure.value = temp;
                yield return null;
            }

            while (temp >= 0)
            {
                temp -= 0.1f;
                component.postExposure.value = temp;
                yield return null;
            }
            
            
        }
    }
}

