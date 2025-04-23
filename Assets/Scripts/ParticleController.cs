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
        // Обчислення відстані до гравця
        float distance = Vector3.Distance(transform.position, player.position);

        // Обчислення кутової швидкості залежно від відстані
        float angularVelocity = 0f;
        if (distance <= proximityThreshold)
        {
            angularVelocity = Mathf.Lerp(0, maxAngularVelocity, 1 - (distance / proximityThreshold));
        }

        // Застосування кутової швидкості до осі Y
        velocityOverLifetime.orbitalY = angularVelocity;
        float emissionRate = 0f;
        if (distance <= proximityThreshold)
        {
            emissionRate = Mathf.Lerp(0, maxEmissionRate, 1 - (distance / proximityThreshold));
        }

        // Застосування кількості частинок
        var rateOverTime = emissionModule.rateOverTime;
        rateOverTime.constant = emissionRate;
        emissionModule.rateOverTime = rateOverTime;
    }
}