using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public float raycastRange;
    public float speed;
    private Animator animator;
    private Vector2 movement;
    public LayerMask layerMask;
    public BoxCollider boxCollider;
    private Rect colliderRect;
    public TileBehavior tileSelected;
    public List<TileBehavior> tileTargeted;
    public Camera cam;
    private bool isClicking;
    private bool isDoubleClicking;
    public Character minor;
    private float timeBetweenClick;
    public System.Action doubleClik;
    private Vector2 positionToReach;
    public LayerMask mouseLayer;

    public bool playWalkSound;
    public bool playDigSound;

    public AudioSource audioSourceWalk;
    public AudioSource audioSourceDig;

    public AudioClip walkSfx;
    public AudioClip digSfx;


    public Animator Animator { get => animator; set => animator = value; }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        //transform.position = ;
        colliderRect = new Rect(boxCollider.center, boxCollider.size);
        tileTargeted = new List<TileBehavior>();
        animator = GetComponentInChildren<Animator>();
    }
    public void Movement(InputAction.CallbackContext context)
    {
        //movement = context.ReadValue<Vector2>(); 
    }
    public void MousMovement(InputAction.CallbackContext context)
    {
        isClicking = context.performed;
        if (context.performed)
        {
            isDoubleClicking = false;

            foreach (var item in tileTargeted)
            {
                item.UnSelect();
            }
            tileTargeted.Clear();
            movement = Vector2.zero;
            if (Time.time - timeBetweenClick < 0.5f)
            {
                doubleClik.Invoke();
                timeBetweenClick = 0;
                tileTargeted.Clear();
                tileSelected = null;
                movement = Vector2.zero;
                isDoubleClicking = true;
                return;
            }
            timeBetweenClick = Time.time;
        }

    }
    /*  private void OnDrawGizmos()
      {
              Handles.DrawSolidRectangleWithOutline(new Rect(24,0,24,24),Color.red,Color.blue);
      }*/
    private void Update()
    {
        if (isClicking && !isDoubleClicking)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Ray mousRay = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
           
            if (Physics.Raycast(mousRay, out hit,Mathf.Infinity, mouseLayer))
            {
                var _tileTargeted = hit.collider.GetComponent<TileBehavior>();
                if (_tileTargeted && !tileTargeted.Contains(_tileTargeted))
                {
                    _tileTargeted.Target();
                    tileTargeted.Add(_tileTargeted);
                }
            }
            else
            {
                positionToReach = Camera.main.ScreenToWorldPoint(mousePos);
            }
        }
        else if (tileTargeted.Count>0 && tileTargeted[0]!= null) 
        {
          
            movement = (tileTargeted[0].transform.position - transform.position);
            if (movement.magnitude < 0.05f)
            {
                movement = Vector2.zero;
                transform.position = tileTargeted[0].transform.position;
                tileTargeted[0].UnSelect();
                tileTargeted.RemoveAt(0);
            }
        }
        else if(positionToReach != Vector2.zero)
        {
            movement = (positionToReach -(Vector2) transform.position);
            if (movement.magnitude < 0.05f)
            {
                movement = Vector2.zero;
                transform.position = positionToReach;
                positionToReach = Vector2.zero;
            }
        }
        else
        {
            movement = Vector2.zero;
        }

        TryMove(movement.normalized);
    }
    public void TryMove(Vector2 direction)
    {
        RaycastHit hit = new RaycastHit();
        Vector2 movingDirection = Vector2.zero;
        if(direction.x>0.1)
        {
           
            movingDirection += Vector2.right;
            for (int i = 0; i < 3; i++)
            {
                if (Raycast(Vector2.right, transform.position + new Vector3(colliderRect.xMax / 2, colliderRect.yMax / 2 - i * colliderRect.size.y / 2),out hit))
                {
                    // Debug.DrawRay(transform.position + new Vector3(colliderRect.xMax / 2, colliderRect.yMax / 2 - i * colliderRect.size.y / 2), Vector3.right, Color.red);
                    
                    tileSelected = hit.collider.GetComponent<TileBehavior>();
                    tileSelected.Select();
                    tileSelected = tileSelected.Digg();
                    movingDirection -= Vector2.right;
                    break;
                }
               
            }
            
        }
        else if(direction.x < -0.1f)
        {
            movingDirection += Vector2.left;
          
            for (int i = 0; i < 3; i++)
            {
                if(Raycast(Vector2.left, transform.position + new Vector3(-colliderRect.xMax / 2, colliderRect.yMax / 2 - i * colliderRect.size.y / 2),out hit))
                {
                    //Debug.DrawRay(transform.position + new Vector3(-colliderRect.xMax/2, colliderRect.yMax / 2 - i * colliderRect.size.y / 2), Vector3.left, color);

                    Dig(hit);

                    movingDirection -= Vector2.left;
                    break;
                }
            }
        }
        else if (direction.y < 0)
        {
            
            movingDirection += Vector2.down;
            for (int i = 0; i < 3; i++)
            {
                
                if ( Raycast(Vector2.down, transform.position - new Vector3(colliderRect.xMax - colliderRect.size.x / 2 - i * colliderRect.size.y / 2, colliderRect.size.y / 2), out hit)) 
                {
                    // Debug.DrawRay(transform.position - new Vector3(colliderRect.xMax - colliderRect.size.x/2 - i * colliderRect.size.y / 2, colliderRect.size.y/2), Vector3.down, color);
                    movingDirection -= Vector2.down;
                    if (direction.y == 0)
                        break;
                    Dig(hit);

                    break;
                }
            }
        }
        else if (direction.y > 0.1f)
        {
            movingDirection += Vector2.up;
            for (int i = 0; i < 3; i++)
            {
                if( Raycast(Vector2.up, transform.position - new Vector3(colliderRect.xMax - colliderRect.size.x / 2 - i * colliderRect.size.y / 2, -colliderRect.yMax / 2),out hit))
                {
                    //Debug.DrawRay(transform.position - new Vector3(colliderRect.xMax - colliderRect.size.x / 2 - i * colliderRect.size.y / 2, -colliderRect.yMax/2 ), Vector3.up, color);

                    Dig(hit);
                    movingDirection -= Vector2.up;
                    break;
                }
            }
        }
        
        if(movingDirection != Vector2.zero)
            Move(movingDirection);
        
    }

    public void Dig(RaycastHit hit)
    {
        //audioSourceDig.PlayOneShot(digSfx, 1F);

        if (!audioSourceDig.isPlaying)
        {
            playDigSound = false;
        }
        else
            playDigSound = true;

        if (playDigSound == false)
        {
            audioSourceDig.Play(0);
            playDigSound = true;
        }

        // ici mettre le son qui creuse
        //Attention cette m�thode est appel� Update donc il faut cr�er un condition qui attend que le son soit fini avant de le rejouer
        tileSelected = hit.collider.GetComponent<TileBehavior>();
        tileSelected.Select();
        tileSelected = tileSelected.Digg();
        animator.SetTrigger("actionc");
    }
    private void Move(Vector2 direction)
    {

        if (!audioSourceWalk.isPlaying)
        {
            playWalkSound = false;
        }
        else
            playWalkSound = true;

        if (playWalkSound == false)
        {
            audioSourceWalk.Play(0);
            playWalkSound = true;
        }

        //audioSourceWalk.PlayOneShot(walkSfx, 1F);

        //ici mettre le son du d�placement
        //Attention cette m�thode est appel� Update donc il faut cr�er un condition qui attend que le son soit fini avant de le rejouer
        transform.Translate(direction * speed * Time.deltaTime);
        TileGenerator.instance.CheckPos(transform.position);
        animator.SetFloat("Forward", direction.y);
        animator.SetFloat("Strafe", direction.x);
    }
    private bool Raycast(Vector2 direction, Vector2 position, out RaycastHit hit)
    {
        bool tempBool = false;
        tempBool= Physics.Raycast(position, direction, out  hit, raycastRange, layerMask);
        return tempBool;
    }
}
