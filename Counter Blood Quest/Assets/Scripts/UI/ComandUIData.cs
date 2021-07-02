using System;

namespace UI
{
    [Serializable]
  public  class ComandUIData
    {
        #region Fields
        public int countBots = 0;
        public int countPlayers = 0;

        #endregion

        #region Constructors

        public ComandUIData ()
        {

        }

        public ComandUIData (int countBots, int countPlayers)
        {
            this.countBots = countBots;
            this.countPlayers = countPlayers;
        }

        #endregion
    }
}
