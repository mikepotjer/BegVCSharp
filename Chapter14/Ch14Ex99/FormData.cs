using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch14Ex99
{
    // This interface is in the System.ComponentModel namespace.
    public class FormData : INotifyPropertyChanged
    {
        private string _textBlockContent;
        private int _minValue;
        private int _maxValue;
        private int _currentValue;

        public string TextBlockContent
        {
            get { return _textBlockContent; }
            set
            {
                _textBlockContent = value;
                OnPropertyChanged(nameof(TextBlockContent));
            }
        }

        public int MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                OnPropertyChanged(nameof(MinValue));
            }
        }

        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                OnPropertyChanged(nameof(MaxValue));
            }
        }

        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                OnPropertyChanged(nameof(CurrentValue));
            }
        }

        public FormData()
        {
            _textBlockContent = "This is the content of the TextBlock control."
                + " We are making it very long to show how the TextBlock can scroll"
                + " to display more data when it is added to a ScrollViewer control."
                + "\n99 bottles of beer on the wall, 99 bottles of beer, take one down,"
                + " pass it around, 98 bottles of beer on the wall."
                + "\n98 bottles of beer on the wall, 98 bottles of beer, take one down,"
                + " pass it around, 97 bottles of beer on the wall."
                + "\n97 bottles of beer on the wall, 97 bottles of beer, take one down,"
                + " pass it around, 96 bottles of beer on the wall."
                + "\n96 bottles of beer on the wall, 96 bottles of beer, take one down,"
                + " pass it around, 95 bottles of beer on the wall."
                + "\n95 bottles of beer on the wall, 95 bottles of beer, take one down,"
                + " pass it around, 94 bottles of beer on the wall."
                + "\n94 bottles of beer on the wall, 94 bottles of beer, take one down,"
                + " pass it around, 93 bottles of beer on the wall."
                + "\n93 bottles of beer on the wall, 93 bottles of beer, take one down,"
                + " pass it around, 92 bottles of beer on the wall.";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
