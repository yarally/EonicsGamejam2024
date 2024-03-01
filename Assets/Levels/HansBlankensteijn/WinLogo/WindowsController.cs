using System;
using System.Collections;
using System.Collections.Generic;
using Levels.HBL_Expert;
using Lib.Door;
using Lib.Interactable;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class WindowsController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float speedIncrease;
    [SerializeField] private Transform attackPaths;
    [SerializeField] private WindowsInteracter winInteract;
    [SerializeField] private ErrorController errController;
    [SerializeField] private PlayerController player;
    [FormerlySerializedAs("waitBetweenErrorSpawn")] [SerializeField] private float waitBetweenAttacks;
    [SerializeField] private FolderController folderController;
    [SerializeField] private TextMeshPro updateTracker;
    [SerializeField] private SpriteRenderer hourglass;
    [SerializeField] private BoxCollider2D hitboxObj;
    [SerializeField] private LightSwitch lightSwitch;
    [SerializeField] private WinCrashController winCrasher;
    private Phase currentPhase;
    private States currentState;
    private Vector3 goal;
    private int attacksLeft;
    private readonly Vector3 defaultPos = new Vector3(9, 5, 0);
    private Transform tf;
    private States[] attackStates = new[] {States.SpawingErrors, States.SpawingFolders};
    
    void Start()
    {
        FindFirstObjectByType<DoorController>().CloseDoor();
        currentPhase = Phase.Beginning;
        currentState = States.Idle;
        tf = transform;
        tf.position = defaultPos;
    }

    // Update is called once per frame
    void Update()
    {

        hitboxObj.enabled = currentState == States.Moving;

        if (currentPhase == Phase.Done)
            return;
        
        if (winInteract.Percentage >= 100 && currentPhase != Phase.Done)
        {
            Done();
            return;
        }

        if (currentState == States.Waiting)
        {
            updateTracker.text = winInteract.DisplayText;
            return;
        }

        else if (currentPhase == Phase.Done)
        {
            updateTracker.text = winInteract.DisplayText;
            return;
        }

        else if (currentState == States.SpawingErrors)
        {
            return;
        }

        else if (currentState == States.SpawingFolders)
        {
            return;
        }
        
        updateTracker.text = winInteract.DisplayText;
        
        if (currentState == States.Moving)
        {
            if (!winInteract.GetComponentInChildren<BoxCollider2D>().enabled)
                winInteract.GetComponentInChildren<BoxCollider2D>().enabled = true;
            return;
        }
        
        if (currentPhase == Phase.Beginning)
        {
            if (winInteract.Percentage < 15)
                return;
            currentState = States.Moving;
            attacksLeft = 5;
            currentPhase = Phase.Middle;
            ChooseAttackPath();
            return;
        }
        
        if (currentPhase == Phase.Middle)
        {
            if (winInteract.Percentage >= 67)
            {
                currentState = States.Moving;
                attacksLeft = 20;
                speed *= speedIncrease;
                currentPhase = Phase.Middle;
                ChooseAttackPath();
                currentPhase = Phase.End;
                return;
            }
            ChooseAttack();
            return;
            
        }

        if (currentPhase == Phase.End)
        {
            ChooseAttack();
        }
        
    }

    private void ChooseAttack()
    {
        States attackState = attackStates[Random.Range(0, attackStates.Length)];
        switch (attackState)
        {
            case (States.SpawingErrors):
                StartCoroutine(SpawnErrors());
                break;
            case (States.SpawingFolders):
                StartCoroutine(SpawnFolders());
                break;
            default:
                break;
        }
    }
    
    private IEnumerator SpawnFolders()
    {
        updateTracker.text = "Purging Files...";
        currentState = States.SpawingFolders;
        yield return new WaitForSeconds(0.5f);   
        for (int i = 0; i < 30; i++)
        {
            float randX = Random.Range(0f, 18f);
            Instantiate(folderController, new Vector3(randX, 12, 0), tf.rotation);
            yield return new WaitForSeconds(0.3f);
        }
        currentState = States.Waiting;
        yield return new WaitForSeconds(GetWaitBetweenAttacks());
        currentState = States.Idle;
    }
    
    private IEnumerator SpawnErrors()
    {
        updateTracker.text = "Fatal Error Detected!";
        currentState = States.SpawingErrors;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 20; i++)
        {
            Instantiate(errController, player.transform.position, tf.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        currentState = States.Waiting;
        yield return new WaitForSeconds(GetWaitBetweenAttacks());
        currentState = States.Idle;
    }
    
    
    
    private void FixedUpdate()
    {
        if (currentState != States.Moving)
            return;
        tf.position = Vector3.MoveTowards(tf.position, goal, speed*Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, goal) <= Single.Epsilon)
        {
            attacksLeft -= 1;
            if (attacksLeft <= 0)
            {
                currentState = States.Idle;
                tf.position = defaultPos;
            }
            else
            {
                ChooseAttackPath();
            }
        }
    }
    
    private void ChooseAttackPath()
    {
        var idx = Random.Range(0, attackPaths.childCount);
        var start = Random.Range(0, 2);
        var currentAttack = attackPaths.GetChild(idx);
        transform.position = currentAttack.GetChild(start).position;
        goal = currentAttack.GetChild((start + 1) % 2).position;
        
        currentState = States.Waiting;
        StartCoroutine(ShowHourGlass());
    }

    private IEnumerator ShowHourGlass()
    {
        
        var hg = Instantiate(hourglass, new Vector3(tf.position.x + (goal.x - tf.position.x) * 0.25f, tf.position.y, 0f), tf.rotation);
        yield return new WaitForSeconds(0.5f);
        currentState = States.Moving;
        yield return new WaitForSeconds(0.5f);
        Destroy(hg.gameObject);
    }
    
    private void Done()
    {
        tf.position = defaultPos;
        currentPhase = Phase.Done;
        updateTracker.text = ":(";
        StopAllCoroutines();
        winInteract.IsDying = true;
        
        Instantiate(lightSwitch, tf.position, tf.rotation);
        StartCoroutine(DeathAnim());
    }

    private IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(0.5f);
        List<WinCrashController> crashers = new List<WinCrashController>();
        for (int i = 0; i < 20; i++)
        {
            var currCrash = Instantiate(winCrasher, tf.position + new Vector3(Random.Range(-6, 6), Random.Range(-6, 6), 0), tf.rotation);
            crashers.Add(currCrash);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        foreach (var crash in crashers)
        {
            Destroy(crash.gameObject);
        }
        Destroy(gameObject);
    }
    
    private float GetWaitBetweenAttacks()
    {
        switch (currentPhase)
        {
            case (Phase.Beginning):
                return waitBetweenAttacks;
            case (Phase.Middle):
                return waitBetweenAttacks;
            case (Phase.End):
                return waitBetweenAttacks / 5;
        }

        return waitBetweenAttacks;
    }
    
}

public enum Phase
{
    Beginning,
    Middle,
    End,
    Done
}


public enum States
{
    Moving,
    Waiting,
    SpawingErrors,
    SpawingFolders,
    Idle
}
