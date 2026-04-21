using UnityEngine;

public class MinoScript : MonoBehaviour
{
    public Vector2Int position;
    private int rotationIndex = 0;

    [SerializeField] private Transform[] blocks;

    //落下用
    [SerializeField] private float fallInterval = 0.1f;
    private float fallTimer = 0f;

    private Vector2Int[][] shapes =
    {
        new Vector2Int[] { new Vector2Int(0,0), new Vector2Int(-1,0), new Vector2Int(1,0), new Vector2Int(0,1) },
        new Vector2Int[] { new Vector2Int(0,0), new Vector2Int(0,1), new Vector2Int(0,-1), new Vector2Int(1,0) },
        new Vector2Int[] { new Vector2Int(0,0), new Vector2Int(-1,0), new Vector2Int(1,0), new Vector2Int(0,-1) },
        new Vector2Int[] { new Vector2Int(0,0), new Vector2Int(0,1), new Vector2Int(0,-1), new Vector2Int(-1,0) }
    };

    public Vector2Int[] CurrentShape => shapes[rotationIndex];

    void Start()
    {
        Initialize(new Vector2Int(5, 15));
    }

    void Update()
    {
        HandleInput();
        HandleAutoFall();
    }

    void HandleInput()
    {
        // 左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CanMove(Vector2Int.left))
                Move(Vector2Int.left);
        }

        // 右移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CanMove(Vector2Int.right))
                Move(Vector2Int.right);
        }

        // 下移動（手動）
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CanMove(Vector2Int.down))
                Move(Vector2Int.down);
        }

        // 回転（WASD）
        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D))
        {
            if (CanRotate())
                Rotate();
        }
    }

    //自動落下
    void HandleAutoFall()
    {
        fallTimer += Time.deltaTime / 2;

        if (fallTimer >= fallInterval)
        {
            fallTimer = 0f;

            if (CanMove(Vector2Int.down))
            {
                Move(Vector2Int.down);
            }
            else
            {
                Debug.Log("着地！");
                // 将来的にここで固定処理
            }
        }
    }

    public void Initialize(Vector2Int startPos)
    {
        position = startPos;
        rotationIndex = 0;
        UpdateVisual();
    }

    public void Move(Vector2Int direction)
    {
        position += direction;
        UpdateVisual();
    }

    public void Rotate()
    {
        rotationIndex = (rotationIndex + 1) % shapes.Length;
        UpdateVisual();
    }

    bool CanMove(Vector2Int direction)
    {
        return true;
    }

    bool CanRotate()
    {
        return true;
    }

    void UpdateVisual()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (i < CurrentShape.Length)
            {
                Vector2Int pos = CurrentShape[i];
                blocks[i].localPosition = new Vector3(pos.x, pos.y, 0);
            }
        }

        // 親の位置で全体を動かす
        transform.position = new Vector3(position.x, position.y, 0);
    }
}