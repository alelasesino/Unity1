using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText, winText;
    public Button btRestart;

    private const int MAX_POINTS = 60;    
    private Rigidbody rb;
    private int count;

    void Start() {
    
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
        btRestart.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    void FixedUpdate() {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
        rb.AddForce(movement * speed);


        if(Input.GetKeyDown("space")) {
            Vector3 jump = new Vector3(0.0f,400.0f,0.0f);
            rb.AddForce(jump);
        }

        if(rb.transform.position.y < -10){
            rb.transform.position = new Vector3(0f, 0f, 0f);
        }

    }

    void OnTriggerEnter(Collider other) {

        if(other.gameObject.CompareTag("Pick Up Plus") || other.gameObject.CompareTag("Pick Up")) {

            other.gameObject.SetActive(false);

            if(other.gameObject.CompareTag("Pick Up Plus"))
                count += 10;
            else
                count += 5;

            setCountText();
            checkWin();

        }

    }
    
    void setCountText() {

        countText.text = "Puntos: " + count.ToString() + "/" + MAX_POINTS;

    }

    void checkWin() {

        if(count >= MAX_POINTS) {
        
            winText.text = "¡Enhorabuena has ganado!";
            Time.timeScale = 0;
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            btRestart.gameObject.SetActive(true);

        }
    
    }

    public void onRestartClick() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void OnCollisionEnter(Collision collision) {


        if(!collision.gameObject.CompareTag("Wall")) {
            
            count--;
            setCountText();

        }

        if(collision.gameObject.CompareTag("CapsuleObstacule")) {

            Vector3 movement = -rb.velocity.normalized * 30;
            rb.AddForce(movement,ForceMode.VelocityChange);

        }

    }
    
}
