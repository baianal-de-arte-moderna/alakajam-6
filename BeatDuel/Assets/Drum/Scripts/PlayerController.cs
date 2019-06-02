using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int currentPlayerIndex;

    public void PlayerChanged(int newPlayerIndex)
    {
        currentPlayerIndex = newPlayerIndex;
        //GetComponent<Text>().text = $"Player {currentPlayerIndex + 1}'s turn.";
        this.transform.Rotate(Vector3.back, 180);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
