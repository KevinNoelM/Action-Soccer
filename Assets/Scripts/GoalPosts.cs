using UnityEngine;
using System.Collections;

public class GoalPosts : MonoBehaviour
{
    [SerializeField]
    protected int teamId;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            GameManager.Instance.GoalScored((teamId + 1) % 2);
        }
    }
}
