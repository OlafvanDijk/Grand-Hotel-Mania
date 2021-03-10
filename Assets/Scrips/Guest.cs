﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Guest : MonoBehaviour
{
    //TODO Change to sprite instead of color
    [SerializeField] private List<Color> colors;
    [SerializeField] private float speed;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private List<Vector2> positions;
    private Transform guestTransform;

    // Start is called before the first frame update
    void Awake()
    {
        guestTransform = this.transform;
        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (positions != null && positions.Count > 0)
        {
            if (guestTransform.position.x == positions[0].x && guestTransform.position.y == positions[0].y)
            {
                positions.RemoveAt(0);
                if (positions.Count <= 0)
                {
                    //Interact With object
                    return;
                }
            }

            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            guestTransform.position = Vector2.MoveTowards(transform.position, positions[0], step);
        }
    }

    private void WalkToAndInteractWith(List<Vector2> positions, Object obj)
    {
        this.positions = positions;
    }

    /// <summary>
    /// Set the sprite of the guest
    /// </summary>
    private void SetSprite()
    {
        if (colors.Count > 0)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            int index = Random.Range(0, colors.Count);
            spriteRenderer.color = colors[index];
        }
    }
}