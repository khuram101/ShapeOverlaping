using UnityEngine;
using UnityEngine.UI;

public class OverlapChecker : MonoBehaviour
{
    [Header("UI References"), SerializeField]
    private Text resultText;
    [SerializeField]
    private Button checkButton;
    [SerializeField]
    private GameObject acknowledgementPanel;

    [Space(5), SerializeField]
    private GameObject sphere, cuboid;
    void Start()
    {
        if (checkButton != null)
        {
            checkButton.onClick.AddListener(CheckOverlap);
        }
    }

    void CheckOverlap()
    {
        bool isOverlapping = DoesOverlap(sphere, cuboid);
        acknowledgementPanel.SetActive(true);

        if (isOverlapping)
        {
            resultText.text = "Overlapping!";
            resultText.color = Color.green;
        }
        else
        {
            resultText.text = "Not Overlapping!";
            resultText.color = Color.red;
        }
    }

    bool DoesOverlap(GameObject sphere, GameObject cuboid)
    {
        Vector3 sphereCenter = sphere.transform.position;
        float sphereRadius = sphere.transform.lossyScale.x / 2f; 
        Vector3 cuboidCenter = cuboid.transform.position;
        Transform cuboidTransform = cuboid.transform;


        Vector3 cuboidHalfSize = cuboidTransform.lossyScale / 2f;

        Vector3 right = cuboidTransform.right * cuboidHalfSize.x;
        Vector3 up = cuboidTransform.up * cuboidHalfSize.y;
        Vector3 forward = cuboidTransform.forward * cuboidHalfSize.z;

        // Find the vector from the cuboid center to the sphere center
        Vector3 sphereToBox = sphereCenter - cuboidCenter;

        
        float xDist = Mathf.Abs(Vector3.Dot(sphereToBox, cuboidTransform.right)) - cuboidHalfSize.x;
        float yDist = Mathf.Abs(Vector3.Dot(sphereToBox, cuboidTransform.up)) - cuboidHalfSize.y;
        float zDist = Mathf.Abs(Vector3.Dot(sphereToBox, cuboidTransform.forward)) - cuboidHalfSize.z;

        // If the sphere is inside the box on any axis, overlap occurs
        float maxDist = Mathf.Max(xDist, yDist, zDist);




        return maxDist <= sphereRadius;
    }



}
