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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            HitBallHandler();
            return;
        }

        if (collision.gameObject.tag == "Wall")
        {
            HitWallHandler();
        }
    }

    private void HitBallHandler()
    {
        Despawn();
    }

    private void HitWallHandler()
    {

    }

    private void Despawn()
    {
        GameObject.Destroy(this.gameObject);
    }
}
