using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [Header("Teleport Logic")]
    public float teleportRadius = 20f;
    public int teleportAttempts = 10;
    public LayerMask enemyLayer;

    /// <summary>
    /// Finds and returns the safest teleport position within a radius.
    /// </summary>
    public Vector2 GetSafeTeleportPosition(Vector2 currentPosition)
    {
        Vector2 bestPosition = currentPosition;
        int lowestEnemyCount = int.MaxValue;

        for (int i = 0; i < teleportAttempts; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle * teleportRadius;
            Vector2 newPosition = currentPosition + randomPoint;

            // Check how many enemies are at the new position
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
