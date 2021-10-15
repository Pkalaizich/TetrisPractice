using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PieceBehaviour : MonoBehaviour
{
    public float timeToFall=0.8f;
    private float previousTime;
    public static int height = 26;
    public static int width = 10;
    public Vector3 rotationPoint;
    private static Transform[,] grid = new Transform[width, height];
    
    private void Awake()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeToFall /=1000;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if(!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }
        if(Time.time - previousTime >((Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))? timeToFall/10:timeToFall))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                CheckGameOver();
                this.enabled = false;
                FindObjectOfType<SpawnPiece>().newPiece();
            }              

            previousTime = Time.time;
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            //if (roundedY >= (height-1))
            //{
            //    Debug.Log("Se paso de altura");
            //    SceneManager.LoadScene(0);
            //}

            grid[roundedX, roundedY] = children;
        }
    }
    bool ValidMove()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX<0|| roundedX>= width || roundedY<0|| roundedY>=height)
            {
                return false;
            }
            if(grid[roundedX,roundedY]!=null)
            {
                return false;
            }
        }
        return true;
    }
    void CheckGameOver()
    {
        for (int i = 0; i < (width); i++)
        {
            if (grid[i, (21)] != null)
            {
                SceneManager.LoadScene(0);
            }
        }
        return;
    }
    void CheckForLines()
    {
        for (int i = height-1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine (int i)
    {
        for (int j=0; j<width;j++)
        {
            if (grid[j,i]==null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j,i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y =i; y< height; y++)
        {
            for (int j =0; j<width;j++)
            {
                if (grid[j,y] !=null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }


    
}
