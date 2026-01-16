using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ConveyorMove : MonoBehaviour
{
    public float speed = 2f;
    
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            Vector3 movement = -transform.right * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}
