using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KarliCards_Gui
{
    // This attribute makes it possible to convert this class into a stream format,
    // like XML.
    [Serializable]
    // Implement an interface that allows us to inform WPF when a property changes.
    public class GameOptions : INotifyPropertyChanged
    {
        private bool _playAgainstComputer = true;
        private int _numberOfPlayers = 2;
        private ComputerSkillLevel _computerSkill = ComputerSkillLevel.Dumb;
        private ObservableCollection<string> _playerNames = new ObservableCollection<string>();

        public List<string> SelectPlayers { get; set; }

        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set
            {
                _numberOfPlayers = value;
                
                // The setter must actively call the PropertyChanged event in order to
                // notify the subscribers of the change.
                OnPropertyChanged(nameof(NumberOfPlayers));
            }
        }

        public bool PlayAgainstComputer
        {
            get { return _playAgainstComputer; }
            set
            {
                _playAgainstComputer = value;
                OnPropertyChanged(nameof(PlayAgainstComputer));
            }
        }

        public ComputerSkillLevel ComputerSkill
        {
            get { return _computerSkill; }
            set
            {
                _computerSkill = value;
                OnPropertyChanged(nameof(ComputerSkill));
            }
        }

        public ObservableCollection<string> PlayerNames
        {
            get { return _playerNames; }
            set
            {
                _playerNames = value;
                OnPropertyChanged("PlayerNames");
            }
        }

        public GameOptions()
        {
            SelectPlayers = new List<string>();
        }

        public void AddPlayer(string playerName)
        {
            // Check for duplicates.
            if (_playerNames.Contains(playerName))
                return;

            _playerNames.Add(playerName);
            OnPropertyChanged("PlayerNames");
        }

        // We need to implement an event handler to send a notification when a property
        // changes.
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [Serializable]
    public enum ComputerSkillLevel
    {
        Dumb,
        Good,
        Cheats
    }
}
