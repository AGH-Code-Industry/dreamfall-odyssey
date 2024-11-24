
using UnityEngine;

namespace DefaultNamespace
{
    public class UnderwaterMovement : MonoBehaviour
    {
        public float speed = 5f;
        public float rotationSpeed = 2f;
        public float buoyancy = 0.5f; //BOUYANCY- sila wyporu
        public float dragInWater = 1f; //tarcie pod woda
        public float gravityScale = 0.2f; //grawitacja pod woda


        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false; //gravity obslugiwana recznie bo jestesmy pod woda
        }

        void FixedUpdate()
        {
            //movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float depth = Input.GetAxis("Depth");

            Vector3 movement = new Vector3(horizontal, vertical, depth);
            rb.AddForce(movement * speed);

            //rotation - mouse
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            //gora dol - nachylenie- rotacja wokol OX
            transform.Rotate(Vector3.right * -mouseY * rotationSpeed);

            //lewo prawo- obracanie- rotcaja wokol OY
            transform.Rotate(Vector3.up * mouseX * rotationSpeed);

            //sila wyporu 
            Vector3 bouyancyForce = Vector3.up * buoyancy;
            rb.AddForce(bouyancyForce, ForceMode.Acceleration);

            //Grawitacja i tarcie
            rb.linearDamping = dragInWater; //ruch bardziej opozniony pod woda niz normalnie
            rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration); //Scale of the influence of gravitation
        }
    }
}

