using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [Header("Teleport Logic")]
    public float teleportRadius = 20f;
    public int teleportAttempts = 10;
    public LayerMask enemyLayer;
    public BoxCollider2D teleportBounds;

    public Vector2 GetSafeTeleportPosition(Vector2 currentPosition)
    {
        Vector2 bestPosition = currentPosition;
        int lowestEnemyCount = int.MaxValue;

        if (teleportBounds == null)
        {
            Debug.LogError("Boundary Collider not set on Teleport Manager!");
            return currentPosition;
        }

        Bounds bounds = teleportBounds.bounds;
        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minY = bounds.min.y;
        float maxY = bounds.max.y;
        
        for (int i = 0; i < teleportAttempts; i++)
        {
            Vector2 randomPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Vector2 newPosition = currentPosition + randomPoint;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 2f, enemyLayer);
            int enemyCount = colliders.Length;

            if (enemyCount < lowestEnemyCount)
            {
                lowestEnemyCount = enemyCount;
                bestPosition = newPosition;
            }
        }
        return bestPosition;
    }
}
