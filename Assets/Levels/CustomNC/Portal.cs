using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal neighbour;
    //private Player player;
    private bool moved;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!moved)
            {
                Debug.Log("pos " + other.transform.position);
                Debug.Log("pos2 " + neighbour.transform.position);
                //other.transform.position = new Vector3(3.4f, transform.position.y, transform.position.z);
                //other.tranform.position
                neighbour.moved = true;
                other.transform.position = neighbour.transform.position;
                //neighbour.moved = false;
                moved = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        moved = false;
    }

        // Start is called before the first frame update
        void Start()
    {
        moved = false;
        //player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
