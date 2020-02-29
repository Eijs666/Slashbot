using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Kunai))]
public class Player : MonoBehaviour
{

    #region Variables
    PlayerController controller;
    Kunai kunaiScript;
    Camera viewCamera;

    public Animator animator;

    //Movement variables
    public float inputX;
    public float inputY;
    public float moveSpeed;

    //Movement variables
    public float damage = 40;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    public float nextAttackTime1 = 0f;
    public float nextAttackTime2 = 0f;
    public float nextAttackTime3 = 0f;
    public float nextAttackTime4 = 0f;

    //Smoke variables
    public GameObject smoke;
    public GameObject cubeRenderer;

    public LayerMask enemyLayers;

    public Transform attackPoint;
    #endregion

    void Start()
    {
        controller = GetComponent<PlayerController>();
        kunaiScript = GetComponent<Kunai>();
        viewCamera = Camera.main;
        
    }

    void Update()
    {
        //Input
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);
        
        //Animation
        if(inputX < 0 || inputY < 0 || inputX > 0.01 || inputY > 00.01)
        {
            animator.SetFloat("Speed", 0.5f);
        }
        else if(inputX == 0 || inputY == 0)
        {
            animator.SetFloat("Speed", 0.0f);
        }

        //RayCast
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.black);

            controller.LookAt(point);
        }

        //Attack
        if(Time.time >= nextAttackTime1)
        {
            if (Input.GetMouseButtonDown(0))
            {

                animator.SetTrigger("Attack");
                kunaiScript.Fireball();
                print("Fire");
                nextAttackTime1 = Time.time + 1f / attackRange;
            }

        }


        //Throw Kunai
        if (Time.time >= nextAttackTime2)
        {
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("AttackThrow");
                kunaiScript.PlayerThrow();
                nextAttackTime2 = Time.time + 3f / attackRange;
            }

        }


        //Ghost
        if (Time.time >= nextAttackTime3)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                VisibilityDisable();
                Invoke("VisibilityEnable", 3f);
                nextAttackTime3 = Time.time + 10f / attackRange;
            }

        }


        //Ultimate
        if (Time.time >= nextAttackTime4)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {

                animator.SetTrigger("Attack");
                kunaiScript.Ultimate();
                nextAttackTime4 = Time.time + 30f / attackRange;
            }

        }
        

    }

    public void VisibilityEnable()
    {
        cubeRenderer.SetActive(true);
        print("GHOST true");
        Smoke();

    }
    public void VisibilityDisable()
    {
        cubeRenderer.SetActive(false);
        print("GHOST false");
        Smoke();

    }

    public void Smoke()
    {
        Instantiate(smoke, transform.position, transform.rotation);

    }



    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
