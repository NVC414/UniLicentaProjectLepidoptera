using UnityEngine;

public class HoverBob : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.25f;
    [SerializeField] private float frequency = 1f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, startPos.y + y, startPos.z);
    }
}