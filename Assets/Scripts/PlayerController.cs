using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    public Button btRestart;

    private Rigidbody rb;
    private int count;
    private bool isWin;

    void Start() {
    
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
        isWin = false;
        btRestart.gameObject.SetActive(false);

    }

    void FixedUpdate() {
    
        if(!isWin) {

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
            rb.AddForce(movement * speed);

        }

    }

    void OnTriggerEnter(Collider other) {
    
        if(other.gameObject.CompareTag("Pick Up")) {
        
            other.gameObject.SetActive(false);
            count++;
            setCountText();
            checkWin();
        }

    }
    
    void setCountText() {

        countText.text = "Puntos: " + count.ToString() + "/12";

    }

    void checkWin() {
    
        isWin = count >=12;

        if(isWin) {
        
            winText.text = "¡Enhorabuena has ganado!";
            btRestart.gameObject.SetActive(true);
        }
    
    }

    public void onRestartClick() {

        winText.text = "";
        transform.position = new Vector3(0.0f,0,0);
        isWin = false;
        count = 0;
        setCountText();
        btRestart.gameObject.SetActive(false);

    }
    
}
