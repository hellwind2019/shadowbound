using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightContorller : MonoBehaviour
{
    // Start is called before the first frame update
    public Light playerLight;
    public float maxIntencity = 5f;
    public float minIntencity = 0.5f;
    public float intencityChangeSpeed = 1f;
    public float lightZoneTemperature = 1500f; // Температура в світлій зоні
    public float darkZoneTemperature = 11000f; // Температура в темряві
    public float temperatureChangeSpeed = 1f; // Швидкість зміни температури

    private bool isInLightZone = false;
    private float ligthZoneIntensity = 0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInLightZone)
        {
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, maxIntencity, ligthZoneIntensity * Time.deltaTime);
            playerLight.colorTemperature = Mathf.Lerp(playerLight.colorTemperature, lightZoneTemperature, temperatureChangeSpeed * Time.deltaTime);

        }
        else
        {
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, minIntencity, intencityChangeSpeed * Time.deltaTime);
            playerLight.colorTemperature = Mathf.Lerp(playerLight.colorTemperature, darkZoneTemperature, temperatureChangeSpeed * Time.deltaTime);

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightZone"))
        {
            isInLightZone = true;
            LightZone lightZone = other.GetComponent<LightZone>();
            ligthZoneIntensity = lightZone.lightIntensity;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightZone"))
        {
            isInLightZone = false;
        }
    }
}
