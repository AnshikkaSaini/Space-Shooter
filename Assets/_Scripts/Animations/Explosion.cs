using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 2);
    }
}