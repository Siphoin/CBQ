using ExitGames.Client.Photon;
using Match;
using UnityEngine;

namespace Client.SO
{
    [CreateAssetMenu(menuName = "SO/Match/Player Match Settings", order = 0)]
    public class PlayerMatchSettings : ScriptableObject
    {
        #region Fields
        [Header("Кол-во денег на старте первого раунда")]
        [SerializeField] long startMoney = 0;
        #endregion

        #region Methods
        public Hashtable CreateNewHashtablePlayer(ComandType comand)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("team", (int)comand);
            hashtable.Add("money", startMoney);
            hashtable.Add("kills", 0);
            hashtable.Add("deaths", 0);
            return hashtable;
        }
        #endregion
    }
}