using UnityEngine;

public class Rotation : MonoBehaviour
{   
    [SerializeField] float xRotationSpeed;
    [SerializeField] float yRotationSpeed;
    [SerializeField] float zRotationSpeed;
    [SerializeField] bool AsynchronedRotation = false;
    [SerializeField] float randomRangeA = 0.80f;
    [SerializeField] float randomRangeB = 1.20f;
    

    void Update()
    {   
        Vector3 rotationSpeed = new Vector3(xRotationSpeed, yRotationSpeed, zRotationSpeed); 
        if (AsynchronedRotation)
        {
            float randomValue = Random.Range(randomRangeA, randomRangeB);
            transform.Rotate(randomValue * rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
        
    }
}