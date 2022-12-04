using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private bool blowUp = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoolDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (!blowUp)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag == "Breakable")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1);
        blowUp = false;
    }
}
