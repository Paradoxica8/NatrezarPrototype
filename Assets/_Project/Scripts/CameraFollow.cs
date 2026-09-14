using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 10f;

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}