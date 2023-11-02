using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int size;
    public Transform gameBoard;
    public Transform gamePiecePrefab;

    private int emptyLocation;
    private List<Transform> gamePieces;
    private bool shuffling = false;


    void Start()
    {
        gamePieces = new List<Transform>();
        CreateGamePieces(0.01f);
        StartShuffle();
    }

    void Update()
    {



        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                for (int i = 0; i < gamePieces.Count; i++)
                {
                    if (gamePieces[i] == hit.transform)
                    {
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) { break; }
                    }
                }
            }
        }
    }

    private void CreateGamePieces(float gapThickness)
    {
        float width = 1 / (float)size;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform gamePiece = Instantiate(gamePiecePrefab, gameBoard);
                gamePieces.Add(gamePiece);

                gamePiece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                      +1 - (2 * width * row) - width,
                                                      0);
                gamePiece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                gamePiece.name = $"{(row * size) + col}";

                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    gamePiece.gameObject.SetActive(false);
                }
                else
                {
                    float gap = gapThickness / 2;
                    Mesh mesh = gamePiece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));

                    mesh.uv = uv;
                }
            }
        }
    }
    
    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            (gamePieces[i], gamePieces[i + offset]) = (gamePieces[i + offset], gamePieces[i]);
            (gamePieces[i].localPosition, gamePieces[i + offset].localPosition) = ((gamePieces[i + offset].localPosition, gamePieces[i].localPosition));
            emptyLocation = i;
            CheckCompletion();
            return true;
        }
        return false;
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < gamePieces.Count; i++)
        {
            if (gamePieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }

    private void StartShuffle()
    {
        if (!shuffling)
        {
            shuffling = true;
            StartCoroutine(WaitShuffle(0.5f));
        }
    }

    private IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size)){
            int random = Random.Range(0, size * size);
            if(random == last) { continue; }
            last = emptyLocation;
            if (SwapIfValid(random, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(random, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(random, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(random, +1, size - 1))
            {
                count++;
            }
        }
    }










}
