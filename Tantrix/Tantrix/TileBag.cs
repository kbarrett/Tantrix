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

        List<List<Tile>> playersHands = new List<List<Tile>>();
        int piecesInHand = 5;

        public TileBag(int numberOfPlayers)
        {
            bag.Add(new Tile(new int[] { 1, Tile.NOMATCH, 3, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Blue, Color.Yellow }));
            bag.Add(new Tile(new int[] { 1, Tile.NOMATCH, 3, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Blue, Color.Red, Color.Yellow }));
            bag.Add(new Tile(new int[] { 1, Tile.NOMATCH, 4, 5, Tile.NOMATCH, Tile.NOMATCH }, new Color[] { Color.Red, Color.Yellow, Color.Blue }));

            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Yellow, Color.Blue }));
            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Red, Color.Blue }));
            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Red, Color.Blue }));
            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Red, Color.Blue }));
            bag.Add(new Tile(new int[] { 2, 3, Tile.NOMATCH, Tile.NOMATCH, 5, Tile.NOMATCH }, new Color[] { Color.Red, Color.Red, Color.Blue }));

            for(int i = 0; i < numberOfPlayers; ++i)
            {
                createPlayersHand();
            }
        }

        void createPlayersHand()
        {
            List<Tile> hand = new List<Tile>(piecesInHand);
            for (int i = 0; i < piecesInHand; ++i) { hand.Add(getRandomTile()); }
            playersHands.Add(hand);
        }

        Tile getRandomTile()
        {
            if (bag.Count == 0) { return null; }
            Random random = new Random();
            int which = random.Next(bag.Count);
            Tile whichTile = bag.ElementAt<Tile>(which);
            bag.Remove(whichTile);
            return whichTile;
        }

        public List<Tile> GetPlayersBag(int player)
        {
            return playersHands.ElementAt<List<Tile>>(player);
        }

        public List<Tile> GetBag()
        {
            return bag;
        }

        public bool placeTile(Tile tile, int currentPlayer)
        {
            List<Tile> playerHand = playersHands.ElementAt<List<Tile>>(currentPlayer);
            if (tile != null && !placedpieces.Contains(tile) && playerHand.Contains(tile))
            {
                placedpieces.Add(tile);
                playerHand.Remove(tile);
                Tile nextTile = getRandomTile();
                if (nextTile != null) { playerHand.Add(nextTile); }
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
