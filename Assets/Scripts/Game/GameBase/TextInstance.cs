using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInstance : MonoBehaviour
{
    public ResourcesTest DamageText;
    public bool isInstance;
    public float time;

    public void Start()
    {
        StartCoroutine(InstanceText());
    }

    protected void Update()
    {
        if (Input.anyKey)
        {
            isInstance = !isInstance;
        }
    }

    protected IEnumerator InstanceText()
    {
        while (gameObject.activeSelf)
        {
            if (isInstance)
            {
                var Text = Instantiate(DamageText, transform.position, Quaternion.identity);
                Text.Show();
                yield return new WaitForSeconds(time);
            }

            yield return null;
        }
    }
}
