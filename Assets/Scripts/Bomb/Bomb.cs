using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public GameObject flame;
    public LayerMask levelMask;
    public AudioClip explosionSound;

    private bool exploded = false;

    public float explosionTime = 2;
    public int bombRange = 2;

    // Use this for initialization
    void Start () {
        Invoke("Explode", explosionTime);
    }

    void Explode()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        Instantiate(flame, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.up));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.down));
        StartCoroutine(CreateExplosions(Vector3.left));

        GetComponent<SpriteRenderer>().enabled = false;
        exploded = true;
        transform.FindChild("Collider").gameObject.SetActive(false);

        Destroy(gameObject, .3f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!exploded && other.gameObject.CompareTag("Flame"))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i <= bombRange; i++)
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, levelMask);

            if (!hit.collider)
            {
                Instantiate(flame, transform.position + (i * direction), Quaternion.identity);
            }
            else {
                break;
            }
            yield return new WaitForSeconds(.05f);
        }
    }
}
