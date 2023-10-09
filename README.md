# MowIt
"Move It" is an idle puzzle game where players are tasked with finding the optimal path to mow lawns for various houses. Players earn money by mowing lawns, which they can then use to upgrade their lawn mower through the in-game market. As players progress through higher-level houses, mowing becomes increasingly challenging, making it essential to continuously enhance their equipment. The game keeps track of when each patch of grass was last cut, and upon re-entering the game, the grass regrows based on the elapsed time, requiring players to re-mow it. Although only two levels were initially designed for testing purposes in the project, the game's code has been developed to allow for the addition of new house levels at any time.

## Mechanics

The player controls the lawnmower with mobile device inputs. These are bottom to top, top to bottom, right to left, left to right. If the player enters another input before a move is finished, these moves are put in the queue and after the current movement is finished, the lawnmower performs the next move if it can be performed.

## Explenation 
Inputs entered before the current movement ends are stored in a Queue (max 4). After the current movement is finished, it learns from the Queue what the next move is and performs the next move. In this way, the player can perform a quick combo.

## Project Video
https://www.youtube.com/shorts/f7RQxo_zlmE
