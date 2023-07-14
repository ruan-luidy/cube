using System;

class Program
{
    static float A, B, C;
    static float cubWidth = 20;
    static int width = 160, heigth = 44;
    static float[] zBuffer = new float[width * heigth];
    static char[] buffer = new char[width * heigth];
    static int backgroundASCIICode = '.';
    static int distanceFromCam = 100;
    static float horizontalOffSet;
    static float K1 = 40;
    static float incrementSpeed = 0.6f;
    static float x, y, z;
    static float ooz;
    static int xp, yp;
    static int idx;
}