using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using UnityEngine;

namespace ARPG.Audio.Item
{
    public class AudioGame : MonoBehaviour
    {
        [HideInInspector]public string aduioID;
        [HideInInspector]public AudioSource AudioSource;
        private List<Action> Event = new List<Action>();

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            AudioSource.Play();
            StartCoroutine(nameof(Wait));
        }

        public void Play(Action func)
        {
            AudioSource.Play();
            Event.Add(func);
            StartCoroutine(nameof(Wait));
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(AudioSource.clip.length);
            aduioID = default;
            AudioSource.clip = null;
            AudioSource.outputAudioMixerGroup = null;
            AudioSource.volume = 0.5f;
            AudioSource.pitch = 1f;
            foreach (var action in Event)
            {
                action?.Invoke();
            }
            gameObject.SetActive(false);
            Event.Clear();
        }
    }
}

