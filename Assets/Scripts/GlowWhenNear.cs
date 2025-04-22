using UnityEngine;

public class GlowWhenNear : MonoBehaviour
{
    public Transform player;
    public float glowDistance = 5f;
    public Color glowColor = Color.cyan;
    public float glowStrength = 5f;

    private Renderer rend;
    private Material localMat;
    private Color originalEmission;
    private bool isGlowing = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        // Створюємо локальний інстанс матеріалу
        localMat = rend.material;

        // Зберігаємо початкове значення емісії
        originalEmission = localMat.GetColor("_EmissionColor");
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        if (dist < glowDistance && !isGlowing)
        {
            localMat.EnableKeyword("_EMISSION");
            localMat.SetColor("_EmissionColor", glowColor * glowStrength);
            isGlowing = true;
        }
        else if (dist >= glowDistance && isGlowing)
        {
            localMat.SetColor("_EmissionColor", originalEmission);
            isGlowing = false;
        }
    }
}
