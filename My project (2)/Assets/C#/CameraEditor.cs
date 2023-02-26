using UnityEngine;

public class CameraEditor : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private float lerp = 5f;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {

        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), Time.deltaTime * lerp);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                                          Mathf.Clamp(transform.position.y, minY, maxY),
                                          transform.position.z);
    }
}
