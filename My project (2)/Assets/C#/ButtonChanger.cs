using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChanger : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToChange;
    public void Click()
    {
        ObjectToChange.SetActive(!ObjectToChange.activeSelf);
    }
}
