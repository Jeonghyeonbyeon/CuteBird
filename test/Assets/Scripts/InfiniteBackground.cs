using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] private float backgroundSpeed = 2f;
    [SerializeField] private Transform[] backgrounds;
    private float backgroundWidth;
    private Vector3 startPosition;

    void Start()
    {
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = backgrounds[0].position;
    }

    void Update()
    {
        foreach (Transform bg in backgrounds)
        {
            bg.position += Vector3.left * backgroundSpeed * Time.deltaTime;

            if (bg.position.x < startPosition.x - backgroundWidth)
            {
                Vector3 newPosition = bg.position;
                newPosition.x += backgroundWidth * backgrounds.Length;
                bg.position = newPosition;
            }
        }
    }
}