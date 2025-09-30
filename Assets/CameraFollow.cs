using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // объект игрока
    public Vector3 offset = new Vector3(0, 8, -8); // смещение камеры

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
            transform.LookAt(player); // камера смотрит на игрока
        }
    }
}
