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
    bool isBlueColor = true;
    public float xVelocity = 3, yVelocity = -9;
    public float timeBeetweenTapToSwapColor = 0.2f;
    float timeLastTap = -1f;
    bool isActive = false;

    public async void Spawn(Vector3 spawnPos)
    {
        transform.position = spawnPos;
        await UniTask.NextFrame(); // This avoid trail renderer coming from where player died
        isActive = true;
        spriteRenderer.enabled = isActive;
        myRB.simulated = isActive;
        myRB.velocity = new Vector2(xVelocity, yVelocity * -1 / 2);
        blueTrail.emitting = isBlueColor;
        redTrail.emitting = !isBlueColor;
    }

    void Start()
    {
        myColor.OnChanged += OnColorChanged;
        myColor.Value = tableColors.blue1;
    }

    void Update()
    {
        if (isActive) Controls();
    }

    void Controls()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            myRB.velocity = new Vector2(xVelocity, yVelocity * -1);
            if (timeLastTap + timeBeetweenTapToSwapColor > Time.time) SwapColor();
            timeLastTap = Time.time;
        }
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
        {
            if (myRB.velocity.y > myRB.velocity.y / 3)
                myRB.velocity = new Vector2(xVelocity, myRB.velocity.y / 3);
        }
        //if (Input.GetMouseButtonUp(1) || Input.GetKeyDown(KeyCode.Space)) SwapColor();
    }

    void SwapColor()
    {
        myColor.Value = spriteRenderer.color == tableColors.blue1 ? tableColors.red1 : tableColors.blue1;
    }

    void OnColorChanged()
    {
        isBlueColor = myColor.Value == tableColors.blue1;
        var newColor = isBlueColor ? tableColors.blue1 : tableColors.red1;
        spriteRenderer.color = newColor;
        blueTrail.emitting = isBlueColor;
        redTrail.emitting = !isBlueColor;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Blue") && !isBlueColor) Die();
        else if (other.CompareTag("Red") && isBlueColor) Die();
        else if (other.CompareTag("Yellow")) Die();
    }

    async void Die()
    {
        isActive = false;
        myRB.velocity = Vector2.zero;
        myRB.simulated = isActive;
        spriteRenderer.enabled = isActive;
        blueTrail.emitting = redTrail.emitting = isActive;
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        GameManager.Instance.Retry();
    }
}
