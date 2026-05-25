using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplicationWithDotNetHost.ViewModels
{
    public abstract partial class DocumentViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private string _title = "";
        [ObservableProperty]
        private bool _isDirty = false;
    }

    public partial class DocumentViewModelBase<T> : DocumentViewModelBase
    {
        [ObservableProperty]
        private T? _content;
    }

    public class TextDocumentViewModel : DocumentViewModelBase<string>
    {
    }
}
