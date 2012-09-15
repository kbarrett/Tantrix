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
            where = new Rectangle((int)(location.X /*- (Game1.width / 2)*/), (int)(location.Y /*- (Game1.height / 2)*/), (int)Game1.width, (int)Game1.height);
        }

        public Piece getAbove() { return above; }
        public void setAbove(Piece newAbove)
        { 
            above = newAbove;
            if (getLeftTop() != null && getLeftTop().getRightBottom() != this) { getLeftTop().setRightBottom(this); }
            if (getRightTop() != null && getRightTop().getLeftBottom() != this) { getRightTop().setLeftBottom(this); }
            if (newAbove.getAbove() != this) { newAbove.setBelow(this); }
        }
        public Piece getLeftTop() { return lefttop; }
        public void setLeftTop(Piece newLT)
        {
            lefttop = newLT;
            if (getAbove() != null && getAbove().getBelow() != this) { getAbove().setBelow(this); }
            if (getLeftBottom() != null && getLeftBottom().getRightTop() != this) { getLeftBottom().setRightTop(this); }
            if (newLT.getRightBottom() != this) { newLT.setRightBottom(this); }
        }
        public Piece getLeftBottom() { return leftbottom; }
        public void setLeftBottom(Piece newLB)
        {
            leftbottom = newLB;
            if (getLeftTop() != null && getLeftTop().getRightBottom() != this) { getLeftTop().setRightBottom(this); }
            if (getBelow() != null && getBelow().getAbove()!=this) { getBelow().setAbove(this); }
            if (newLB.getRightTop() != this) { newLB.setRightTop(this); }
        }
        public Piece getRightTop() { return righttop; }
        public void setRightTop(Piece newRT)
        { 
            righttop = newRT;
            if (getAbove() != null && getAbove().getBelow()!=this) { getAbove().setBelow(this); }
            if (getRightBottom() != null && getRightBottom().getLeftTop()!=this) { getRightBottom().setLeftTop(this); }
            if (newRT.getLeftBottom() != this) { newRT.setLeftBottom(this); }
        }
        public Piece getRightBottom() { return rightbottom; }
        public void setRightBottom(Piece newRB)
        { 
            rightbottom = newRB;
            if (getRightTop() != null && getRightTop().getLeftBottom() != this) { getRightTop().setLeftBottom(this); }
            if (getBelow() != null && getBelow().getAbove() != this) { getBelow().setAbove(this); }
            if (newRB.getLeftTop() != this) { newRB.setLeftTop(this); }
        }
        public Piece getBelow() { return below; }
        public void setBelow(Piece newBelow)
        { 
            below = newBelow;
            if (getLeftBottom() != null && getLeftBottom().getRightTop() != this) { getLeftBottom().setRightTop(this); }
            if (getRightBottom() != null && getRightBottom().getLeftTop() != this) { getRightBottom().setLeftTop(this); }
            if (newBelow.getAbove() != this) { newBelow.setAbove(this); }
        }
        public Vector2 getLocation() { return location; }

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
