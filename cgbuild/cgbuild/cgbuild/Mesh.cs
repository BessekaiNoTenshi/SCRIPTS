﻿/* Mesh - all the things that define a mesh. triangles, edges, verticies... later UVs...
*
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleGraphics
{
    class Mesh : Program
    {
        public Mesh(point3[] meshVerts, triangle[] meshFaces, point2[] meshUvs, Material[] mtls = null)
        {
            verts = meshVerts;
            vertCount = verts.Length;
            faces = meshFaces;
            faceCount = faces.Length;
            uvVerts = meshUvs;
            matIDs = mtls;
        }
        public struct triangle
        {
            public int[] vertIDs;
            public int[] uvIds;
            public int matID;
            public triangle(int v1, int v2, int v3, int vt1, int vt2, int vt3, int mid)
            {
                vertIDs = new int[3];
                vertIDs[0] = v1;
                vertIDs[1] = v2;
                vertIDs[2] = v3;
                uvIds = new int[3];
                uvIds[0] = vt1;
                uvIds[1] = vt2;
                uvIds[2] = vt3;
                matID = mid;
            }
        };
        public struct point3
        {
            public float x, y, z;
            public point3(float newX, float newY, float newZ = 0)
            {
                x = newX;
                y = newY;
                z = newZ;
            }
        };
        public struct point2
        {
            public float x, y;
            public point2(float newX, float newY)
            {
                x = newX;
                y = newY;
            }
        };
        public point3[] verts;
        public triangle[] faces;
        public point2[] uvVerts;
        public int vertCount;
        public int faceCount;
        public Material[] matIDs;
        public void translate(point3 translation)
        {
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = Matrix.add(verts[i], translation);
            }
        }
        public void rotate(double angle, byte dir)
        {
            //Rotate about centerpoint of the shape: translate the center of our shape to the origin, rotate, translate back.
            float cosAngle = (float)Math.Cos(angle);
            float sinAngle = (float)Math.Sin(angle);
            Matrix rotMtrx = null;
            point3 startCoords = verts[0];
            translate(new Mesh.point3(-startCoords.x, -startCoords.y, -startCoords.z));
            #region rotations
            /* Rotate about X Axis {{1,0, 0},
{0, cosAngle,sinAngle*100},
{0, -sinAngle/100, cosAngle}}, 3, 3);
* Rotate about Y Axis {{cosAngle,0,-sinAngle*100},
{0, 1, 0},
{sinAngle, 0, cosAngle}}, 3, 3);
Rotate about Z axis
* {{cosAngle,-sinAngle,0},
{sinAngle, cosAngle,0},
{0, 0, 1}},3,3);
*/
            #endregion
            int x = Environment.TickCount;
            if (dir == 0)
                rotMtrx = new Matrix(new float[,] {{1,0,0},
{0, cosAngle,sinAngle*100},
{0, -sinAngle/100, cosAngle}}, 3, 3);
            else if (dir == 1)
                rotMtrx = new Matrix(new float[,] {{cosAngle,0,-sinAngle*100},
{0, 1, 0},
{sinAngle/100, 0, cosAngle}}, 3, 3);
            else
                rotMtrx = new Matrix(new float[,] {{cosAngle,-sinAngle,0},
{sinAngle, cosAngle,0},
{0, 0, 1}}, 3, 3);
            x = Environment.TickCount - x;
            for (int i = 0; i < vertCount; i++)
            {
                verts[i] = rotMtrx.multiply(verts[i]);
            }
            translate(new Mesh.point3(startCoords.x, startCoords.y, startCoords.z));
        }
    }
}