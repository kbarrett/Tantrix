﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tantrix
{
    class Board
    {
        List<Piece> board;
        
        TileBag tileBag;

        float startOfBoard = 0;
        float oldStartOfBoard = 0;

        float offsetFromTilesAbove = 0;

        Vector2 cameraOffset = Vector2.Zero;

        public Board()
        {
            board = new List<Piece>();
            board.Add(new Piece(new Vector2(50, 50)));

            tileBag = new TileBag();
        }

        public void Draw(SpriteBatch spriteBatch, Tile clickedOnTile = null)
        {
            DrawAvaliableTiles(spriteBatch, clickedOnTile);

            foreach (Piece p in board)
            {
                p.Draw(spriteBatch, cameraOffset + new Vector2 (0, startOfBoard - oldStartOfBoard + offsetFromTilesAbove));
            }

            cameraOffset = Vector2.Zero;
            offsetFromTilesAbove = 0;
            oldStartOfBoard = startOfBoard;
            startOfBoard = 0;
        }

        public void PlaceTile(Tile tile, float x, float y)
        {
            foreach (Piece piece in board)
            {
                if (piece.IsCompatible(tile, x, y) && tileBag.placeTile(tile))
                {
                    piece.PlaceTile(tile);

                    updateBoard(piece);
                    break;
                }
            }
        }

        void updateBoard(Piece piece)
        {
            int temp = 0;
            if (piece.getLeftTop() == null)
            {
                Piece newLT = new Piece(piece.getLocation() - new Vector2(75, -50));
                piece.setLeftTop(newLT);
                board.Add(newLT);
                temp = 50;
            }
            if (piece.getLeftBottom() == null)
            {
                Piece newLB = new Piece(piece.getLocation() - new Vector2(75, 50));
                piece.setLeftBottom(newLB);
                board.Add(newLB);
            }
            if (piece.getRightTop() == null)
            {
                Piece newRT = new Piece(piece.getLocation() - new Vector2(-75, -50));
                piece.setRightTop(newRT);
                board.Add(newRT);
                temp = 50;
            }
            if (piece.getRightBottom() == null)
            {
                Piece newRB = new Piece(piece.getLocation() - new Vector2(-75, 50));
                piece.setRightBottom(newRB);
                board.Add(newRB);
            }
            if (piece.getAbove() == null)
            {
                Piece newAbove = new Piece(piece.getLocation() - new Vector2(0, -100));
                piece.setAbove(newAbove);
                board.Add(newAbove);
                temp = 100;
            }
            if (piece.getBelow() == null)
            {
                Piece newBelow = new Piece(piece.getLocation() - new Vector2(0, 100));
                piece.setBelow(newBelow);
                board.Add(newBelow);
            }

            offsetFromTilesAbove += temp;
        }

        public void DrawAvaliableTiles(SpriteBatch spriteBatch, Tile clickedOnTile = null)
        {
            List<Tile> bag = tileBag.GetBag();
            int yvalue = 0;
            int offset = 0;
            for (int i = 0; i<bag.Count<Tile>(); i++)
            {
                if (((i-offset) * 100) > Game1.screenWidth)
                {
                    yvalue += (int)Game1.height;
                    offset = i;
                }

                Tile tile = bag.ElementAt<Tile>(i);
                if (tile != clickedOnTile)
                {
                    tile.Draw(spriteBatch, new Vector2(50 + 100 * (i - offset), 50 + yvalue));
                }
            }

            if(offset == bag.Count<Tile>())
            {
                spriteBatch.Draw(Game1.tilebagtexture, new Rectangle(0, 0, Game1.screenWidth, yvalue), null, Color.Pink, 0, Vector2.Zero, SpriteEffects.None, 0);
                startOfBoard = yvalue;
            }
            else
            {
                spriteBatch.Draw(Game1.tilebagtexture, new Rectangle(0, 0, Game1.screenWidth, yvalue + (int)Game1.height), null, Color.Pink, 0, Vector2.Zero, SpriteEffects.None, 0);
                startOfBoard = yvalue + Game1.height;
            }
        }

        public Tile getTileOnScreen(float x, float y)
        {
            foreach (Tile tile in tileBag.GetBag())
            {
                if (tile.Collides(x, y))
                {
                    return tile;
                }
            }
            return null;
        }

        public void moveCamera(float curX, float curY, float prevX, float prevY)
        {
            cameraOffset = new Vector2(- prevX + curX, - prevY + curY);
        }
    }
}