using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    private float attackRange = 1f;
    private BanditBehavior bandit;
    private Vector3 playerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject banditObject = GameObject.Find("Bandit");
        if (banditObject != null)
        {
            bandit = banditObject.GetComponent<BanditBehavior>();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckAndHitIfInRange() {
            float distance = Vector2.Distance(transform.position, bandit.transform.position);
            if (distance <= attackRange) {
                BanditHealth banditHealth = bandit.GetComponent<BanditHealth>();
                if (banditHealth != null)
                {
                    banditHealth.HurtBandit();
            }

        }
    }
}

