using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class SceneManage : MonoBehaviour
{
    public void ToScene(string sceneName)
    {
        SceneTransition.SwitchToScene(sceneName);
    }
}
