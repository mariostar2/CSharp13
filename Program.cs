using System;

namespace CSharp13
{
    internal class Program
    {
          //차원 배열 (행렬)
           static void Main(string[] args)
           {
              /* int[] array = { 1, 2 };
               Console.WriteLine(array.Length);

               //2개짜리 1차원 배열을 3개 가지고 있다
               int[,] arrays = new int[3, 2]
               {
                     { 1,2},
                     { 3,4},
                     { 5,6},

               };
               Console.WriteLine($"2차원 배열의 행(Y)개수 : {array.GetLength(0)}");
               Console.WriteLine($"2차원 배열의 행(x)개수 : {array.GetLength(1)}");
               Console.WriteLine($"2차원 배열의 개수: {array.GetLength(0) * arrays.GetLength(1)}");

               arrays[0, 0] = 1;
               arrays[0, 1] = 2;
               arrays[1, 0] = 3;
               arrays[1, 1] = 4;
               arrays[2, 0] = 5;
               arrays[2, 1] = 6;
   */

                GameManager gameManager = new GameManager();
                gameManager.GameStart();
            


        }
    }
}
