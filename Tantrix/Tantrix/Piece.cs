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
        enum AdjacentPiece
        {
            Below,
            RightBottom,
            RightTop,
            Above,
            LeftTop,
            LeftBottom
        }

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

        public Piece(Vector2 location)
        {
            this.location = location;

            updateRectangle();
        }

        Piece adjacentPieces(AdjacentPiece piece)
        {
            switch (piece)
            {
                case AdjacentPiece.Above: return above;
                case AdjacentPiece.Below: return below;
                case AdjacentPiece.LeftTop: return lefttop;
                case AdjacentPiece.LeftBottom: return leftbottom;
                case AdjacentPiece.RightTop: return righttop;
                case AdjacentPiece.RightBottom: return rightbottom;
                default: return null;
            }
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
            Piece temp = below;
            below = board.getPieceOnScreen(location + BelowOffSet);
            if (temp == null && below != null) { below.CheckAdjacentPieces(board); }
        }
        private void checkAbove(Board board)
        {
            Piece temp = above;
            above = board.getPieceOnScreen(location + AboveOffSet);
            if (temp == null && above != null) { above.CheckAdjacentPieces(board); }
        }
        private void checkLeftTop(Board board)
        {
            Piece temp = lefttop;
            lefttop = board.getPieceOnScreen(location + LeftTopOffSet);
            if (temp == null && lefttop != null) { lefttop.CheckAdjacentPieces(board); }
        }
        private void checkLeftBottom(Board board)
        {
            Piece temp = leftbottom;
            leftbottom = board.getPieceOnScreen(location + LeftBottomOffSet);
            if (temp == null && leftbottom != null) { leftbottom.CheckAdjacentPieces(board); }
        }
        private void checkRightBottom(Board board)
        {
            Piece temp = rightbottom;
            rightbottom = board.getPieceOnScreen(location + RightBottomOffSet);
            if (temp == null && rightbottom != null) { rightbottom.CheckAdjacentPieces(board); }
        }
        private void checkRightTop(Board board)
        {
            Piece temp = righttop;
            righttop = board.getPieceOnScreen(location + RightTopOffSet);
            if (temp == null && righttop != null) { righttop.CheckAdjacentPieces(board); }
        }

        public void CheckAdjacentPieces(Board board)
        {
            checkAbove(board);
            checkLeftTop(board);
            checkBelow(board);
            checkLeftBottom(board);
            checkRightTop(board);
            checkRightBottom(board);
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
                    if (adjacentPieces((AdjacentPiece)i) == null) { continue; }
                    Tile otherTile = adjacentPieces((AdjacentPiece)i).tile;
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
