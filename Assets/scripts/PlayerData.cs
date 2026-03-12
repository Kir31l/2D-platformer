using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 10f;
    public int jumpCount = 2;

    [Header("Physics")]
    public float gravityScale = 3f;
}
