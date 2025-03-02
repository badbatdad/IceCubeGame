using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 5f; 
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _step = 0.1f;
    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");  
        float verticalInput = Input.GetAxis("Vertical");    

        Move(horizontalInput, verticalInput, movementSpeed);

        
    }

    
    
    private void Move(float horizontalInput, float verticalInput, float movementSpeed)
    {
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        if (TryMove(direction))
        {
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0, verticalInput * movementSpeed * Time.deltaTime);
        }
    }

    private bool TryMove(Vector3 direction)
    {
        var ray = new Ray(transform.position, direction);

        if(Physics.Raycast(ray, _step, _obstacleMask))
            return false;
        else
            return true;
    }
    
}