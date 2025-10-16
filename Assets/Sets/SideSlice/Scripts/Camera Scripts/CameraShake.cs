using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 1f;
    public AnimationCurve animCurve;

    public bool start = false;

    // Start is called before the first frame update
    void Update()
    {
        if(start)
        {
            start = false;
            StartCoroutine(ShakeEffect());
        }
    }

    public void LightScreenShake()
    {
        StartCoroutine(ShakeEffect());
    }

    IEnumerator ShakeEffect()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = animCurve.Evaluate(elapsedTime / duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPos;
    }
}
