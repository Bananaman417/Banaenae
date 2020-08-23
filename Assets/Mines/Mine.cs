using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject Explosion;

    public void Explode()
    {
        Instantiate(Explosion, transform);
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(.7f);
        Destroy(gameObject);
    }
}