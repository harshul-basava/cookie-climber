// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public class holdGenerator : MonoBehaviour
// {
//     public float width;
//     public float height;
//     public GameObject hold;
//     public GameObject mm;

//     float r; //radius
//     int dim; //dimensions
//     int k; //number of tries
//     int n; //number of jumps

//     float cellSize;
//     int cellWidth;
//     int cellHeight;

//     Vector2 [,] grid;

//     void Start ()
//     {
//         n = 0;
//         r = 1.172f;
//         dim = 2;
//         k = 10;

//         cellSize = (float) (r / Math.Sqrt(dim));
//         cellWidth = (int) Math.Ceiling(width / cellSize) + 1;
//         cellHeight = (int) Math.Ceiling(height / cellSize) + 1;
//         grid = new Vector2[cellWidth, cellHeight];
        
//         Vector2 og = new Vector2(width * 0.5f, 0f);
//         List<Vector2> p = GeneratePoints(og);

//         for(int i = 0; i < p.Count; i++)
//         {
//             var curr = p[i];
//             curr.x -= width*0.5f;
//             curr.y -= height*0.5f;

//             curr.x *= 2;
//             curr.y *= 2;

//             Debug.Log(curr);
//             Instantiate(hold, new Vector3(curr.x, curr.y, -0.5f), Quaternion.identity, this.transform);
//         }
//     }

//     public List<Vector2> GeneratePoints(Vector2 origin) 
//     {
//         var empty = new Vector2(-1, -1);
//         for(int i = 0; i < cellWidth; i++)
//         {
//             for(int j = 0; j < cellHeight; j++)
//             {
//                 grid[i, j] = empty;
//             }
//         }

//         List<Vector2> points = new List<Vector2>();
//         List<Vector2> active = new List<Vector2>();

//         points.Add(origin);
//         active.Add(origin);
//         insertToGrid(origin, cellSize);
//         Debug.Log(origin);

//         while (active.Count > 0) 
//         {
//             int index = (int) UnityEngine.Random.Range(0, active.Count);
//             Vector2 curr = active[index];
//             bool found = false;

//             float epsilon = 0.00001f;
//             float s = UnityEngine.Random.Range(0.0f, 1f);

//             for (int i = 0; i < k; i++)
//             {
//                 float theta = 2 * Mathf.PI * (i + (k*s));
//                 theta = theta/k;
//                 // Debug.Log(theta + " on iteration: " + i);
//                 // Debug.Log(theta);
//                 float rad = r + epsilon;
//                 float x = rad * Mathf.Cos(theta);
//                 float y = rad * Mathf.Sin(theta);
//                 // Debug.Log("(" + x + ", " + y + ")");
//                 Vector2 offset_direction = new Vector2(x, y);

//                 Vector2 sample = curr + offset_direction;
//                 // Debug.Log(sample);

//                 if (isValid(sample)) 
//                 {
//                     found = true;
//                     points.Add(sample);
//                     active.Add(sample);
//                     insertToGrid(sample, cellSize);
//                     continue;
//                 }
//             }

//             if (!found) 
//             {
//                 active.RemoveAt(index);
//             }
//         }
//         return points;
//     }

//     public bool isValid(Vector2 sample)
//     {
//         if (sample.x >= 0 && sample.x <= width && sample.y >= 0 && sample.y <= height)
//         {
//             int x = (int) Math.Floor(sample.x / cellSize);
//             int y = (int) Math.Floor(sample.y / cellSize);
//             if (grid[x, y].x > 0)
//             {
//                 return false;
//             }
//             int iMin = Mathf.Max(x-1, 0);
//             int iMax = Mathf.Min(x+1, cellWidth-1);
//             int jMin = Mathf.Max(y-1, 0);
//             int jMax = Mathf.Min(y+1, cellHeight-1);

//             for (int i = iMin; i < iMax; i++)
//             {
//                 for (int j = jMin; j < jMax; j++)
//                 {
//                     Vector2 currSamp = grid[i, j];
//                     if (currSamp.x >= 0 && currSamp.y >= 0)
//                     {
//                         float d = (float) (Math.Pow((currSamp.x - x), 2) + Math.Pow((currSamp.y - y), 2));
//                         if (d < Math.Pow(r, 2))
//                         {
//                             return false;
//                         }
//                     }
//                 }
//             }
//             return true;
//         } 
//         return false;
//     }

//     public void insertToGrid(Vector2 point, float cellSize) 
//     {
//         var xIndex = (int) (point.x / cellSize);
//         var yIndex = (int) ((point.y / cellSize));

//         grid[xIndex, yIndex] = point;
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && !mm.GetComponent<movementManagement>().jump) 
//         {
//             n += 1;
//             height = 2.5f;
//             width = 6;
//             r = 1.1f;

//             cellSize = (float) (r / Math.Sqrt(dim));
//             cellHeight = (int) Math.Ceiling(height / cellSize) + 1;
//             cellWidth = (int) Math.Ceiling(width / cellSize) + 1;

//             grid = new Vector2[cellWidth, cellHeight];

//             Vector2 og = new Vector2(width * 0.5f, 0f);
//             List<Vector2> p = GeneratePoints(og);

//             for(int i = 0; i < p.Count; i++)
//             {
//                 var curr = p[i];
//                 curr.x -= width*0.5f;
//                 curr.y -= height*0.5f;

//                 curr.x *= 2;
//                 curr.y *= 2;

//                 curr.y += 7.5f + 1.5f*n;

//                 Debug.Log(curr);
//                 if (curr.x <= 5 && curr.x >= -5)
//                 {
//                     Instantiate(hold, new Vector3(curr.x, curr.y, -0.5f), Quaternion.identity, this.transform);
//                 }
//             }
//         } 
//     }
// }
