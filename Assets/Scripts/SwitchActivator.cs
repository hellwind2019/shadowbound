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

    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerNearby){
            outline.enabled = true;
        }
        else{
            outline.enabled = false;
        }
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isActive)
            {
                localMat.EnableKeyword("_EMISSION");
                localMat.SetColor("_EmissionColor", glowColor * glowStrength);
                isActive = true;
                
            }
            else
            {
                localMat.SetColor("_EmissionColor", originalEmission);
                isActive = false;
            }
        }


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = false;
    }
}
