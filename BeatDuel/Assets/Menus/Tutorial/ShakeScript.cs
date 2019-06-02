using UnityEngine;
using UnityEngine.UI;

public class ShakeScript : MonoBehaviour
{
    RectTransform rectTransform;
    Vector2 initialPos;
    Vector2 targetPos;
    public Vector2 shake = new Vector2(0f, 8f);
    int iter;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPos = rectTransform.localPosition;
        targetPos = initialPos + shake;
        shake *= -1;
        iter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(rectTransform.localPosition, targetPos) < 0.1f)
        {
            targetPos = initialPos + shake;
            shake *= -1;
            iter = 1;
        }

        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, targetPos, Time.deltaTime * iter);
        iter++;
    }
}
