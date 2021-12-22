using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public ScriptableColors tableColors;
    public SpriteRenderer spriteRenderer;
    public TrailRenderer blueTrail, redTrail;
    public ColorObs myColor = new ColorObs();
    public Rigidbody2D myRB;
    bool blueColor = true;
    public float xVelocity = 1, yVelocity = -9;

    void Start()
    {
        myColor.OnChanged += OnColorChanged;
        myColor.Value = tableColors.blue1;
        myRB.velocity = new Vector2(xVelocity, yVelocity * -1 / 2);
    }

    void Update()
    {
        Controls();
    }

    void Controls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRB.velocity = new Vector2(xVelocity, yVelocity * -1);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (myRB.velocity.y > myRB.velocity.y / 3)
                myRB.velocity = new Vector2(xVelocity, myRB.velocity.y / 3);
        }
        if (Input.GetMouseButtonUp(1) || Input.GetKeyDown(KeyCode.Space)) SwapColor();
    }

    void SwapColor()
    {
        myColor.Value = spriteRenderer.color == tableColors.blue1 ? tableColors.red1 : tableColors.blue1;
    }

    void OnColorChanged()
    {
        blueColor = myColor.Value == tableColors.blue1;
        var newColor = blueColor ? tableColors.blue1 : tableColors.red1;
        spriteRenderer.color = newColor;
        blueTrail.emitting = blueColor;
        redTrail.emitting = !blueColor;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Blue") && !blueColor) Die();
        else if (other.CompareTag("Red") && blueColor) Die();
        else if (other.CompareTag("Yellow")) Die();
    }

    async void Die()
    {
        transform.DetachChildren();
        transform.gameObject.SetActive(false);
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        SceneManager.LoadScene(0);
    }
}
