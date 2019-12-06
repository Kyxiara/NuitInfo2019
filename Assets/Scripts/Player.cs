using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private float distance;

    public Rigidbody2D rb;
    public Animator animator;

    public GameObject points;
    private GameObject pointsGO;
    private DialogueManager dialogueManager;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueManager.dialogueActive)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            distance += Mathf.Abs(movement.x) + Mathf.Abs(movement.y);
            if (distance > 1000f && pointsGO == null)
            {
                distance = 0;
                var pos = new Vector3(0, 1.5f, 0);
                pointsGO = Instantiate(points, pos + transform.position, Quaternion.identity);
                pointsGO.transform.parent = transform;
                StartCoroutine(DisplayPoints());
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * moveSpeed);
    }

    private IEnumerator DisplayPoints() {
        yield return new WaitForSeconds(1.5f);
        Destroy(pointsGO);
        pointsGO = null;
    }
}
