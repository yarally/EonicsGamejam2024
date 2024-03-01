using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class FolderController : MonoBehaviour
{
    [SerializeField] private TextMeshPro textbox;
    private float speed;
    private Transform tf;
    static readonly (string, float)[] TypeArray = new[]
        {("System32", 1f), 
            ("Unity", 2f), 
            ("homework", 3f), 
            ("WinSxS", 1f), 
            ("Program files", 2f), 
            ("Appdata", 1f), 
            ("ARK - Survival Evolved", 4f)
        };
    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        int idx = Random.Range(0, TypeArray.Length);
        var (text, size) = TypeArray[idx];
        speed = 10 / size;
        textbox.text = text;
        tf.localScale *= size;
    }

    // Update is called once per frame
    void Update()
    {
        if (tf.transform.position.y < -1f - tf.localScale.y)
        {
            Destroy(gameObject);
            return;
        }
        tf.Translate(Vector3.down * Time.deltaTime * speed);
        
    }
}
