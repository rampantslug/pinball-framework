using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common
{
    /// <summary>
    /// Representation of a player in a pinball game.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// This player's Id
        /// </summary>
        public ushort Id { get; set; }

        /// <summary>
        /// This player's score
        /// </summary>
        public long Score { get; set; }

        /// <summary>
        /// This players name (optional)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of extra balls this player has accumulated
        /// </summary>
        public int ExtraBalls { get; set; }

        /// <summary>
        /// The number of seconds that this player has had the ball in play.
        /// </summary>
        public double GameTime { get; set; }


        public Player(string name)
        {
            this.Name = name;
        }
    }
}
