using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    private Rigidbody selectedRigidbody;
    [SerializeField] 
    private float moveSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 5f;
    void Start()
    {
        selectedRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void UpdateMovementAndRotation(float moveInput, float rotationInput)
    {
        
        Vector3 moveDirection = selectedRigidbody.transform.forward * moveInput * moveSpeed;
        selectedRigidbody.linearVelocity = new Vector3(moveDirection.x, selectedRigidbody.linearVelocity.y, moveDirection.z);
        selectedRigidbody.transform.Rotate(Vector3.up * rotationInput * rotationSpeed * Time.fixedDeltaTime);
    }
}
