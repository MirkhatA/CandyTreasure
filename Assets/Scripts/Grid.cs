using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public enum PieceType
    {
        EMPTY,
        NORMAL,
        BUBBLE,
        COUNT,
    };

    [System.Serializable]
    public struct PiecePrefab
    {
        public PieceType type;
        public GameObject prefab;
    };

    public int x;
    public int y;
    public float timeOfFilling;
    public PiecePrefab[] piecePrefabs;
    public GameObject bgPref;

    private bool _canSwipe;
    private bool _isInversed = false;
    private GamePiece[,] _pieceList;
    private GamePiece _piecePress;
    private GamePiece _pieceEnter;
    private LevelManager _lvlMan;
    private MoneyManager _mnyMan;
    private Dictionary<PieceType, GameObject> piecePref;

    private void Start()
    {
        piecePref = new Dictionary<PieceType, GameObject>();
        _lvlMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _mnyMan = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();

        for (int i = 0; i < piecePrefabs.Length; i++) {
            if (!piecePref.ContainsKey(piecePrefabs[i].type)) {
                piecePref.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }

        _pieceList = new GamePiece[x, y];
        for (int x = 0; x < this.x; x++) {
            for (int y = 0; y < this.y; y++)
            {
                SpawnNewPieces(x, y, PieceType.EMPTY);
            }
        }

        StartCoroutine(FillPieces());
    }

    public IEnumerator FillPieces() {
        var needsToBeReffiled = true;
        _canSwipe = false;

        while (needsToBeReffiled) {
            yield return new WaitForSeconds(timeOfFilling);

            while (FillStep()) {
                _isInversed = !_isInversed;
                yield return new WaitForSeconds(timeOfFilling);
            }

            needsToBeReffiled = ClearAllValidMatches();
        }
        _canSwipe = true;

    }

    public bool FillStep()
    {
        var pieceIsMoved = false;

        for (int y = this.y - 2; y >= 0; y--) {
            for (int xLoop = 0; xLoop < x; xLoop++) {
                var xItem = xLoop;
                if (_isInversed) xItem = this.x - 1 - xLoop;
                var piece = _pieceList[xItem, y];
                if (piece.IsMovable()) {
                    var belowP = _pieceList[xItem, y + 1];
                    if (belowP.Type == PieceType.EMPTY) {
                        piece.MovableComponent.MovePiece(xItem, y + 1, timeOfFilling);
                        _pieceList[xItem, y + 1] = piece;
                        SpawnNewPieces(xItem, y, PieceType.EMPTY);
                        pieceIsMoved = true;
                    }
                    else {
                        for (int diag = -1; diag <= 1; diag++) {
                            if (diag != 0) {
                                int diagX = xItem + diag;
                                if (_isInversed) diagX = xItem - diag;
                                if (diagX >= 0 && diagX < this.x) {
                                    var pieceForDiag = _pieceList[diagX, y + 1];
                                    if (pieceForDiag.Type == PieceType.EMPTY) {
                                        var isTherePieceAbove = true;
                                        for (int aboveY = y; aboveY >= 0; aboveY--) {
                                            var aboveP = _pieceList[diagX, aboveY];
                                            if (aboveP.IsMovable()) break;
                                            else if (!aboveP.IsMovable() && aboveP.Type != PieceType.EMPTY) {
                                                isTherePieceAbove = false;
                                                break;
                                            }
                                        }
                                        if (!isTherePieceAbove) {
                                            Destroy(pieceForDiag.gameObject);
                                            piece.MovableComponent.MovePiece(diagX, y + 1, timeOfFilling);
                                            _pieceList[diagX, y + 1] = piece;
                                            SpawnNewPieces(xItem, y, PieceType.EMPTY);
                                            pieceIsMoved = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        for (int x = 0; x < this.x; x++) {
            var pBelow = _pieceList[x, 0];

            if (pBelow.Type == PieceType.EMPTY) {
                var nPiece = (GameObject)Instantiate(piecePref[PieceType.NORMAL], GetWorldPosition(x, -1), Quaternion.identity);
                nPiece.transform.parent = transform;

                _pieceList[x, 0] = nPiece.GetComponent<GamePiece>();
                _pieceList[x, 0].Init(x, -1, this, PieceType.NORMAL);
                _pieceList[x, 0].MovableComponent.MovePiece(x, 0, timeOfFilling);
                _pieceList[x, 0].ColorComponent.SetColor((ColorPiece.TypeOfColor)Random.Range(0, _pieceList[x, 0].ColorComponent.NumColors));
                pieceIsMoved = true;
            }
        }

        return pieceIsMoved;
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(transform.position.x - this.x / 2.0f + x, transform.position.y + this.y / 2.0f - y);
    }

    public GamePiece SpawnNewPieces(int x, int y, PieceType type)
    {
        var nPiece = (GameObject)Instantiate(piecePref[type], GetWorldPosition(x, y), Quaternion.identity);
        nPiece.transform.parent = transform;
        _pieceList[x, y] = nPiece.GetComponent<GamePiece>();
        _pieceList[x, y].Init(x, y, this, type);
        return _pieceList[x, y];
    }

    public bool CanBeAdjaced(GamePiece pieceOne, GamePiece pieceTwo) {
        return (pieceOne.X == pieceTwo.X && (int)Mathf.Abs(pieceOne.Y - pieceTwo.Y) == 1) || (pieceOne.Y == pieceTwo.Y && (int)Mathf.Abs(pieceOne.X - pieceTwo.X) == 1);
    }

    public void SwapTwoPieces(GamePiece pieceOne, GamePiece pieceTwo) {
        if (pieceOne.IsMovable() && pieceTwo.IsMovable() && _canSwipe) {
            _pieceList[pieceOne.X, pieceOne.Y] = pieceTwo;
            _pieceList[pieceTwo.X, pieceTwo.Y] = pieceOne;

            if (IsMatched(pieceOne, pieceTwo.X, pieceTwo.Y) != null || IsMatched(pieceTwo, pieceOne.X, pieceOne.Y) != null) {
                var pieceOneX = pieceOne.X;
                var pieceOneY = pieceOne.Y;

                pieceOne.MovableComponent.MovePiece(pieceTwo.X, pieceTwo.Y, timeOfFilling);
                pieceTwo.MovableComponent.MovePiece(pieceOneX, pieceOneY, timeOfFilling);

                ClearAllValidMatches();
                StartCoroutine(FillPieces());
            } else {
                _pieceList[pieceOne.X, pieceOne.Y] = pieceOne;
                _pieceList[pieceTwo.X, pieceTwo.Y] = pieceTwo;
            }
        }
    }

    public void PressP(GamePiece piece) {
        _piecePress = piece;
    }

    public void EnterP(GamePiece piece) {
        _pieceEnter = piece;
    }

    public void RelP()
    {
        if (CanBeAdjaced(_piecePress, _pieceEnter)) {
            SwapTwoPieces(_piecePress, _pieceEnter);
        }
    }

    public List<GamePiece> IsMatched(GamePiece mainPiece, int pieceX, int pieceY)
    {
        if (mainPiece.IsColored()) {
            var color = mainPiece.ColorComponent.Color;
            var horizontalPieces = new List<GamePiece>();
            var verticalPieces = new List<GamePiece>();
            var matchingPieces = new List<GamePiece>();
            horizontalPieces.Add(mainPiece);

            for (int dirItem = 0; dirItem <= 1; dirItem++) {
                for (int xOffset = 1; xOffset < x; xOffset++) {
                    int xItem;
                    if (dirItem == 0) xItem = pieceX - xOffset;
                    else xItem = pieceX + xOffset;
                    if (xItem < 0 || xItem >= this.x) break;
                    if (_pieceList[xItem, pieceY].IsColored() && _pieceList[xItem, pieceY].ColorComponent.Color == color) horizontalPieces.Add(_pieceList[xItem, pieceY]);
                    else break;
                }
            }
            if (horizontalPieces.Count >= 3) for (int i = 0; i < horizontalPieces.Count; i++) matchingPieces.Add(horizontalPieces[i]);
            if (matchingPieces.Count >= 3) return matchingPieces;
            horizontalPieces.Clear();
            verticalPieces.Clear();
            verticalPieces.Add(mainPiece);
            for (int dirItem = 0; dirItem <= 1; dirItem++) {
                for (int yOffset = 1; yOffset < y; yOffset++) {
                    int yItem;
                    if (dirItem == 0) yItem = pieceY - yOffset;
                    else yItem = pieceY + yOffset;
                    if (yItem < 0 || yItem >= this.y) break;
                    if (_pieceList[pieceX, yItem].IsColored() && _pieceList[pieceX, yItem].ColorComponent.Color == color) verticalPieces.Add(_pieceList[pieceX, yItem]);
                    else break;
                }
            }
            if (verticalPieces.Count >= 3) for (int i = 0; i < verticalPieces.Count; i++) matchingPieces.Add(verticalPieces[i]);
            if (matchingPieces.Count >= 3) return matchingPieces;
        }
        return null;
    }

    public bool ClearAllValidMatches()
    {
        var needToBeReffiled = false;

        for (int y = 0; y < this.y; y++) {
            for (int x = 0; x < this.x; x++) {
                if (_pieceList[x, y].IsClearable()) {
                    var match = IsMatched(_pieceList[x, y], x, y);

                    if (match != null) {
                        for (int i = 0; i < match.Count; i++) {
                            if (ClearPiece(match[i].X, match[i].Y)) {
                                needToBeReffiled = true;
                                _lvlMan.IncreasePoints();
                                _mnyMan.IncreaseMoney();
                            }
                        }
                    }
                }
            }
        }

        return needToBeReffiled;
    }

    public bool ClearPiece(int x, int y) {
        if (_pieceList[x, y].IsClearable() && !_pieceList[x, y].ClearableComponent.IsBeingCleared) {
            _pieceList[x, y].ClearableComponent.Clear();
            SpawnNewPieces(x, y, PieceType.EMPTY);

            return true;
        }

        return false;
    }
}
