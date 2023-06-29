using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Pieces Fields")]
    [SerializeField] private List<PlayerPiece> pieces = new List<PlayerPiece>();
    public List<PlayerPiece> Pieces => pieces;
    
    [SerializeField] private GameObject effectPiece;
    public GameObject EffectPieceDeath => effectPiece;
    
    [SerializeField] private Color deathPieceColor;
    public Color DeathPieceColor => deathPieceColor;
    
    [SerializeField] private Color pieceColor;
    public Color PieceColor => pieceColor;
    
    [SerializeField] private float timeAnimationDeath;
    public float TimeAnimationDeath => timeAnimationDeath;

    [Header("Control")]
    [SerializeField] private float _maxMoveSpeed = 10;
    [SerializeField] private float _smoothTime = 0.3f;

    [Header("Other")] 
    [SerializeField] private GameManager gameManager;

    private PlayerState _state;
    private Rigidbody2D _rigidbody2D;

    private Vector2 _currentVelocity;
    private Vector2 _lastPos;
    private Vector2 _mouseForce;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        var mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (_state == PlayerState.Selected)
        {
            Move(mousePos);
            _mouseForce = (mousePos - _lastPos) / Time.deltaTime;
            _mouseForce = Vector2.ClampMagnitude(_mouseForce, _maxMoveSpeed);
            _lastPos = mousePos;
        }
        
        if (Input.GetMouseButtonDown(0) && _state == PlayerState.Default)
        {
            _state = PlayerState.Selected;
        }
        else if (Input.GetMouseButtonUp(0) && _state == PlayerState.Selected)
        {
            _state = PlayerState.Default;
            _rigidbody2D.AddForce(_mouseForce, ForceMode2D.Impulse);
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void Move(Vector2 mousePos)
    {
        var pos = new Vector2(mousePos.x, mousePos.y);
        var nextPos = Vector2.SmoothDamp(transform.position, pos, ref _currentVelocity,
            _smoothTime, _maxMoveSpeed);
        _rigidbody2D.MovePosition(nextPos);

    }

    public void CheckPieces(PlayerPiece piece)
    {
        pieces.Remove(piece);
        
        switch (piece.Type)
        {
            case PlayerPiece.PieceType.Vital:
                gameManager.GameOver();
                break;
            case PlayerPiece.PieceType.Other:
                if (pieces.Count < 2) gameManager.GameOver();
                break;
        }
    }

    public void StopMovement()
    {
        _state = PlayerState.Stop;
    }

    private enum PlayerState
    {
        Default,
        Selected,
        Stop
    }
}