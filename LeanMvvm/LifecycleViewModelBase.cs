using CommunityToolkit.Mvvm.ComponentModel;

namespace LeanMvvm
{
    public class LifecycleViewModelBase : ObservableObject, ILifecycleViewModel
    {
        private bool _isInitialized = false;
        private CancellationTokenSource? _cts;
        /// <summary>
        /// Asynchronously performs initialization logic for the implementing view model.
        /// Called once before first <see cref="LoadAsync(CancellationToken)"/>
        /// </summary>
        /// <remarks>Override this method to provide custom initialization logic.</remarks>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous initialization operation.</returns>
        public virtual Task InitializeAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        /// <summary>
        /// Asynchronously loads the resource or data associated with the current instance. Called every time
        /// view is loaded.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous load operation.</param>
        /// <returns>A task that represents the asynchronous load operation.</returns>
        public virtual Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        /// <summary>
        /// Asynchronously unloads resources used by the current instance. Called every time view is unloaded.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the unload operation.</param>
        /// <returns>A task that represents the asynchronous unload operation.</returns>
        public virtual Task UnloadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        async Task ILifecycleViewModel.OnLoadedAsync()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            if (!_isInitialized)
            {
                await InitializeAsync(_cts.Token);
                _isInitialized = true;
            }
            await LoadAsync(_cts.Token);
        }

        async Task ILifecycleViewModel.OnUnloadedAsync()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            await UnloadAsync(_cts.Token);
        }
    }
}
