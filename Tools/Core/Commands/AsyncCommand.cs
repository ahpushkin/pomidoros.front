using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Commands
{
    public class AsyncCommand : ICommand
    {
        private readonly Func<object, Task> _runMethodAsync;
        private readonly Func<object, bool> _canExecute;

        public AsyncCommand(
            Func<object, Task> runMethodAsync,
            Func<object, bool> canExecute = null)
        {
            if (runMethodAsync == null)
                throw new ArgumentNullException();
            
            _runMethodAsync = runMethodAsync;
            _canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            var result = true;
            
            try
            {
                if (_canExecute != null)
                    result = _canExecute(parameter);
            }
            catch (OperationCanceledException oce)
            {
                Console.WriteLine(oce);
                Debugger.Break();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debugger.Break();
                throw;
            }
            
            return result;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (CanExecute(parameter))
                    await _runMethodAsync(parameter);
            }
            catch (OperationCanceledException oce)
            {
                Console.WriteLine(oce);
                Debugger.Break();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debugger.Break();
                throw;
            }
        }

        public event EventHandler CanExecuteChanged;

        public void CallCanExecuteChanged(object sender, EventArgs eventArgs)
            => CanExecuteChanged?.Invoke(sender, eventArgs);
    }
    
    public class AsyncCommand<TParameter> : ICommand
    {
        private readonly Func<TParameter, Task> _runMethodAsync;
        private readonly Func<TParameter, bool> _canExecute;

        public AsyncCommand(
            Func<TParameter, Task> runMethodAsync,
            Func<TParameter, bool> canExecute = null)
        {
            if (runMethodAsync == null)
                throw new ArgumentNullException();
            
            _runMethodAsync = runMethodAsync;
            _canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            var result = true;
            
            try
            {
                if (_canExecute != null)
                    result = _canExecute((TParameter)parameter);
            }
            catch (OperationCanceledException oce)
            {
                Console.WriteLine(oce);
                Debugger.Break();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debugger.Break();
                throw;
            }
            
            return result;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (CanExecute(parameter))
                    await _runMethodAsync((TParameter)parameter);
            }
            catch (OperationCanceledException oce)
            {
                Console.WriteLine(oce);
                Debugger.Break();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debugger.Break();
                throw;
            }
        }

        public event EventHandler CanExecuteChanged;

        public void CallCanExecuteChanged(object sender, EventArgs eventArgs)
            => CanExecuteChanged?.Invoke(sender, eventArgs);
    }
}