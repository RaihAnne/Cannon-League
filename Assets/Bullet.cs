using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform BulletTransform;
    [SerializeField] private Rigidbody2D BulletRigidBody;
    [SerializeField] private float InitialSpeed = 1;

    private int wallBounceCount = 0;

    public void Launch(Vector2 direction)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(true);
        }
        BulletRigidBody.AddForce(direction * InitialSpeed, ForceMode2D.Impulse);
        FaceBulletToDirection(direction);
    }

    private void FaceBulletToDirection(Vector2 direction)
    {
        BulletTransform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, direction));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ball")
        {
            HitBallHandler();
            return;
        }

        if (collision.gameObject.tag == "Wall")
        {
            FaceBulletToDirection(BulletRigidBody.velocity);
            HitWallHandler();
            return;
        }
    }
 

    private void HitBallHandler()
    {
        Despawn();
    }

    private void HitWallHandler()
    {
        if (wallBounceCount < 1)
        {
            wallBounceCount++;
            return;
        }

        Despawn();
    }

    private void Despawn()
    {
        GameObject.Destroy(this.gameObject);
    }
}
