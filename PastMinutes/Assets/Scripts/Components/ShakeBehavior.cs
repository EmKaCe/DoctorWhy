using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ShakeBehavior : MonoBehaviour
{

    // Quelle: https://medium.com/@mattThousand/basic-2d-screen-shake-in-unity-9c27b56b516
    // Transform of the GameObject you want to shake
    private Transform transform;

    // Desired duration of the shake effect
    private float shakeDuration = 5.0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.7f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;


    UnityAction<int, string[]> shakeListener;

    void Start()
    {
        EventManager.StartListening(EventSystem.CameraShake(), shakeListener);
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
    void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }

        shakeListener = new UnityAction<int, string[]>(TriggerShake);
    }
    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    public void TriggerShake(int empty , string[] empty2)
    {
        Debug.Log("shake it");
        shakeDuration = 6.0f;
    }
}
