using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class polygonCulling : MonoBehaviour
{
    public List<PolygonCollider2D> polygonList;
    public float range = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        polygonList = FindObjectsByType<PolygonCollider2D>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
        StartCoroutine(corUpdatePolygons());
    }
    public IEnumerator corUpdatePolygons()
    {
        while (true)
        {
            foreach (PolygonCollider2D polygonCollider2D in polygonList)
            {
                if(polygonCollider2D == null)
                {
                    continue;
                }
                //print(Vector3.Distance(transform.position, polygonCollider2D.transform.position));
                if (Vector3.Distance(transform.position, polygonCollider2D.transform.position) > range)
                {
                    polygonCollider2D.enabled = false;
                }
                else
                {
                    polygonCollider2D.enabled = true;
                }
            }
            yield return new WaitForSeconds(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
