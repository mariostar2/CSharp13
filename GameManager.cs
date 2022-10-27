using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp13
{
    internal class GameManager
    {
        struct Vector2
        {
            public int x;
            public int y;

            public Vector2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            
            //백터끼리 연산 가능하게 만들어준다 
            // = 우리가 만든 Vector2자료형은 더할 수 없다(더한다는 행위는 컴퓨터가 판단을 할수 없다.)
            // 그래서 더할 수 있게 정의해준다
            public static Vector2 operator+(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.x + v2.x, v1.y + v2.y);
            }  
            public static Vector2 operator-(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.x - v2.x, v1.y - v2.y);
            }
        }

        enum VECTOR
        {
            //방향을 나타내는 열거형 
            Up,
            Down,
            Left,
            Right,

        }

        Vector2[] vectorToPos = { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
        const char SIGN_PLAYER = '★';
        const char SIGN_WALL = '■';
        const char SIGN_BLANK = '　';

        const int SIZE_WIDTH = 7;
        const int SIZE_HEIGHT = 7;

        char[,] backgrounds;                                // 배경 2차원 배열
        Vector2 WORLD_SIZE;                                 //백그라운드의 크기.       
        Vector2 playerPos;                                  //플레이어 위치
      
        bool isGame;                                        //게임은 실행중인가?

        public void GameStart()
        {
            InIt();
            while (isGame)
            {
                Input();
                Render();
            }
            SetCursor(WORLD_SIZE);
        }

        //최초에 1번 초기 값을 할당하는 함수.
        private void InIt()
        {
            //백그라운드 초기화
            WORLD_SIZE = new Vector2(7, 7);
            backgrounds = new char[WORLD_SIZE.y, WORLD_SIZE.x];
            for(int y = 0; y<WORLD_SIZE.y; y++)
            {
                for(int x = 0; x < WORLD_SIZE.x; x++)
                {
                    //화면의 외곽선 부분에 WALL을 대입한다.
                    if (x == 0 || x == WORLD_SIZE.x-1 || y == 0 || y == WORLD_SIZE.y - 1)
                        backgrounds[y, x] = SIGN_WALL;
                    else
                        backgrounds[y, x] = SIGN_BLANK;
                }
            }
           
            //플레이어의 위치에 플레이어를 그린다
            playerPos = new Vector2();
            playerPos.x = WORLD_SIZE.x / 2;
            playerPos.y = WORLD_SIZE.y / 2;

            //장애물 리스트 초기화.
    
            //게임 시작
            isGame = true;

            //커서 가리기.
            Console.CursorVisible = false;
        }

        // 유저의 입력을 받는 함수
        private void Input()
        {
            //유저가 키입력을 하지 않으면 ..
            if (!Console.KeyAvailable)
                return;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                //화살표 키보드
                case ConsoleKey.LeftArrow:
                    if (IsMovePlayer(playerPos, VECTOR.Left))
                        playerPos += vectorToPos[(int)VECTOR.Left];                       
                    break;

                case ConsoleKey.RightArrow:
                    if (IsMovePlayer(playerPos, VECTOR.Right))
                        playerPos += vectorToPos[(int)VECTOR.Right];
                    break;

                case ConsoleKey.UpArrow:
                    if (IsMovePlayer(playerPos, VECTOR.Up))
                        playerPos += vectorToPos[(int)VECTOR.Up];
                    break;

                case ConsoleKey.DownArrow:
                    if (IsMovePlayer(playerPos, VECTOR.Down))
                        playerPos += vectorToPos[(int)VECTOR.Down];
                    break;
            }
        }
        //특정 위치에서 원하는 방향으로 움직일수 있나요?
        private bool IsMovePlayer(Vector2 pos, VECTOR vector)
        {
            //VECTOR자료형 값을 int값으로 변환후 원하는 방향 값을 정한다.
            //이후 매개변수 Pos에 더한다.
            //가상의 위치를 잡아 그곳이 벽이라면 플레이어는 움직일 수 없는 사실을 전달한다.
            pos += vectorToPos[(int)vector];
            return backgrounds[playerPos.y,playerPos.x] !=SIGN_WALL;
        }
        
        //화면을 그리는 함수 
        private void Render()
        {
            SetCursor(0, 0);
            for (int y = 0; y < WORLD_SIZE.y; y++)
            {
                for (int x = 0; x < WORLD_SIZE.x; x++)
                {
                    //x,y위치에 무엇을 그릴 것인가?
                    bool isPlayer = x == playerPos.x && y == playerPos.y;
                    Console.Write(isPlayer ? SIGN_PLAYER : backgrounds[y, x]);                   
                }
                Console.WriteLine();
            }      
        }

        private void SetCursorX(int x)
        {
            Console.CursorTop = x;
        }
        private void SetCursorY(int y)
        {
            Console.CursorTop = y;
        }
        private void SetCursor(int x, int y)
        {
            //이 게임에서 가로2칸이 1칸으로 취급한다
            Console.CursorLeft = x * 2;
            Console.CursorTop = y;
        }

        private void SetCursor(Vector2 position)
        {
            SetCursor(position.x, position.y);
        }

    }
}
