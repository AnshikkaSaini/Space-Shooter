using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float scrollSpeedY = 0.1f;
    private Vector2 currentOffset;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        currentOffset = meshRenderer.materials[0].mainTextureOffset; // Initialize
    }

    void FixedUpdate()
    {
        currentOffset.y += scrollSpeedY * Time.fixedDeltaTime;
        meshRenderer.materials[0].mainTextureOffset = currentOffset;
    }
}