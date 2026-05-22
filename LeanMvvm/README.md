# LeanMVVM

LeanMVVM is a minimal set of helpers for Avalonia MVVM applications, 
focused on **View ↔ ViewModel lifecycle integration** without introducing a new framework or abstraction layer.

It is designed to work **on top of existing CommunityToolkit.MVVM solutions**, 
providing only the missing glue code in a simple and explicit way.

---

## ✨ Philosophy

LeanMVVM follows a few simple principles:

- ✅ **No framework** – does not replace MVVM, just complements it  
- ✅ **No magic** – behavior is explicit and easy to understand  
- ✅ **Fail-fast** – errors are not swallowed  
- ✅ **Minimal surface area** – only small, focused helpers  
- ✅ **Control stays with the developer**  

---

## 🚀 Getting Started

### 1. Implement lifecycle in your ViewModel

```csharp
public class MainViewModel : LifecycleViewModelBase
{
    protected override async Task InitializeAsync(CancellationToken ct)
    {
        // Called once
    }

    protected override async Task LoadAsync(CancellationToken ct)
    {
        // Called every time the view is loaded
    }

    protected override async Task UnloadAsync(CancellationToken ct)
    {
        // Called every time the view is unloaded
    }
}
```

---

### 2. Attach lifecycle to the view

```csharp
public MainView()
{
    InitializeComponent();
    this.UseViewModelLifecycle();
}
```

---

## 🔄 Lifecycle Overview

LeanMVVM maps UI lifecycle events to ViewModel methods:

| UI Event   | ViewModel Method |
|-----------|----------------|
| Loaded     | InitializeAsync (once) + LoadAsync |
| Unloaded   | UnloadAsync |

---

## ⚠️ Error Handling

LeanMVVM **does not handle exceptions** from lifecycle methods.

```csharp
await vm.OnLoadedAsync();
```

- Exceptions will propagate.
- The application may terminate.
- This is intentional.

> Lifecycle code is considered part of the critical execution path.  
> Implementations are responsible for handling errors when needed.

## Closing

```csharp
public class EditorViewModel : LifecycleViewModelBase, IConfirmClose
{
    public bool IsDirty { get; set; }

    public bool CanClose()
    {
        if (!IsDirty)
            return true;

        // show dialog (sync wrapper over async UI)
        return ConfirmClose();
    }
}
```

---

## 🧠 Why not a framework?

Many MVVM helper libraries evolve into complex frameworks over time.

LeanMVVM intentionally **avoids**:

- navigation abstractions  
- state management  
- message buses  
- reactive pipelines  

If you need those, use a dedicated framework.

LeanMVVM focuses only on:

> ✅ **simple, explicit lifecycle wiring**

---

## 🧩 Design Notes

- Uses ILifecycleViewModel instead of forcing inheritance  
- Provides LifecycleViewModelBase as a convenience base class  
- Uses CancellationToken instead of locks or semaphores  
- Avoids async fire-and-forget patterns  

---

## 📌 Inspiration

This project was inspired by TinyMVVM: https://github.com/dhindrik/TinyMvvm

LeanMVVM differs by:

- focusing on **maximum simplicity and explicit behavior**  
- avoiding framework-like abstractions  
- targeting integration with modern .NET MVVM tooling  

---

## ✅ When to use LeanMVVM

- You use Avalonia UI
- You use CommunityToolkit.MVVM
- You need View ↔ ViewModel lifecycle support
- You want minimal, explicit code
- You want to avoid additional frameworks

---

## ❌ When not to use it

- You need full navigation/state framework
- You prefer reactive pipelines (ReactiveUI, Rx)
- You want opinionated architecture

---

## 📄 License

TBD
