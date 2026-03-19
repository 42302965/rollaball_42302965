using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour{
 
       private Rigidbody rb; 
       private int count;


       private float movementX;
       private float movementY;


       public float speed = 0;


       public TextMeshProUGUI countText;

 
       public GameObject winTextObject;

       public AudioClip sonidoMoneda;
       private AudioSource audioSource;
       public AudioClip sonidoGameOver;


void Start(){
       rb = GetComponent<Rigidbody>();
       count = 0;

       SetCountText();

       winTextObject.SetActive(false);

       audioSource = GetComponent<AudioSource>();
       }

void OnMove(InputValue movementValue){

       Vector2 movementVector = movementValue.Get<Vector2>();

       movementX = movementVector.x; 
       movementY = movementVector.y; 
       }

private void FixedUpdate(){

       Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

       rb.AddForce(movement * speed); 
       }

 
void OnTriggerEnter(Collider other){
       
       if (other.gameObject.CompareTag("PickUp")){

              other.gameObject.SetActive(false);

              count = count + 1;

              SetCountText();
              audioSource.PlayOneShot(sonidoMoneda);
              }
       }

void SetCountText(){

       countText.text = "Count: " + count.ToString();

if (count >= 9){

       winTextObject.SetActive(true);
       Destroy(GameObject.FindGameObjectWithTag("Enemy"));

       }
}

private void OnCollisionEnter(Collision collision){

if (collision.gameObject.CompareTag("Enemy")){

       audioSource.PlayOneShot(sonidoGameOver);

       //detener por completo el movimiento del juego
       Time.timeScale = 0f;
 
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

       Destroy(gameObject, sonidoGameOver.length); 

       }
}


}
