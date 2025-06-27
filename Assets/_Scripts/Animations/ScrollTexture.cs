using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public float scrollSpeedY = 0.1f;
    private Vector2 currentOffset;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        currentOffset = meshRenderer.materials[0].mainTextureOffset; // Initialize
    }

    private void FixedUpdate()
    {
        currentOffset.y += scrollSpeedY * Time.fixedDeltaTime;
        meshRenderer.materials[0].mainTextureOffset = currentOffset;
    }
}