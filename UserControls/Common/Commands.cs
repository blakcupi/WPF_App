using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UserControls.Common
{
    public sealed class RelayCommand : ICommand
    {
        private readonly Action _exec;
        private readonly Func<bool>? _canExec;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _exec = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExec = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExec?.Invoke() ?? true;

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            _exec();
        }

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty); 
    }

    public sealed class RelayCommand<T> : ICommand
    {
        private readonly Action<T?> _exec;
        private readonly Predicate<T?>? _canExec;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<T?> execute, Predicate<T?>? canExecute = null)
        {
            _exec = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExec = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (!TryConvert(parameter, out var value)) return false;

            return _canExec?.Invoke(value) ?? true;
        }

        public void Execute(object? parameter)
        {
            if (!TryConvert(parameter, out var value)) return;
            if (!(_canExec?.Invoke(value) ?? true)) return;

            _exec(value);
        }

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        // null이면 default(T), T 타입이면 그대로, 그 외 타입이면 실패 처리 (InvalidCastException 방지)
        private static bool TryConvert(object? parameter, out T? value)
        {
            if (parameter is T typed)
            {
                value = typed;
                return true;
            }

            if (parameter is null && default(T) is null)
            {
                value = default;
                return true;
            }

            value = default;
            return false;
        }
    }

    public sealed class RelayCommandAsync : ICommand
    {
        private readonly Func<object?, Task> _exec;
        private readonly Predicate<object?>? _canExec;
        private readonly Action<Exception>? _onError;
        private bool _isExecuting;

        public event EventHandler? CanExecuteChanged;

        public RelayCommandAsync(Func<object?, Task> execute, Predicate<object?>? canExecute = null, Action<Exception>? onError = null)
        {
            _exec = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExec = canExecute;
            _onError = onError;
        }

        public bool CanExecute(object? parameter) => !_isExecuting && (_canExec?.Invoke(parameter) ?? true);

        // ICommand 진입점: async void는 여기 한 곳으로 제한하고, 실제 로직은 ExecuteAsync에 위임
        public async void Execute(object? parameter)
        {
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception ex) when (_onError != null)
            {
                // onError가 없으면 예외는 그대로 전파됨 (앱 전역 예외 처리기에서 처리)
                _onError(ex);
            }
        }

        // 코드에서 직접 await 하거나 단위 테스트할 때 사용
        public async Task ExecuteAsync(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            _isExecuting = true;
            OnCanExecuteChanged();

            try
            {
                await _exec(parameter);
            }
            finally
            {
                _isExecuting = false;
                OnCanExecuteChanged();
            }
        }

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public sealed class RelayCommandAsync<T> : ICommand
    {
        private readonly Func<T?, Task> _exec;
        private readonly Predicate<T?>? _canExec;
        private readonly Action<Exception>? _onError;
        private bool _isExecuting;

        public event EventHandler? CanExecuteChanged;
        
        public RelayCommandAsync(Func<T?, Task> execute, Predicate<T?>? canExecute = null, Action<Exception>? onError = null)
        {
            _exec = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExec = canExecute;
            _onError = onError;
        }

        public bool CanExecute(object? parameter)
        {
            if (_isExecuting) return false;
            if (!TryConvert(parameter, out var value)) return false;

            return _canExec?.Invoke(value) ?? true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception ex) when (_onError != null)
            {
                _onError(ex);
            }
        }

        public async Task ExecuteAsync(object? parameter)
        {
            if (!CanExecute(parameter)) return;
            if (!TryConvert(parameter, out var value)) return;

            _isExecuting = true;
            OnCanExecuteChanged();

            try
            {
                await _exec(value);
            }
            finally
            {
                _isExecuting = false;
                OnCanExecuteChanged();
            }
        }

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        private static bool TryConvert(object? parameter, out T? value)
        {
            if (parameter is T typed)
            {
                value = typed;
                return true;
            }

            if (parameter is null && default(T) is null)
            {
                value = default;
                return true;
            }

            value = default;
            return false;
        }
    }
}
