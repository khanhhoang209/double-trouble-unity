using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float buoyancyForce = 10f; // The force that pushes the object up
    public float waterDrag = 2f;  // linear damping applied when in water
    public float sinkDepth = 0.2f;      // độ chìm sâu khi có trọng lượng
    public float maxSinkRatio = 0.01f;   // tỷ lệ tối đa chìm so với halfHeight (0-1)
    public float floatSmooth = 2f;      // tốc độ nổi lên/lên xuống
    private Rigidbody2D rb;
    private bool isInWater = false;
    private bool hasWeight = false;    // trạng thái có vật đứng lên
    private float waterSurfaceY = 0f; // World Y of water surface when entered
    private float halfHeight; // Half height of this object's collider
    private float originalDamping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // prevent rotation to keep icebox upright
        rb.freezeRotation = true;
        originalDamping = rb.linearDamping;  // save original damping
        // calculate half-height of collider for float clamping
        halfHeight = GetComponent<Collider2D>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        // xử lý khi đang ở trong nước hoặc có player đứng trên
        if (CompareTag("IceBox") && (isInWater || hasWeight))
        {
            // tính vị trí center: nếu không có player, float 50% (centerY = surfaceY)
            // nếu có player, chìm hoàn toàn (centerY = surfaceY - halfHeight)
            float floatCenterY = waterSurfaceY;
            float sinkCenterY = waterSurfaceY - halfHeight;
            float targetY = hasWeight ? sinkCenterY : floatCenterY;
            // Lerp mượt từ vị trí hiện tại đến targetY
            Vector2 pos = rb.position;
            float newY = Mathf.Lerp(pos.y, targetY, floatSmooth * Time.fixedDeltaTime);
            rb.position = new Vector2(pos.x, newY);
            // khóa vận tốc dọc để tránh jitter
            // khóa vận tốc dọc để tránh jitter
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only set in-water flag for Icebox
        if (CompareTag("IceBox") && other.CompareTag("Water"))
        {
            isInWater = true;
            rb.linearDamping = waterDrag;  // increase damping to reduce bounce
            // record top of water collider
            waterSurfaceY = other.bounds.max.y;
            Debug.Log("Enter Water: " + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Only clear in-water flag for Icebox
        if (CompareTag("IceBox") && other.CompareTag("Water"))
        {
            // chỉ thoát nước khi phần trên đã vượt mặt nước
            float topY = rb.position.y + halfHeight;
            if (topY > waterSurfaceY)
            {
                isInWater = false;
                rb.linearDamping = originalDamping;  // restore damping
            }
        }
    }

    // Add continuous buoyancy update
    private void OnTriggerStay2D(Collider2D other)
    {
        if (CompareTag("IceBox") && other.CompareTag("Water"))
        {
            isInWater = true;
            // update water surface in case depth changes
            waterSurfaceY = other.bounds.max.y;
        }
    }

    // detect khi Player đứng lên/xuống
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
            hasWeight = true;
    }

    // detect khi Player đứng trên icebox
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
            hasWeight = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
            hasWeight = false;
    }
}
