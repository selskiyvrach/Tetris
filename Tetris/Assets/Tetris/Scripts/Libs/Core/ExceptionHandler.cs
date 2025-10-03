using System;
using System.Collections.Generic;

namespace Libs.Core
{
    public class ExceptionHandler
    {
        private readonly List<Exception> _exceptions = new();
        
        public void RunSafely(Action action)
        {
            try
            {
                action();
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                _exceptions.Add(e);
            }
        }

        public void ThrowIfAnyAndReset()
        {
            if (_exceptions.Count == 0) 
                return;
            
            var exception = new AggregateException(_exceptions.ToArray());
            _exceptions.Clear();
            throw exception;
        }
    }
}