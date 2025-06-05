using System.Collections;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public BallController ballController;
    public float shakeMagnitude = 0f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        shakeMagnitude = ballController.power / 15000.0f;
        Vector3 newPos = originalPosition + Random.insideUnitSphere * shakeMagnitude;
        transform.localPosition = newPos;
    }
}