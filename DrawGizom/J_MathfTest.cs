using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：J_MathfTest  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/7/26 9:29:57
// ================================
namespace Assets.JackCheng.Script
{
    //[ExecuteInEditMode]
    public class J_MathfTest : MonoBehaviour
    {
        [Range(1, 1000)]
        public int len = 10;
        [Range(1, 100)]
        public int factor = 10;

        public Camera cam;

        private Transform tx;
        private List<Vector3> arrayPoints = new List<Vector3>();

        private List<GameObject> listChilds = new List<GameObject>();

        void Start()
        {
            tx = cam.transform;
        }

        void Update()
        {
            //DrawTriangle();
        }

        void OnGUI()
        {
            //GUILayout.BeginHorizontal();

            //GUILayout.TextField(Mathf.Sin(Mathf.Deg2Rad * 30).ToString());

            //GUILayout.TextField(Mathf.Sin(Mathf.Deg2Rad * 45).ToString());

            //GUILayout.TextField(Mathf.Sin(Mathf.Deg2Rad * 60).ToString());

            //GUILayout.TextField(Mathf.Sin(Mathf.Deg2Rad * 90).ToString());

            //GUILayout.TextField(Mathf.Sin(Mathf.Deg2Rad * 180).ToString());

            //GUILayout.EndHorizontal();
        }

        void OnDrawGizmos()
        {
            //Gizmos.color = Color.green;

            //DrawSin();
            //DrawCircle();
            //DrawSector();
            //DrawScrew();
            //DrawSphere();
        }
        #region Circle
        [Range(1,100)]
        public int circleR = 10;
        [Range(1,1000)]
        public int circlePoint = 100;
        private List<Vector3> listUpRight = new List<Vector3>();
        private List<Vector3> listDownRight = new List<Vector3>();
        private List<Vector3> listUpLeft = new List<Vector3>();
        private List<Vector3> listDownLeft = new List<Vector3>();
        void DrawCircle() 
        {
            float x = 0;
            float y = 0;
            //0~R
            listUpRight.Clear();
            listDownRight.Clear();
            for (int i = 0; i <= circlePoint; i++) { 
                x = circleR - (float)i * circleR/circlePoint;
                y = Mathf.Sqrt(circleR * circleR - x * x);

                listUpRight.Add(new Vector3(x, y, 0));
                listDownRight.Add(new Vector3(x, -y, 0));
            }

            x = 0;
            y = 0;
            //-R~0
            listUpLeft.Clear();
            listDownLeft.Clear();
            for (int i = 0; i <= circlePoint; i++)
            {
                x = (float)-1 * i * circleR / circlePoint;
                y = Mathf.Sqrt(circleR * circleR - x * x);

                listUpLeft.Add(new Vector3(x, y, 0));
                listDownLeft.Add(new Vector3(x, -y, 0));
            }

            Draw(listUpRight);
            Draw(listDownRight);
            Draw(listUpLeft);
            Draw(listDownLeft);
        }

        public Vector3 sectorDir = Vector3.forward;
        [Range(1,360)]
        public float sectorAngle = 60;
        public float sectorDistence = 10;

        public int sectorAverage = 10;

        private List<Vector3> listSectorRight = new List<Vector3>();
        private List<Vector3> listSectorLeft = new List<Vector3>();

        Quaternion tempQua;

        void DrawSector() 
        {
            tempQua = Quaternion.FromToRotation(Vector3.forward, sectorDir);
            listSectorRight.Clear();
            //Right
            Vector3 tempV = Vector3.zero;
            for (int i = 0; i < sectorAverage; i++) 
            {
                tempV =tempQua * new Vector3(Mathf.Sin((i * sectorAngle * 0.5f / sectorAverage) * Mathf.Deg2Rad) * sectorDistence, Mathf.Cos(i * sectorAngle * 0.5f / sectorAverage * Mathf.Deg2Rad) * sectorDistence, 0);
                listSectorRight.Add(tempV);
                Gizmos.DrawLine(Vector3.zero, tempV);
            }

            listSectorLeft.Clear();
            //Left
            tempV = Vector3.zero;
            for (int i = 0; i < sectorAverage; i++)
            {
                tempV =tempQua * new Vector3(-1 * Mathf.Sin((i * sectorAngle * 0.5f / sectorAverage) * Mathf.Deg2Rad) * sectorDistence, Mathf.Cos(i * sectorAngle * 0.5f / sectorAverage * Mathf.Deg2Rad) * sectorDistence, 0);
                listSectorLeft.Add(tempV);
                Gizmos.DrawLine(Vector3.zero, tempV);
            }

            Draw(listSectorRight);
            Draw(listSectorLeft);
        }

        #endregion

        #region Screw
        public Vector3 screwDir = Vector3.forward;
        
        public float screwAngle = 60;
        public float screwDistence = 10;

        public int screwAverage = 10;
        public int screwFactor = 2;

        private List<Vector3> listScrew = new List<Vector3>();
        void DrawScrew() {
            tempQua = Quaternion.FromToRotation(Vector3.forward, screwDir);
            listScrew.Clear();
            //Right
            Vector3 tempV = Vector3.zero;
            for (int i = 0; i < screwAverage; i++)
            {
                tempV = tempQua * new Vector3(Mathf.Sin((i * screwAngle  / screwAverage) * Mathf.Deg2Rad) * screwDistence, 
                                              Mathf.Cos(i * screwAngle  / screwAverage * Mathf.Deg2Rad) * screwDistence, 
                                              i * screwFactor);
                listScrew.Add(tempV);
                Gizmos.DrawLine(tempQua * new Vector3(0, 0, i * screwFactor), tempV);
            }

            Draw(listScrew);
        }

        #endregion

        #region Sin
        void DrawSin() {
            arrayPoints.Clear();
            for (int i = 1; i < len; i++)
            {
                arrayPoints.Add(new Vector3((float)factor * i / 100, Mathf.Sin(Mathf.Deg2Rad * i), 0));
            }

            for (int i = 0; i < arrayPoints.Count; i++)
            {
                if (i + 1 < arrayPoints.Count - 1)
                    Gizmos.DrawLine(arrayPoints[i], arrayPoints[i + 1]);

            }
        }
        #endregion 

        #region Triangle
        void DrawTriangle()
        {
            if (listChilds.Count == 0)
            {
                listChilds = new List<GameObject>();
                for (int i = 0; i < 4; i++)
                {
                    GameObject obj = new GameObject("child" + i);
                    obj.transform.SetParent(this.transform, false);
                    listChilds.Add(obj);
                }

            }
            Vector3[] vNear = GetCorners(cam.nearClipPlane);

            Vector3[] vFar = GetCorners(cam.farClipPlane);

            
            for (int i = 0; i < 4; i++)
            {
                MeshFilter mf = listChilds[i].GetComponent<MeshFilter>();
                if (mf == null)
                    mf = listChilds[i].AddComponent<MeshFilter>();

                if (i == 0)
                {
                    Mesh mesh = new Mesh();
                    mf.sharedMesh = mesh;
                    mesh.vertices = new Vector3[]{
                    vNear[0],vFar[0],vFar[1],
                    vNear[0],vFar[1],vNear[1],
                    };

                    mesh.triangles = new int[]{
                    0,1,2,
                    3,4,5,
                    };

                    mesh.RecalculateNormals();
                    mesh.RecalculateBounds();

                    // 使用Shader构建一个材质，并设置材质的颜色。
                    Material material = new Material(Shader.Find("Diffuse"));
                    material.SetColor("_Color", Color.red);

                    MeshRenderer renderer = listChilds[i].GetComponent<MeshRenderer>();
                    if (renderer == null)
                        renderer = listChilds[i].AddComponent<MeshRenderer>();
                    renderer.sharedMaterial = material;
                }
                else if (i == 1)
                {
                    Mesh mesh = new Mesh();
                    mf.sharedMesh = mesh;
                    mesh.vertices = new Vector3[]{

                    vNear[1],vFar[1],vFar[2],
                    vNear[1],vFar[2],vNear[2],
                    };

                    mesh.triangles = new int[]{
                    0,1,2,
                    3,4,5,
                    };

                    mesh.RecalculateNormals();
                    mesh.RecalculateBounds();

                    // 使用Shader构建一个材质，并设置材质的颜色。
                    Material material = new Material(Shader.Find("Diffuse"));
                    material.SetColor("_Color", Color.green);

                    MeshRenderer renderer = listChilds[i].GetComponent<MeshRenderer>();
                    if (renderer == null)
                        renderer = listChilds[i].AddComponent<MeshRenderer>();
                    renderer.sharedMaterial = material;

                }
                else if (i == 2)
                {
                    Mesh mesh = new Mesh();
                    mf.sharedMesh = mesh;
                    mesh.vertices = new Vector3[]{

                    vNear[2],vFar[2],vFar[3],
                    vNear[2],vFar[3],vNear[3],
                    };

                    mesh.triangles = new int[]{
                    0,1,2,
                    3,4,5,
                    };

                    mesh.RecalculateNormals();
                    mesh.RecalculateBounds();

                    // 使用Shader构建一个材质，并设置材质的颜色。
                    Material material = new Material(Shader.Find("Diffuse"));
                    material.SetColor("_Color", Color.blue);

                    MeshRenderer renderer = listChilds[i].GetComponent<MeshRenderer>();
                    if (renderer == null)
                        renderer = listChilds[i].AddComponent<MeshRenderer>();
                    renderer.sharedMaterial = material;
                }
                else if (i == 3)
                {
                    Mesh mesh = new Mesh();
                    mf.sharedMesh = mesh;
                    mesh.vertices = new Vector3[]{
                    vNear[3],vFar[3],vFar[0],
                    vNear[3],vFar[0],vNear[0],
                    };

                    mesh.triangles = new int[]{
                    0,1,2,
                    3,4,5,
                    2,1,0,
                    5,4,3,
                    };

                    mesh.RecalculateNormals();
                    mesh.RecalculateBounds();

                    // 使用Shader构建一个材质，并设置材质的颜色。
                    Material material = new Material(Shader.Find("Diffuse"));
                    material.SetColor("_Color", Color.yellow);

                    MeshRenderer renderer = listChilds[i].GetComponent<MeshRenderer>();
                    if (renderer == null)
                        renderer = listChilds[i].AddComponent<MeshRenderer>();
                    renderer.sharedMaterial = material;
                }



                // 构建一个MeshRender并把上面创建的材质赋值给它，
                // 然后使其把上面构造的Mesh渲染到屏幕上。

                
            }
        }
        #endregion

        #region Sphere
        private List<Vector3> listSphere = new List<Vector3>();

        public int sphereAverage=20;

        public int sphereCircleAverage = 15;

        public int sphereR = 10;
        void DrawSphere() 
        {
            listSphere.Clear();
            //Forward
            float x = 0;
            for (int i = 0; i < sphereAverage; i++)
            {
                x = (float)i * sphereR / sphereAverage;
                SpherePiece((float)i * sphereR / sphereAverage, Mathf.Sqrt(sphereR * sphereR - x * x));
            }
            //Back
            for (int i = 1; i < sphereAverage; i++)
            {
                x = (float)i * sphereR / sphereAverage;
                SpherePiece((float)-1 * i * sphereR / sphereAverage, Mathf.Sqrt(sphereR * sphereR - x * x));
            }
            Draw(listSphere);
        }
        private void SpherePiece(float z,float r) 
        {
            Vector3 tempV = Vector3.zero;
            for (int i = 0; i < sphereCircleAverage; i++)
            {
                tempV = new Vector3(Mathf.Sin((i * 360 / sphereCircleAverage) * Mathf.Deg2Rad) * r,
                                              Mathf.Cos(i * 360 / sphereCircleAverage * Mathf.Deg2Rad) * r,
                                              z);
                listSphere.Add(tempV);
                Gizmos.DrawLine(Vector3.zero, tempV);
            }
        }
        #endregion

        void Draw(List<Vector3> listP) 
        {
            for (int i = 0; i < listP.Count; i++)
            {
                if (i + 1 < listP.Count)
                    Gizmos.DrawLine(listP[i], listP[i + 1]);
                else if (i + 1 == listP.Count)
                    Gizmos.DrawLine(listP[i], listP[0]);
            }
        }

        Vector3[] GetCorners(float distance)
        {
            if (cam == null)
            {
                cam = Camera.main;
                tx = cam.transform;
            }
            Vector3[] corners = new Vector3[4];

            float halfFOV = (cam.fieldOfView * 0.5f) * Mathf.Deg2Rad;
            float aspect = cam.aspect;

            float height = distance * Mathf.Tan(halfFOV);
            float width = height * aspect;

            // UpperLeft
            corners[0] = tx.position - (tx.right * width);
            corners[0] += tx.up * height;
            corners[0] += tx.forward * distance;

            // UpperRight
            corners[1] = tx.position + (tx.right * width);
            corners[1] += tx.up * height;
            corners[1] += tx.forward * distance;

            // LowerRight
            corners[2] = tx.position + (tx.right * width);
            corners[2] -= tx.up * height;
            corners[2] += tx.forward * distance;

            // LowerLeft
            corners[3] = tx.position - (tx.right * width);
            corners[3] -= tx.up * height;
            corners[3] += tx.forward * distance;





            return corners;
        }


    }
}
