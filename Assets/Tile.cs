using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TileType
{
    EMPTY,
    BELL
}

public enum TileDirections
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public GameObject bellPrefab;

    public TileType tileType;

    Bell bell;

    public TileDirections initialDirection = TileDirections.DOWN;

    public bool isVisited = false;

    public Color visitedColor;

    SpriteRenderer sprite;

    public SpriteRenderer verticalInd;
    public SpriteRenderer horizontalInd;

    GameManager gameManager;

    private void Awake()
    {
        DrawBell();

        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }
    public void Visited(Vector2 direction)
    {
        isVisited = true;
    }

    public void VisitedWithoutBlock(Vector2 direction)
    {
        switch (GetDirectionInEnum(direction))
        {
            case TileDirections.UP:
            case TileDirections.DOWN: verticalInd.gameObject.SetActive(true);
                break;
            case TileDirections.LEFT:
            case TileDirections.RIGHT: horizontalInd.gameObject.SetActive(true);
                break;
        }
    }

    public Bell GetBell()
    {
        return bell;
    }

    public void DrawBell()
    {
        if (tileType == TileType.BELL)
        {
            bell = Instantiate(bellPrefab, transform).GetComponent<Bell>();
            bell.InitializeBell(GetAngle(initialDirection));
        }
    }

    float GetAngle(TileDirections direction)
    {
        if (direction == TileDirections.UP) { return 180; }
        else if (direction == TileDirections.DOWN) { return 0; }
        else if (direction == TileDirections.LEFT) { return 270; }
        else { return 90; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameManager.isGameBlocked())
            return;

        if(bell)
            bell.RotateBell();
    }

    public TileDirections GetDirectionInEnum(Vector2 direction)
    {
        if (direction == Vector2.up) { return TileDirections.UP; }
        else if (direction == Vector2.down) { return TileDirections.DOWN; }
        else if (direction == Vector2.left) { return TileDirections.LEFT; }
        else { return TileDirections.RIGHT; }
    }

    public void PerformMove()
    {
        if (tileType == TileType.BELL)
        {
            sprite.color = visitedColor;
            bell.PerformMove();
        }
        else
        {
            sprite.color = visitedColor;
            Debug.Log("Empty tile move");
        }
    }
}
