using UnityEngine;
using UnityEngine.AI;

public class AIPathBlocker : MonoBehaviour
{
    void Start()
    {
        // Ensure the object has a NavMeshObstacle component
        NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();

        if (obstacle == null)
        {
            // Add a NavMeshObstacle component and enable carving
            obstacle = gameObject.AddComponent<NavMeshObstacle>();
            obstacle.carving = true;
        }
        else
        {
            // Make sure carving is enabled
            obstacle.carving = true;
        }

        // Adjust the size and shape of the obstacle to match the object
        SetObstacleShape(obstacle);
    }

    private void SetObstacleShape(NavMeshObstacle obstacle)
    {
        Collider collider = GetComponent<Collider>();

        if (collider != null)
        {
            if (collider is BoxCollider)
            {
                // If the object has a BoxCollider, set the obstacle to match the box size
                obstacle.shape = NavMeshObstacleShape.Box;
                obstacle.size = ((BoxCollider)collider).size;
            }
            else if (collider is SphereCollider)
            {
                // If the object has a SphereCollider, set the obstacle to match the sphere radius
                obstacle.shape = NavMeshObstacleShape.Capsule; // Closest to a sphere shape in NavMeshObstacle
                float diameter = ((SphereCollider)collider).radius * 2;
                obstacle.size = new Vector3(diameter, diameter, diameter);
            }
            else if (collider is CapsuleCollider)
            {
                // If the object has a CapsuleCollider, set the obstacle to match the capsule size
                obstacle.shape = NavMeshObstacleShape.Capsule;
                float diameter = ((CapsuleCollider)collider).radius * 2;
                obstacle.size = new Vector3(diameter, ((CapsuleCollider)collider).height, diameter);
            }
            else
            {
                // Default to a box shape if no specific collider type is handled
                obstacle.shape = NavMeshObstacleShape.Box;
                obstacle.size = collider.bounds.size;
            }
        }
        else
        {
            Debug.LogWarning("AIPathBlocker: No Collider found on the object. The NavMeshObstacle size may not be set correctly.");
        }

        // Set the obstacle center to match the object's position
        obstacle.center = collider.bounds.center - transform.position;
    }
}
