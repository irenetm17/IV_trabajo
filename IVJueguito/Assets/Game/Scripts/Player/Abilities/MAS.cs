using UnityEngine;

public class MAS : MonoBehaviour
{
    public float amplitude = 1f;
    public float speed = 1f;

    private Vector3 startPosition;

    void Awake()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        float omega = 2f * Mathf.PI * speed;
        float dy = amplitude * Mathf.Sin(omega * Time.time);
        transform.localPosition = startPosition + Vector3.up * dy;
    }
}
