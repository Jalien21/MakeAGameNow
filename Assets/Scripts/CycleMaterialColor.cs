﻿using UnityEngine;
using System.Collections;

public class CycleMaterialColor : MonoBehaviour
{
    public string colorPropertyName = "Color";
    public float speed = 0.1f;
    public float saturation = 1.0f;
    public float brightness = 1.0f;

    public float hue = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hue += speed * Time.deltaTime;
        while(hue > 1.0f)
        {
            hue -= 1.0f;
        }

        while(hue < 0.0f)
        {
            hue += 1.0f;
        }
        GetComponent<Renderer>().material.SetColor(colorPropertyName, new HSBColor(hue, saturation, brightness).ToColor());
    }
}