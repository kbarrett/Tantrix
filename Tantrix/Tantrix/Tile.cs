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
    class Tile
    {
        enum LineType
        {
            Curly,
            Swoopy,
            Straight,
            SwoopyRef,
            CurlyRef,
            None
        }

        public static int NOMATCH = -1;

        int[] ends;
        Color[] colours;
        Vector2 location;
        LineType[] types;

        bool placed = false;

        Rectangle where;
        /*float lhs = 0;
        float rhs = 0;
        float top = 0;
        float bottom = 0;*/

        public Tile(int[] ends, Color[] whichcolours)
        {
            if (ends.Length == 6 && whichcolours.Length == 3)
            {
                this.ends = ends;
                this.colours = new Color[6];
                this.types = new LineType[6];
                int current = 0;
                for (int start = 0; start < 6; ++start)
                {    
                    int end = ends[start];

                    if (end != NOMATCH)
                    {
                        LineType linetype = LineType.None;
                        switch(Math.Abs(start - end))
                        {
                            case 1 :
                            {
                                linetype = LineType.Curly;
                                break;
                            }
                            case 2:
                            {
                                linetype = LineType.Swoopy;
                                break;
                            }
                            case 3:
                            {
                                linetype = LineType.Straight;
                                break;
                            }
                            case 4:
                            {
                                linetype = LineType.SwoopyRef;
                                break;
                            }
                            case 5:
                            {
                                linetype = LineType.CurlyRef;
                                break;
                            }
                        }
                        types[start] = linetype;
                        types[end] = linetype;

                        colours[start] = whichcolours[current];
                        colours[end] = whichcolours[current];
                        ++current;
                    }
                    else if (end > 3)
                    {
                        types[start] = LineType.None;
                        types[end] = LineType.None;
                    }
                }
            }
        }

        public void PlaceTile(Vector2 location)
        {
            SetLocation(location);
            this.placed = true;
        }

        public void SetLocation(Vector2 location)
        {
            this.location = location;
            where = new Rectangle((int)(location.X - (Game1.width / 2)), (int)(location.Y - (Game1.height / 2)), (int)Game1.width, (int)Game1.height);
            /*lhs = location.X - Game1.width / 2;
            rhs = location.X + Game1.width / 2;
            top = location.Y - Game1.height / 2;
            bottom = location.Y + Game1.height / 2;*/
        }

        public bool Collides(float x, float y)
        {
            /*if (placed)
            {*/
                //return (lhs < x && x < rhs && top < y && y < bottom);
            /*}
            else
            {
                return false;
            }*/

            return where.Contains(new Point((int)x, (int)y));
        }

        public void Draw(SpriteBatch spriteBatch, bool partOfTileBag = false, bool clickedOnTile = false)
        {
            Draw(spriteBatch, location, true, partOfTileBag, clickedOnTile);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool setLoc = true, bool partOfTileBag = false, bool clickedOnTile = false)
        {
            if (setLoc) { SetLocation(location); }

            float depth = 0.1f;
            if (partOfTileBag) { depth = 0.7f; }
            if (clickedOnTile) { depth = 0.9f; }

            spriteBatch.Draw(Game1.tilebackground, location, null, Color.Black, 0, Game1.centre, 1.0f, SpriteEffects.None, depth);

            for(int start = 0; start < ends.Length; ++start)
            {
                int end = ends[start];
                if (end != NOMATCH)
                {
                    switch(types[start])
                    {
                        case LineType.Curly:
                        {
                            spriteBatch.Draw(Game1.curlypiece, location, null, colours[start], (float)(-1 * start * Math.PI / 3), Game1.centre, 1.0f, SpriteEffects.None, depth + 0.1f);
                            break;
                        }
                        case LineType.CurlyRef:
                        {
                            spriteBatch.Draw(Game1.curlypiece, location, null, colours[start], (float)(start * Math.PI / 3), Game1.centre, 1.0f, SpriteEffects.None, depth + 0.1f);
                            break;
                        }
                        case LineType.Swoopy:
                        {
                            spriteBatch.Draw(Game1.swoopypiece, location, null, colours[start], (float)(-1 * start * Math.PI / 3), Game1.centre, 1.0f, SpriteEffects.None, depth + 0.1f);
                            break;
                        }
                        case LineType.SwoopyRef:
                        {
                            spriteBatch.Draw(Game1.swoopypiece, location, null, colours[start], (float)(start * Math.PI / 3), Game1.centre, 1.0f, SpriteEffects.None, depth + 0.1f);
                            break;
                        }
                        case LineType.Straight:
                        {
                            spriteBatch.Draw(Game1.straightpiece, location, null, colours[start], (float)(start * Math.PI / 3), Game1.centre, 1.0f, SpriteEffects.None, depth + 0.1f);
                            break;
                        }
                    }
                }
            }
        }

        internal Color[] getColours()
        {
            return colours;
        }

        public void Rotate()
        {
            int[] tempends = (int[])ends.Clone();
            LineType[] temptypes = (LineType[])types.Clone();
            Color[] tempcolours = (Color[])colours.Clone();

            for (int i = 1; i < 7; ++i)
            {
                ends[i % 6] = tempends[(i - 1) % 6];
                colours[i % 6] = tempcolours[(i - 1) % 6];
                types[i % 6] = temptypes[(i - 1) % 6];
            }
        }
    }
}
