using System;

class Program
{
    static float A, B, C;
    static float cubeWidth = 20;
    static int width = 160, heigth = 44;
    static float[] zBuffer = new float[width * heigth];
    static char[] buffer = new char[width * heigth];
    static int backgroundASCIICode = '.';
    static int distanceFromCam = 100;
    static float horizontalOffset;
    static float K1 = 40;
    static float incrementSpeed = 0.6f;
    static float x, y, z;
    static float ooz;
    static int xp, yp;
    static int idx;

    static float CalculateX(int i, int j, int k)
    {
        return j * (float)Math.Sin(A) * (float)Math.Sin(B) * (float)Math.Cos(C) - k * (float)Math.Cos(A) * (float)Math.Sin(B) * (float)Math.Cos(C) +
            j * (float)Math.Cos(A) * (float)Math.Sin(C) + k * (float)Math.Sin(A) * (float)Math.Sin(C) + i * (float)Math.Cos(B) * (float)Math.Cos(C);
    }

    static float CalculateY(int i, int j, int k)
    {
        return j * (float)Math.Cos(A) * (float)Math.Cos(C) + k * (float)Math.Sin(A) * (float)Math.Cos(C) -
            j * (float)Math.Sin(A) * (float)Math.Sin(B) * (float)Math.Sin(C) + k * (float)Math.Cos(A) * (float)Math.Sin(B) * (float)Math.Sin(C) -
            i * (float)Math.Cos(B) * (float)Math.Sin(C);
    }

    static float CalculateZ(int i, int j, int k)
    {
        return k * (float)Math.Cos(A) * (float)Math.Cos(B) - j * (float)Math.Sin(A) * (float)Math.Cos(B) + i * (float)Math.Sin(B);
    }

    static void CalculateForSurface(float cubeX, float cubeY, float cubeZ, char ch)
    {
        x = CalculateX((int)cubeX, (int)cubeY, (int)cubeZ);
        y = CalculateY((int)cubeX, (int)cubeY, (int)cubeZ);
        z = CalculateZ((int)cubeX, (int)cubeY, (int)cubeZ) + distanceFromCam;

        ooz = 1 / z;

        xp = (int)(width / 2 + horizontalOffset + K1 * ooz * x * 2);
        yp = (int)(heigth / 2 + K1 * ooz * z);

        idx = xp + yp * width;
        if (idx >= 0 && idx < width * heigth)
        {
            if (ooz > zBuffer[idx])
            {
                zBuffer[idx] = ooz;
                buffer[idx] = ch;
            }
        }
    }

    static void Main()
    {
        Console.Clear();
        while (true)
        {
            Array.Fill(buffer, backgroundASCIICode);
            Array.Fill(zBuffer, 0);

            cubeWidth = 20;
            horizontalOffset = -2 * cubeWidth;
            //Primeiro Cubo
            for (float cubeX = -cubeWidth; cubeX < cubeWidth; cubeX += incrementSpeed)
            {
                for (float cubeY = -cubeWidth; cubeY < cubeWidth; cubeY += incrementSpeed)
                {
                    CalculateForSurface(cubeX, cubeY, -cubeWidth, '@');
                    CalculateForSurface(cubeWidth, cubeY, cubeX, '$');
                    CalculateForSurface(-cubeWidth, cubeY, -cubeX, '~');
                    CalculateForSurface(-cubeX, cubeY, cubeWidth, '#');
                    CalculateForSurface(cubeX, -cubeWidth, -cubeY, ';');
                    CalculateForSurface(cubeX, cubeWidth, cubeY, '+');
                }
            }

            Console.SetCursorPosition(0, 0);
            for (int k = 0; k < width * heigth; k++)
            {
                Console.WriteLine(buffer[k]);
                if (k % width == width - 1)
                    Console.WriteLine();
            }

            A += 0.05f;
            B += 0.05f;
            C += 0.01f;
            System.Threading.Thread.Sleep(16); //Tipo 60fps
        }
    }
}
