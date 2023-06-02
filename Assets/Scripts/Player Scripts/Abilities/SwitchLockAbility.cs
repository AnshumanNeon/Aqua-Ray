using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLockAbility : MonoBehaviour
{
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            LockSwitcher switcher = hitCollider.gameObject.GetComponent<LockSwitcher>();

            if(switcher != null && Input.GetKeyDown(KeyCode.V))
            {
                switcher.Switch();
                print("a");
            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
