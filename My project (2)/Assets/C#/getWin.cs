using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getWin : MonoBehaviour
{

    bool InRange;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && InRange) Debug.Log("You've got the coin");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) InRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) InRange = false;
    }
}
