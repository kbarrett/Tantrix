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
    class TileBag
    {
        List<Tile> bag = new List<Tile>();

        List<Tile> placedpieces = new List<Tile>();

        public TileBag()
        {
            bag.Add(new Tile(new int[] { 1, Tile.NOMATCH, 3, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Blue, Color.Yellow }));
            bag.Add(new Tile(new int[] { 1, Tile.NOMATCH, 3, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Blue, Color.Red, Color.Yellow }));
            bag.Add(new Tile(new int[] { 1, Tile.NOMATCH, 4, 5, Tile.NOMATCH, Tile.NOMATCH }, new Color[] { Color.Red, Color.Yellow, Color.Blue }));

            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Yellow, Color.Blue }));
            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Red, Color.Blue }));
            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Red, Color.Blue }));
            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Red, Color.Blue }));
            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Red, Color.Blue }));
        }

        int cur = 0;
        public Tile getRandomTile()
        {
            if (cur > bag.Count<Tile>()) { cur = 0; }
            return bag.ElementAt<Tile>(cur++);
        }

        public List<Tile> GetBag()
        {
            return bag;
        }

        public bool placeTile(Tile tile)
        {
            if (tile != null && !placedpieces.Contains(tile) && bag.Contains(tile))
            {
                placedpieces.Add(tile);
                bag.Remove(tile);
                return true;
            }
            return false;
        }

        /*public void Draw(SpriteBatch spriteBatch)
        {
            /*foreach (Tile tile in bag)
            {
                tile.Draw(spriteBatch, new Vector2 (150* bag.IndexOf(tile),150*bag.IndexOf(tile)));
            }

            foreach(Tile tile in placedpieces)
            {
                tile.Draw(spriteBatch);
            }
        }*/
    }
}
