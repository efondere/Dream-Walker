using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Vector2 dimensions, startPosition;
    public float parallaxFactor;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        dimensions= GetComponent<SpriteRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp = cam.transform.position * (1 - parallaxFactor);
        Vector2 distance = cam.transform.position * parallaxFactor;
        Vector3 newPosition = startPosition + distance;
        newPosition.z = transform.position.z;
        transform.position = newPosition;

        if (temp.x > startPosition.x + (dimensions.x / 2))
        {
            startPosition.x += dimensions.x;
        }
        else if (temp.x < startPosition.x - (dimensions.x / 2))
        {
            startPosition.x -= dimensions.x;
        }

        if (temp.y > startPosition.y + (dimensions.y / 2))
        {
            startPosition.y += dimensions.y;
        }
        else if (temp.y < startPosition.y - (dimensions.y / 2))
        {
            startPosition.y -= dimensions.y;
        }
    }
}
