using UnityEngine;
using System.Collections;

public class PlayerPossessionController : MonoBehaviour
{
    private bool playerHasPossession = false;
    public bool PlayerHasPossession
    {
        get { return playerHasPossession;  }
    }

    private Rigidbody ball = null;
    public Rigidbody Ball
    {
       get { return ball; }
    }

    private SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            playerHasPossession = true;
            ball = coll.GetComponentInParent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            playerHasPossession = false;
            ball = null;
        }
    }

    private void FixedUpdate()
    {
        if (playerHasPossession)
        {
            //PullBallToPlayer();
        }
    }

    private void PullBallToPlayer()
    {
        float speed = Vector3.Distance(ball.transform.position, sphereCollider.transform.position) * 1.25f;
        Vector3 direction = ball.transform.position - sphereCollider.transform.position;
        ball.AddRelativeForce(direction.normalized * speed, ForceMode.Force);
    }
}
