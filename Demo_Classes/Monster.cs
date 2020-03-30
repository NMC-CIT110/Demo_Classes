using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Classes
{
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
        private List<(string item, int quantity)> _inventory;


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

        public List<(string item, int quantity)> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Monster()
        {

        }

        public Monster(string name, int age, Attitude mood, bool isActive)
        {
            _name = name;
            _age = age;
            _mood = mood;
            _isAlive = isActive;
        }

        #endregion

        #region METHODS

        public string Greeting()
        {
            return $"Hello, my name is {_name}";
        }

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
                return $"It appears that {} is now dead and unable to respond.";
            }
        }

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
