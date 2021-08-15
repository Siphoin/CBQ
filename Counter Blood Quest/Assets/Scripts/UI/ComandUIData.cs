using System;

namespace UI
{
    [Serializable]
  public  class ComandUIData
    {
        public int countBots = 0;
        public int countPlayers = 0;

        public ComandUIData ()
        {

        }

        public ComandUIData (int countBots, int countPlayers)
        {
            this.countBots = countBots;
            this.countPlayers = countPlayers;
        }

    }
}
