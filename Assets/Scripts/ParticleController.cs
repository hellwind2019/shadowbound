using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private Transform player; // Посилання на гравця
    [SerializeField] private float maxAngularVelocity = 10f; // Максимальна кутова швидкість
    [SerializeField] private float proximityThreshold = 5f; // Дистанція, на якій швидкість максимальна
    [SerializeField] private int maxEmissionRate = 50; // Максимальна кількість частинок


    private ParticleSystem particleSystemInstance;
    private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;
    private ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
        // Отримуємо компонент ParticleSystem
        particleSystemInstance = GetComponent<ParticleSystem>();
        velocityOverLifetime = particleSystemInstance.velocityOverLifetime;
        emissionModule = particleSystemInstance.emission;
        // Вмикаємо Velocity Over Lifetime

    }

    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, player.position);

      
        float angularVelocity = 0f;
        if (distance <= proximityThreshold)
        {
            angularVelocity = Mathf.Lerp(0, maxAngularVelocity, 1 - (distance / proximityThreshold));
        }

    
        velocityOverLifetime.orbitalY = angularVelocity;
        float emissionRate = 0f;
        if (distance <= proximityThreshold)
        {
            emissionRate = Mathf.Lerp(0, maxEmissionRate, 1 - (distance / proximityThreshold));
        }

        var rateOverTime = emissionModule.rateOverTime;
        rateOverTime.constant = emissionRate;
        emissionModule.rateOverTime = rateOverTime;
    }
}