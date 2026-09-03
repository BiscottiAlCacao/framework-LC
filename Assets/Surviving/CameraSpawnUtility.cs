using UnityEngine;

public static class CameraSpawnUtility
{
    public static Vector3 GetPositionOutsideView(Camera cam, Vector3 pivot, float margin, float maxExtraDistance)
    {
        float halfHeight = GetVisibleHalfHeight(cam, pivot);
        float halfWidth = halfHeight * cam.aspect;

        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

        float edgeDistance = RectangleEdgeDistance(halfWidth, halfHeight, angle);
        float spawnDistance = edgeDistance + margin + Random.Range(0f, maxExtraDistance);

        return pivot + direction * spawnDistance;
    }

    private static float GetVisibleHalfHeight(Camera cam, Vector3 pivot)
    {
        if (cam.orthographic)
            return cam.orthographicSize;

        float distanceToPivot = Vector3.Dot(pivot - cam.transform.position, cam.transform.forward);
        return distanceToPivot * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }
    private static float RectangleEdgeDistance(float halfWidth, float halfHeight, float angleRad)
    {
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);

        float distanceX = Mathf.Abs(cos) > 0.0001f ? halfWidth / Mathf.Abs(cos) : float.MaxValue;
        float distanceY = Mathf.Abs(sin) > 0.0001f ? halfHeight / Mathf.Abs(sin) : float.MaxValue;

        return Mathf.Min(distanceX, distanceY);
    }
}