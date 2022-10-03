using CoreBarrelsApp.MVVM.View;
using CoreBarrelsApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBarrelsApp
{
    public static class ModelHolder
    {
        public static CalcView CalcView { get; set; }
        public static CalcPoreView CalcPoreView { get; set; }
        public static KompasView KompasView { get; set; }
    }
}
