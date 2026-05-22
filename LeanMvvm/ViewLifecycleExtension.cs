using Avalonia.Controls;
using System.Runtime.CompilerServices;

namespace LeanMvvm
{
    public static class ViewLifecycleExtension
    {
        private static readonly ConditionalWeakTable<Control, object?> _attached = new();
        public static void UseViewModelLifecycle(this Control view)
        {
            // Double attach guard
            if (_attached.TryGetValue(view, out _))
                return;

            _attached.Add(view, null);

            view.Loaded += async (_, _) =>
            {
                if (view.DataContext is ILifecycleViewModel vm)
                    await vm.OnLoadedAsync(); // Fail-fast on exception
            };

            view.Unloaded += async (_, _) =>
            {
                if (view.DataContext is ILifecycleViewModel vm)
                    await vm.OnUnloadedAsync(); // Fail-fast on exception
            };
        }


        public static void UseViewModelClosing(this Window window)
        {
            window.Closing += (_, e) =>
            {
                if (window.DataContext is IConfirmClose vm)
                {
                    if (!vm.CanClose())
                        e.Cancel = true;
                }
            };
        }

    }
}
