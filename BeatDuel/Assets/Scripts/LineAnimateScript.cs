/* vim: set ts=4 sts=4 sw=4 expandtab: */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimateScript : MonoBehaviour
{
    #region constants
    private const float DEFAULT_DURATION = 2.0f;
    #endregion constants

    #region editor_variables
    public float _duration = DEFAULT_DURATION;
    #endregion editor_variables

    private float time = 0f;
    private float limits = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseScreenLimits();
        InitialiseLinePosition();
    }

    void InitialiseLinePosition()
    {
        this.transform.position = new Vector3(-limits, this.transform.localScale.z / 2, 0f);
    }

    void InitialiseScreenLimits()
    {
        Camera mainCamera = Camera.main;
        limits = mainCamera.orthographicSize * mainCamera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate line displacement since last frame
        time = (Time.deltaTime + time);
        float timeStep = time / _duration;
        double newPosition = Mathf.Lerp(-limits, limits, timeStep);
        double displacement = newPosition - this.transform.position.x;

        // Move line to the right
        this.transform.position += (float)displacement * Vector3.right;

        // Make sure we keep time between 0 and _duration
        time = time % _duration;
    }
}
