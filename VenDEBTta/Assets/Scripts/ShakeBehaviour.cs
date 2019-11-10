using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{

    private Transform transformToShake;

    private float shakeDuration = 0f;

    private float shakeMagnitude = 0.3f;

    private float dampingSpeed = 1.0f;

    Vector3 initialPosition;

    // Start is called before the first frame update
    void Awake()
    {
        if(transformToShake == null)
        {
            transformToShake = GetComponent<Transform>();
        }
    }

    private void OnEnable()
    {
        initialPosition = transformToShake.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transformToShake.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float duration)
    {
        //Debug.Log("SHake bOi!");
        shakeDuration = duration;
    }
}
