using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplicationWithDotNetHost.ViewModels
{
    public abstract partial class SidebarViewModelBase : ObservableObject
    {
    }
    public partial class HomeSidebarViewModel : SidebarViewModelBase
    {
    }

    public partial class ExplorerSidebarViewModel : SidebarViewModelBase
    {
    }
}
