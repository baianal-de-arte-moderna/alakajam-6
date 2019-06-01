using UnityEngine;

public class CreditsTextScript : MonoBehaviour
{
    public int position;
    public bool isLast = false;

    RectTransform rTransform;

    Vector3 startPosition;
    float velocity = 0.004f;
    Vector3 endPosition;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        rTransform = GetComponent<RectTransform>();

        startPosition = rTransform.position;
        endPosition = startPosition * -1;
        if (isLast)
        {
            endPosition = Vector3.zero;
        }
        Invoke("Activate", position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            rTransform.position = Vector3.Lerp(
                rTransform.position,
                endPosition,
                velocity
            );

            if (isLast)
            {
                rTransform.Rotate(Vector3.forward, 1f);
            }
        }
    }

    void Activate()
    {
        active = true;
    }
}
