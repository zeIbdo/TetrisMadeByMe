# Tetris

A desktop Tetris game developed from scratch using **C# and WPF**, with an object-oriented design separating game state, grid management, block behavior, and individual block types.

## Technologies

* **C#**
* **WPF**
* **XAML**
* **Object-Oriented Programming**

## Features

* Seven standard Tetris block types
* Block movement and rotation
* Clockwise and counter-clockwise rotation
* Collision detection
* Automatic block placement
* Row clearing
* Score tracking
* Game-over detection
* Next-block queue

## Architecture

The game logic is separated into dedicated classes responsible for different parts of the game:

* **GameState** — Controls the main game state and player actions
* **GameGrid** — Handles the grid, occupied cells, boundaries, and row clearing
* **Block** — Provides common functionality for all block types
* **BlockQueue** — Manages upcoming blocks
* **Position** — Represents block positions within the grid

Each individual block type inherits from the common `Block` base class.

## Core Mechanics

The game implements movement and rotation with collision checking. Invalid movements or rotations are reverted when a block would exceed the grid boundaries or overlap occupied cells.

Completed rows are detected, removed, and replaced by shifting the remaining rows downward. The player's score is updated based on cleared rows.

## Repository

GitHub: https://github.com/zeIbdo/TetrisMadeByMe
