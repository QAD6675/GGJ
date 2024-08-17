using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        SetLayers();
    }

 void SetLayers()
{
    parallaxLayers.Clear();

    ParallaxLayer[] layers = GetComponentsInChildren<ParallaxLayer>();

    for (int i = 0; i < layers.Length; i++)
    {
        ParallaxLayer layer = layers[i];
        layer.name = "Layer-" + i;
        parallaxLayers.Add(layer);
    }
}

    void Move(float delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}
