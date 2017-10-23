using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Representation of a player in a pinball game.
    /// </summary>
    public class Player
    {
        /*
Total play time
Play time for each house

Total Games
Games for each house

Highest score
Highest Score for each house

Average Score
Average score for each house

Feature A completed no.
Feature B completed no.
...
*/



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
