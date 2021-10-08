using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateMaterials : MonoBehaviour
{
    public Texture2D tex1;
    public Texture2D tex2;
    public Texture2D tex3;
    public Texture2D tex4;

    private float timeChanged;

    private List<GameObject> cubes;

    private void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("cube").ToList();
        timeChanged = Time.time;
    }

    public void UpdateListCubes()
    {
        cubes = GameObject.FindGameObjectsWithTag("cube").ToList();
    }

    public void Update()
    {
        if(Time.time - timeChanged > .3f)
        {
            timeChanged = Time.time;
            
            foreach(GameObject cube in cubes)
            {
                if(cube != null)
                {
                    Texture2D nextTexture = Mathf.FloorToInt(Random.Range(0, 4)) <= 1 ? tex1 : (Mathf.FloorToInt(Random.Range(0, 4)) <= 1 ? tex2 : (Mathf.FloorToInt(Random.Range(0, 4)) <= 1 ? tex3 : tex4));
                    if (cube.transform.childCount == 0)
                        cube.GetComponent<MeshRenderer>().material.mainTexture = nextTexture;
                }
            }
        }
    }
}
