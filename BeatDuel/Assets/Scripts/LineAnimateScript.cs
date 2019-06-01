/* vim: set ts=4 sts=4 sw=4 expandtab: */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimateScript : MonoBehaviour
{
    #region editor_variables
    [SerializeField]
    private AudioSource audioLoop;
    #endregion editor_variables

    private float time = 0f;
    private float limits = 0f;
    private float CYCLE_DURATION;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseScreenLimits();
        InitialiseLinePosition();
        InitialiseCycleDuration();
    }

    void InitialiseCycleDuration()
    {
        this.CYCLE_DURATION = audioLoop.clip.length / 2;
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
        time = audioLoop.time;
        if (audioLoop.time > CYCLE_DURATION)
        {
            time -= CYCLE_DURATION;
        }
        float timeStep = time / CYCLE_DURATION;
        double newPosition = Mathf.Lerp(-limits, limits, timeStep);
        double displacement = newPosition - this.transform.position.x;

        // Move line to the right
        this.transform.position += (float)displacement * Vector3.right;
    }
}
