using System;

class Program
{
    static float A, B, C;
    static float cubeWidth = 20;
    static int width = 160, height = 44;
    static float[] zBuffer = new float[width * height];
    static char[] buffer = new char[width * height];
    static int backgroundASCIICode = '.';
    static int distanceFromCam = 100;
    static float horizontalOffset;
    static float K1 = 40;
    static float incrementSpeed = 0.6f;
    static float x, y, z;
    static float ooz;
    static int xp, yp;
    static int idx;

    static float CalculateX(float i, float j, float k)
    {
        return j * (float)Math.Sin(A) * (float)Math.Sin(B) * (float)Math.Cos(C) - k * (float)Math.Cos(A) * (float)Math.Sin(B) * (float)Math.Cos(C) +
            j * (float)Math.Cos(A) * (float)Math.Sin(C) + k * (float)Math.Sin(A) * (float)Math.Sin(C) + i * (float)Math.Cos(B) * (float)Math.Cos(C);
    }

    static float CalculateY(float i, float j, float k)
    {
        return j * (float)Math.Cos(A) * (float)Math.Cos(C) + k * (float)Math.Sin(A) * (float)Math.Cos(C) -
            j * (float)Math.Sin(A) * (float)Math.Sin(B) * (float)Math.Sin(C) + k * (float)Math.Cos(A) * (float)Math.Sin(B) * (float)Math.Sin(C) -
            i * (float)Math.Cos(B) * (float)Math.Sin(C);
    }

    static float CalculateZ(float i, float j, float k)
    {
        return k * (float)Math.Cos(A) * (float)Math.Cos(B) - j * (float)Math.Sin(A) * (float)Math.Cos(B) + i * (float)Math.Sin(B);
    }

    static void CalculateForSurface(float cubeX, float cubeY, float cubeZ, char ch)
    {
        x = CalculateX(cubeX, cubeY, cubeZ);
        y = CalculateY(cubeX, cubeY, cubeZ);
        z = CalculateZ(cubeX, cubeY, cubeZ) + distanceFromCam;

        ooz = 1 / z;

        xp = (int)(width / 2 + horizontalOffset + K1 * ooz * x * 2);
        yp = (int)(height / 2 + K1 * ooz * y);

        idx = xp + yp * width;
        if (idx >= 0 && idx < width * height)
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
            Array.Fill<char>(buffer, backgroundASCIICode);
            Array.Fill<float>(zBuffer, 0);

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
            for (int k = 0; k < width * height; k++)
            {
                Console.Write(buffer[k]);
                if ((k + 1) % width == 0)
                    Console.WriteLine();
            }

            A += 0.05f;
            B += 0.05f;
            C += 0.01f;
            System.Threading.Thread.Sleep(16); //tipo 60fps
        }
    }
}
