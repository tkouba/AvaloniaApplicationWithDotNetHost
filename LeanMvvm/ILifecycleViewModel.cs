namespace LeanMvvm
{
    public interface ILifecycleViewModel
    {
        /// <summary>
        /// Performs asynchronous operations when the component is loaded.
        /// </summary>
        /// <remarks>
        /// Exceptions are not caught by the framework. If this method throws,
        /// the exception will propagate and may crash the application.
        /// Implementations are responsible for handling errors when needed.
        /// </remarks>
        /// <returns>A task that represents the asynchronous load operation.</returns>
        Task OnLoadedAsync();
        /// <summary>
        /// Performs asynchronous operations when the component is unloaded.
        /// </summary>
        /// <remarks>
        /// Exceptions are not caught by the framework. If this method throws,
        /// the exception will propagate and may crash the application.
        /// Implementations are responsible for handling errors when needed.
        /// </remarks>
        /// <returns>A task that represents the asynchronous unload operation.</returns>
        Task OnUnloadedAsync();
    }

    public interface IConfirmClose
    {
        bool CanClose(); // sync decision
    }

}
