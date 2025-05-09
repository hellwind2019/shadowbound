using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using cakeslice;
using TMPro;
using UnityEngine;

public class SwitchActivator : MonoBehaviour
{
    // Start is called before the first frame update

    private Renderer switchRenderer;
    public GameObject indicator;
    public GameObject controlledLight;
    private Material localMat;
    private Color originalEmission;
    public Color glowColor = Color.cyan;
    public float glowStrength = 5f;
    private Outline outline;


    private bool isActive, isPlayerNearby = false;
    void Start()
    {
        outline = GetComponent<Outline>();
        switchRenderer = indicator.GetComponent<Renderer>();
        localMat = switchRenderer.material;
        originalEmission = localMat.GetColor("_EmissionColor");
        outline.enabled = false;
    }

  
    void Update()
    {
       if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleSwitch();
        }

    }
    private void ToggleSwitch()
    {
        if (!isActive)
        {
            AcivateSwitch();
        }
        else
        {
            DeactivateSwitch();
        }
    }

    private void DeactivateSwitch()
    {
        localMat.SetColor("_EmissionColor", originalEmission);
        isActive = false;
        controlledLight.SetActive(false);
    }

    private void AcivateSwitch()
    {
        localMat.EnableKeyword("_EMISSION");
        localMat.SetColor("_EmissionColor", glowColor * glowStrength);
        isActive = true;
        controlledLight.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            outline.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            outline.enabled = false;
        }
    }
}
