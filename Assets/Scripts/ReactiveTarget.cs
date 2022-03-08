using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReactToHit()
    {
        WanderingAI behaviour = GetComponent<WanderingAI>();
        if(behaviour != null)
        {
            behaviour.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    // Update is called once per frame
    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
