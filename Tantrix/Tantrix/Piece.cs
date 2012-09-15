using System;
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
    class Piece
    {
        Vector2 location;

        Rectangle where;

        Tile tile {get; set;}

        Piece above;
        Piece lefttop;
        Piece leftbottom;
        Piece righttop;
        Piece rightbottom;
        Piece below;

        public static Vector2 LeftTopOffSet = new Vector2(-75, -50);
        public static Vector2 LeftBottomOffSet = new Vector2(-75, 50);
        public static Vector2 RightTopOffSet = new Vector2(75, -50);
        public static Vector2 RightBottomOffSet = new Vector2(75, 50);
        public static Vector2 AboveOffSet = new Vector2(0, -100);
        public static Vector2 BelowOffSet = new Vector2(0, 100);

        Piece[] adjacentPieces = new Piece[6];

        public Piece(Vector2 location)
        {
            this.location = location;

            adjacentPieces[0] = getBelow();
            adjacentPieces[1] = getLeftTop();
            adjacentPieces[2] = getLeftBottom();
            adjacentPieces[3] = getAbove();
            adjacentPieces[4] = getRightBottom();
            adjacentPieces[5] = getRightTop();

            updateRectangle();
        }

        void updateRectangle()
        {
            where = new Rectangle((int)(location.X - (Game1.height / 2)), (int)(location.Y - (Game1.height / 2)), (int)Game1.height, (int)Game1.height);
        }

        public Piece getAbove() { return above; }
        public Piece getLeftTop() { return lefttop; }
        public Piece getLeftBottom() { return leftbottom; }
        public Piece getRightTop() { return righttop; }
        public Piece getRightBottom() { return rightbottom; }
        public Piece getBelow() { return below; }
        public Vector2 getLocation() { return location; }


        private void checkBelow(Board board)
        {
            below = board.getPieceOnScreen(location + BelowOffSet);
        }
        private void checkAbove(Board board)
        {
            above = board.getPieceOnScreen(location + AboveOffSet);
        }
        private void checkLeftTop(Board board)
        {
            lefttop = board.getPieceOnScreen(location + LeftTopOffSet);
        }
        private void checkLeftBottom(Board board)
        {
            leftbottom = board.getPieceOnScreen(location + LeftBottomOffSet);
        }
        private void checkRightBottom(Board board)
        {
            rightbottom = board.getPieceOnScreen(location + RightBottomOffSet);
        }
        private void checkRightTop(Board board)
        {
            righttop = board.getPieceOnScreen(location + RightTopOffSet);
        }

        public void CheckAdjacentPieces(Board board)
        {
            checkAbove(board);
            checkLeftTop(board);
            checkBelow(board);
        }

        public bool Collides(float x, float y)
        {
            return where.Contains((int) x, (int) y);
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 offSet)
        {
            location = location + offSet;
            updateRectangle();

            if (tile != null)
            {
                tile.Draw(spriteBatch, location);
            }
            else
            { 
                spriteBatch.Draw(Game1.tilebackground, location, null, Color.Red, 0, Game1.centre, 1.0f, SpriteEffects.None, 0.0f);
            }
        }

        public void PlaceTile(Tile tile)
        {
            this.tile = tile;
            tile.PlaceTile(location);
        }

        public bool IsCompatible(Tile tile, float x, float y)
        {
            if (this.tile == null && where.Contains(new Point((int)x, (int)y)))
            {
                Color[] colours = tile.getColours();
                for(int i = 0; i < 6; i++)
                {
                    Color thisColour = colours[i];
                    if (adjacentPieces[i] == null) { continue; }
                    Tile otherTile = adjacentPieces[i].tile;
                    if (otherTile == null) { continue; }
                    Color thatColour = otherTile.getColours()[(i+3)%6];
                    if (thisColour == null || thatColour == null) { continue; }
                    if (thisColour != thatColour) { return false; }
                }
                return true;
            }
            return false;
        }
    }
}
