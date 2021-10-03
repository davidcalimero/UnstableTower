using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateMaterials : MonoBehaviour
{
    public Material[] materials;
    public bool[] isTex1;

    public Texture2D tex1;
    public Texture2D tex2;

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
        if(Time.time - timeChanged > .7f)
        {
            timeChanged = Time.time;

            for(int i = 0; i < materials.Length; i++)
            {
                Material mat = materials[i];
                if (isTex1[i])
                {
                    isTex1[i] = false;
                    mat.mainTexture = tex2;
                }
                else
                {
                    isTex1[i] = true;
                    mat.mainTexture = tex1;
                }
            }
            
            foreach(GameObject cube in cubes)
            {
                if(cube.transform.childCount == 0)
                    cube.transform.localEulerAngles = new Vector3(90 * Mathf.FloorToInt(Random.Range(0, 4)), 90 * Mathf.FloorToInt(Random.Range(0, 4)), 90 * Mathf.FloorToInt(Random.Range(0, 4)));
            }
        }
    }
}
