using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentHandler : MonoBehaviour
{
    private BoxCollider2D bound;
    public GameObject studentPrefab;
    public int studentNum;

    public void UpdateStudentNum ()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i=0; i<studentNum; i++)
        {
            GameObject go = Instantiate(studentPrefab, transform);
            go.transform.position = new Vector2((Random.value * bound.bounds.size.x - bound.bounds.extents.x) + 540f, (Random.value * bound.bounds.size.y - bound.bounds.extents.y) + 960f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bound = GetComponent<BoxCollider2D>();
        UpdateStudentNum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
