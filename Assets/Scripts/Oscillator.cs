using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private float speed = 2f;     // units per second
    [SerializeField] private float distance = 5f;  // length of the travel path

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Moves 0 -> distance -> 0 -> distance... at a constant speed
        float offset = Mathf.PingPong(Time.time * speed, distance);

        transform.position = startPosition + new Vector3(0f, 0f, offset);
    }
}
