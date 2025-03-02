using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; 
    [SerializeField] private float speedMultiplier = 0.04f;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _step = 0.1f;
    [SerializeField] private float enemyRotateDegrees = 100;
    
    // Update is called once per frame
    void Update()
    {
        Move(movementSpeed, enemyRotateDegrees);
    }
    private void Move(float movementSpeed, float RotateDegrees)
    {
        Vector3 direction = transform.forward;
        
        RotateEnemy(direction, enemyRotateDegrees);
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

    }
    private void RotateEnemy(Vector3 direction, float rotateDegrees)
    {
        var ray = new Ray(transform.position, direction);

        if(Physics.Raycast(ray, _step, _obstacleMask)){
            transform.Rotate(Vector3.up, rotateDegrees+ Random.Range(-15,15));
            movementSpeed+=speedMultiplier; 
        }
    }
}
