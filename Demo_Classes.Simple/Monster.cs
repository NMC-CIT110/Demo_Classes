using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Classes
{
    /// <summary>
    /// Monster class to demonstrate methods, setting and displaying properties
    /// </summary>
    public class Monster
    {
        Random randomNumbers = new Random();

        #region ENUMERABLES

        public enum Attitude
        {
            none,
            happy,
            sad,
            angry,
            nice
        }

        
        #endregion

        #region FIELDS

        private string _name;
        private int _age;
        private Attitude _mood;
        private bool _isAlive;
        private List<(string itemName, int quantity)> _inventory;
        private Dictionary<TreasureType, int> _treasureChest;

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public Attitude Mood
        {
            get { return _mood; }
            set { _mood = value; }
        }

        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        public List<(string itemName, int quantity)> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public Dictionary<TreasureType, int> TreasureChest
        {
            get { return _treasureChest; }
            set { _treasureChest = value; }
        }


        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor
        /// Note: the inventory list and treasure chest dictionary are instantiated 
        /// </summary>
        public Monster()
        {
            //
            // instantiate (create) a list for the Monster object's inventory
            //
            _inventory = new List<(string item, int quantity)>();

            //
            // instantiate (create) a dictionary for the Monster object's treasure chest and set all values to zero
            //
            InitializeTreasureChest();
        }

        /// <summary>
        /// construct with parameters to set Monster object's properties
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="age">age</param>
        /// <param name="mood">mood</param>
        /// <param name="isAlive">is monster alive</param>
        public Monster(string name, int age, Attitude mood, bool isAlive)
        {
            _name = name;
            _age = age;
            _mood = mood;
            _isAlive = isAlive;

            //
            // instantiate (create) a list for the Monster object's inventory
            //
            _inventory = new List<(string item, int quantity)>();

            //
            // instantiate (create) a dictionary for the Monster object's treasure chest and set all values to zero
            //
            InitializeTreasureChest();
        }

        /// <summary>
        /// initialize the dictionary for the treasure chest with treasure types as the key and zero as the quantity value
        /// </summary>
        private void InitializeTreasureChest()
        {
            _treasureChest = new Dictionary<TreasureType, int>();

            _treasureChest.Add(TreasureType.gold, 0);
            _treasureChest.Add(TreasureType.silver, 0);
            _treasureChest.Add(TreasureType.bronze, 0);
            _treasureChest.Add(TreasureType.diamond, 0);
            _treasureChest.Add(TreasureType.ruby, 0);
            _treasureChest.Add(TreasureType.emerald, 0);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// returns a string for the Monster object's greeting
        /// </summary>
        /// <returns>greeting string</returns>
        public string Greeting()
        {
            return $"Hello, my name is {_name}";
        }

        /// <summary>
        /// returns a string comment about how the Monster object is feeling
        /// </summary>
        /// <returns>comment string</returns>
        public string TalkTo()
        {
            if (_isAlive)
            {
                switch (_mood)
                {
                    case Attitude.none:
                        return "I am not sure how I am feeling today.";

                    case Attitude.happy:
                        return "I am so happy to meet you. You seem like a wonderful person to me.";

                    case Attitude.sad:
                        return "I don't think I am having a very nice day. It all seems so sad.";

                    case Attitude.angry:
                        return "I am so angry that I want to eat someone!";

                    case Attitude.nice:
                        return "Is there anything I can do for you?";

                    default:
                        return "Today seems like a nice day. I hope you are doing well.";
                }
            }
            else
            {
                return $"It appears that {_name} is now dead and unable to respond.";
            }
        }
        
        /// <summary>
        /// randomly, changes and returns a new mood
        /// </summary>
        /// <returns>new mood</returns>
        public Attitude NewMood()
        {
            int moodQuotient = randomNumbers.Next(1, 101);

            if (moodQuotient <= 25)
                _mood = Attitude.angry;
            else if (moodQuotient <= 50)
                _mood = Attitude.happy;
            else if (moodQuotient <= 50)
                _mood = Attitude.nice;
            else
                _mood = Attitude.sad;

            return _mood;
        }

        #endregion
    }
}
