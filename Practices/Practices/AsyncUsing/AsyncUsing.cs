using System;
using System.Threading.Tasks;

namespace Practices.AsyncUsing
{
    public class AsyncUsing<T> where T:class 
    {
        /// <summary>
        ///     Constructor #1
        /// </summary>
        /// <param name="instance">Component instance</param>
        /// <param name="usageAction">Usage Action</param>
        /// <param name="disposAction">Dispose Action</param>
        /// <param name="hideErrors"></param>
        public AsyncUsing(T instance, Action<T> usageAction, Action<T> disposAction, bool hideErrors = false)
        {
            Instance = instance;
            UsageAction = usageAction;
            DisposeAction = disposAction;
            HideErrors = hideErrors;
        }

        /// <summary>
        ///     Constructor #2
        /// </summary>
        /// <param name="createFunc">Instance Create Function</param>
        /// <param name="usageAction">Usage Action</param>
        /// <param name="disposAction">Dispose Action</param>
        /// <param name="hideErrors"></param>
        public AsyncUsing(Func<T> createFunc, Action<T> usageAction, Action<T> disposAction, bool hideErrors = false)
        {
            Instance = createFunc();
            UsageAction = usageAction;
            DisposeAction = disposAction;
            HideErrors = hideErrors;
        }

        /// <summary>
        ///     Component Instance
        /// </summary>
        public T Instance { get; private set; }

        /// <summary>
        ///     Usage Action
        /// </summary>
        public Action<T> UsageAction { get; private set; }

        /// <summary>
        ///     Dispose Action
        /// </summary>
        public Action<T> DisposeAction { get; private set; }

        /// <summary>
        ///     Throw errors if false (in Asp.Net be care full with async errors [Rapid-Fail Protection] https://www.iis.net/configreference/system.applicationhost/applicationpools/add/failure)
        /// </summary>
        public bool HideErrors { get; set; }

        /// <summary>
        ///     Start Usage Action
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            var task = new Task(() =>
            {
                try
                {
                    UsageAction(Instance);
                }
                catch (Exception ex)
                {
                    if (HideErrors)
                    {
                        OnUsageErrorWrapper(ex);
                    }
                    else
                    {
                        throw;
                    }
                }
            });
#pragma warning disable 4014
            // Warning disabled because task was not started yet.
            task.ContinueWith(subTask =>
#pragma warning restore 4014
            {
                try
                {
                    DisposeAction(Instance);
                }
                catch (Exception ex)
                {
                    if (HideErrors)
                    {
                        OnDisposeErrorWrapper(ex);
                    }
                    else
                    {
                        throw;
                    }
                }
            });
            task.Start();
            await task;
        }


        /// <summary>
        ///     Triggered on Usage Action Error
        /// </summary>
        public event EventHandler<Exception> OnUsageError;

        private void OnUsageErrorWrapper(Exception ex)
        {
            if (OnUsageError == null) return;
            OnUsageError(this, ex);
        }

        /// <summary>
        ///     Triggered on Dispose Action Error
        /// </summary>
        public event EventHandler<Exception> OnDisposeError;
        private void OnDisposeErrorWrapper(Exception ex)
        {
            if (OnDisposeError == null) return;
            OnDisposeError(this, ex);
        }
    }
}