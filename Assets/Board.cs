using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile[][] tiles;

    int currentX;
    int currentY;

    Dictionary<Vector2, int> visitedPositions;

    int totalBellsCount = 0;

    List<Tile> resultList;

    bool animateGame = false;

    float elapsedTime = 0;
    float previousTime = 0;

    int currentMoveIndex = 0;

    bool isGameComplete = false;

    public AudioManager audioManager;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        resultList = new List<Tile>();

        visitedPositions = new Dictionary<Vector2, int>();

        tiles = new Tile[4][];
        
        tiles[0] = new Tile[4];
        tiles[1] = new Tile[4];
        tiles[2] = new Tile[4];
        tiles[3] = new Tile[4];

        currentX = 0;
        currentY = 0;

        PopulateBoard();
    }

    void PopulateBoard()
    {
        int j = 0;

        for(int i = 0; i < transform.childCount; i++)
        {
            Tile tile = transform.GetChild(i).GetComponent<Tile>();

            if(tile.GetBell())
            {
                totalBellsCount++;
            }

            tiles[j][i%4] = transform.GetChild(i).GetComponent<Tile>();
            if ((i+1) % 4 == 0)
                j++;
        }

        
    }

    void ClearValues()
    {
        visitedPositions.Clear();
        resultList.Clear();
    }

    public void PlayGame()
    {
        gameManager.isGameLocked = true;

        if (isGameComplete)
            return;

        ClearValues();
        Vector2 pos = Vector2.zero;
        Vector2 dir = tiles[0][0].GetBell().GetFacingDirection();

        visitedPositions.Add(pos, 1);

        tiles[Mathf.RoundToInt(pos.x)][Mathf.RoundToInt(pos.y)].Visited(GetModifiedDirection(dir));

        resultList.Add(tiles[Mathf.RoundToInt(pos.x)][Mathf.RoundToInt(pos.y)]);

        int counter = 0;

        while (true)
        {
            counter++;
            if (counter > 150)
                break;

            Vector2 position = FindNextBellInDirection(pos + GetModifiedDirection(dir), GetModifiedDirection(dir));

            if (position.x == -1 || position.y == -1)
            {
                Debug.Log("connections incorrect");
                break;
            }

            bool isVisited = CheckIfVisited(position);

            if (isVisited)
            {
                Debug.Log("loop found");
                break;
            }
            else
            {
                visitedPositions.Add(position, 1);
                tiles[Mathf.RoundToInt(position.x)][Mathf.RoundToInt(position.y)].Visited(GetModifiedDirection(dir));

                resultList.Add(tiles[Mathf.RoundToInt(position.x)][Mathf.RoundToInt(position.y)]);
            }

            if (visitedPositions.Count == totalBellsCount)
            {
                Debug.Log("Game complete");
                isGameComplete = true;
                break;
            }
            Debug.Log(position);

            Bell bell = tiles[Mathf.RoundToInt(position.x)][Mathf.RoundToInt(position.y)].GetBell();

            dir = bell.GetFacingDirection();
            pos = position;
        }

        Debug.Log("visited count" + resultList.Count);
        animateGame = true;
    }

    bool CheckIfVisited(Vector2 position)
    {
        return visitedPositions.ContainsKey(position);
    }


    Vector2 FindNextBellInDirection(Vector2 position, Vector2 direction)
    {
        if (position.x < 0 || position.y < 0 || position.x > 3 || position.y > 3)
        {
            return new Vector2(-1, -1);
        }

        Tile tile = tiles[Mathf.RoundToInt(position.x)][Mathf.RoundToInt(position.y)];

        if (tile.tileType == TileType.BELL)
        {
            if (Vector2.Dot( GetModifiedDirection( tile.GetBell().GetFacingDirection()), direction) != -1)
                return position;
            else
                return new Vector2(-1, -1);
        }
        else
        {
            resultList.Add(tiles[Mathf.RoundToInt(position.x)][Mathf.RoundToInt(position.y)]);
            position += direction;
            return FindNextBellInDirection(position, direction);
        }
    }

    Vector2 GetModifiedDirection(Vector2 dir)
    {
        if (dir == Vector2.up)
        {
            return Vector2.left;
        }
        else if (dir == Vector2.down)
        {
            return Vector2.right;
        }
        else if (dir == Vector2.left)
        {
            return Vector2.down;
        }
        else if (dir == Vector2.right)
        {
            return Vector2.up;
        }
        return Vector2.zero;
    }

    private void Update()
    {
        if (!animateGame)
            return;
        
        elapsedTime += Time.deltaTime;

        if(elapsedTime - previousTime >= 1)
        {
            PerformNextMove();
            previousTime = elapsedTime;
        }

    }

    void PerformNextMove()
    {
        resultList[currentMoveIndex].PerformMove();
        
        if(currentMoveIndex < resultList.Count - 1)
            currentMoveIndex += 1;
        else
        {
            if (isGameComplete)
            {

                gameManager.GameOverSuccessWindow();
                audioManager.PlayGameOverSound();
            }
            else
            {
                audioManager.PlayGameFailedSound();
                gameManager.GameOverFailureWindow();
            }
            Debug.Log("Game over");
            animateGame = false;
        }
    }
}
