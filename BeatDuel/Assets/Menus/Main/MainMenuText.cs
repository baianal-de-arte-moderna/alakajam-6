using UnityEngine;
using TMPro;

public class MainMenuText : MonoBehaviour
{
    public float rotationSpeed = 5f;
    TMP_Text m_TextComponent;
    float hue;
    float underlayX;
    float underlayY;
    // Start is called before the first frame update
    void Awake()
    {
        m_TextComponent = GetComponent<TMP_Text>();
        hue = 0f;
        underlayX = 0f;
        underlayY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_TextComponent.outlineColor = Color.HSVToRGB(hue, 1, 1);
        hue += 0.01f;
        hue %= 1f;

        underlayX += Random.Range(-0.1f, 0.1f);
        underlayX %= 1f;
        underlayY += Random.Range(-0.1f, 0.1f);
        underlayY %= 1f;
        m_TextComponent.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, underlayX);
        m_TextComponent.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, underlayY);

        m_TextComponent.rectTransform.Rotate(Vector3.forward, rotationSpeed);
    }
}
