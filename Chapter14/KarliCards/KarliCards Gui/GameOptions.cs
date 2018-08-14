using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Xml.Serialization;
using Ch13CardLib;

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
        private bool _computerPlaysWithOpenHand;
        private ObservableCollection<string> _playerNames = new ObservableCollection<string>();

        public List<string> SelectedPlayers { get; set; }

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

        public bool ComputerPlaysWithOpenHand
        {
            get { return _computerPlaysWithOpenHand; }
            set
            {
                _computerPlaysWithOpenHand = value;
                OnPropertyChanged(nameof(ComputerPlaysWithOpenHand));
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
            SelectedPlayers = new List<string>();
        }

        public void AddPlayer(string playerName)
        {
            // Check for duplicates.
            if (_playerNames.Contains(playerName))
                return;

            _playerNames.Add(playerName);
            OnPropertyChanged("PlayerNames");
        }

        /// <summary>
        /// Saves the current settings of this class instance to a file.
        /// </summary>
        public void Save()
        {
            using (var stream = File.Open("GameOptions.xml", FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(GameOptions));
                serializer.Serialize(stream, this);
            }
        }

        /// <summary>
        /// A static method to allow the class to create an instance of itself.
        /// </summary>
        /// <returns>A GameOptions instance containing any available existing settings.</returns>
        public static GameOptions Create()
        {
            if (File.Exists("GameOptions.xml"))
            {
                // Read existing settings from the file.
                using (var stream = File.OpenRead("GameOptions.xml"))
                {
                    var serializer = new XmlSerializer(typeof(GameOptions));
                    return serializer.Deserialize(stream) as GameOptions;
                }
            }
            else
                // There are no saved settings, so create an instance using the default settings.
                return new GameOptions();
        }

        // We need to implement an event handler to send a notification when a property
        // changes.
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Set up a routed command to link to the GameClient UI. Set a shortcut key for the options item.
        public static RoutedCommand OptionsCommand = new RoutedCommand("Show Options", typeof(GameOptions),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.O, ModifierKeys.Control) }));
    }
}
