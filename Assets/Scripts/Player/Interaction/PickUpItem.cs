﻿using System;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer pickUpTips;

    private Item CurrentItem { get; set; }
    private bool IsTouchingAnyItem { get; set; }
    private PlayerInputHandler PlayerInputHandler => _player.InputHandler;

    private PlayerController _player;
    private InventoryManager _inventoryManager;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }
    
    private void Start()
    {
        _inventoryManager = _player.InventoryManager;
        
        _player.Core.Movement.Flipped += OnPlayerFlipped;
    }

    private void OnDestroy()
    {
        _player.Core.Movement.Flipped -= OnPlayerFlipped;
    }

    private void Update()
    {
        if (PlayerInputHandler.InteractInput && IsTouchingAnyItem)
        {
            _inventoryManager.PickUpItem(CurrentItem);
        }
    }

    private void OnPlayerFlipped()
    {
        pickUpTips.flipX = !pickUpTips.flipX;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            CurrentItem = other.GetComponent<Item>();
            IsTouchingAnyItem = true;
            pickUpTips.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            CurrentItem = null;
            IsTouchingAnyItem = false;
            pickUpTips.gameObject.SetActive(false);
        }
    }
}