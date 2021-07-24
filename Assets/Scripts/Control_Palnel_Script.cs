using UnityEngine;

public class Control_Palnel_Script : MonoBehaviour
{
    public GameObject MainActor;
    private Rigidbody2D rb;
    public int jumpForce;

    void Start()
    {
        rb = MainActor.GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        Ball_Controll();
    }

    void OnTouchDown()
    {
        Ball_Controll();
    }

    void Ball_Controll()
    {
        if (gameObject.activeSelf == true && MainActor.activeSelf == true)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition, whereToMove;
                Vector2 input_Vector;

                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                whereToMove = (touchPosition - MainActor.transform.position).normalized;

                input_Vector.x = whereToMove.x;
                input_Vector.y = whereToMove.y;

                rb.AddForce(input_Vector * jumpForce, ForceMode2D.Force); //Добавление силы прыжка
            }
        }
    }
}
