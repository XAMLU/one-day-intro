using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace GitHubBrowser.Controls
{
    public class ValidationProperty<T> : ObservableObject
    {
        public void Validate() => _validate?.Invoke(Value, Errors);
        Action<T, ObservableCollection<string>> _validate { get; }

        private Action _afterValidation;

        public ValidationProperty(T value,
            Action<T, ObservableCollection<string>> validate,
            Action afterValidation)
        {
            _validate = validate;
            _afterValidation = afterValidation;
            Errors.CollectionChanged += (s, e) =>
            {
                RaisePropertyChanged(nameof(IsValid));
                RaisePropertyChanged(nameof(ErrorString));
            };
            Value = value;
            Validate();
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                Set(ref _value, value);
                var originalValue = IsValid;
                Validate();
                if (originalValue != IsValid)
                {
                    _afterValidation?.Invoke();
                }
            }
        }

        public ObservableCollection<string> Errors { get; }
            = new ObservableCollection<string>();

        public string ErrorString
            => Errors.Aggregate(string.Empty, (agg, value) 
                => string.IsNullOrEmpty(value) ? string.Empty : $"{agg}{value}. ");

        public bool IsValid
            => !Errors.Any();
    }
}
