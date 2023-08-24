using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.UI.Image;

public class Puzzle : MonoBehaviour
{
    public bool ReadOnly=true;
    public GameObject block;
    public int sizeX,sizeY;
    public readonly int sizeZ = 2;
    Vector3 unitX,unitY,unitZ;
    static public readonly Color[] color = { Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };
    static public readonly string[] colorName = { "black", "blue", "cyan", "gray", "green", "magenta", "red", "white", "yellow" };
    public Color[,,] data;
    IEnumerator fading(GameObject block,float rate)
    {
        yield return rate;
        Color color = block.GetComponent<Renderer>().material.color;
        float a=(Mathf.Sin(Time.deltaTime * rate / Mathf.PI) + 0.5f) * 0.5f;
        Color newColor = new Color(color.r, color.g, color.b, a);
        block.GetComponent<Renderer>().material.color = newColor;
        Puzzle.block_Blink(block,true, rate);
    }
    static public void block_Blink(GameObject whichblock,bool blink,float rate)
    {
        Puzzle script=whichblock.transform.parent.GetComponent<Puzzle>();
        if(blink)
            script.StartCoroutine(script.fading(whichblock,rate));
        else
            script.StopAllCoroutines();
    }
    static public bool Compare(GameObject object1, GameObject object2)
    {
        Puzzle script1 = object1.GetComponent<Puzzle>();
        Puzzle script2 = object2.GetComponent<Puzzle>();
        if (script1 != null && script2 != null)
        {
            if (script1.sizeX != script2.sizeX || script2.sizeY != script1.sizeY)
                return false;
            else
            {
                for (int k = 0; k < script1.sizeZ; k++)
                    for (int j = 0; j < script1.sizeY; j++)
                        for (int i = 0; i < script1.sizeX; i++)
                        {
                            if (script1.data[i, j, k] != script2.data[i, j, k])
                            {
                                Debug.Log("color not match: " + script1.data[i, j, k].ToString() + " not equal to " + script2.data[i, j, k].ToString());
                                return false;
                            }
                        }
            }
        }
        return true;
    }
    static GameObject block_GetBlock(GameObject whichpuzzle, Vector3Int coordinate)
    {
        foreach (Transform t in whichpuzzle.transform)
            if (t.localPosition == coordinate)
                return t.gameObject;
        return null;
    }
    static public Vector3Int block_GetCoordinate(GameObject whichblock)
    {
        Vector3Int v=new Vector3Int();
        Puzzle script = whichblock.transform.parent.GetComponent<Puzzle>();
        v.x=(int)(whichblock.transform.localPosition.x/script.unitX.x);
        v.y=(int)(whichblock.transform.localPosition.y/script.unitY.y);
        v.z=(int)(whichblock.transform.localPosition.z/script.unitZ.z);
        Debug.Log("ID: "+whichblock.GetInstanceID()+"; Coordinate: "+v.ToString());
        return v;
    }
    static public void block_SetColor(GameObject whichblock, Color whichcolor)
    {
        Vector3Int v=block_GetCoordinate(whichblock);
        Puzzle script = whichblock.transform.parent.GetComponent<Puzzle>();
        if(!script.ReadOnly)
        {
            script.data[v.x, v.y, v.z] = whichcolor;
            whichblock.GetComponent<Renderer>().material.color = whichcolor;
            whichblock.GetComponent<AudioSource>().Play();
            for (int i = 0; i < Puzzle.color.Length; i++)
                if (Puzzle.color[i] == script.data[v.x, v.y, v.z])
                {
                    Debug.Log(Puzzle.colorName[i]);
                    break;
                }
        }
    }
    public int GetColor()
    {
        List<Color> list = new List<Color>();
        for (int k = 0; k <sizeZ; k++)
            for (int j = 0; j < sizeY; j++)
                for (int i = 0; i < sizeX; i++)
                    if (!list.Contains(data[i,j,k]))
                        list.Add(data[i, j, k]);
        return list.Count;
    }
    public void SetColor(Color[,,] whichcolor)
    {
        Puzzle s=GetComponent<Puzzle>();
        for (int k = 0; k < s.sizeZ; k++)
            for (int j = 0; j < s.sizeY; j++)
                for (int i=0;i<s.sizeX;i++)
                    block_SetColor(Puzzle.block_GetBlock(gameObject,new Vector3Int(i,j,k)), whichcolor[i,j,k]);
    }
    public void debug(List<Color> colors)
    {
        Hashtable h = new Hashtable();
        for(int i=0;i<color.Length;i++)
            h.Add(colorName[i], color[i]);
        foreach (Color col in colors)
            foreach (DictionaryEntry e in h)
                if ((Color)e.Value == col)
                    Debug.Log(e.Key);
        Debug.Log("ID: "+GetInstanceID());
        Debug.Log("Size: "+sizeX+"x"+sizeY+"x"+sizeZ);
        Debug.Log("Volume: "+gameObject.transform.childCount);
        Debug.Log("Object has " + GetColor() + " Color");
        Debug.Log("Modifiable: " + !ReadOnly);
        h.Clear();
    }
    public void clear()
    {
        foreach (Transform t in transform)
            Destroy(t.gameObject);
    }
    public GameObject create(int sizeX,int sizeY,int maxColor,bool modifiable)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        ReadOnly = !modifiable;
        data = new Color[this.sizeX, this.sizeY, this.sizeZ];
        List<Color> list = color.ToList();
        int c=Random.Range(1,maxColor+1);
        while (list.Count > c)
            list.Remove(list[Random.Range(0, list.Count)]);
        for(int k=0;k<this.sizeZ;k++)
        {
            for (int j = 0; j < this.sizeY; j++)
            {
                for (int i = 0; i < this.sizeX; i++)
                {
                    int rng = Random.Range(0, list.Count);
                    GameObject g = Instantiate(block,unitX * i + unitY * j+unitZ*k, block.transform.rotation);
                    g.transform.parent = gameObject.transform;
                    g.GetComponent<Renderer>().material.color = list[rng];
                    data[i, j, k] = list[rng];
                }

            }
        }
        debug(list);
        return gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        unitX=block.transform.localScale.x*Vector3.right;
        unitY=block.transform.localScale.y*Vector3.up;
        unitZ=block.transform.localScale.z * Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
