using TMPro;
using UnityEngine;

public class MainMenuStart : MonoBehaviour
{
    TMP_Text m_TextComponent;

    public Color targetColor = Color.clear;
    public float changeSpeed = 0.05f;
    public float changeDelay = 1.5f;

    float lastMod = 0f;

    void Awake()
    {
        m_TextComponent = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        m_TextComponent.color = Color.Lerp(
            m_TextComponent.color,
            targetColor,
            changeSpeed
        );

        float mod = Time.unscaledTime % changeDelay;
        if (mod < lastMod)
        {
            if (targetColor == Color.clear)
            {
                targetColor = Color.white;
            }
            else
            {
                targetColor = Color.clear;
            }
        }
        lastMod = mod;
    }
}
