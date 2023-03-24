using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform BulletTransform;
    [SerializeField] private Rigidbody2D BulletRigidBody;
    [SerializeField] private float InitialSpeed = 1;

    public void Launch(Vector2 direction)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(true);
        }
        BulletRigidBody.AddForce(direction * InitialSpeed, ForceMode2D.Impulse);
    }
}
