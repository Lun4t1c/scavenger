using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleBase : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float amplitude = 0.25f;
    public float frequency = 6f;
    public float speed = 30f;

    private Vector3 startPosition;

    // Start is called before the first frame update
    public virtual void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        float yOffset = amplitude * Mathf.Sin(Time.time * frequency);
        float movementSpeed = speed * Time.deltaTime;

        Vector3 newPosition = startPosition + new Vector3(0f, yOffset, 0f);
        transform.position = Vector3.Lerp(transform.position, newPosition, movementSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        gameObject.SetActive(false);
    }
}
