
using UnityEngine;

public class MenuChange : MonoBehaviour
{

    [SerializeField] Animator componentAnimator;
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Text;
    static bool flag;
    // Start is called before the first frame update
    private void Start()
    {
        if(flag)
        {
            componentAnimator.SetTrigger("SecondStart");
        }
    }
    public void ShowMenuPanel()
    {
        componentAnimator.SetTrigger("ShowMenuPanel");
        flag = true;
    }
}
