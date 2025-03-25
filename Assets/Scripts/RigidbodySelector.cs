using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class RigidbodySelector : MonoBehaviour
{
    private Rigidbody selectedRigidbody;
    public float moveSpeed = 5f;

    //[SerializeField]
    // private InputSystem_Actions controls;
    [SerializeField] private InputActionProperty moveAction;

    private Vector2 moveInput;
    Mouse mouse;

    Camera mainCamera;
    private GameObject selectedObject;

    void Awake()
    {


    }
    void Start()
    {
        mainCamera = Camera.main;
    }



    private void FixedUpdate()
    {

        mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            SelectRigidbody();
        }
        if (selectedRigidbody != null)
        {
            MoveSelectedRigidbody();
        }

    }
    void SelectRigidbody()
    {

        Vector3 mousePosition = mouse.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

#if UNITY_EDITOR

            if (rb != null)
            Debug.Log("RB: "+ rb.gameObject.name);
#endif
            if (rb != null && rb.gameObject.CompareTag("Shape"))
            {
                if (selectedObject == null)
                {
                    selectedObject = rb.transform.GetChild(1).gameObject;
                }
                else
                    selectedObject.SetActive(false);
                selectedRigidbody = rb;
                selectedObject = rb.transform.GetChild(1).gameObject;
                selectedObject.SetActive(true);
                Debug.Log("Selected: " + rb.gameObject.name);
            }
        }
    }

    void MoveSelectedRigidbody()
    {
        if (selectedRigidbody != null)
        {
            Vector2 newInput = moveAction.action.ReadValue<Vector2>();

            if (moveInput != Vector2.zero)
            {
                newInput = moveInput;
            }

            selectedRigidbody.GetComponent<RigidbodyMovement>().UpdateMovementAndRotation(newInput.y, newInput.x);
        }

    }

    #region **UI Button Controls**
    public void OnMoveUp(BaseEventData eventData)
    {
        moveInput = new Vector2(0, 1);
    }

    public void OnMoveDown(BaseEventData eventData)
    {
        moveInput = new Vector2(0, -1);
    }

    public void OnMoveRight(BaseEventData eventData)
    {
        moveInput = new Vector2(1, 0);
    }

    public void OnMoveLeft(BaseEventData eventData)
    {
        moveInput = new Vector2(-1, 0);
    }

    public void OnStopMove(BaseEventData eventData)
    {
        moveInput = Vector2.zero;
    }
    #endregion

    #region Shape Scale
    public void ScaleShape(int _scale)
    {
        if (selectedRigidbody != null)
        {
            selectedRigidbody.gameObject.transform.localScale = new Vector3(_scale, _scale, _scale);
        }
    }

    #endregion
}
