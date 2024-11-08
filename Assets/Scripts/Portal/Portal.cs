using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    private Vector2 newPosition;

    private void Start()
    {
        ChangePosition();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        Debug.Log("Portal Position: " + transform.position);

        Player player = FindObjectOfType<Player>();
        if (player != null && player.HasWeapon())
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4.5f, 4.5f));
        Debug.Log("New Position: " + newPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().LoadScene("Main");
        }
    }
}
