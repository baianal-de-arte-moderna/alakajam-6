using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgTile : MonoBehaviour
{
    SpriteRenderer render;
    public float colorDelay = 2f;
    Color targetColor;
    bool active = false;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        render.color = Color.clear;
        targetColor = Color.clear;
        if (Random.Range(0f, 1f) > 0.5f)
        {
            Invoke("ClearColor", 0f);
        }
        else
        {
            Invoke("ChangeColor", 0f);            
        }
        Invoke("Activate", colorDelay / 2f);
    }

    void ChangeColor()
    {
        targetColor = Color.HSVToRGB(Random.Range(0f, 1f), 0.6f, 0.6f);
        Invoke("ClearColor", colorDelay + Random.Range(0f, colorDelay));
    }

    void ClearColor()
    {
        targetColor = Color.clear;
        Invoke("ChangeColor", colorDelay + Random.Range(0f, colorDelay));
    }

    void Activate()
    {
        active = true;
    }

    void Update()
    {
        if (active)
        {
            render.color = Color.Lerp(
                render.color,
                targetColor,
                colorDelay * Time.deltaTime
            );
        }
    }
}
