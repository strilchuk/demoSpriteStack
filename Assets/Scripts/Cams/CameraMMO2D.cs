// Камера всегда следует за игроком
using UnityEngine;

public class CameraMMO2D : MonoBehaviour
{
    [Header("Target Follow")]
    public Transform target;
    // Смещение, чтобы например сфокусироваться на голове или кабине
    public Vector2 offset = Vector2.zero;

    // Сглаживание движения
    [Header("Dampening")]
    public float damp = 1;

    void LateUpdate()
    {
        if (!target) return;

        Vector2 goal = (Vector2)target.position + offset;

        Vector2 position = Vector2.Lerp(transform.position, goal, Time.deltaTime * damp);

        // Конвертируем в 3D, но оставляем ось Z чтобы оставаться в 2D плоскости
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
